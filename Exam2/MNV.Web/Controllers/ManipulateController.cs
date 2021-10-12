using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MNV.Core.IServices;
using MNV.Core.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MNV.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ManipulateController : ControllerBase
    {
        private readonly ILogger<ManipulateController> _logger;
        private readonly IApiService _apiService;

        public ManipulateController(ILogger<ManipulateController> logger,
            IApiService apiService)
        {
            _logger = logger;
            _apiService = apiService;
        }

        [HttpGet("num-of-occurences"),AllowAnonymous]
        public IActionResult NumberOfOccurences()
        {
            var result1 = this.GetApiResult("https://join.reckon.com/test2/textToSearch");
            var result2 = this.GetApiResult("https://join.reckon.com/test2/subTexts");

            var model1 = Newtonsoft.Json.JsonConvert.DeserializeObject<MainTextModel>(result1);

            string[] subtexts = this.SubTexts(result2);
            this.ResultIndex(model1.Text, "Peter");
            var result = this.ReturnValue(model1.Text, subtexts);
            this.PostApi("https://join.reckon.com/test2/submitResults", result);
            return Ok(new { result });
        }

        #region Private Method(s)
        private string GetApiResult(string url)
        {
            try
            {
                var result = _apiService.Get(url, Core.Enums.ApiServiceAuthType.None).Result;
                if (string.IsNullOrEmpty(result.Trim()))
                {
                    return GetApiResult(url);
                }
                return result;
            }
            catch
            {
                return GetApiResult(url);
            }
        }

        private string PostApi(string url, ManipulationResult model)
        {
            try
            {
                var result = _apiService.Post(url, model, Core.Enums.ApiServiceAuthType.None).Result;
                if (string.IsNullOrEmpty(result.Trim()))
                {
                    return PostApi(url, model);
                }
                return result;
            }
            catch
            {
                return PostApi(url, model);
            }
        }

        private string[] SubTexts(string result)
        {
            JObject o = JObject.Parse(result);
            JArray arr = (JArray)o["subTexts"];
            string subtexts1 = "";
            foreach (var a in arr)
            {
                subtexts1 += $"{a},";
            }
            subtexts1 = subtexts1.TrimEnd(',');


            string[] subtexts = subtexts1.Split(',').ToArray();

            return subtexts;
        } 

        private ManipulationResult ReturnValue(string text, string[] subtexts)
        {
            var res = new ManipulationResult()
            {
                Candidate = "McNiel Viray",
                Text = text,
            };
            var results = new List<ManipulatedString>();
            foreach( var subtext in subtexts)
            {
                var result = new ManipulatedString();
                result.SubText = subtext;
                result.Result = this.ResultIndex(text, subtext);
                results.Add(result);
            }
            if(results.Count() > 0)
            {
                res.Results = results.ToArray();
            }
            //string ret = Newtonsoft.Json.JsonConvert.SerializeObject(res);

            return res;
        }

        private string ResultIndex(string str, string str2)
        {
            int searchStringLength = str2.Trim().Length;

            char[] ch = str.ToCharArray();
            string word = "";
            int index = 0;
        
            string resString = string.Empty;
            for (var j = 0; j < str.Length; j++)
            {
                if (word.Length == 0)
                    index = j;

                word += ch[j];
                if (word.Length == searchStringLength)
                {
                    if (word.ToLower() == str2.ToLower())
                        resString += $"{index+1},";

                    word = "";
                    j = index;
                }
            }
            if (!string.IsNullOrEmpty(resString.Trim()))
                resString = resString.TrimEnd(',');
            else
                resString = "“<No Output>”";
           

            return resString;
        }
        #endregion
    }
}
