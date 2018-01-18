using System;
using MVC.Models;
using MVC.Domain;
using MVC.BusinessObject;

namespace MVC.ViewModels.Base
{ 
    
     public class TelefonoesViewModelBase
     { 
         public MVC.Models.TelefonoesModel TelefonoesModel { get; set; }
         public CrudOperation Operation { get; set; }
         public string ViewControllerName { get; set; }
         public string ViewActionName { get; set; }
         public string ViewReturnUrl { get; set; }
         public ClientesCollection ClientesDropDownListData { get; set; }
     } 
} 
