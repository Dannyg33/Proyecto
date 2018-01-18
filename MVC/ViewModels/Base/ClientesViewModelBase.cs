using System;
using MVC.Models;
using MVC.Domain;
using MVC.BusinessObject;

namespace MVC.ViewModels.Base
{ 
   
     public class ClientesViewModelBase
     { 
         public MVC.Models.ClientesModel ClientesModel { get; set; }
         public CrudOperation Operation { get; set; }
         public string ViewControllerName { get; set; }
         public string ViewActionName { get; set; }
         public string ViewReturnUrl { get; set; }
     } 
} 
