﻿using System.Web;
using System.Web.Mvc;

namespace WebAnime.API2
{
    public abstract class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
