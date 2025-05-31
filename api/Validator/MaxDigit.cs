using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.ComponentModel.DataAnnotations;

namespace api.Validator
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class MaxDigit : ValidationAttribute
    {
        public int MaxDigits { get; }

        public MaxDigit(int maxDigits)
        {
            MaxDigits = maxDigits;
        }

        public override bool IsValid(object value)
        {
            if (value == null)
                return true; // Let [Required] handle null

            if (long.TryParse(value.ToString(), out long number))
            {
                return number.ToString().Length <= MaxDigits;
            }

            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} must be a number with no more than {MaxDigits} digits.";
        }
    }
}