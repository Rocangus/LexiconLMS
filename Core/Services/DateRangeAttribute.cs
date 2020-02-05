using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LexiconLMS.Services.Validation
{
    public class DateRangeAttribute : ValidationAttribute
    {
        public DateRangeAttribute(int[] first, int[] last)
        {
            First = new DateTime(first[0], first[1], first[2]);
            Last = new DateTime(last[0], last[1], last[2]);
        }

        public DateTime First { get; }
        public DateTime Last { get; }

        public string GetErrorMessage() => $"The date must be specified between {First} and {Last}.";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value is DateTime date && date < Last && date > First)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(GetErrorMessage());
        }
    }
}
