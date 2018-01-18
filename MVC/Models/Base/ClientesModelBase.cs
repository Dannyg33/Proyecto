using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MVC.Models.Base
{
    
     public class ClientesModelBase
     {
        
         [Display(Name = "Cliente Id")]
         public int ClienteId { get; set; } 

        
         [Display(Name = "Nombre")]
         public string Nombre { get; set; } 

        
         [Display(Name = "Apellido")]
         public string Apellido { get; set; } 

        
         [Required(ErrorMessage = "{0} is required!")]
         [Range(typeof(Int32), "-2147483648", "2147483647", ErrorMessage = "{0} must be an integer!")]
         [Display(Name = "Edad")]
         public int Edad { get; set; } 

         
         [Required(ErrorMessage = "{0} is required!")]
         [DataType(DataType.Date, ErrorMessage = "{0} must be a valid date!")]
         [Display(Name = "Fecha Nacimiento")]
         public DateTime FechaNacimiento { get; set; } 

     }
}
