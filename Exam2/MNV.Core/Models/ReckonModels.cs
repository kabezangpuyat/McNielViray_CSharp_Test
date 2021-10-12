using System;
using System.Collections.Generic;
using System.Text;

namespace MNV.Core.Models
{
    public class MainTextModel
    {
        public string Text { get; set; }
    }

    public class SubTextModel
    {
        public string SubTexts { get; set; }
    }

    public class ManipulationResult
    {
        public string Candidate { get; set; }
        public string Text { get; set; }
        
        public ManipulatedString[] Results { get; set; }
    }

    public class ManipulatedString
    {
        public string SubText { get; set; }
        public string Result { get; set; }
    }
}
