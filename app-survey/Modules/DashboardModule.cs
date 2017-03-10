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
    public class DashboardModule : BaseNancyModule
    {
        private readonly ILogger _logger;
        private readonly ISurveyDashboardStore _surveyDashboardStore;        
        #region ctors

           
        public DashboardModule(ILogger logger, ISurveyDashboardStore surveyDashboardStore)
            : base("/api/dashboard", logger)
        {
            if (surveyDashboardStore == null)
            {
                throw new ArgumentNullException(nameof(surveyDashboardStore));
            }

            _surveyDashboardStore = surveyDashboardStore;
            _logger = logger;

            
            Get["/", true] = GetAllAsync;
            Get["/{surveyid}", true] = GetBySurveyAsync;
        }

        #endregion

        #region end points

        private async Task<object> GetAllAsync(dynamic parameters, CancellationToken cancellationToken)
        {
            var result = await _surveyDashboardStore.GetResponsesAsync(cancellationToken);
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

            var result = await _surveyDashboardStore.GetResponsesBySurveyAsync(surveyId, cancellationToken);
            return Response.AsText(new JavaScriptSerializer().Serialize(result), "application/json");
        }

        #endregion
    }
}