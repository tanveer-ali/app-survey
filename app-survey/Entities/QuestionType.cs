using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurveyService.Entities
{
    /// <summary>
    /// QuestionType
    /// </summary>
    [Table("QuestionType")]
    public class QuestionType
    {
        /// <summary>
        /// Question  Id
        /// </summary>
        [Key]
        [Column("QuestionTypeID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte QuestionTypeId { get; set; }

        /// <summary>
        /// question type code
        /// </summary>        
        [Required]
        [StringLength(100)]
        public string TypeCode { get; set; }

        /// <summary>
        /// Min Label
        /// </summary>  
        [StringLength(100)]                      
        public string MinLabel { get; set; }

        /// <summary>
        /// Max Label
        /// </summary>                
        [StringLength(100)]
        public string MaxLabel { get; set; }

        public bool IsMultiOptions { get; set; }

    }
}
