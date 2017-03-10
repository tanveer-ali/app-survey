using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SurveyService.ViewModels;

namespace SurveyService.DataStores
{
    public interface ISurveyDashboardStore
    {
        Task<AnswerCountViewModel> GetResponsesAsync(CancellationToken cancellationToken);        
        Task<AnswerCountViewModel> GetResponsesBySurveyQuestionAsync(int surveyId, int questionId, CancellationToken cancellationToken);
        Task<ICollection<AnswerCountViewModel>> GetResponsesBySurveyAsync(int surveyId, CancellationToken cancellationToken);
    }
}