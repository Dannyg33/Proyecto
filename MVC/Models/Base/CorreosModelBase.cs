using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MVC.Models.Base
{
   
     public class CorreosModelBase
     {
       
         [Display(Name = "Correo ID")]
         public int CorreoID { get; set; } 

        
         [Display(Name = "Mail")]
         public string Mail { get; set; } 

       
         [Required(ErrorMessage = "{0} is required!")]
         [Display(Name = "Principal")]
         public bool Principal { get; set; } 

       
         [Display(Name = "Cliente Clienteid")]
         public int? ClienteClienteid { get; set; } 

     }
}
