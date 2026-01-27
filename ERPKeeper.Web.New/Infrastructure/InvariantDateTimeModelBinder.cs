using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Infrastructure
{
    public class InvariantDateTimeModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));

            var modelName = bindingContext.ModelName;
            var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);

            if (valueProviderResult == ValueProviderResult.None)
                return Task.CompletedTask;

            bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

            var value = valueProviderResult.FirstValue;

            if (string.IsNullOrEmpty(value))
                return Task.CompletedTask;

            // Parse using InvariantCulture
            if (DateTime.TryParse(value, CultureInfo.InvariantCulture, 
                DateTimeStyles.None, out DateTime dateTime))
            {
                bindingContext.Result = ModelBindingResult.Success(dateTime);
            }
            else
            {
                bindingContext.ModelState.TryAddModelError(
                    modelName,
                    $"Invalid date format. Expected format: MM/dd/yyyy");
            }

            return Task.CompletedTask;
        }
    }

    public class InvariantDateTimeModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (context.Metadata.ModelType == typeof(DateTime) ||
                context.Metadata.ModelType == typeof(DateTime?))
            {
                return new InvariantDateTimeModelBinder();
            }

            return null;
        }
    }
}