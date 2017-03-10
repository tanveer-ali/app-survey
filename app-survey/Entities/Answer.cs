using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurveyService.Entities
{
    /// <summary>
    /// Answer
    /// </summary>
    [Table("Answer")]
    public class Answer
    {
        /// <summary>
        /// Question  Id
        /// </summary>
        [Key]
        [Column("AnswerID")]            
        public int AnswerId { get; set; }

        /// <summary>
        /// SurveyResponseId
        /// </summary>  
        [Column("SurveyResponseID")]       
        public int SurveyResponseId { get; set; }

        /// <summary>
        /// Question Id
        /// </summary>
         [Column("QuestionID")] 
        public int QuestionId { get; set; }

        /// <summary>
        /// Answer
        /// </summary>
        [StringLength(1000)]
        public string AnswerText { get; set; }

        public virtual SurveyResponse SurveyResponse { get; set; }
        public virtual Question Question { get; set; }

        public virtual ICollection<AnswerOption> AnswerOptions { get; set; }
    }
}
