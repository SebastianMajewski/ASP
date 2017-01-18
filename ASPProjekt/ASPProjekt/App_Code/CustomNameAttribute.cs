namespace ASPProjekt
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;

    public class CustomNameAttribute : ValidationAttribute
    {
        public override string FormatErrorMessage(string name)
        {
            return string.Format(CultureInfo.CurrentCulture, this.ErrorMessageString, name);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(value as string))
            {
                return new ValidationResult("Nazwa jest pusta.");
            }

            var s = (string)value;
            if (!char.IsUpper(s[0]))
            {
                return new ValidationResult("Pierwsza litera musi byc duża.");
            }

            return null;
        }
    }
}