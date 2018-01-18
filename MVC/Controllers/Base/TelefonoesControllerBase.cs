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
     
     public class TelefonoesControllerBase : Controller
     { 

         
         public ActionResult Index()
         {
             return View();
         }

         
         public ActionResult Nuevo()
         {
             return GetAddViewModel();
         }

         
         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Nuevo(TelefonoesViewModel viewModel, string returnUrl)
         {
             if (ModelState.IsValid)
             {
                 try
                 {
                     AddEditTelefonoes(viewModel, CrudOperation.Add);

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
             TelefonoesViewModel viewModel = new TelefonoesViewModel();
             viewModel.TelefonoesModel = null;
             viewModel.Operation = CrudOperation.Add;
             viewModel.ViewControllerName = "Telefonoes";
             viewModel.ViewActionName = "Nuevo";
             viewModel.ClientesDropDownListData = Clientes.SelectClientesDropDownListData();

             if (Request.UrlReferrer != null)
                 viewModel.ViewReturnUrl = Request.UrlReferrer.PathAndQuery;
             else
                 viewModel.ViewReturnUrl = "Index";

             return View(viewModel);
         }

                  public ActionResult Actualizar(int id)
         {
             return GetUpdateViewModel(id);
         }

         
         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Actualizar(int id, TelefonoesViewModel viewModel, string returnUrl)
         {
             if (ModelState.IsValid)
             {
                 try
                 {
                     AddEditTelefonoes(viewModel, CrudOperation.Update);

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
             Telefonoes objTelefonoes = Telefonoes.SelectByPrimaryKey(id);

             Models.TelefonoesModel model = new Models.TelefonoesModel();
             model.TelefonoID = objTelefonoes.TelefonoID;
             model.Numero = objTelefonoes.Numero;
             model.Principal = objTelefonoes.Principal;
             model.ClienteClienteid = objTelefonoes.ClienteClienteid;

             TelefonoesViewModel viewModel = new TelefonoesViewModel();
             viewModel.TelefonoesModel = model;
             viewModel.Operation = CrudOperation.Update;
             viewModel.ViewControllerName = "Telefonoes";
             viewModel.ViewActionName = "Actualizar";
             viewModel.ClientesDropDownListData = Clientes.SelectClientesDropDownListData();

             if (Request.UrlReferrer != null)
                 viewModel.ViewReturnUrl = Request.UrlReferrer.PathAndQuery;
             else
                 viewModel.ViewReturnUrl = "Index";

             return View(viewModel);
         }

         
         [HttpPost]
         public ActionResult Delete(int id, TelefonoesViewModel viewModel, string returnUrl)
         {
             Telefonoes.Delete(id);
             return Json(true);
         }

         
         public ActionResult Detalles(int id)
         {
             Telefonoes objTelefonoes = Telefonoes.SelectByPrimaryKey(id);

             Models.TelefonoesModel model = new Models.TelefonoesModel();
             model.TelefonoID = objTelefonoes.TelefonoID;
             model.Numero = objTelefonoes.Numero;
             model.Principal = objTelefonoes.Principal;
             model.ClienteClienteid = objTelefonoes.ClienteClienteid;

             TelefonoesViewModel viewModel = new TelefonoesViewModel();
             viewModel.TelefonoesModel = model;
             viewModel.ClientesDropDownListData = Clientes.SelectClientesDropDownListData();

             if (Request.UrlReferrer != null)
                 viewModel.ViewReturnUrl = Request.UrlReferrer.PathAndQuery;
             else
                 viewModel.ViewReturnUrl = "Index";

             return View(viewModel);
         }

         
         public ActionResult ListarCRUD()
         {
             return View();
         }

         
         public ActionResult ListaBuscar()
         {
             return View(GetViewModel());
         }

         
         private void AddEditTelefonoes(TelefonoesViewModel viewModel, CrudOperation operation)
         {
             Models.TelefonoesModel model = viewModel.TelefonoesModel;
             Telefonoes objTelefonoes;

             if (operation == CrudOperation.Add)
                objTelefonoes = new Telefonoes();
             else
                objTelefonoes = Telefonoes.SelectByPrimaryKey(model.TelefonoID);

             objTelefonoes.TelefonoID = model.TelefonoID;
             objTelefonoes.Numero = model.Numero;
             objTelefonoes.Principal = model.Principal;
             objTelefonoes.ClienteClienteid = model.ClienteClienteid;

             if (operation == CrudOperation.Add)
                objTelefonoes.Insert();
             else
                objTelefonoes.Update();
         }

         private TelefonoesViewModel GetViewModel()
         {
             TelefonoesViewModel viewModel = new TelefonoesViewModel();
             viewModel.TelefonoesModel = null;
             viewModel.ClientesDropDownListData = Clientes.SelectClientesDropDownListData();

             return viewModel;
         }

         private TelefonoesCollection GetFilteredData(string sidx, string sord, string filters, out int totalRecords, int rows, int startRowIndex, string sortExpression)
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
                     int? telefonoID = null;
                     string numero = String.Empty;
                     bool? principal = null;
                     int? clienteClienteid = null;

                     foreach (string filter in filterArray)
                     {
                         string[] fieldsArray = Regex.Split(filter, ",");
                         fieldName.Add(fieldsArray[0].Replace("\"field\":", "").Replace("\"", "").ToLower().Trim());
                         data.Add(fieldsArray[2].Replace("\"data\":", "").Replace("\"", "").ToLower().Trim());
                     }

                     foreach (string item in fieldName)
                     {
                         if (item == "telefonoid")
                             telefonoID = Convert.ToInt32(data[ctr]);

                         if (item == "numero")
                             numero = data[ctr];

                         if (item == "principal")
                             principal = Convert.ToBoolean(data[ctr]);

                         if (item == "clienteclienteid")
                             clienteClienteid = Convert.ToInt32(data[ctr]);

                         ctr++;
                     }

                     totalRecords = Telefonoes.GetRecordCountDynamicWhere(telefonoID, numero, principal, clienteClienteid);
                     return Telefonoes.SelectSkipAndTakeDynamicWhere(telefonoID, numero, principal, clienteClienteid, rows, startRowIndex, sortExpression);
                 }
             }

             totalRecords = Telefonoes.GetRecordCount();
             return Telefonoes.SelectSkipAndTake(rows, startRowIndex, sortExpression);
         }

         
         public ActionResult GridData(string sidx, string sord, int page, int rows)
         {
             int totalRecords = 0;
             int startRowIndex = ((page * rows) - rows) + 1;
             TelefonoesCollection objTelefonoesCol = Telefonoes.SelectSkipAndTake(rows, startRowIndex, out totalRecords, sidx + " " + sord);
             int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

             if (objTelefonoesCol == null)
                 return Json("{ total = 0, page = 0, records = 0, rows = null }", JsonRequestBehavior.AllowGet);

             var jsonData = new
             {
                 total = totalPages,
                 page,
                 records = totalRecords,
                 rows = (
                     from objTelefonoes in objTelefonoesCol
                     select new
                     {
                         id = objTelefonoes.TelefonoID,
                         cell = new string[] { 
                             objTelefonoes.TelefonoID.ToString(),
                             objTelefonoes.Numero,
                             objTelefonoes.Principal.ToString(),
                             objTelefonoes.ClienteClienteid.HasValue ? objTelefonoes.ClienteClienteid.Value.ToString() : ""
                         }
                     }).ToArray()
             };

             return Json(jsonData, JsonRequestBehavior.AllowGet);
         }

         
         public ActionResult GridDataWithFilters(string sidx, string sord, int page, int rows, string filters)
         {
             int totalRecords = 0;
             int startRowIndex = ((page * rows) - rows) + 1;
             TelefonoesCollection objTelefonoesCol = this.GetFilteredData(sidx, sord, filters, out totalRecords, rows, startRowIndex, sidx + " " + sord);
             int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

             if (objTelefonoesCol == null)
                 return Json("{ total = 0, page = 0, records = 0, rows = null }", JsonRequestBehavior.AllowGet);

             var jsonData = new
             {
                 total = totalPages,
                 page,
                 records = totalRecords,
                 rows = (
                     from objTelefonoes in objTelefonoesCol
                     select new
                     {
                         id = objTelefonoes.TelefonoID,
                         cell = new string[] { 
                             objTelefonoes.TelefonoID.ToString(),
                             objTelefonoes.Numero,
                             objTelefonoes.Principal.ToString(),
                             objTelefonoes.ClienteClienteid.HasValue ? objTelefonoes.ClienteClienteid.Value.ToString() : ""
                         }
                     }).ToArray()
             };

             return Json(jsonData, JsonRequestBehavior.AllowGet);
         }
     } 
} 
