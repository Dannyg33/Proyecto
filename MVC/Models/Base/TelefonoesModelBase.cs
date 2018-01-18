using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MVC.Models.Base
{
     
     public class TelefonoesModelBase
     {
         
         [Display(Name = "Telefono ID")]
         public int TelefonoID { get; set; } 

        
         [Display(Name = "Numero")]
         public string Numero { get; set; } 

        
         [Required(ErrorMessage = "{0} is required!")]
         [Display(Name = "Principal")]
         public bool Principal { get; set; } 

        
         [Display(Name = "Cliente Clienteid")]
         public int? ClienteClienteid { get; set; } 

     }
}
