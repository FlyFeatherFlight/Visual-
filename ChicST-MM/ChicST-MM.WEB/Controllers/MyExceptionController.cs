using log4net;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ChicST_MM.WEB.Controllers
{
    /// <summary>
    /// 异常处理过滤器，使用log4net记录日志，并跳转至错误页面
    /// </summary>

    public class MyExceptionControl : Controller
    {

        public ViewResult ErrorLog() {

            return View();

        }
    }
}