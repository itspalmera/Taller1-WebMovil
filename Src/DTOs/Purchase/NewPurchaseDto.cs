using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Taller1_WebMovil.Src.DTOs.Purchase
{
    public class NewPurchaseDto
    {
        /// <summary>
        /// Country where the purchase was made.
        /// </summary>
        [Required(ErrorMessage = "El campo country es obligatorio.")]
        public string country { get; set; }= string.Empty;

        /// <summary>
        /// City where the purchase was made.
        /// </summary>
        [Required(ErrorMessage = "El campo city es obligatorio.")]
        public string city { get; set; }= string.Empty;

        /// <summary>
        /// District where the purchase was made.
        /// </summary>
        [Required(ErrorMessage = "El campo district es obligatorio.")]
        public string district { get; set; }= string.Empty;

        /// <summary>
        /// Street where the purchase was made.
        /// </summary>
        [Required(ErrorMessage = "El campo street es obligatorio.")]
        public string street { get; set; }= string.Empty; 
    }
}