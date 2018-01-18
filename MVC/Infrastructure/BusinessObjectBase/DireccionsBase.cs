using System;
using System.Data;
using MVC.DataLayer;
using MVC.BusinessObject;
using MVC.Models;
using System.Web.Script.Serialization;

namespace MVC.BusinessObject.Base
{
    
     public class DireccionsBase : MVC.Models.DireccionsModel
     {
         
         [ScriptIgnore]
         public Lazy<Clientes> Clientes
         {
             get
             {
                 int value;
                 bool hasValue = Int32.TryParse(ClienteClienteid.ToString(), out value);

                 if (hasValue)
                     return new Lazy<Clientes>(() => BusinessObject.Clientes.SelectByPrimaryKey(value));
                 else
                     return null;
             }
             set{ }
         } 


         public DireccionsBase()
         {
         }

     
         public static Direccions SelectByPrimaryKey(int direccionId)
         {
             return DireccionsDataLayer.SelectByPrimaryKey(direccionId);
         }

     
         public static int GetRecordCount()
         {
             return DireccionsDataLayer.GetRecordCount();
         }

        
         public static int GetRecordCountByClienteClienteid(int clienteClienteid)
         {
             return DireccionsDataLayer.GetRecordCountByClienteClienteid(clienteClienteid);
         }

      
         public static int GetRecordCountDynamicWhere(int? direccionId, string calle1, string calle2, string numeroCasa, string canton, string provincia, bool? principal, int? clienteClienteid)
         {
             return DireccionsDataLayer.GetRecordCountDynamicWhere(direccionId, calle1, calle2, numeroCasa, canton, provincia, principal, clienteClienteid);
         }

         public static DireccionsCollection SelectSkipAndTake(int rows, int startRowIndex, out int totalRowCount, string sortByExpression)
         {
             totalRowCount = GetRecordCount();
             sortByExpression = GetSortExpression(sortByExpression);

             return DireccionsDataLayer.SelectSkipAndTake(sortByExpression, startRowIndex, rows);
         }

         public static DireccionsCollection SelectSkipAndTake(int rows, int startRowIndex, string sortByExpression)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             return DireccionsDataLayer.SelectSkipAndTake(sortByExpression, startRowIndex, rows);
         }
        
         public static DireccionsCollection SelectSkipAndTakeByClienteClienteid(int rows, int startRowIndex, out int totalRowCount, string sortByExpression, int clienteClienteid)
         {
             totalRowCount = DireccionsDataLayer.GetRecordCountByClienteClienteid(clienteClienteid);
             sortByExpression = GetSortExpression(sortByExpression);
             return DireccionsDataLayer.SelectSkipAndTakeByClienteClienteid(sortByExpression, startRowIndex, rows, clienteClienteid);
         }

