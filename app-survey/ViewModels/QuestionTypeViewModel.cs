using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurveyService.ViewModels
{
    public class QuestionTypeViewModel
    {
        public int QuestionTypeId { get; set; }
        public string QuestionTypeCode { get; set; }
        public string MinLabel { get; set; }
        public string MaxLabel { get; set; }
    }
}