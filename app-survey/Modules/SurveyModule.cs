using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Nancy;
using Nancy.Json;
using Nancy.ModelBinding;
using Serilog;
using SurveyService.DataStores;
using SurveyService.ViewModels;

namespace SurveyService.Modules
{
    public class SurveyModule : BaseNancyModule
    {
        private readonly ILogger _logger;
        private readonly ISurveyStore _surveyStore;
        private readonly IQuestionStore _questionStore;
        private readonly IAnswerStore _answerStore;
        #region ctors

           
        public SurveyModule(ILogger logger, ISurveyStore surveyStore, IQuestionStore questionStore, IAnswerStore answerStore)
            : base("/api/survey", logger)
        {
            if (surveyStore == null)
            {
                throw new ArgumentNullException(nameof(surveyStore));
            }
            if (questionStore == null)
            {
                throw new ArgumentNullException(nameof(questionStore));
            }
            if (answerStore == null)
            {
                throw new ArgumentNullException(nameof(answerStore));
            }

            _questionStore = questionStore;
            _answerStore = answerStore;          
            _surveyStore = surveyStore;
            _logger = logger;

            
            Get["/", true] = GetAllAsync;
            Get["/questions/{surveyid}", true] = GetBySurveyAsync;
            Post["/{surveyId}", true] = SubmitSurveyAsync;
        }

        #endregion

        #region end points

        private async Task<object> GetAllAsync(dynamic parameters, CancellationToken cancellationToken)
        {
            var result = await _surveyStore.GetAllAsync(cancellationToken);
            return Response.AsText(new JavaScriptSerializer().Serialize(result), "application/json");
        }

        private async Task<object> GetBySurveyAsync(dynamic parameters, CancellationToken cancellationToken)
        {
            int surveyId;
            try
            {
                surveyId = (int) parameters.surveyId;
            }
            catch (FormatException ex)
            {
                _logger.Warning(ex, "Survey Id: {SurveyId} in path should be type: int",
                    parameters.surveyId.ToString());
                throw;
            }

            var result = await _questionStore.GetBySurveyAsync(surveyId, cancellationToken);
            return Response.AsText(new JavaScriptSerializer().Serialize(result), "application/json");
        }

        private async Task<dynamic> SubmitSurveyAsync(dynamic parameters, CancellationToken cancellationToken)
        {
            IList<QuestionViewModel> surveyAnswers;
            int surveyId;
            try
            {
                surveyId = (int) parameters.surveyId;
            }
            catch (FormatException ex)
            {
                _logger.Warning(ex, "Survey Id: {SurveyId} in path should be type: int",
                    parameters.surveyId.ToString());
                throw;
            }
            try
            {
                surveyAnswers = this.BindAndValidate<List<QuestionViewModel>>();
            }
            catch (ModelBindingException ex)
            {
                _logger.Warning(ex, "QuestionViewModel binding error.");
                throw;
            }

            if (!ModelValidationResult.IsValid)
                throw new Exception("Model Not Valid");

            try
            {
                await _answerStore.AddAsync(surveyId, surveyAnswers, cancellationToken);
                return HttpStatusCode.Created;
            }
            catch (Exception ex)
            {
                _logger.Warning(ex, "Exception for adding response for surveyId: {surveyId}",
                    surveyId);
                throw;
            }
        }
       

        #endregion
    }
}