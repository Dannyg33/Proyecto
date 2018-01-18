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
     public class CorreosControllerBase : Controller
     { 

         /// GET: /Correos/
         public ActionResult Index()
         {
             return View();
         }

         /// GET: /Correos/Nuevo
         public ActionResult Nuevo()
         {
             return GetAddViewModel();
         }

         /// POST: /Correos/Nuevo
         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Nuevo(CorreosViewModel viewModel, string returnUrl)
         {
             if (ModelState.IsValid)
             {
                 try
                 {
                     // add new record
                     AddEditCorreos(viewModel, CrudOperation.Add);

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
             CorreosViewModel viewModel = new CorreosViewModel();
             viewModel.CorreosModel = null;
             viewModel.Operation = CrudOperation.Add;
             viewModel.ViewControllerName = "Correos";
             viewModel.ViewActionName = "Nuevo";
             viewModel.ClientesDropDownListData = Clientes.SelectClientesDropDownListData();

             if (Request.UrlReferrer != null)
                 viewModel.ViewReturnUrl = Request.UrlReferrer.PathAndQuery;
             else
                 viewModel.ViewReturnUrl = "Index";

             return View(viewModel);
         }

         /// GET: /Correos/Actualizar/5
         public ActionResult Actualizar(int id)
         {
             return GetUpdateViewModel(id);
         }

         /// POST: /Correos/Actualizar/5
         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Actualizar(int id, CorreosViewModel viewModel, string returnUrl)
         {
             if (ModelState.IsValid)
             {
                 try
                 {
                     // update record
                     AddEditCorreos(viewModel, CrudOperation.Update);

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
             Correos objCorreos = Correos.SelectByPrimaryKey(id);

             Models.CorreosModel model = new Models.CorreosModel();
             model.CorreoID = objCorreos.CorreoID;
             model.Mail = objCorreos.Mail;
             model.Principal = objCorreos.Principal;
             model.ClienteClienteid = objCorreos.ClienteClienteid;

             CorreosViewModel viewModel = new CorreosViewModel();
             viewModel.CorreosModel = model;
             viewModel.Operation = CrudOperation.Update;
             viewModel.ViewControllerName = "Correos";
             viewModel.ViewActionName = "Actualizar";
             viewModel.ClientesDropDownListData = Clientes.SelectClientesDropDownListData();

             if (Request.UrlReferrer != null)
                 viewModel.ViewReturnUrl = Request.UrlReferrer.PathAndQuery;
             else
                 viewModel.ViewReturnUrl = "Index";

             return View(viewModel);
         }

         /// POST: /Correos/Delete/5
         [HttpPost]
         public ActionResult Delete(int id, CorreosViewModel viewModel, string returnUrl)
         {
             Correos.Delete(id);
             return Json(true);
         }

         /// GET: /Correos/Detalles/5
         public ActionResult Detalles(int id)
         {
             Correos objCorreos = Correos.SelectByPrimaryKey(id);

             Models.CorreosModel model = new Models.CorreosModel();
             model.CorreoID = objCorreos.CorreoID;
             model.Mail = objCorreos.Mail;
             model.Principal = objCorreos.Principal;
             model.ClienteClienteid = objCorreos.ClienteClienteid;

             CorreosViewModel viewModel = new CorreosViewModel();
             viewModel.CorreosModel = model;
             viewModel.ClientesDropDownListData = Clientes.SelectClientesDropDownListData();

             if (Request.UrlReferrer != null)
                 viewModel.ViewReturnUrl = Request.UrlReferrer.PathAndQuery;
             else
                 viewModel.ViewReturnUrl = "Index";

             return View(viewModel);
         }

         /// GET: /Correos/ListarCRUD
         public ActionResult ListarCRUD()
         {
             return View();
         }

         /// GET: /Correos/ListaBuscar
         public ActionResult ListaBuscar()
         {
             return View(GetViewModel());
         }

         /// GET: /Correos/AddEditCorreos
         private void AddEditCorreos(CorreosViewModel viewModel, CrudOperation operation)
         {
             Models.CorreosModel model = viewModel.CorreosModel;
             Correos objCorreos;

             if (operation == CrudOperation.Add)
                objCorreos = new Correos();
             else
                objCorreos = Correos.SelectByPrimaryKey(model.CorreoID);

             objCorreos.CorreoID = model.CorreoID;
             objCorreos.Mail = model.Mail;
             objCorreos.Principal = model.Principal;
             objCorreos.ClienteClienteid = model.ClienteClienteid;

             if (operation == CrudOperation.Add)
                objCorreos.Insert();
             else
                objCorreos.Update();
         }

         private CorreosViewModel GetViewModel()
         {
             CorreosViewModel viewModel = new CorreosViewModel();
             viewModel.CorreosModel = null;
             viewModel.ClientesDropDownListData = Clientes.SelectClientesDropDownListData();

             return viewModel;
         }

         private CorreosCollection GetFilteredData(string sidx, string sord, string filters, out int totalRecords, int rows, int startRowIndex, string sortExpression)
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
                     int? correoID = null;
                     string mail = String.Empty;
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
                         if (item == "correoid")
                             correoID = Convert.ToInt32(data[ctr]);

                         if (item == "mail")
                             mail = data[ctr];

                         if (item == "principal")
                             principal = Convert.ToBoolean(data[ctr]);

                         if (item == "clienteclienteid")
                             clienteClienteid = Convert.ToInt32(data[ctr]);

                         ctr++;
                     }

                     totalRecords = Correos.GetRecordCountDynamicWhere(correoID, mail, principal, clienteClienteid);
                     return Correos.SelectSkipAndTakeDynamicWhere(correoID, mail, principal, clienteClienteid, rows, startRowIndex, sortExpression);
                 }
             }

             totalRecords = Correos.GetRecordCount();
             return Correos.SelectSkipAndTake(rows, startRowIndex, sortExpression);
         }

         /// GET: /Correos/GridData
         public ActionResult GridData(string sidx, string sord, int page, int rows)
         {
             int totalRecords = 0;
             int startRowIndex = ((page * rows) - rows) + 1;
             CorreosCollection objCorreosCol = Correos.SelectSkipAndTake(rows, startRowIndex, out totalRecords, sidx + " " + sord);
             int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

             if (objCorreosCol == null)
                 return Json("{ total = 0, page = 0, records = 0, rows = null }", JsonRequestBehavior.AllowGet);

             var jsonData = new
             {
                 total = totalPages,
                 page,
                 records = totalRecords,
                 rows = (
                     from objCorreos in objCorreosCol
                     select new
                     {
                         id = objCorreos.CorreoID,
                         cell = new string[] { 
                             objCorreos.CorreoID.ToString(),
                             objCorreos.Mail,
                             objCorreos.Principal.ToString(),
                             objCorreos.ClienteClienteid.HasValue ? objCorreos.ClienteClienteid.Value.ToString() : ""
                         }
                     }).ToArray()
             };

             return Json(jsonData, JsonRequestBehavior.AllowGet);
         }

         /// GET: /Correos/GridDataWithFilters
         public ActionResult GridDataWithFilters(string sidx, string sord, int page, int rows, string filters)
         {
             int totalRecords = 0;
             int startRowIndex = ((page * rows) - rows) + 1;
             CorreosCollection objCorreosCol = this.GetFilteredData(sidx, sord, filters, out totalRecords, rows, startRowIndex, sidx + " " + sord);
             int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

             if (objCorreosCol == null)
                 return Json("{ total = 0, page = 0, records = 0, rows = null }", JsonRequestBehavior.AllowGet);

             var jsonData = new
             {
                 total = totalPages,
                 page,
                 records = totalRecords,
                 rows = (
                     from objCorreos in objCorreosCol
                     select new
                     {
                         id = objCorreos.CorreoID,
                         cell = new string[] { 
                             objCorreos.CorreoID.ToString(),
                             objCorreos.Mail,
                             objCorreos.Principal.ToString(),
                             objCorreos.ClienteClienteid.HasValue ? objCorreos.ClienteClienteid.Value.ToString() : ""
                         }
                     }).ToArray()
             };

             return Json(jsonData, JsonRequestBehavior.AllowGet);
         }
     } 
} 
