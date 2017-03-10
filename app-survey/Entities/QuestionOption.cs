using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurveyService.Entities
{
    /// <summary>
    /// QuestionOption
    /// </summary>
    [Table("QuestionOption")]
    public class QuestionOption
    {
        /// <summary>
        /// Question Option Id
        /// </summary>
        [Key]
        [Column("OptionID")]
        public int OptionId { get; set; }

        /// <summary>
        /// Question Id
        /// </summary>
        [Column("QuestionID")]
        public int QuestionId { get; set; }

        /// <summary>
        /// OptionLabel
        /// </summary>        
        [Required]
        [StringLength(500)]
        public string OptionLabel { get; set; }

         /// <summary>
        /// Order of options
        /// </summary>
        [Required]        
        public short OptionOrder { get; set; }       
        
        public virtual  Question Question { get; set; }
    }
}
