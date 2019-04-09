using ChicST_MM.WEB.App_Start;
using ChicST_MM.WEB.CustomAttributes;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ChicST_MM.WEB
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoFacConfig.Register();


            //开启一个线程,扫描异常信息队列.
            var filePath = Server.MapPath("/Log/");
            ThreadPool.QueueUserWorkItem((a) =>
            {
                while (true)
                {
                    //判断队列中是否有数据
                    if (Log4ExceptionAttribute.Exceptions.Any())
                    {
                        //出队一条异常信息
                        var ex = Log4ExceptionAttribute.Exceptions.Dequeue();
                        //若异常信息不为空
                        if (ex == null) continue;
                        //将异常信息写入到日志文件中
                        var logger = LogManager.GetLogger("errorMsg");
                        logger.Error(ex.ToString());
                    }
                    else
                    {
                        //若异常信息队列为空，则线程休息三秒
                        Thread.Sleep(3000);
                    }
                }
            }, filePath);
        }
    }
}
