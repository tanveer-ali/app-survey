using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurveyService.Entities
{
    /// <summary>
    /// Survey
    /// </summary>
    [Table("Survey")]
    public class Survey
    {
        /// <summary>
        /// Survey  Id
        /// </summary>
        [Key]
        [Column("SurveyID")]            
        public int SurveyId { get; set; }

        /// <summary>
        /// Name
        /// </summary>        
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// DateCreated Utc
        /// </summary>
        [Column("DateCreatedUTC")]
        public DateTime DateCreatedUtc { get; set; }

        public ICollection<SurveyQuestion> SurveyQuestions { get; set; }

    }
}
