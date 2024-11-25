using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Taller1_WebMovil.Src.Validators
{

    /// <summary>
    /// Custom validation attribute to validate the Chilean RUT (Rol Único Tributario).
    /// </summary>
    public class RutValidation : ValidationAttribute
    {

        /// <summary>
        /// Validates the RUT format and checks the verification digit.
        /// </summary>
        /// <param name="value">The RUT value to validate.</param>
        /// <param name="validationContext">The context in which the validation is performed.</param>
        /// <returns>
        /// A <see cref="ValidationResult"/> indicating whether the validation succeeded or failed.
        /// Returns <see cref="ValidationResult.Success"/> if the validation passes, otherwise returns an error message.
        /// </returns>
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            if (value == null)
            {
                return new ValidationResult($"El rut no tiene el formato correcto. (Ejemplo: 11.111.111-1)1");
            }
            //Comprueba que este en un formato 1.111.111-1 o 11.111.111-1
            if (!Regex.IsMatch(value.ToString(), @"^\d{1,2}\.\d{3}\.\d{3}-[\dKk]{1}$"))
            {
                return new ValidationResult($"El rut no tiene el formato correcto. (Ejemplo: 11.111.111-1)2 ${value}");
            }
            //Quita los . del rut y comprueba que contenga solo numeros 0 al 9, k y -
            var rut = value.ToString()?.Replace(".", "").ToUpper();;
            if (!Regex.IsMatch(rut, @"^[0-9\.\-kK]+$"))
            {
                return new ValidationResult($"El rut no tiene el formato correcto. (Ejemplo: 11.111.111-1)3");
            }
            //divide el rut desde el - y verifica que no hayan mas de 2 grupos
            string[] rutParts = rut.Split('-');
            if (rutParts.Length != 2)
            {
                return new ValidationResult($"El rut no tiene el formato correcto. (Ejemplo: 11.111.111-1)4");
            }
            string digitVerificator = rutParts[1];
            string digitRut = rutParts[0];

            if (digitVerificator.Length != 1)
            {
                return new ValidationResult($"El rut no tiene el formato correcto. (Ejemplo: 11.111.111-1)5");
            }
            if (!Regex.IsMatch(digitRut, @"^[0-9]+$"))
            {
                return new ValidationResult($"El rut no tiene el formato correcto. (Ejemplo: 11.111.111-1)6");
            }
            //Dar vuelta el rut
            string rutInverse = "";
            for (int i = digitRut.Length - 1; i > -1; i--)
            {
                rutInverse = rutInverse + digitRut[i];
            }
            //Suma y multiplicacion
            int cont = 2;
            int sum = 0;
            int mul = 0;
            for (int i = 0; i < rutInverse.Length; i++)
            {
                if (cont == 8)
                {
                    cont = 2;
                }
                mul = (rutInverse[i] - '0') * cont;
                sum = sum + mul;
                cont++;
            }
            //division por 11
            int div = sum / 11;
            int num = div * 11;
            int num2 = Math.Abs(sum - num);
            string digit = (11 - num2).ToString();
            if (digit == "11")
            {
                digit = "0";
            }
            else if (digit == "10")
            {
                digit = "K";
            }
            if (digit == digitVerificator)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("El rut no es válido.");
            }
        }
    }
}