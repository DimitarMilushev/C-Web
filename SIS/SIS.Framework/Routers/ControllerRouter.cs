using SIS.Framework.ActionResults.Base;
using SIS.Framework.ActionResults.Contents;
using SIS.Framework.Controllers.Base;
using SIS.HTTP.Enums;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.WebServer;
using SIS.WebServer.Api;
using SIS.WebServer.Attributes.Methods;
using SIS.WebServer.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SIS.Framework.Routers
{
    public class ControllerRouter : IHttpHandler
    {
        private Controller GetController(string controllerName,
            IHttpRequest request)
        {
            if (string.IsNullOrWhiteSpace(controllerName))
                return null;

            var fullControllerName = string.Format("{0}.{1}.{2}{3}, {0}",
                MvcContext.Get.AssemblyName,
                MvcContext.Get.ControllerFolder,
                controllerName,
                MvcContext.Get.ControllerSuffix);

            var controllerType = Type.GetType(fullControllerName);

            var controller = (Controller)Activator.CreateInstance(controllerType);
            return controller;
        }

        //private MethodInfo GetMethod(string requestMethod,
        //    Controller controller, string actionName)
        //{
        //    MethodInfo method = null;

        //    foreach(var methodInfo in GetSuitableMethods(controller, actionName))
        //    {
        //        var attributes = methodInfo
        //            .GetCustomAttributes()
        //            .Where(attr => attr is HttpMethodAttribute);

        //        if (!attributes.Any() && requestMethod.ToUpper() == "GET")
        //            return methodInfo;

        //        foreach(var attribute in attributes)
        //        {
        //            if (attribute.IsValid(requestMethod))
        //                return methodInfo;
        //        }
        //    }

        //    return method;
        //}

        private IHttpResponse PrepareResponse(Controller controller,
            MethodInfo action, IHttpRequest request)
        {
            object[] actionParameter = this.MapActionParameters(action, request);

            IActionResult actionResult = (IActionResult)action.Invoke(controller, null);
            string invocationResult = actionResult.Invoke();

            if (actionResult is IViewable)
                return new HtmlResult(invocationResult, HttpResponseStatusCode.Ok);
            else if (actionResult is IRedirectable)
                return new RedirectResult(invocationResult);
            else
                throw new InvalidOperationException("The view result is not supported.");

            
        }

        private object[] MapActionParameters(MethodInfo action, IHttpRequest request)
        {
            var actionParamsArray = action.GetParameters();

            object[] mappedActionParameters = new ParameterInfo[actionParamsArray.Length];
            for (int i = 0; i < actionParamsArray.Length; i++)
            {
                var actionParameter = actionParamsArray[i];
                object mappedActionParam = new object();

                if (actionParameter.ParameterType.IsPrimitive ||
                    actionParameter.ParameterType == typeof(string))
                {
                    mappedActionParam = this.ProcessPrimitiveParameter(actionParameter, request);
                    if (mappedActionParam == null)
                        break;

                    mappedActionParameters[i] = mappedActionParam;
                }
                else
                {

                    mappedActionParam = this.ProcessPrimitiveParameter(actionParameter, request);
                    if (mappedActionParam == null)
                        break;

                    mappedActionParameters[i] = ProcessBindingModelParameters(actionParameter, request);
                }
            }
            return mappedActionParameters;
        }

        private object ProcessBindingModelParameters(ParameterInfo actionParameter, IHttpRequest request)
        {
            Type bindingModelType = actionParameter.ParameterType;

            var bindingModelInstance = Activator.CreateInstance(bindingModelType);
            var bindingModelProperties = bindingModelType.GetProperties();

            foreach (var property in bindingModelProperties)
            {
                try
                {
                    object value = this.GetParameterFromRequestData(request, property.Name);
                    property.SetValue(bindingModelInstance, Convert.ChangeType(value, property.PropertyType));
                }
                catch
                {
                    Console.WriteLine($"The {property.Name} field could not be mapped.");
                }
            }
            return Convert.ChangeType(bindingModelInstance, bindingModelType);
        }

        private object ProcessPrimitiveParameter(ParameterInfo actionParameter, IHttpRequest request)
        {
            object value = this.GetParameterFromRequestData(request, actionParameter.Name);
            return Convert.ChangeType(value, actionParameter.ParameterType);
        }

        private object GetParameterFromRequestData(IHttpRequest request, string name)
        {
            if (request.QueryData.ContainsKey(name))
                return request.QueryData[name];
            if (request.FormData.ContainsKey(name))
                return request.FormData[name];

            return null;
        }

        public IHttpResponse Handle(IHttpRequest request)
        {

            string controllerName = string.Empty;
            string actionName = string.Empty;
            string requestMethod = request.RequestMethod.ToString();

            if (request.Url == @"/")
            {
                controllerName = "Home";
                actionName = "Index";
            }
            else
            {
                string[] requestUrlSplit 
                    = request.Url.Split('/', StringSplitOptions.RemoveEmptyEntries);
    
                controllerName = requestUrlSplit[0];
                 actionName = requestUrlSplit[1];
            }

            var controller = this.GetController(controllerName, request);

            MethodInfo action = this.GetAction(requestMethod, controller, actionName);

            if (controller == null || action == null)
                throw new NullReferenceException();

            return this.PrepareResponse(controller, action, request);
        }

        private MethodInfo GetAction(string requestMethod, Controller controller, string actionName)
        {
            var actions = this.GetSuitableMethods(controller, actionName).ToList();

            if (!actions.Any())
                return null;

            foreach (var action in actions)
            {
                var httpMethodAttributes = action
                    .GetCustomAttributes()
                    .Where(ca => ca is HttpMethodAttribute)
                    .Cast<HttpMethodAttribute>();

                if (!httpMethodAttributes.Any() && requestMethod.ToLower() == "get")
                    return action;

                foreach (var httpMethodAttribute in httpMethodAttributes)
                {
                    if (httpMethodAttribute.IsValid(requestMethod))
                        return action; 
                }
            }
            return null;
        }

        private IEnumerable<MethodInfo> GetSuitableMethods(Controller controller, string actionName)
        {
            if (controller == null)
                return new MethodInfo[0];

            return controller.GetType()
                .GetMethods().Where(x => x.Name.ToLower() == actionName.ToLower());
        }
    }
}
