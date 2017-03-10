using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurveyService.Entities
{
    /// <summary>
    /// Question
    /// </summary>
    [Table("Question")]
    public class Question
    {
        /// <summary>
        /// Question  Id
        /// </summary>
        [Key]
        [Column("QuestionID")]
        public int QuestionId { get; set; }

        /// <summary>
        /// QuestionTypeID
        /// </summary>
        [Column("QuestionTypeID")]
        public byte QuestionTypeId { get; set; }

        /// <summary>
        /// question label
        /// </summary>        
        [Required]
        [StringLength(500)]
        public string QuestionLabel { get; set; }

        public virtual QuestionType QuestionType { get; set; }

        public ICollection<SurveyQuestion> SurveyQuestions { get; set; }

        public ICollection<QuestionOption> QuestionOptions { get; set; }
    }
}