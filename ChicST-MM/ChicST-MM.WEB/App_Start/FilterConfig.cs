﻿using ChicST_MM.WEB.CustomAttributes;
using System.Web.Mvc;

namespace ChicST_MM.WEB
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
          //  filters.Add(new ExceptionControl());
            filters.Add(new AuthorizationFilterAttribute());
           // filters.Add(new Log4ExceptionAttribute());
        }
    }
}