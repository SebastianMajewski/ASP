namespace ASPProjekt.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ModelValidator
    {
            public static IList<ValidationResult> Validate(object model)
            {
                var results = new List<ValidationResult>();
                var validationContext = new ValidationContext(model, null, null);
                Validator.TryValidateObject(model, validationContext, results, true);
                (model as IValidatableObject)?.Validate(validationContext);
                return results;
            }
    }
}