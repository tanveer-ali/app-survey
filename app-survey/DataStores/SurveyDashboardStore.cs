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
    public class SurveyDashboardStore : ISurveyDashboardStore
    {
        private readonly SurveyDatabaseContext _context;


        public SurveyDashboardStore(SurveyDatabaseContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            _context = context;
        }

        public async Task<AnswerCountViewModel> GetResponsesAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = new AnswerCountViewModel();
            var queryResult = await (from c in _context.SurveyResponses.Include(c => c.Survey)
                group c by c.SurveyId
                into grp
                select new
                {
                    SurveyId = grp.Key,
                    Label = grp.Max(c => c.Survey.Name),
                    Count = grp.Count()
                }).ToListAsync(cancellationToken);

            result.Labels = queryResult.Select(c => c.Label).ToArray();
            result.Counts = queryResult.Select(c => c.Count).ToArray();
            result.SurveyIds = queryResult.Select(c => c.SurveyId).ToArray();

            return result;
        }

        //public async Task<ICollection<QuestionViewModel>> GetQuestionsBySurveyIdAsync(int surveyId, CancellationToken cancellationToken)
        //{
        //    cancellationToken.ThrowIfCancellationRequested();

        //    var query = from c in _context.Answers.Include(c => c.SurveyResponse).Include(c => c.Question)
        //        join d in _context.SurveyQuestions on c.SurveyResponse.SurveyId equals d.SurveyId
        //        where c.SurveyResponse.SurveyId == surveyId
        //        orderby d.QuestionOrder
        //        select new QuestionViewModel
        //        {
        //            QuestionId = c.QuestionId,
        //            QuestionLabel = c.Question.QuestionLabel,
        //            QuestionOrder = d.QuestionOrder
        //        };

        //    return await query.ToListAsync(cancellationToken);
        //}

        public async Task<ICollection<AnswerCountViewModel>> GetResponsesBySurveyAsync(int surveyId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = new List<AnswerCountViewModel>();
            var queryResult = await (from e in
                (from qo in _context.QuestionOptions.Include(c => c.Question).Include(c => c.Question.QuestionType)
                    join sq in _context.SurveyQuestions on qo.QuestionId equals sq.QuestionId
                    join ao in _context.AnswerOptions.Include(c => c.Answer).Include(c => c.Answer.SurveyResponse)
                    on qo.OptionId equals ao.OptionId into grp
                    from g in grp.DefaultIfEmpty()
                    where sq.SurveyId == surveyId && qo.Question.QuestionType.IsMultiOptions
                    select new
                    {
                        qo.QuestionId,
                        qo.OptionId,
                        qo.OptionLabel,
                        qo.Question.QuestionLabel,
                        AnswerOptionId = (g == null) ? (int?) null : g.OptionId
                    })
                group e by new {e.QuestionId, e.OptionId}
                into optionGrp
                select new
                {
                    optionGrp.Key.QuestionId,
                    QuestionLabel = optionGrp.Max(t => t.QuestionLabel),
                    Label = optionGrp.Max(t => t.OptionLabel),
                    Count = optionGrp.Count(t => t.AnswerOptionId != null)
                }).ToListAsync(cancellationToken);

            var answers = queryResult.GroupBy(c => c.QuestionId).Select(c => new AnswerCountViewModel
            {
                Id = c.Key,
                QuestionLabel = c.Select(d=>d.QuestionLabel).First(),
                Labels = c.Select(d => d.Label).ToArray(),
                Counts = c.Select(d => d.Count).ToArray()
            });
            //add for multi options answers
            result.AddRange(answers);

            queryResult = await (from e in 
                (from a in _context.Answers.Include(c => c.SurveyResponse).Include(c => c.Question).Include(c => c.Question.QuestionType)
                where a.SurveyResponse.SurveyId == surveyId && !a.Question.QuestionType.IsMultiOptions
                select new
                {
                    a.QuestionId,
                    a.Question.QuestionLabel,
                    a.AnswerText
                })
                group e by new {e.QuestionId, e.AnswerText}
                into textGrp
                select new
                {
                    textGrp.Key.QuestionId,
                    QuestionLabel = textGrp.Max(d => d.QuestionLabel),
                    Label = textGrp.Key.AnswerText,
                    Count = textGrp.Count()
                }).ToListAsync(cancellationToken);

            answers = queryResult.GroupBy(c => c.QuestionId).Select(c => new AnswerCountViewModel
            {
                Id = c.Key,
                QuestionLabel = c.Select(d=>d.QuestionLabel).First(),
                Labels = c.Where(d => d.Label != null).Select(d => d.Label).ToArray(),
                Counts = c.Where(d => d.Label != null).Select(d => d.Count).ToArray()
            });
            //add for text answers
            result.AddRange(answers);

            return result.OrderBy(c => c.Id).ToList();
        }

        public async Task<AnswerCountViewModel> GetResponsesBySurveyQuestionAsync(int surveyId, int questionId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var isMultiOptionsQuestion = _context.Questions.Include(c => c.QuestionType).Select(c => c.QuestionType.IsMultiOptions).FirstOrDefault();
            var result = new AnswerCountViewModel {Id = questionId};
            if (isMultiOptionsQuestion)
            {
                var queryResult = await (from sr in _context.SurveyResponses
                    join a in _context.Answers on sr.SurveyResponseId equals a.SurveyResponseId
                    join qo in _context.QuestionOptions on a.QuestionId equals qo.QuestionId
                    join ao in _context.AnswerOptions on new {A = qo.OptionId, B = a.AnswerId} equals new {A = ao.OptionId, B = ao.AnswerId} into grp
                    where sr.SurveyId == surveyId && a.QuestionId == questionId
                    from g in grp.DefaultIfEmpty()
                    group g by qo.OptionId
                    into optionGrp
                    select new
                    {
                        Label = optionGrp.Max(c => c.QuestionOption.OptionLabel),
                        Count = optionGrp.Count(t => t != null)
                    }).ToListAsync(cancellationToken);


                result.Labels = queryResult.Select(c => c.Label).ToArray();
                result.Counts = queryResult.Select(c => c.Count).ToArray();
            }
            else
            {
                var queryResult = await (from c in _context.Answers.Include(c => c.SurveyResponse)
                    where c.QuestionId == questionId && c.SurveyResponse.SurveyId == surveyId
                    group c by c.AnswerText
                    into textGrp
                    select new
                    {
                        Label = textGrp.Key,
                        Count = textGrp.Count()
                    }).ToListAsync(cancellationToken);

                result.Labels = queryResult.Select(c => c.Label).ToArray();
                result.Counts = queryResult.Select(c => c.Count).ToArray();
            }

            return result;
        }
    }
}