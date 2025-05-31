using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.ComponentModel.DataAnnotations;


namespace api.Validator
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class PositiveValueValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return true; // Let [Required] handle null

            if (long.TryParse(value.ToString(), out long number))
            {
                return number > 0;
            }

            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} must be a positive number.";
        }
    }
}