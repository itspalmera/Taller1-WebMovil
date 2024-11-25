using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Taller1_WebMovil.Src.Validators
{
    /// <summary>
    /// Custom validation attribute to validate the gender input.
    /// </summary>
    public class GenderValidation : ValidationAttribute
    {

        /// <summary>
        /// Validates the provided gender input.
        /// </summary>
        /// <param name="value">The gender value to validate.</param>
        /// <param name="validationContext">The context in which the validation is performed.</param>
        /// <returns>
        /// A <see cref="ValidationResult"/> indicating whether the validation succeeded or failed.
        /// Returns <see cref="ValidationResult.Success"/> if the validation passes, otherwise returns an error message.
        /// </returns>
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult($"El género indicado no es válido.");
            }
            
            var option = value.ToString();
            if (!Regex.IsMatch(option, @"^[0-9]+$"))
            {
                return new ValidationResult($"El género indicado no es válido.");
            }
            if(int.Parse(option) <= 0){
                return new ValidationResult($"El género indicado no es válido.");
            }
            if(int.Parse(option) > 4){
                return new ValidationResult($"El género indicado no es válido.");
            }
            return ValidationResult.Success;
        }
    }
}