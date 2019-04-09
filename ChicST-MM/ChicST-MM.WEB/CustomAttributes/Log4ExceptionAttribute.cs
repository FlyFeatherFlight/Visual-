using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ChicST_MM.WEB.CustomAttributes
{
    public class Log4ExceptionAttribute: HandleErrorAttribute
    {
        public static Queue<Exception> Exceptions = new Queue<Exception>();
        // ILog log = LogManager.GetLogger(typeof(ExceptionControl));
        public override void OnException(ExceptionContext filterContext)
        {
          
            base.OnException(filterContext);
            

            if (!filterContext.ExceptionHandled)
            {
                var ex = filterContext.Exception;
                string message = string.Format("消息类型：{0}\r\n消息内容：{1}\r\n引发异常的方法：{2}\r\n引发异常源：{3}"
                , ex.GetType().Name
                , ex.Message
                , ex.TargetSite
                , ex.Source + ex.StackTrace
                );
                //将异常数据入队
                Exceptions.Enqueue(ex);
                //记录日志
                // log.Error(message);
                //转向
        
         
                    //ActionResult result = new ViewResult() { ViewName = "ErrorLog" };
                     //filterContext.Result = result;
                   


               // filterContext.Result = new RedirectToRouteResult("MyError", new RouteValueDictionary(new { controller = "MyExceptionControl", action = "ErrorLog" }), false);


               //filterContext.HttpContext.Response.RedirectToRoute("MyError", new {controller = "MyExceptionControl", action = "ErrorLog"});

               filterContext.ExceptionHandled = true;

            }
            base.OnException(filterContext);
        }
    }
}