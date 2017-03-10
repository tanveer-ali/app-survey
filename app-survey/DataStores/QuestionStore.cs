using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using SurveyService.Dal;
using SurveyService.ViewModels;

namespace SurveyService.DataStores
{
    public class QuestionStore : IQuestionStore
    {
        private readonly SurveyDatabaseContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuestionStore"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public QuestionStore(SurveyDatabaseContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            _context = context;
        }

        public async Task<ICollection<QuestionViewModel>> GetBySurveyAsync(int surveyId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var query = from c in _context.SurveyQuestions
                where c.SurveyId == surveyId
                orderby c.QuestionOrder
                select new QuestionViewModel
                {
                    QuestionId = c.QuestionId,
                    QuestionLabel = c.Question.QuestionLabel,
                    QuestionOrder = c.QuestionOrder,
                    Required = c.Required,
                    QuestionType = new QuestionTypeViewModel
                    {
                        QuestionTypeId = c.Question.QuestionType.QuestionTypeId,
                        QuestionTypeCode = c.Question.QuestionType.TypeCode,
                        MinLabel = c.Question.QuestionType.MinLabel,
                        MaxLabel = c.Question.QuestionType.MaxLabel
                    },
                    QuestionOptions = c.Question.QuestionOptions.OrderBy(d=>d.OptionOrder).Select(d => new QuestionOptionViewModel {
                        OptionId = d.OptionId,
                        OptionLabel = d.OptionLabel,
                        OptionOrder = d.OptionOrder
                    }).ToList()
                };

            return await query.ToListAsync(cancellationToken);
        }
    }
}