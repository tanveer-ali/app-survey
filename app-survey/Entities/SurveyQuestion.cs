using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurveyService.Entities
{
    /// <summary>
    /// SurveyQuestion
    /// </summary>
    [Table("SurveyQuestion")]
    public class SurveyQuestion
    {
        /// <summary>
        /// SurveyId
        /// </summary>
        [Key]
        [Column("SurveyID", Order = 0)]
        public int SurveyId { get; set; }

        /// <summary>
        /// QuestionId
        /// </summary>
        [Key]
        [Column("QuestionID", Order = 1)]
        public int QuestionId { get; set; }

        /// <summary>
        /// QuestionOrder
        /// </summary>        
        [Required]
        public short QuestionOrder { get; set; }

        /// <summary>
        /// Required
        /// </summary>
        public bool Required { get; set; }

        public virtual Survey Survey { get; set; }

        public virtual Question Question { get; set; }
    }
}