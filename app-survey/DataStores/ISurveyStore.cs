using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SurveyService.Entities;

namespace SurveyService.DataStores
{
    public interface ISurveyStore
    {
        Task<ICollection<Survey>> GetAllAsync(CancellationToken cancellationToken);
    }
}