using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Taller1_WebMovil.Src.Validators
{
    public class GenderValidation : ValidationAttribute
    {
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