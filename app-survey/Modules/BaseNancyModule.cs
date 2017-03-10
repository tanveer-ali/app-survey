using System;
using System.IO;
using Nancy;
using Nancy.Extensions;
using Serilog;

namespace SurveyService.Modules
{
    /// <summary>
    /// Base Nancy Module class for all the modules 
    /// </summary>
    public class BaseNancyModule : NancyModule
    {
        /// <summary>
        /// Request Body String populated at before request pipeline
        /// </summary>
        private string RequestBodyString { get; set; }

        /// <summary>
        /// logger 
        /// </summary>
        private ILogger Logger { get; }

        /// <summary>
        /// ctor with logger
        /// </summary>
        /// <param name="logger">logger</param>
        public BaseNancyModule(ILogger logger) : this(string.Empty, logger)
        {
        }

        /// <summary>
        /// ctor with module url and logger
        /// </summary>
        /// <param name="modulePath">base url of module</param>
        /// <param name="logger">logger</param>
        public BaseNancyModule(string modulePath, ILogger logger) : base(modulePath)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            Logger = logger;

            Before += ctx =>
            {
                RequestBodyString = ctx.Request.Body.AsString();
                if (ctx.Request.Body.CanSeek)
                {
                    ctx.Request.Body.Seek(0, SeekOrigin.Begin);
                }
                return null;
            };

            
        }  
    }
}