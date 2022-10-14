﻿using System.Web;

namespace System.App.Services.Security
{
    public static class CacheManager
    {
        public static void Clear()
        {
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.Cache.SetExpires(DateTime.Now);
            HttpContext.Current.Response.Cache.SetNoServerCaching();
            HttpContext.Current.Response.Cache.SetNoStore();
        }
    }
}