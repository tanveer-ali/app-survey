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

namespace SurveyService.DataStores
{
    public class AnswerStore : IAnswerStore
    {
        private readonly SurveyDatabaseContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuestionStore"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public AnswerStore(SurveyDatabaseContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            _context = context;
        }

        public async Task AddAsync(int surveyId, IList<QuestionViewModel> surveyAnswers, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var answers = surveyAnswers.Select(c => new Answer
            {
                AnswerText = c.Answer,                
                QuestionId = c.QuestionId,
                AnswerOptions = c.QuestionOptions.Where(d => d.Selected).Select(d => new AnswerOption {OptionId = d.OptionId}).ToList()
            }).ToList();

            var surveyResponse = new SurveyResponse
            {
                SurveyId = surveyId,
                DateCompletedUtc = DateTime.UtcNow,
                Answers = answers
            };
            _context.SurveyResponses.Add(surveyResponse);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}