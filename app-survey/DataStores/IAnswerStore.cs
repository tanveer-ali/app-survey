using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SurveyService.ViewModels;

namespace SurveyService.DataStores
{
    public interface IAnswerStore
    {
        Task AddAsync(int surveyId, IList<QuestionViewModel> surveyAnswers, CancellationToken cancellationToken);
    }
}