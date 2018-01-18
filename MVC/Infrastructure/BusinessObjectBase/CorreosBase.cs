using System;
using System.Data;
using MVC.DataLayer;
using MVC.BusinessObject;
using MVC.Models;
using System.Web.Script.Serialization;

namespace MVC.BusinessObject.Base
{
     
     public class CorreosBase : MVC.Models.CorreosModel
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


         
         public CorreosBase()
         {
         }

         
         public static Correos SelectByPrimaryKey(int correoID)
         {
             return CorreosDataLayer.SelectByPrimaryKey(correoID);
         }

         
         public static int GetRecordCount()
         {
             return CorreosDataLayer.GetRecordCount();
         }

         
         public static int GetRecordCountByClienteClienteid(int clienteClienteid)
         {
             return CorreosDataLayer.GetRecordCountByClienteClienteid(clienteClienteid);
         }

         
         public static int GetRecordCountDynamicWhere(int? correoID, string mail, bool? principal, int? clienteClienteid)
         {
             return CorreosDataLayer.GetRecordCountDynamicWhere(correoID, mail, principal, clienteClienteid);
         }

         
         public static CorreosCollection SelectSkipAndTake(int rows, int startRowIndex, out int totalRowCount, string sortByExpression)
         {
             totalRowCount = GetRecordCount();
             sortByExpression = GetSortExpression(sortByExpression);

             return CorreosDataLayer.SelectSkipAndTake(sortByExpression, startRowIndex, rows);
         }

         
         public static CorreosCollection SelectSkipAndTake(int rows, int startRowIndex, string sortByExpression)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             return CorreosDataLayer.SelectSkipAndTake(sortByExpression, startRowIndex, rows);
         }

         
         public static CorreosCollection SelectSkipAndTakeByClienteClienteid(int rows, int startRowIndex, out int totalRowCount, string sortByExpression, int clienteClienteid)
         {
             totalRowCount = CorreosDataLayer.GetRecordCountByClienteClienteid(clienteClienteid);
             sortByExpression = GetSortExpression(sortByExpression);
             return CorreosDataLayer.SelectSkipAndTakeByClienteClienteid(sortByExpression, startRowIndex, rows, clienteClienteid);
         }

         
         public static CorreosCollection SelectSkipAndTakeByClienteClienteid(int rows, int startRowIndex, string sortByExpression, int clienteClienteid)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             return CorreosDataLayer.SelectSkipAndTakeByClienteClienteid(sortByExpression, startRowIndex, rows, clienteClienteid);
         }

         
         public static CorreosCollection SelectSkipAndTakeDynamicWhere(int? correoID, string mail, bool? principal, int? clienteClienteid, int rows, int startRowIndex, out int totalRowCount, string sortByExpression)
         {
             totalRowCount = GetRecordCountDynamicWhere(correoID, mail, principal, clienteClienteid);
             sortByExpression = GetSortExpression(sortByExpression);
             return CorreosDataLayer.SelectSkipAndTakeDynamicWhere(correoID, mail, principal, clienteClienteid, sortByExpression, startRowIndex, rows);
         }

         
         public static CorreosCollection SelectSkipAndTakeDynamicWhere(int? correoID, string mail, bool? principal, int? clienteClienteid, int rows, int startRowIndex, string sortByExpression)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             return CorreosDataLayer.SelectSkipAndTakeDynamicWhere(correoID, mail, principal, clienteClienteid, sortByExpression, startRowIndex, rows);
         }

         
         public static CorreosCollection SelectAll()
         {
             return CorreosDataLayer.SelectAll();
         }

         
         public static CorreosCollection SelectAll(string sortExpression)
         {
             CorreosCollection objCorreosCol = CorreosDataLayer.SelectAll();
             return SortByExpression(objCorreosCol, sortExpression);
         }

         
         public static CorreosCollection SelectAllDynamicWhere(int? correoID, string mail, bool? principal, int? clienteClienteid)
         {
             return CorreosDataLayer.SelectAllDynamicWhere(correoID, mail, principal, clienteClienteid);
         }

         
         public static CorreosCollection SelectAllDynamicWhere(int? correoID, string mail, bool? principal, int? clienteClienteid, string sortExpression)
         {
             CorreosCollection objCorreosCol = CorreosDataLayer.SelectAllDynamicWhere(correoID, mail, principal, clienteClienteid);
             return SortByExpression(objCorreosCol, sortExpression);
         }

         
         public static CorreosCollection SelectCorreosCollectionByClienteClienteid(int clienteId)
         {
             return CorreosDataLayer.SelectCorreosCollectionByClienteClienteid(clienteId);
         }

         
         public static CorreosCollection SelectCorreosCollectionByClienteClienteid(int clienteId, string sortExpression)
         {
             CorreosCollection objCorreosCol = CorreosDataLayer.SelectCorreosCollectionByClienteClienteid(clienteId);
             return SortByExpression(objCorreosCol, sortExpression);
         }

         
         public static CorreosCollection SelectCorreosDropDownListData()
         {
             return CorreosDataLayer.SelectCorreosDropDownListData();
         }

         
         public static CorreosCollection SortByExpression(CorreosCollection objCorreosCol, string sortExpression)
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
                 case "CorreoID":
                     objCorreosCol.Sort(MVC.BusinessObject.Correos.ByCorreoID);
                     break;
                 case "Mail":
                     objCorreosCol.Sort(MVC.BusinessObject.Correos.ByMail);
                     break;
                 case "Principal":
                     objCorreosCol.Sort(MVC.BusinessObject.Correos.ByPrincipal);
                     break;
                 case "ClienteClienteid":
                     objCorreosCol.Sort(MVC.BusinessObject.Correos.ByClienteClienteid);
                     break;
                 default:
                     break;
             }

             if (isSortDescending)
                 objCorreosCol.Reverse();

             return objCorreosCol;
         }

         
         public int Insert()
         {
             Correos objCorreos = (Correos)this;
             return CorreosDataLayer.Insert(objCorreos);
         }

         
         public void Update()
         {
             Correos objCorreos = (Correos)this;
             CorreosDataLayer.Update(objCorreos);
         }

         
         public static void Delete(int correoID)
         {
             CorreosDataLayer.Delete(correoID);
         }

         private static string GetSortExpression(string sortByExpression)
         {
             if (String.IsNullOrEmpty(sortByExpression) || sortByExpression == " asc")
                 sortByExpression = "CorreoID";
             else if (sortByExpression.Contains(" asc"))
                 sortByExpression = sortByExpression.Replace(" asc", "");

             return sortByExpression;
         }

         
         public static Comparison<Correos> ByCorreoID = delegate(Correos x, Correos y)
         {
             return x.CorreoID.CompareTo(y.CorreoID);
         };

         
         public static Comparison<Correos> ByMail = delegate(Correos x, Correos y)
         {
             string value1 = x.Mail ?? String.Empty;
             string value2 = y.Mail ?? String.Empty;
             return value1.CompareTo(value2);
         };

         
         public static Comparison<Correos> ByPrincipal = delegate(Correos x, Correos y)
         {
             return x.Principal.CompareTo(y.Principal);
         };

         
         public static Comparison<Correos> ByClienteClienteid = delegate(Correos x, Correos y)
         {
             return Nullable.Compare(x.ClienteClienteid, y.ClienteClienteid);
         };

     }
}
