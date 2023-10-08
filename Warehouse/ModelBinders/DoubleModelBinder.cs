using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace Warehouse.ModelBinders
{
    public class DoubleModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            ValueProviderResult valueResult = bindingContext
                .ValueProvider
                .GetValue(bindingContext.ModelName);

            if (valueResult != ValueProviderResult.None && !string.IsNullOrEmpty(valueResult.FirstValue))
            {
                double actualValue = 0;
                bool sucsess = false;

                try
                {
                    string doubleValue = valueResult.FirstValue;
                    doubleValue = doubleValue.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                    doubleValue = doubleValue.Replace(",", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);

                    actualValue = Convert.ToDouble(doubleValue, CultureInfo.CurrentCulture);
                    sucsess = true;
                }
                catch (FormatException fe)
                {
                    bindingContext.ModelState.AddModelError(bindingContext.ModelName, fe, bindingContext.ModelMetadata);
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
