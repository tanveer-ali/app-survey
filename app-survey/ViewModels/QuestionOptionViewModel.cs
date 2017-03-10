using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurveyService.ViewModels
{
    public class QuestionOptionViewModel
    {
        public int OptionId { get; set; }
        public string OptionLabel { get; set; }
        public int OptionOrder { get; set; }
        public bool Selected { get; set; }

    }
}