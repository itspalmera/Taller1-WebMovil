using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Taller1_WebMovil.Src.Validators
{
    public class AgeValidation : ValidationAttribute
    {
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

        public bool IsLeapYear(int year){
            return (year % 4 == 0 && year % 100 != 0) || (year % 400 == 0);
        }
        public bool IsValidDateFormat(string text, string format)
        {
            DateTime date;
            return !DateTime.TryParseExact(text, format, null, System.Globalization.DateTimeStyles.None, out date);
        }
    }
}