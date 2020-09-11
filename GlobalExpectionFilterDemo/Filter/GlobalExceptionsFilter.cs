using GlobalExpectionFilterDemo.Log;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalExpectionFilterDemo.Filter
{
        public class GlobalExceptionsFilter : IExceptionFilter
        {
                private readonly IHostingEnvironment _env;
                private readonly ILoggerHelper _loggerHelper;

                public GlobalExceptionsFilter(IHostingEnvironment env, ILoggerHelper loggerHelper )
                {
                        _env = env;
                        _loggerHelper = loggerHelper;
                }

                public void OnException(ExceptionContext context )
                {
                        var json = new JsonErrorResponse();
                        json.Message = context.Exception.Message;
                        if (_env.IsDevelopment())
                        {
                                json.DevelopmentMessage = context.Exception.StackTrace;
                        }
                        context.Result = new InternalServerErrorObjectResult(json);

                        _loggerHelper.Error(json.Message, WriteLog(json.Message, context.Exception));

                }

                public string WriteLog( string throwMsg , Exception ex )
                {
                        return string.Format("【Custom Error】：{0} \r\n【Error Type】：{1} \r\n【Error Info】：{2} \r\n【Stack Call】：{3}" , new object [ ] { throwMsg,
                ex.GetType().Name, ex.Message, ex.StackTrace });
                }
        }
        public class InternalServerErrorObjectResult : ObjectResult
        {
                public InternalServerErrorObjectResult( object value ) : base(value)
                {
                        StatusCode = StatusCodes.Status500InternalServerError;
                }
        }

        public class JsonErrorResponse
        {
                public string Message { get; set; }
                public string DevelopmentMessage { get; set; }
        }
}
