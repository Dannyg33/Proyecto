using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MVC.Models.Base
{
     public class DireccionsModelBase
     {
        
         [Display(Name = "Direccion Id")]
         public int DireccionId { get; set; } 

       
         [Display(Name = "Calle1")]
         public string Calle1 { get; set; } 

       
         [Display(Name = "Calle2")]
         public string Calle2 { get; set; } 

        
         [Display(Name = "Numero Casa")]
         public string NumeroCasa { get; set; } 

       
         [Display(Name = "Canton")]
         public string Canton { get; set; } 

      
         [Display(Name = "Provincia")]
         public string Provincia { get; set; } 

         [Required(ErrorMessage = "{0} is required!")]
         [Display(Name = "Principal")]
         public bool Principal { get; set; } 

        
         [Display(Name = "Cliente Clienteid")]
         public int? ClienteClienteid { get; set; } 

     }
}
