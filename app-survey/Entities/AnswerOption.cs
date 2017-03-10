using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurveyService.Entities
{
    /// <summary>
    /// AnswerOption
    /// </summary>
    [Table("AnswerOption")]
    public class AnswerOption
    {
        /// <summary>
        /// Question  Id
        /// </summary>
        [Key]
        [Column("AnswerID", Order = 0)]            
        public int AnswerId { get; set; }

        /// <summary>
        /// SurveyResponseId
        /// </summary>  
        [Key]
        [Column("OptionID", Order = 1)]   
        [ForeignKey("QuestionOption")]    
        public int OptionId { get; set; }

        public virtual Answer Answer { get; set; }

        public virtual QuestionOption QuestionOption { get; set; }
    }
}
