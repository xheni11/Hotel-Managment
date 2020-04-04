using Castle.DynamicProxy;
using log4net;
using M19G1.Common.Exceptions;
using M19G1.Exceptions;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace M19G1.Common.Logging
{
    public class Interceptor : IInterceptor
    {
        private static readonly ILog Logger = LogManager.GetLogger(System.Environment.MachineName);
        public void Intercept(IInvocation invocation)
        {
            try
            {
                invocation.Proceed();
            }
            catch (Exception e)
            {
                Logger.Error(e.Message, e);
                HandleExceptionAsync(e);
                throw;
            }
        }
        private void HandleExceptionAsync(Exception exception)
        {
           // context.Response.ContentType = JsonContentType;

            ErrorDetails error = new ErrorDetails();

            switch (exception)
            {
                case EmailTemplateNotFoundException emailNotFoundException:
                    error.StatusCode = 550;
                    error.Message = $"Email template file not found: {emailNotFoundException.Message}";
                    break;
                //case AppException appException:
                //    error.StatusCode = (int)HttpStatusCode.InternalServerError;
                //    error.Message = appException.Message;
                //    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                //    break;
                //case ForbiddenException forbiddenException:
                //    error.StatusCode = (int)HttpStatusCode.Forbidden;
                //    error.Message = $"Access Forbidden: {forbiddenException}";
                //    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                //    break;
                //case NotFoundException notFoundException:
                //    error.StatusCode = (int)HttpStatusCode.NotFound;
                //    error.Message = notFoundException.Message;
                //    context.Response.StatusCode = StatusCodes.Status404NotFound;
                //    break;
                //case ValidationExceptionError validationExceptionError:
                //    error.StatusCode = (int)HttpStatusCode.BadRequest;
                //    error.Message = validationExceptionError.Message;
                //    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                //    break;s
                default:
                    error.StatusCode = 500;
                    error.Message = $"Internal Server Error: {exception.Message}";
   
                    //context.Response.StatusCode = StatusCodes.Status500InternalServerError;
              break;
            }

             //context.Response.WriteAsync(error.ToString());
        }
    }
}
