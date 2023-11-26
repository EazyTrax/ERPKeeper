using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web.Mvc;

namespace ERPKeeper.WebFrontEnd.Library
{

    public class DateTimeModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            DateTime dateTime;
            var isDate = DateTime.TryParse(value.AttemptedValue, Thread.CurrentThread.CurrentUICulture, DateTimeStyles.None, out dateTime);

            if (!isDate)
            {
                // bindingContext.ModelState.AddModelError(bindingContext.ModelName, ModelValidationResources.InvalidDateTime);
                return DateTime.Now;
            }

            return dateTime;
        }
    }
}