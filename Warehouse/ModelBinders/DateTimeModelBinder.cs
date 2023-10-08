using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace Warehouse.ModelBinders
{
    public class DateTimeModelBinder : IModelBinder
    {
        private readonly string customDateFormat;

        public DateTimeModelBinder(string _customDateFormat)
        {
            this.customDateFormat = _customDateFormat;
        }
    
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            ValueProviderResult valueResult = bindingContext
                .ValueProvider
                .GetValue(bindingContext.ModelName);

            if (valueResult != ValueProviderResult.None && !string.IsNullOrEmpty(valueResult.FirstValue))
            {
                DateTime actualValue = DateTime.MinValue;
                bool sucsess = false;
                string dateValue = valueResult.FirstValue;
                try
                {
                    actualValue = DateTime.ParseExact(dateValue, customDateFormat, CultureInfo.InvariantCulture);
                    
                    sucsess = true;
                }
                catch (FormatException)
                {
                    try
                    {
                        actualValue = DateTime.Parse(dateValue, new CultureInfo("bg-bg"));
                    }
                    catch(Exception e)
                    {
                        bindingContext.ModelState.AddModelError(bindingContext.ModelName, e, bindingContext.ModelMetadata);
                    }
                   
                }
                catch (Exception e)
                {
                    bindingContext.ModelState.AddModelError(bindingContext.ModelName, e, bindingContext.ModelMetadata);
                }

                if (sucsess)
                {
                    bindingContext.Result = ModelBindingResult.Success(actualValue);
                }

            }





            return Task.CompletedTask;
        }
    }
}