         public static DireccionsCollection SelectSkipAndTakeByClienteClienteid(int rows, int startRowIndex, string sortByExpression, int clienteClienteid)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             return DireccionsDataLayer.SelectSkipAndTakeByClienteClienteid(sortByExpression, startRowIndex, rows, clienteClienteid);
         }

         public static DireccionsCollection SelectSkipAndTakeDynamicWhere(int? direccionId, string calle1, string calle2, string numeroCasa, string canton, string provincia, bool? principal, int? clienteClienteid, int rows, int startRowIndex, out int totalRowCount, string sortByExpression)
         {
             totalRowCount = GetRecordCountDynamicWhere(direccionId, calle1, calle2, numeroCasa, canton, provincia, principal, clienteClienteid);
             sortByExpression = GetSortExpression(sortByExpression);
             return DireccionsDataLayer.SelectSkipAndTakeDynamicWhere(direccionId, calle1, calle2, numeroCasa, canton, provincia, principal, clienteClienteid, sortByExpression, startRowIndex, rows);
         }

        
         public static DireccionsCollection SelectSkipAndTakeDynamicWhere(int? direccionId, string calle1, string calle2, string numeroCasa, string canton, string provincia, bool? principal, int? clienteClienteid, int rows, int startRowIndex, string sortByExpression)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             return DireccionsDataLayer.SelectSkipAndTakeDynamicWhere(direccionId, calle1, calle2, numeroCasa, canton, provincia, principal, clienteClienteid, sortByExpression, startRowIndex, rows);
         }

      
         public static DireccionsCollection SelectAll()
         {
             return DireccionsDataLayer.SelectAll();
         }

     
         public static DireccionsCollection SelectAll(string sortExpression)
         {
             DireccionsCollection objDireccionsCol = DireccionsDataLayer.SelectAll();
             return SortByExpression(objDireccionsCol, sortExpression);
         }

       
         public static DireccionsCollection SelectAllDynamicWhere(int? direccionId, string calle1, string calle2, string numeroCasa, string canton, string provincia, bool? principal, int? clienteClienteid)
         {
             return DireccionsDataLayer.SelectAllDynamicWhere(direccionId, calle1, calle2, numeroCasa, canton, provincia, principal, clienteClienteid);
         }

         public static DireccionsCollection SelectAllDynamicWhere(int? direccionId, string calle1, string calle2, string numeroCasa, string canton, string provincia, bool? principal, int? clienteClienteid, string sortExpression)
         {
             DireccionsCollection objDireccionsCol = DireccionsDataLayer.SelectAllDynamicWhere(direccionId, calle1, calle2, numeroCasa, canton, provincia, principal, clienteClienteid);
             return SortByExpression(objDireccionsCol, sortExpression);
         }

        
         public static DireccionsCollection SelectDireccionsCollectionByClienteClienteid(int clienteId)
         {
             return DireccionsDataLayer.SelectDireccionsCollectionByClienteClienteid(clienteId);
         }

       
         public static DireccionsCollection SelectDireccionsCollectionByClienteClienteid(int clienteId, string sortExpression)
         {
             DireccionsCollection objDireccionsCol = DireccionsDataLayer.SelectDireccionsCollectionByClienteClienteid(clienteId);
             return SortByExpression(objDireccionsCol, sortExpression);
         }

    
         public static DireccionsCollection SelectDireccionsDropDownListData()
         {
             return DireccionsDataLayer.SelectDireccionsDropDownListData();
         }

     
         public static DireccionsCollection SortByExpression(DireccionsCollection objDireccionsCol, string sortExpression)
         {
             bool isSortDescending = sortExpression.ToLower().Contains(" desc");

             if (isSortDescending)
             {
                 sortExpression = sortExpression.Replace(" DESC", "");
                 sortExpression = sortExpression.Replace(" desc", "");
             }
             else
             {
                 sortExpression = sortExpression.Replace(" ASC", "");
                 sortExpression = sortExpression.Replace(" asc", "");
             }

             switch (sortExpression)
             {
                 case "DireccionId":
                     objDireccionsCol.Sort(MVC.BusinessObject.Direccions.ByDireccionId);
                     break;
                 case "Calle1":
                     objDireccionsCol.Sort(MVC.BusinessObject.Direccions.ByCalle1);
                     break;
                 case "Calle2":
                     objDireccionsCol.Sort(MVC.BusinessObject.Direccions.ByCalle2);
                     break;
                 case "NumeroCasa":
                     objDireccionsCol.Sort(MVC.BusinessObject.Direccions.ByNumeroCasa);
                     break;
                 case "Canton":
                     objDireccionsCol.Sort(MVC.BusinessObject.Direccions.ByCanton);
                     break;
                 case "Provincia":
                     objDireccionsCol.Sort(MVC.BusinessObject.Direccions.ByProvincia);
                     break;
                 case "Principal":
                     objDireccionsCol.Sort(MVC.BusinessObject.Direccions.ByPrincipal);
                     break;
                 case "ClienteClienteid":
                     objDireccionsCol.Sort(MVC.BusinessObject.Direccions.ByClienteClienteid);
                     break;
                 default:
                     break;
             }

             if (isSortDescending)
                 objDireccionsCol.Reverse();

             return objDireccionsCol;
         }

       
         public int Insert()
         {
             Direccions objDireccions = (Direccions)this;
             return DireccionsDataLayer.Insert(objDireccions);
         }

      
         public void Update()
         {
             Direccions objDireccions = (Direccions)this;
             DireccionsDataLayer.Update(objDireccions);
         }

    
         public static void Delete(int direccionId)
         {
             DireccionsDataLayer.Delete(direccionId);
         }

         private static string GetSortExpression(string sortByExpression)
         {
             if (String.IsNullOrEmpty(sortByExpression) || sortByExpression == " asc")
                 sortByExpression = "DireccionId";
             else if (sortByExpression.Contains(" asc"))
                 sortByExpression = sortByExpression.Replace(" asc", "");

             return sortByExpression;
         }

       
         public static Comparison<Direccions> ByDireccionId = delegate(Direccions x, Direccions y)
         {
             return x.DireccionId.CompareTo(y.DireccionId);
         };

       
         public static Comparison<Direccions> ByCalle1 = delegate(Direccions x, Direccions y)
         {
             string value1 = x.Calle1 ?? String.Empty;
             string value2 = y.Calle1 ?? String.Empty;
             return value1.CompareTo(value2);
         };

      
         public static Comparison<Direccions> ByCalle2 = delegate(Direccions x, Direccions y)
         {
             string value1 = x.Calle2 ?? String.Empty;
             string value2 = y.Calle2 ?? String.Empty;
             return value1.CompareTo(value2);
         };

      
         public static Comparison<Direccions> ByNumeroCasa = delegate(Direccions x, Direccions y)
         {
             string value1 = x.NumeroCasa ?? String.Empty;
             string value2 = y.NumeroCasa ?? String.Empty;
             return value1.CompareTo(value2);
         };

   
         public static Comparison<Direccions> ByCanton = delegate(Direccions x, Direccions y)
         {
             string value1 = x.Canton ?? String.Empty;
             string value2 = y.Canton ?? String.Empty;
             return value1.CompareTo(value2);
         };

       
         public static Comparison<Direccions> ByProvincia = delegate(Direccions x, Direccions y)
         {
             string value1 = x.Provincia ?? String.Empty;
             string value2 = y.Provincia ?? String.Empty;
             return value1.CompareTo(value2);
         };

        
         public static Comparison<Direccions> ByPrincipal = delegate(Direccions x, Direccions y)
         {
             return x.Principal.CompareTo(y.Principal);
         };

        
         public static Comparison<Direccions> ByClienteClienteid = delegate(Direccions x, Direccions y)
         {
             return Nullable.Compare(x.ClienteClienteid, y.ClienteClienteid);
         };

     }
}
