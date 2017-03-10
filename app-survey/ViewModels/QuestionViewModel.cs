using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurveyService.ViewModels
{
    public class QuestionViewModel
    {
        public int QuestionId { get; set; }
        public string QuestionLabel { get; set; }
        public int QuestionOrder { get; set; }
        public bool Required { get; set; }
        public string Answer { get; set; }
        public QuestionTypeViewModel QuestionType { get; set; }
        public ICollection<QuestionOptionViewModel> QuestionOptions { get; set; }
    }
}