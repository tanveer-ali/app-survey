using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SurveyService.Entities;
using SurveyService.ViewModels;

namespace SurveyService.DataStores
{
    public interface IQuestionStore
    {
        /// <summary>
        /// Gets the QuestionViewModel by survey Id asynchronously.
        /// </summary>
        /// <param name="surveyId">The surveyId.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A collection of all locale fields</returns>
        Task<ICollection<QuestionViewModel>> GetBySurveyAsync(int surveyId, CancellationToken cancellationToken);
    }   
}
