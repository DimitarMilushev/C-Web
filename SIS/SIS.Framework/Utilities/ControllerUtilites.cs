﻿using SIS.WebServer;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.Framework.Utilities
{
    public static class ControllerUtilites
    {
        public static string GetControllerName(object controller)
            => controller.GetType()
            .Name
            .Replace(MvcContext.Get.ControllerSuffix, string.Empty);

        public static string GetViewFullyQualifiedName(string controllerName, string viewName)
            => string.Format("../../../{0}/{1}/{2}.html",
                MvcContext.Get.ViewsFolder,
                controllerName,
                viewName);
    }
}
