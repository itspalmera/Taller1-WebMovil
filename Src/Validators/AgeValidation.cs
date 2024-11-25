using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Taller1_WebMovil.Src.Validators
{
    /// <summary>
    /// Custom validation attribute to validate age by checking the provided birth date.
    /// </summary>
    public class AgeValidation : ValidationAttribute
    {

         /// <summary>
        /// Validates the provided birth date against the current date.
        /// </summary>
        /// <param name="value">The birth date value to validate.</param>
        /// <param name="validationContext">The context in which the validation is performed.</param>
        /// <returns>
        /// A <see cref="ValidationResult"/> indicating whether the validation succeeded or failed.
        /// Returns <see cref="ValidationResult.Success"/> if the validation passes, otherwise returns an error message.
        /// </returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value ==null){
                return new ValidationResult("La fecha ingresada no es valida.(dd/mm/yyyy)");
            }
            if(IsValidDateFormat(value.ToString(), "dd/MM/yyyy" )){
                return new ValidationResult("El formato de la fecha no es valido.(dd/mm/yyyy)");
            }
            
            DateTime birthDate;
            bool changeFormat = DateTime.TryParseExact(value.ToString(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out birthDate);
            DateTime currentDate;
            bool currentDateB = DateTime.TryParseExact((DateTime.Now).ToString("dd/MM/yyyy"),"dd/MM/yyyy",null,System.Globalization.DateTimeStyles.None,out currentDate);

            int compare = DateTime.Compare(birthDate,currentDate);
            if(compare > 0){
                return new ValidationResult("La fecha de nacimiento no puede superior a la actual.");
            }else if(compare < 0){
                return ValidationResult.Success;
            }else{
                return new ValidationResult("La fecha de nacimiento no puede ser igual a la actual.");
            }
        }

        /// <summary>
        /// Determines if a given year is a leap year.
        /// </summary>
        /// <param name="year">The year to check.</param>
        public bool IsLeapYear(int year){
            return (year % 4 == 0 && year % 100 != 0) || (year % 400 == 0);
        }

        /// <summary>
        /// Checks if a given string matches the specified date format.
        /// </summary>
        /// <param name="text">The date string to validate.</param>
        /// <param name="format">The expected date format.</param>
        /// <returns>True if the date string is invalid, otherwise false.</returns>
        public bool IsValidDateFormat(string text, string format)
        {
            DateTime date;
            return !DateTime.TryParseExact(text, format, null, System.Globalization.DateTimeStyles.None, out date);
        }
    }
}