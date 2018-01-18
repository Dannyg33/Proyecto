using System;
using System.Text;
using System.Linq;
using System.Web.Mvc;
using MVC;
using MVC.BusinessObject;
using MVC.Models;
using MVC.ViewModels;
using MVC.Domain;
using System.Collections.Generic;
using System.Configuration;
using System.Text.RegularExpressions;

namespace MVC.Controllers.Base
{ 
 
     public class ClientesControllerBase : Controller
     { 

        
         /// GET: /Clientes/
        
         public ActionResult Index()
         {
             return View();
         }

     
         /// GET: /Clientes/Nuevo
        
         public ActionResult Nuevo()
         {
             return GetAddViewModel();
         }

      
         /// POST: /Clientes/Nuevo
        
         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Nuevo(ClientesViewModel viewModel, string returnUrl)
         {
             if (ModelState.IsValid)
             {
                 try
                 {
                     // nuevo
                     AddEditClientes(viewModel, CrudOperation.Add);

                     if (Url.IsLocalUrl(returnUrl))
                         return Redirect(returnUrl);
                     else
                         return RedirectToAction("Index");
                 }
                 catch(Exception ex)
                 {
                     ModelState.AddModelError("", ex.Message);
                 }
             }

         
             return GetAddViewModel();
         }

         private ActionResult GetAddViewModel()
         {
             ClientesViewModel viewModel = new ClientesViewModel();
             viewModel.ClientesModel = null;
             viewModel.Operation = CrudOperation.Add;
             viewModel.ViewControllerName = "Clientes";
             viewModel.ViewActionName = "Nuevo";

             if (Request.UrlReferrer != null)
                 viewModel.ViewReturnUrl = Request.UrlReferrer.PathAndQuery;
             else
                 viewModel.ViewReturnUrl = "Index";

             return View(viewModel);
         }

         
         /// GET: /Clientes/Actualizar/5
     
         public ActionResult Actualizar(int id)
         {
             return GetUpdateViewModel(id);
         }

         /// POST: /Clientes/Actualizar/5
         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Actualizar(int id, ClientesViewModel viewModel, string returnUrl)
         {
             if (ModelState.IsValid)
             {
                 try
                 {
                     // actualizar
                     AddEditClientes(viewModel, CrudOperation.Update);

                     if (Url.IsLocalUrl(returnUrl))
                         return Redirect(returnUrl);
                     else
                         return RedirectToAction("Index");
                 }
                 catch(Exception ex)
                 {
                     ModelState.AddModelError("", ex.Message);
                 }
             }

           
             return GetUpdateViewModel(id);
         }

         public ActionResult GetUpdateViewModel(int id)
         {
             // selecciono cliente por primary key
             Clientes objClientes = Clientes.SelectByPrimaryKey(id);

             // asigno valores
             Models.ClientesModel model = new Models.ClientesModel();
             model.ClienteId = objClientes.ClienteId;
             model.Nombre = objClientes.Nombre;
             model.Apellido = objClientes.Apellido;
             model.Edad = objClientes.Edad;
             model.FechaNacimiento = objClientes.FechaNacimiento;

           
             ClientesViewModel viewModel = new ClientesViewModel();
             viewModel.ClientesModel = model;
             viewModel.Operation = CrudOperation.Update;
             viewModel.ViewControllerName = "Clientes";
             viewModel.ViewActionName = "Actualizar";

             if (Request.UrlReferrer != null)
                 viewModel.ViewReturnUrl = Request.UrlReferrer.PathAndQuery;
             else
                 viewModel.ViewReturnUrl = "Index";

             return View(viewModel);
         }

     
         /// POST: /Clientes/Delete/5
      
         [HttpPost]
         public ActionResult Delete(int id, ClientesViewModel viewModel, string returnUrl)
         {
             Clientes.Delete(id);
             return Json(true);
         }

       
         /// GET: /Clientes/Detalles/5
     
         public ActionResult Detalles(int id)
         {
             Clientes objClientes = Clientes.SelectByPrimaryKey(id);

             Models.ClientesModel model = new Models.ClientesModel();
             model.ClienteId = objClientes.ClienteId;
             model.Nombre = objClientes.Nombre;
             model.Apellido = objClientes.Apellido;
             model.Edad = objClientes.Edad;
             model.FechaNacimiento = objClientes.FechaNacimiento;

             ClientesViewModel viewModel = new ClientesViewModel();
             viewModel.ClientesModel = model;

             if (Request.UrlReferrer != null)
                 viewModel.ViewReturnUrl = Request.UrlReferrer.PathAndQuery;
             else
                 viewModel.ViewReturnUrl = "Index";

             return View(viewModel);
         }

         /// GET: /Clientes/ListarCRUD
         public ActionResult ListarCRUD()
         {
             return View();
         }

         /// GET: /Clientes/ListaBuscar
         public ActionResult ListaBuscar()
         {
             return View(GetViewModel());
         }

