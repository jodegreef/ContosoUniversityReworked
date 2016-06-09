//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Web;
//using System.Web.Mvc;

//namespace ContosoUniversity.Infrastructure
//{
//    public static class UrlHelperExtensions
//    {
//        public static string Action<TController>(this UrlHelper helper, Expression<Action<TController>> action)
//            where TController : Controller
//        {
//            var url = LinkBuilder.BuildUrlFromExpression(helper.RequestContext, RouteTable.Routes, action);

//            return UrlHelper.GenerateContentUrl("~/" + url, helper.RequestContext.HttpContext);
//        }
//    }
//}