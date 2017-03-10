using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using SurveyService.Dal;
using SurveyService.Entities;
using SurveyService.ViewModels;
using QuestionType = SurveyService.ViewModels.QuestionTypeViewModel;

namespace SurveyService.DataStores
{
    public class SurveyStore : ISurveyStore
    {
        private readonly SurveyDatabaseContext _context;

       
        public SurveyStore(SurveyDatabaseContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            _context = context;
        }

        public async Task<ICollection<Survey>> GetAllAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await _context.Surveys.ToListAsync(cancellationToken);
        }
    }
}