         /// GET: /Clientes/AddEditClientes
         private void AddEditClientes(ClientesViewModel viewModel, CrudOperation operation)
         {
             Models.ClientesModel model = viewModel.ClientesModel;
             Clientes objClientes;

             if (operation == CrudOperation.Add)
                objClientes = new Clientes();
             else
                objClientes = Clientes.SelectByPrimaryKey(model.ClienteId);

             objClientes.ClienteId = model.ClienteId;
             objClientes.Nombre = model.Nombre;
             objClientes.Apellido = model.Apellido;
             objClientes.Edad = model.Edad;
             objClientes.FechaNacimiento = model.FechaNacimiento;

             if (operation == CrudOperation.Add)
                objClientes.Insert();
             else
                objClientes.Update();
         }

         private ClientesViewModel GetViewModel()
         {
             ClientesViewModel viewModel = new ClientesViewModel();
             viewModel.ClientesModel = null;

             return viewModel;
         }

         private ClientesCollection GetFilteredData(string sidx, string sord, string filters, out int totalRecords, int rows, int startRowIndex, string sortExpression)
         {
             if (!String.IsNullOrEmpty(filters))
             {
                 if (filters.Contains("field") && filters.Contains("op") && filters.Contains("data"))
                 {
                     filters = filters.Replace("{\"groupOp\":\"AND\",\"rules\":[{", "");
                     filters = filters.Replace("}]}", "");

                     string[] filterArray = Regex.Split(filters, "},{");
                     List<string> fieldName = new List<string>();
                     List<string> data = new List<string>();
                     int ctr = 0;
                     int? clienteId = null;
                     string nombre = String.Empty;
                     string apellido = String.Empty;
                     int? edad = null;
                     DateTime? fechaNacimiento = null;

                     foreach (string filter in filterArray)
                     {
                         string[] fieldsArray = Regex.Split(filter, ",");
                         fieldName.Add(fieldsArray[0].Replace("\"field\":", "").Replace("\"", "").ToLower().Trim());
                         data.Add(fieldsArray[2].Replace("\"data\":", "").Replace("\"", "").ToLower().Trim());
                     }

                     foreach (string item in fieldName)
                     {
                         if (item == "clienteid")
                             clienteId = Convert.ToInt32(data[ctr]);

                         if (item == "nombre")
                             nombre = data[ctr];

                         if (item == "apellido")
                             apellido = data[ctr];

                         if (item == "edad")
                             edad = Convert.ToInt32(data[ctr]);

                         if (item == "fechanacimiento")
                             fechaNacimiento = Convert.ToDateTime(data[ctr]);

                         ctr++;
                     }

                     totalRecords = Clientes.GetRecordCountDynamicWhere(clienteId, nombre, apellido, edad, fechaNacimiento);
                     return Clientes.SelectSkipAndTakeDynamicWhere(clienteId, nombre, apellido, edad, fechaNacimiento, rows, startRowIndex, sortExpression);
                 }
             }

             totalRecords = Clientes.GetRecordCount();
             return Clientes.SelectSkipAndTake(rows, startRowIndex, sortExpression);
         }

         /// GET: /Clientes/GridData
         public ActionResult GridData(string sidx, string sord, int page, int rows)
         {
             int totalRecords = 0;
             int startRowIndex = ((page * rows) - rows) + 1;
             ClientesCollection objClientesCol = Clientes.SelectSkipAndTake(rows, startRowIndex, out totalRecords, sidx + " " + sord);
             int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

             if (objClientesCol == null)
                 return Json("{ total = 0, page = 0, records = 0, rows = null }", JsonRequestBehavior.AllowGet);

             var jsonData = new
             {
                 total = totalPages,
                 page,
                 records = totalRecords,
                 rows = (
                     from objClientes in objClientesCol
                     select new
                     {
                         id = objClientes.ClienteId,
                         cell = new string[] { 
                             objClientes.ClienteId.ToString(),
                             objClientes.Nombre,
                             objClientes.Apellido,
                             objClientes.Edad.ToString(),
                             objClientes.FechaNacimiento.ToShortDateString()
                         }
                     }).ToArray()
             };

             return Json(jsonData, JsonRequestBehavior.AllowGet);
         }

         /// GET: /Clientes/GridDataWithFilters
         public ActionResult GridDataWithFilters(string sidx, string sord, int page, int rows, string filters)
         {
             int totalRecords = 0;
             int startRowIndex = ((page * rows) - rows) + 1;
             ClientesCollection objClientesCol = this.GetFilteredData(sidx, sord, filters, out totalRecords, rows, startRowIndex, sidx + " " + sord);
             int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

             if (objClientesCol == null)
                 return Json("{ total = 0, page = 0, records = 0, rows = null }", JsonRequestBehavior.AllowGet);

             var jsonData = new
             {
                 total = totalPages,
                 page,
                 records = totalRecords,
                 rows = (
                     from objClientes in objClientesCol
                     select new
                     {
                         id = objClientes.ClienteId,
                         cell = new string[] { 
                             objClientes.ClienteId.ToString(),
                             objClientes.Nombre,
                             objClientes.Apellido,
                             objClientes.Edad.ToString(),
                             objClientes.FechaNacimiento.ToShortDateString()
                         }
                     }).ToArray()
             };

             return Json(jsonData, JsonRequestBehavior.AllowGet);
         }
     } 
} 
