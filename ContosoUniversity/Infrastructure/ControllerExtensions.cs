using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace ContosoUniversity.Infrastructure
{
    public static class ControllerExtensions
    {
        public static ActionResult RedirectToActionJson<TController>(this Controller controller, string actionUrl)
    where TController : Controller
        {
            return controller.JsonNet(new
            {
                redirect = actionUrl
            }
            );
        }

        public static ActionResult RedirectToActionJson<TController>(this TController controller, string actionUrl)
            where TController : Controller
        {
            return controller.JsonNet(new
            {
                redirect = actionUrl
            }
            );
        }


        public static ContentResult JsonNet(this Controller controller, object model)
        {
            var serialized = JsonConvert.SerializeObject(model, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return new ContentResult
            {
                Content = serialized,
                ContentType = "application/json"
            };
        }
    }
}