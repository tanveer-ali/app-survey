using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurveyService.Entities
{
    /// <summary>
    /// SurveyResponse
    /// </summary>
    [Table("SurveyResponse")]
    public class SurveyResponse
    {
        /// <summary>
        /// SurveyResponse  Id
        /// </summary>
        [Key]
        [Column("SurveyResponseID")]            
        public int SurveyResponseId { get; set; }

        /// <summary>
        /// SurveyId
        /// </summary>                
        [Column("SurveyID")]
        public int SurveyId { get; set; }

        /// <summary>
        /// DateCompleted Utc
        /// </summary>
        [Column("DateCompletedUTC")]
        public DateTime DateCompletedUtc { get; set; }

        public virtual Survey Survey { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }

    }
}
