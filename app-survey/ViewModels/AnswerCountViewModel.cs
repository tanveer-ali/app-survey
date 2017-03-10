using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurveyService.ViewModels
{
    public class AnswerCountViewModel
    {
        public int Id { get; set; }
        public string QuestionLabel { get; set; }
        public string[] Labels { get; set; }
        public int[] Counts { get; set; }

        public int[] SurveyIds { get; set; }
    }
}