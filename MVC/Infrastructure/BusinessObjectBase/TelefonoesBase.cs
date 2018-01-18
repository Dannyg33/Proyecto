using System;
using System.Data;
using MVC.DataLayer;
using MVC.BusinessObject;
using MVC.Models;
using System.Web.Script.Serialization;

namespace MVC.BusinessObject.Base
{
     
     public class TelefonoesBase : MVC.Models.TelefonoesModel
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


      
         public TelefonoesBase()
         {
         }

        
         public static Telefonoes SelectByPrimaryKey(int telefonoID)
         {
             return TelefonoesDataLayer.SelectByPrimaryKey(telefonoID);
         }

         public static int GetRecordCount()
         {
             return TelefonoesDataLayer.GetRecordCount();
         }

        
         public static int GetRecordCountByClienteClienteid(int clienteClienteid)
         {
             return TelefonoesDataLayer.GetRecordCountByClienteClienteid(clienteClienteid);
         }

      
         public static int GetRecordCountDynamicWhere(int? telefonoID, string numero, bool? principal, int? clienteClienteid)
         {
             return TelefonoesDataLayer.GetRecordCountDynamicWhere(telefonoID, numero, principal, clienteClienteid);
         }

        
         public static TelefonoesCollection SelectSkipAndTake(int rows, int startRowIndex, out int totalRowCount, string sortByExpression)
         {
             totalRowCount = GetRecordCount();
             sortByExpression = GetSortExpression(sortByExpression);

             return TelefonoesDataLayer.SelectSkipAndTake(sortByExpression, startRowIndex, rows);
         }

        
         public static TelefonoesCollection SelectSkipAndTake(int rows, int startRowIndex, string sortByExpression)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             return TelefonoesDataLayer.SelectSkipAndTake(sortByExpression, startRowIndex, rows);
         }

        
         public static TelefonoesCollection SelectSkipAndTakeByClienteClienteid(int rows, int startRowIndex, out int totalRowCount, string sortByExpression, int clienteClienteid)
         {
             totalRowCount = TelefonoesDataLayer.GetRecordCountByClienteClienteid(clienteClienteid);
             sortByExpression = GetSortExpression(sortByExpression);
             return TelefonoesDataLayer.SelectSkipAndTakeByClienteClienteid(sortByExpression, startRowIndex, rows, clienteClienteid);
         }

        
         public static TelefonoesCollection SelectSkipAndTakeByClienteClienteid(int rows, int startRowIndex, string sortByExpression, int clienteClienteid)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             return TelefonoesDataLayer.SelectSkipAndTakeByClienteClienteid(sortByExpression, startRowIndex, rows, clienteClienteid);
         }
        
         public static TelefonoesCollection SelectSkipAndTakeDynamicWhere(int? telefonoID, string numero, bool? principal, int? clienteClienteid, int rows, int startRowIndex, out int totalRowCount, string sortByExpression)
         {
             totalRowCount = GetRecordCountDynamicWhere(telefonoID, numero, principal, clienteClienteid);
             sortByExpression = GetSortExpression(sortByExpression);
             return TelefonoesDataLayer.SelectSkipAndTakeDynamicWhere(telefonoID, numero, principal, clienteClienteid, sortByExpression, startRowIndex, rows);
         }

        
         public static TelefonoesCollection SelectSkipAndTakeDynamicWhere(int? telefonoID, string numero, bool? principal, int? clienteClienteid, int rows, int startRowIndex, string sortByExpression)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             return TelefonoesDataLayer.SelectSkipAndTakeDynamicWhere(telefonoID, numero, principal, clienteClienteid, sortByExpression, startRowIndex, rows);
         }

       
         public static TelefonoesCollection SelectAll()
         {
             return TelefonoesDataLayer.SelectAll();
         }

        
         public static TelefonoesCollection SelectAll(string sortExpression)
         {
             TelefonoesCollection objTelefonoesCol = TelefonoesDataLayer.SelectAll();
             return SortByExpression(objTelefonoesCol, sortExpression);
         }

       
         public static TelefonoesCollection SelectAllDynamicWhere(int? telefonoID, string numero, bool? principal, int? clienteClienteid)
         {
             return TelefonoesDataLayer.SelectAllDynamicWhere(telefonoID, numero, principal, clienteClienteid);
         }

       
         public static TelefonoesCollection SelectAllDynamicWhere(int? telefonoID, string numero, bool? principal, int? clienteClienteid, string sortExpression)
         {
             TelefonoesCollection objTelefonoesCol = TelefonoesDataLayer.SelectAllDynamicWhere(telefonoID, numero, principal, clienteClienteid);
             return SortByExpression(objTelefonoesCol, sortExpression);
         }

       
         public static TelefonoesCollection SelectTelefonoesCollectionByClienteClienteid(int clienteId)
         {
             return TelefonoesDataLayer.SelectTelefonoesCollectionByClienteClienteid(clienteId);
         }

       
         public static TelefonoesCollection SelectTelefonoesCollectionByClienteClienteid(int clienteId, string sortExpression)
         {
             TelefonoesCollection objTelefonoesCol = TelefonoesDataLayer.SelectTelefonoesCollectionByClienteClienteid(clienteId);
             return SortByExpression(objTelefonoesCol, sortExpression);
         }

        
         public static TelefonoesCollection SelectTelefonoesDropDownListData()
         {
             return TelefonoesDataLayer.SelectTelefonoesDropDownListData();
         }

      
         public static TelefonoesCollection SortByExpression(TelefonoesCollection objTelefonoesCol, string sortExpression)
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
                 case "TelefonoID":
                     objTelefonoesCol.Sort(MVC.BusinessObject.Telefonoes.ByTelefonoID);
                     break;
                 case "Numero":
                     objTelefonoesCol.Sort(MVC.BusinessObject.Telefonoes.ByNumero);
                     break;
                 case "Principal":
                     objTelefonoesCol.Sort(MVC.BusinessObject.Telefonoes.ByPrincipal);
                     break;
                 case "ClienteClienteid":
                     objTelefonoesCol.Sort(MVC.BusinessObject.Telefonoes.ByClienteClienteid);
                     break;
                 default:
                     break;
             }

             if (isSortDescending)
                 objTelefonoesCol.Reverse();

             return objTelefonoesCol;
         }

     
         public int Insert()
         {
             Telefonoes objTelefonoes = (Telefonoes)this;
             return TelefonoesDataLayer.Insert(objTelefonoes);
         }

     
         public void Update()
         {
             Telefonoes objTelefonoes = (Telefonoes)this;
             TelefonoesDataLayer.Update(objTelefonoes);
         }

       
         public static void Delete(int telefonoID)
         {
             TelefonoesDataLayer.Delete(telefonoID);
         }

         private static string GetSortExpression(string sortByExpression)
         {
             if (String.IsNullOrEmpty(sortByExpression) || sortByExpression == " asc")
                 sortByExpression = "TelefonoID";
             else if (sortByExpression.Contains(" asc"))
                 sortByExpression = sortByExpression.Replace(" asc", "");

             return sortByExpression;
         }

      
         public static Comparison<Telefonoes> ByTelefonoID = delegate(Telefonoes x, Telefonoes y)
         {
             return x.TelefonoID.CompareTo(y.TelefonoID);
         };

     
         public static Comparison<Telefonoes> ByNumero = delegate(Telefonoes x, Telefonoes y)
         {
             string value1 = x.Numero ?? String.Empty;
             string value2 = y.Numero ?? String.Empty;
             return value1.CompareTo(value2);
         };

         public static Comparison<Telefonoes> ByPrincipal = delegate(Telefonoes x, Telefonoes y)
         {
             return x.Principal.CompareTo(y.Principal);
         };

         public static Comparison<Telefonoes> ByClienteClienteid = delegate(Telefonoes x, Telefonoes y)
         {
             return Nullable.Compare(x.ClienteClienteid, y.ClienteClienteid);
         };

     }
}
