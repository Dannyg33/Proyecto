using System;
using System.Data;
using System.Data.SqlClient;
using MVC.BusinessObject;
using System.Configuration;

namespace MVC.DataLayer.Base
{
    
     public class ClientesDataLayerBase
     {
       
         public ClientesDataLayerBase()
         {
         }

         public static Clientes SelectByPrimaryKey(int clienteId)
         {
              Clientes objClientes = null;
              string storedProcName = "[dbo].[Clientes_SelectByPrimaryKey]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      
                      command.Parameters.AddWithValue("@clienteId", clienteId);

                      using (SqlDataAdapter da = new SqlDataAdapter(command))
                      {
                          DataTable dt = new DataTable();
                          da.Fill(dt);

                          if (dt != null)
                          {
                              if (dt.Rows.Count > 0)
                              {
                                  objClientes = CreateClientesFromDataRowShared(dt.Rows[0]);
                              }
                          }
                      }
                  }
              }

              return objClientes;
         }

        
         public static int GetRecordCount()
         {
             return GetRecordCountShared("[dbo].[Clientes_GetRecordCount]", null, null, true, null);
         }

         public static int GetRecordCountShared(string storedProcName = null, string param = null, object paramValue = null, bool isUseStoredProc = true, string dynamicSqlScript = null)
         {
              int recordCount = 0;

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      using (SqlDataAdapter da = new SqlDataAdapter(command))
                      {
                          DataTable dt = new DataTable();
                          da.Fill(dt);

                          if (dt != null)
                          {
                              if (dt.Rows.Count > 0)
                              {
                                  recordCount = (int)dt.Rows[0]["RecordCount"];
                              }
                          }
                      }
                  }
              }

              return recordCount;
         }

       
         public static int GetRecordCountDynamicWhere(int? clienteId, string nombre, string apellido, int? edad, DateTime? fechaNacimiento)
         {
              int recordCount = 0;
              string storedProcName = "[dbo].[Clientes_GetRecordCountWhereDynamic]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                     
                      AddSearchCommandParamsShared(command, clienteId, nombre, apellido, edad, fechaNacimiento);

                      using (SqlDataAdapter da = new SqlDataAdapter(command))
                      {
                          DataTable dt = new DataTable();
                          da.Fill(dt);

                          if (dt != null)
                          {
                              if (dt.Rows.Count > 0)
                              {
                                  recordCount = (int)dt.Rows[0]["RecordCount"];
                              }
                          }
                      }
                  }
              }

              return recordCount;
         }

       
         public static ClientesCollection SelectSkipAndTake(string sortByExpression, int startRowIndex, int rows)
         {
             return SelectShared("[dbo].[Clientes_SelectSkipAndTake]", null, null, true, null, sortByExpression, startRowIndex, rows);
         }

        
         public static ClientesCollection SelectSkipAndTakeDynamicWhere(int? clienteId, string nombre, string apellido, int? edad, DateTime? fechaNacimiento, string sortByExpression, int startRowIndex, int rows)
         {
              ClientesCollection objClientesCol = null;
              string storedProcName = "[dbo].[Clientes_SelectSkipAndTakeWhereDynamic]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                    
                      command.Parameters.AddWithValue("@start", startRowIndex);
                      command.Parameters.AddWithValue("@numberOfRows", rows);
                      command.Parameters.AddWithValue("@sortByExpression", sortByExpression);

                    
                      AddSearchCommandParamsShared(command, clienteId, nombre, apellido, edad, fechaNacimiento);

                      using (SqlDataAdapter da = new SqlDataAdapter(command))
                      {
                          DataTable dt = new DataTable();
                          da.Fill(dt);

                          if (dt != null)
                          {
                              if (dt.Rows.Count > 0)
                              {
                                  objClientesCol = new ClientesCollection();

                                  foreach (DataRow dr in dt.Rows)
                                  {
                                      Clientes objClientes = CreateClientesFromDataRowShared(dr);
                                      objClientesCol.Add(objClientes);
                                  }
                              }
                          }
                      }
                  }
              }

              return objClientesCol;
         }

        
         public static ClientesCollection SelectAll()
         {
             return SelectShared("[dbo].[Clientes_SelectAll]", String.Empty, null);
         }

        
         public static ClientesCollection SelectAllDynamicWhere(int? clienteId, string nombre, string apellido, int? edad, DateTime? fechaNacimiento)
         {
              ClientesCollection objClientesCol = null;
              string storedProcName = "[dbo].[Clientes_SelectAllWhereDynamic]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                    
                      AddSearchCommandParamsShared(command, clienteId, nombre, apellido, edad, fechaNacimiento);

                      using (SqlDataAdapter da = new SqlDataAdapter(command))
                      {
                          DataTable dt = new DataTable();
                          da.Fill(dt);

                          if (dt != null)
                          {
                              if (dt.Rows.Count > 0)
                              {
                                  objClientesCol = new ClientesCollection();

                                  foreach (DataRow dr in dt.Rows)
                                  {
                                      Clientes objClientes = CreateClientesFromDataRowShared(dr);
                                      objClientesCol.Add(objClientes);
                                  }
                              }
                          }
                      }
                  }
              }

              return objClientesCol;
         }

      
         public static ClientesCollection SelectClientesDropDownListData()
         {
              ClientesCollection objClientesCol = null;
              string storedProcName = "[dbo].[Clientes_SelectDropDownListData]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      using (SqlDataAdapter da = new SqlDataAdapter(command))
                      {
                          DataTable dt = new DataTable();
                          da.Fill(dt);

                          if (dt != null)
                          {
                              if (dt.Rows.Count > 0)
                              {
                                  objClientesCol = new ClientesCollection();

                                  foreach (DataRow dr in dt.Rows)
                                  {
                                      Clientes objClientes = new Clientes();
                                      objClientes.ClienteId = (int)dr["ClienteId"];

                                      if (dr["Nombre"] != System.DBNull.Value)
                                          objClientes.Nombre = (string)(dr["Nombre"]);
                                      else
                                          objClientes.Nombre = null;

                                      objClientesCol.Add(objClientes);
                                  }
                              }
                          }
                      }
                  }
              }

              return objClientesCol;
         }

         public static ClientesCollection SelectShared(string storedProcName, string param, object paramValue, bool isUseStoredProc = true, string dynamicSqlScript = null, string sortByExpression = null, int? startRowIndex = null, int? rows = null)
         {
              ClientesCollection objClientesCol = null;

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                    
                      if (!String.IsNullOrEmpty(sortByExpression) && startRowIndex != null && rows != null)
                      {
                          command.Parameters.AddWithValue("@start", startRowIndex.Value);
                          command.Parameters.AddWithValue("@numberOfRows", rows.Value);
                          command.Parameters.AddWithValue("@sortByExpression", sortByExpression);
                      }

                      using (SqlDataAdapter da = new SqlDataAdapter(command))
                      {
                          DataTable dt = new DataTable();
                          da.Fill(dt);

                          if (dt != null)
                          {
                              if (dt.Rows.Count > 0)
                              {
                                  objClientesCol = new ClientesCollection();

                                  foreach (DataRow dr in dt.Rows)
                                  {
                                      Clientes objClientes = CreateClientesFromDataRowShared(dr);
                                      objClientesCol.Add(objClientes);
                                  }
                              }
                          }
                      }
                  }
              }

              return objClientesCol;
         }

      
         public static int Insert(Clientes objClientes)
         {
             string storedProcName = "[dbo].[Clientes_Insert]";
             return InsertUpdate(objClientes, false, storedProcName);
         }

      
         public static void Update(Clientes objClientes)
         {
             string storedProcName = "[dbo].[Clientes_Update]";
             InsertUpdate(objClientes, true, storedProcName);
         }

         private static int InsertUpdate(Clientes objClientes, bool isUpdate, string storedProcName)
         {
              int newlyCreatedClienteId = objClientes.ClienteId;

              object nombre = objClientes.Nombre;
              object apellido = objClientes.Apellido;

              if (String.IsNullOrEmpty(objClientes.Nombre))
                  nombre = System.DBNull.Value;

              if (String.IsNullOrEmpty(objClientes.Apellido))
                  apellido = System.DBNull.Value;

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                    
                      if (isUpdate)
                      {
                        
                          command.Parameters.AddWithValue("@clienteId", objClientes.ClienteId);
                      }

                      command.Parameters.AddWithValue("@nombre", nombre);
                      command.Parameters.AddWithValue("@apellido", apellido);
                      command.Parameters.AddWithValue("@edad", objClientes.Edad);
                      command.Parameters.AddWithValue("@fechaNacimiento", objClientes.FechaNacimiento);

                      if (isUpdate)
                          command.ExecuteNonQuery();
                      else
                          newlyCreatedClienteId = (int)command.ExecuteScalar();
                  }
              }

              return newlyCreatedClienteId;
         }

     
         public static void Delete(int clienteId)
         {
              string storedProcName = "[dbo].[Clientes_Delete]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                    
                      command.Parameters.AddWithValue("@clienteId", clienteId);

                      command.ExecuteNonQuery();
                  }
              }
         }

       
         private static void AddSearchCommandParamsShared(SqlCommand command, int? clienteId, string nombre, string apellido, int? edad, DateTime? fechaNacimiento)
         {
              if(clienteId != null)
                  command.Parameters.AddWithValue("@clienteId", clienteId);
              else
                  command.Parameters.AddWithValue("@clienteId", System.DBNull.Value);

              if(!String.IsNullOrEmpty(nombre))
                  command.Parameters.AddWithValue("@nombre", nombre);
              else
                  command.Parameters.AddWithValue("@nombre", System.DBNull.Value);

              if(!String.IsNullOrEmpty(apellido))
                  command.Parameters.AddWithValue("@apellido", apellido);
              else
                  command.Parameters.AddWithValue("@apellido", System.DBNull.Value);

              if(edad != null)
                  command.Parameters.AddWithValue("@edad", edad);
              else
                  command.Parameters.AddWithValue("@edad", System.DBNull.Value);

              if(fechaNacimiento != null)
                  command.Parameters.AddWithValue("@fechaNacimiento", fechaNacimiento);
              else
                  command.Parameters.AddWithValue("@fechaNacimiento", System.DBNull.Value);

         }

        
         private static Clientes CreateClientesFromDataRowShared(DataRow dr)
         {
             Clientes objClientes = new Clientes();

             objClientes.ClienteId = (int)dr["ClienteId"];

             if (dr["Nombre"] != System.DBNull.Value)
                 objClientes.Nombre = dr["Nombre"].ToString();
             else
                 objClientes.Nombre = null;

             if (dr["Apellido"] != System.DBNull.Value)
                 objClientes.Apellido = dr["Apellido"].ToString();
             else
                 objClientes.Apellido = null;
             objClientes.Edad = (int)dr["Edad"];
             objClientes.FechaNacimiento = (DateTime)dr["FechaNacimiento"];

             return objClientes;
         }
     }
}
