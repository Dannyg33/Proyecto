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
     public class DireccionsControllerBase : Controller
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
         public ActionResult Nuevo(DireccionsViewModel viewModel, string returnUrl)
         {
             if (ModelState.IsValid)
             {
                 try
                 {
                     AddEditDireccions(viewModel, CrudOperation.Add);

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
             DireccionsViewModel viewModel = new DireccionsViewModel();
             viewModel.DireccionsModel = null;
             viewModel.Operation = CrudOperation.Add;
             viewModel.ViewControllerName = "Direccions";
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
         public ActionResult Actualizar(int id, DireccionsViewModel viewModel, string returnUrl)
         {
             if (ModelState.IsValid)
             {
                 try
                 {
                     AddEditDireccions(viewModel, CrudOperation.Update);

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
             Direccions objDireccions = Direccions.SelectByPrimaryKey(id);

             Models.DireccionsModel model = new Models.DireccionsModel();
             model.DireccionId = objDireccions.DireccionId;
             model.Calle1 = objDireccions.Calle1;
             model.Calle2 = objDireccions.Calle2;
             model.NumeroCasa = objDireccions.NumeroCasa;
             model.Canton = objDireccions.Canton;
             model.Provincia = objDireccions.Provincia;
             model.Principal = objDireccions.Principal;
             model.ClienteClienteid = objDireccions.ClienteClienteid;

             DireccionsViewModel viewModel = new DireccionsViewModel();
             viewModel.DireccionsModel = model;
             viewModel.Operation = CrudOperation.Update;
             viewModel.ViewControllerName = "Direccions";
             viewModel.ViewActionName = "Actualizar";
             viewModel.ClientesDropDownListData = Clientes.SelectClientesDropDownListData();

             if (Request.UrlReferrer != null)
                 viewModel.ViewReturnUrl = Request.UrlReferrer.PathAndQuery;
             else
                 viewModel.ViewReturnUrl = "Index";

             return View(viewModel);
         }

         [HttpPost]
         public ActionResult Delete(int id, DireccionsViewModel viewModel, string returnUrl)
         {
             Direccions.Delete(id);
             return Json(true);
         }

         public ActionResult Detalles(int id)
         {
             Direccions objDireccions = Direccions.SelectByPrimaryKey(id);

             Models.DireccionsModel model = new Models.DireccionsModel();
             model.DireccionId = objDireccions.DireccionId;
             model.Calle1 = objDireccions.Calle1;
             model.Calle2 = objDireccions.Calle2;
             model.NumeroCasa = objDireccions.NumeroCasa;
             model.Canton = objDireccions.Canton;
             model.Provincia = objDireccions.Provincia;
             model.Principal = objDireccions.Principal;
             model.ClienteClienteid = objDireccions.ClienteClienteid;

             DireccionsViewModel viewModel = new DireccionsViewModel();
             viewModel.DireccionsModel = model;
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
         private void AddEditDireccions(DireccionsViewModel viewModel, CrudOperation operation)
         {
             Models.DireccionsModel model = viewModel.DireccionsModel;
             Direccions objDireccions;

             if (operation == CrudOperation.Add)
                objDireccions = new Direccions();
             else
                objDireccions = Direccions.SelectByPrimaryKey(model.DireccionId);

             objDireccions.DireccionId = model.DireccionId;
             objDireccions.Calle1 = model.Calle1;
             objDireccions.Calle2 = model.Calle2;
             objDireccions.NumeroCasa = model.NumeroCasa;
             objDireccions.Canton = model.Canton;
             objDireccions.Provincia = model.Provincia;
             objDireccions.Principal = model.Principal;
             objDireccions.ClienteClienteid = model.ClienteClienteid;

             if (operation == CrudOperation.Add)
                objDireccions.Insert();
             else
                objDireccions.Update();
         }

         private DireccionsViewModel GetViewModel()
         {
             DireccionsViewModel viewModel = new DireccionsViewModel();
             viewModel.DireccionsModel = null;
             viewModel.ClientesDropDownListData = Clientes.SelectClientesDropDownListData();

             return viewModel;
         }

         private DireccionsCollection GetFilteredData(string sidx, string sord, string filters, out int totalRecords, int rows, int startRowIndex, string sortExpression)
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
                     int? direccionId = null;
                     string calle1 = String.Empty;
                     string calle2 = String.Empty;
                     string numeroCasa = String.Empty;
                     string canton = String.Empty;
                     string provincia = String.Empty;
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
                         if (item == "direccionid")
                             direccionId = Convert.ToInt32(data[ctr]);

                         if (item == "calle1")
                             calle1 = data[ctr];

                         if (item == "calle2")
                             calle2 = data[ctr];

                         if (item == "numerocasa")
                             numeroCasa = data[ctr];

                         if (item == "canton")
                             canton = data[ctr];

                         if (item == "provincia")
                             provincia = data[ctr];

                         if (item == "principal")
                             principal = Convert.ToBoolean(data[ctr]);

                         if (item == "clienteclienteid")
                             clienteClienteid = Convert.ToInt32(data[ctr]);

                         ctr++;
                     }

                     totalRecords = Direccions.GetRecordCountDynamicWhere(direccionId, calle1, calle2, numeroCasa, canton, provincia, principal, clienteClienteid);
                     return Direccions.SelectSkipAndTakeDynamicWhere(direccionId, calle1, calle2, numeroCasa, canton, provincia, principal, clienteClienteid, rows, startRowIndex, sortExpression);
                 }
             }

             totalRecords = Direccions.GetRecordCount();
             return Direccions.SelectSkipAndTake(rows, startRowIndex, sortExpression);
         }
         public ActionResult GridData(string sidx, string sord, int page, int rows)
         {
             int totalRecords = 0;
             int startRowIndex = ((page * rows) - rows) + 1;
             DireccionsCollection objDireccionsCol = Direccions.SelectSkipAndTake(rows, startRowIndex, out totalRecords, sidx + " " + sord);
             int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

             if (objDireccionsCol == null)
                 return Json("{ total = 0, page = 0, records = 0, rows = null }", JsonRequestBehavior.AllowGet);

             var jsonData = new
             {
                 total = totalPages,
                 page,
                 records = totalRecords,
                 rows = (
                     from objDireccions in objDireccionsCol
                     select new
                     {
                         id = objDireccions.DireccionId,
                         cell = new string[] { 
                             objDireccions.DireccionId.ToString(),
                             objDireccions.Calle1,
                             objDireccions.Calle2,
                             objDireccions.NumeroCasa,
                             objDireccions.Canton,
                             objDireccions.Provincia,
                             objDireccions.Principal.ToString(),
                             objDireccions.ClienteClienteid.HasValue ? objDireccions.ClienteClienteid.Value.ToString() : ""
                         }
                     }).ToArray()
             };

             return Json(jsonData, JsonRequestBehavior.AllowGet);
         }

         public ActionResult GridDataWithFilters(string sidx, string sord, int page, int rows, string filters)
         {
             int totalRecords = 0;
             int startRowIndex = ((page * rows) - rows) + 1;
             DireccionsCollection objDireccionsCol = this.GetFilteredData(sidx, sord, filters, out totalRecords, rows, startRowIndex, sidx + " " + sord);
             int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

             if (objDireccionsCol == null)
                 return Json("{ total = 0, page = 0, records = 0, rows = null }", JsonRequestBehavior.AllowGet);

             var jsonData = new
             {
                 total = totalPages,
                 page,
                 records = totalRecords,
                 rows = (
                     from objDireccions in objDireccionsCol
                     select new
                     {
                         id = objDireccions.DireccionId,
                         cell = new string[] { 
                             objDireccions.DireccionId.ToString(),
                             objDireccions.Calle1,
                             objDireccions.Calle2,
                             objDireccions.NumeroCasa,
                             objDireccions.Canton,
                             objDireccions.Provincia,
                             objDireccions.Principal.ToString(),
                             objDireccions.ClienteClienteid.HasValue ? objDireccions.ClienteClienteid.Value.ToString() : ""
                         }
                     }).ToArray()
             };

             return Json(jsonData, JsonRequestBehavior.AllowGet);
         }
     } 
} 
