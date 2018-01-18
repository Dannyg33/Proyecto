using System;
using System.Data;
using MVC.DataLayer;
using MVC.BusinessObject;
using MVC.Models;
using System.Web.Script.Serialization;

namespace MVC.BusinessObject.Base
{
     
     public class ClientesBase : MVC.Models.ClientesModel
     {
         
         [ScriptIgnore]
         public Lazy<CorreosCollection> CorreosCollection
         {
             get
             {
                 int value;
                 bool hasValue = Int32.TryParse(ClienteId.ToString(), out value);

                 if (hasValue)
                     return new Lazy<CorreosCollection>(() => MVC.BusinessObject.Correos.SelectCorreosCollectionByClienteClienteid(value));
                 else
                     return null;
             }
             set { }
         }

         
         [ScriptIgnore]
         public Lazy<DireccionsCollection> DireccionsCollection
         {
             get
             {
                 int value;
                 bool hasValue = Int32.TryParse(ClienteId.ToString(), out value);

                 if (hasValue)
                     return new Lazy<DireccionsCollection>(() => MVC.BusinessObject.Direccions.SelectDireccionsCollectionByClienteClienteid(value));
                 else
                     return null;
             }
             set { }
         }

         
         [ScriptIgnore]
         public Lazy<TelefonoesCollection> TelefonoesCollection
         {
             get
             {
                 int value;
                 bool hasValue = Int32.TryParse(ClienteId.ToString(), out value);

                 if (hasValue)
                     return new Lazy<TelefonoesCollection>(() => MVC.BusinessObject.Telefonoes.SelectTelefonoesCollectionByClienteClienteid(value));
                 else
                     return null;
             }
             set { }
         }


         
         public ClientesBase()
         {
         }

         
         public static Clientes SelectByPrimaryKey(int clienteId)
         {
             return ClientesDataLayer.SelectByPrimaryKey(clienteId);
         }

         
         public static int GetRecordCount()
         {
             return ClientesDataLayer.GetRecordCount();
         }

         
         public static int GetRecordCountDynamicWhere(int? clienteId, string nombre, string apellido, int? edad, DateTime? fechaNacimiento)
         {
             return ClientesDataLayer.GetRecordCountDynamicWhere(clienteId, nombre, apellido, edad, fechaNacimiento);
         }

         
         public static ClientesCollection SelectSkipAndTake(int rows, int startRowIndex, out int totalRowCount, string sortByExpression)
         {
             totalRowCount = GetRecordCount();
             sortByExpression = GetSortExpression(sortByExpression);

             return ClientesDataLayer.SelectSkipAndTake(sortByExpression, startRowIndex, rows);
         }

         
         public static ClientesCollection SelectSkipAndTake(int rows, int startRowIndex, string sortByExpression)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             return ClientesDataLayer.SelectSkipAndTake(sortByExpression, startRowIndex, rows);
         }

         
         public static ClientesCollection SelectSkipAndTakeDynamicWhere(int? clienteId, string nombre, string apellido, int? edad, DateTime? fechaNacimiento, int rows, int startRowIndex, out int totalRowCount, string sortByExpression)
         {
             totalRowCount = GetRecordCountDynamicWhere(clienteId, nombre, apellido, edad, fechaNacimiento);
             sortByExpression = GetSortExpression(sortByExpression);
             return ClientesDataLayer.SelectSkipAndTakeDynamicWhere(clienteId, nombre, apellido, edad, fechaNacimiento, sortByExpression, startRowIndex, rows);
         }

         
         public static ClientesCollection SelectSkipAndTakeDynamicWhere(int? clienteId, string nombre, string apellido, int? edad, DateTime? fechaNacimiento, int rows, int startRowIndex, string sortByExpression)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             return ClientesDataLayer.SelectSkipAndTakeDynamicWhere(clienteId, nombre, apellido, edad, fechaNacimiento, sortByExpression, startRowIndex, rows);
         }

         
         public static ClientesCollection SelectAll()
         {
             return ClientesDataLayer.SelectAll();
         }

         
         public static ClientesCollection SelectAll(string sortExpression)
         {
             ClientesCollection objClientesCol = ClientesDataLayer.SelectAll();
             return SortByExpression(objClientesCol, sortExpression);
         }

         
         public static ClientesCollection SelectAllDynamicWhere(int? clienteId, string nombre, string apellido, int? edad, DateTime? fechaNacimiento)
         {
             return ClientesDataLayer.SelectAllDynamicWhere(clienteId, nombre, apellido, edad, fechaNacimiento);
         }

         
         public static ClientesCollection SelectAllDynamicWhere(int? clienteId, string nombre, string apellido, int? edad, DateTime? fechaNacimiento, string sortExpression)
         {
             ClientesCollection objClientesCol = ClientesDataLayer.SelectAllDynamicWhere(clienteId, nombre, apellido, edad, fechaNacimiento);
             return SortByExpression(objClientesCol, sortExpression);
         }

         
         public static ClientesCollection SelectClientesDropDownListData()
         {
             return ClientesDataLayer.SelectClientesDropDownListData();
         }

         
         public static ClientesCollection SortByExpression(ClientesCollection objClientesCol, string sortExpression)
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
                 case "ClienteId":
                     objClientesCol.Sort(MVC.BusinessObject.Clientes.ByClienteId);
                     break;
                 case "Nombre":
                     objClientesCol.Sort(MVC.BusinessObject.Clientes.ByNombre);
                     break;
                 case "Apellido":
                     objClientesCol.Sort(MVC.BusinessObject.Clientes.ByApellido);
                     break;
                 case "Edad":
                     objClientesCol.Sort(MVC.BusinessObject.Clientes.ByEdad);
                     break;
                 case "FechaNacimiento":
                     objClientesCol.Sort(MVC.BusinessObject.Clientes.ByFechaNacimiento);
                     break;
                 default:
                     break;
             }

             if (isSortDescending)
                 objClientesCol.Reverse();

             return objClientesCol;
         }

         
         public int Insert()
         {
             Clientes objClientes = (Clientes)this;
             return ClientesDataLayer.Insert(objClientes);
         }

         
         public void Update()
         {
             Clientes objClientes = (Clientes)this;
             ClientesDataLayer.Update(objClientes);
         }

         
         public static void Delete(int clienteId)
         {
             ClientesDataLayer.Delete(clienteId);
         }

         private static string GetSortExpression(string sortByExpression)
         {
             if (String.IsNullOrEmpty(sortByExpression) || sortByExpression == " asc")
                 sortByExpression = "ClienteId";
             else if (sortByExpression.Contains(" asc"))
                 sortByExpression = sortByExpression.Replace(" asc", "");

             return sortByExpression;
         }

         
         public static Comparison<Clientes> ByClienteId = delegate(Clientes x, Clientes y)
         {
             return x.ClienteId.CompareTo(y.ClienteId);
         };

         
         public static Comparison<Clientes> ByNombre = delegate(Clientes x, Clientes y)
         {
             string value1 = x.Nombre ?? String.Empty;
             string value2 = y.Nombre ?? String.Empty;
             return value1.CompareTo(value2);
         };

         
         public static Comparison<Clientes> ByApellido = delegate(Clientes x, Clientes y)
         {
             string value1 = x.Apellido ?? String.Empty;
             string value2 = y.Apellido ?? String.Empty;
             return value1.CompareTo(value2);
         };

         
         public static Comparison<Clientes> ByEdad = delegate(Clientes x, Clientes y)
         {
             return x.Edad.CompareTo(y.Edad);
         };

         
         public static Comparison<Clientes> ByFechaNacimiento = delegate(Clientes x, Clientes y)
         {
             return x.FechaNacimiento.CompareTo(y.FechaNacimiento);
         };

     }
}
