using System;
using System.Data;
using System.Data.SqlClient;
using MVC.BusinessObject;
using System.Configuration;

namespace MVC.DataLayer.Base
{
    
     public class TelefonoesDataLayerBase
     {
        
         public TelefonoesDataLayerBase()
         {
         }

         
         public static Telefonoes SelectByPrimaryKey(int telefonoID)
         {
              Telefonoes objTelefonoes = null;
              string storedProcName = "[dbo].[Telefonoes_SelectByPrimaryKey]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                    
                      command.Parameters.AddWithValue("@telefonoID", telefonoID);

                      using (SqlDataAdapter da = new SqlDataAdapter(command))
                      {
                          DataTable dt = new DataTable();
                          da.Fill(dt);

                          if (dt != null)
                          {
                              if (dt.Rows.Count > 0)
                              {
                                  objTelefonoes = CreateTelefonoesFromDataRowShared(dt.Rows[0]);
                              }
                          }
                      }
                  }
              }

              return objTelefonoes;
         }

         public static int GetRecordCount()
         {
             return GetRecordCountShared("[dbo].[Telefonoes_GetRecordCount]", null, null, true, null);
         }

       
         public static int GetRecordCountByClienteClienteid(int clienteClienteid)
         {
             return GetRecordCountShared("[dbo].[Telefonoes_GetRecordCountByClienteClienteid]", "clienteClienteid", clienteClienteid, true, null);
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

                   
                      switch (param)
                      {
                          case "clienteClienteid":
                              command.Parameters.AddWithValue("@clienteClienteid", paramValue);
                              break;
                          default:
                              break;
                      }

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

         
         public static int GetRecordCountDynamicWhere(int? telefonoID, string numero, bool? principal, int? clienteClienteid)
         {
              int recordCount = 0;
              string storedProcName = "[dbo].[Telefonoes_GetRecordCountWhereDynamic]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                  
                      AddSearchCommandParamsShared(command, telefonoID, numero, principal, clienteClienteid);

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

        
         public static TelefonoesCollection SelectSkipAndTake(string sortByExpression, int startRowIndex, int rows)
         {
             return SelectShared("[dbo].[Telefonoes_SelectSkipAndTake]", null, null, true, null, sortByExpression, startRowIndex, rows);
         }

         
         public static TelefonoesCollection SelectSkipAndTakeByClienteClienteid(string sortByExpression, int startRowIndex, int rows, int clienteClienteid)
         {
             return SelectShared("[dbo].[Telefonoes_SelectSkipAndTakeByClienteClienteid]", "clienteClienteid", clienteClienteid, true, null, sortByExpression, startRowIndex, rows);
         }

        
         public static TelefonoesCollection SelectSkipAndTakeDynamicWhere(int? telefonoID, string numero, bool? principal, int? clienteClienteid, string sortByExpression, int startRowIndex, int rows)
         {
              TelefonoesCollection objTelefonoesCol = null;
              string storedProcName = "[dbo].[Telefonoes_SelectSkipAndTakeWhereDynamic]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                    
                      command.Parameters.AddWithValue("@start", startRowIndex);
                      command.Parameters.AddWithValue("@numberOfRows", rows);
                      command.Parameters.AddWithValue("@sortByExpression", sortByExpression);

            
                      AddSearchCommandParamsShared(command, telefonoID, numero, principal, clienteClienteid);

                      using (SqlDataAdapter da = new SqlDataAdapter(command))
                      {
                          DataTable dt = new DataTable();
                          da.Fill(dt);

                          if (dt != null)
                          {
                              if (dt.Rows.Count > 0)
                              {
                                  objTelefonoesCol = new TelefonoesCollection();

                                  foreach (DataRow dr in dt.Rows)
                                  {
                                      Telefonoes objTelefonoes = CreateTelefonoesFromDataRowShared(dr);
                                      objTelefonoesCol.Add(objTelefonoes);
                                  }
                              }
                          }
                      }
                  }
              }

              return objTelefonoesCol;
         }

         public static TelefonoesCollection SelectAll()
         {
             return SelectShared("[dbo].[Telefonoes_SelectAll]", String.Empty, null);
         }

       
         public static TelefonoesCollection SelectAllDynamicWhere(int? telefonoID, string numero, bool? principal, int? clienteClienteid)
         {
              TelefonoesCollection objTelefonoesCol = null;
              string storedProcName = "[dbo].[Telefonoes_SelectAllWhereDynamic]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                  
                      AddSearchCommandParamsShared(command, telefonoID, numero, principal, clienteClienteid);

                      using (SqlDataAdapter da = new SqlDataAdapter(command))
                      {
                          DataTable dt = new DataTable();
                          da.Fill(dt);

                          if (dt != null)
                          {
                              if (dt.Rows.Count > 0)
                              {
                                  objTelefonoesCol = new TelefonoesCollection();

                                  foreach (DataRow dr in dt.Rows)
                                  {
                                      Telefonoes objTelefonoes = CreateTelefonoesFromDataRowShared(dr);
                                      objTelefonoesCol.Add(objTelefonoes);
                                  }
                              }
                          }
                      }
                  }
              }

              return objTelefonoesCol;
         }

       
         public static TelefonoesCollection SelectTelefonoesCollectionByClienteClienteid(int clienteId)
         {
             return SelectShared("[dbo].[Telefonoes_SelectAllByClienteClienteid]", "clienteClienteid", clienteId);
         }

       
         public static TelefonoesCollection SelectTelefonoesDropDownListData()
         {
              TelefonoesCollection objTelefonoesCol = null;
              string storedProcName = "[dbo].[Telefonoes_SelectDropDownListData]";

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
                                  objTelefonoesCol = new TelefonoesCollection();

                                  foreach (DataRow dr in dt.Rows)
                                  {
                                      Telefonoes objTelefonoes = new Telefonoes();
                                      objTelefonoes.TelefonoID = (int)dr["TelefonoID"];

                                      if (dr["Numero"] != System.DBNull.Value)
                                          objTelefonoes.Numero = (string)(dr["Numero"]);
                                      else
                                          objTelefonoes.Numero = null;

                                      objTelefonoesCol.Add(objTelefonoes);
                                  }
                              }
                          }
                      }
                  }
              }

              return objTelefonoesCol;
         }

         public static TelefonoesCollection SelectShared(string storedProcName, string param, object paramValue, bool isUseStoredProc = true, string dynamicSqlScript = null, string sortByExpression = null, int? startRowIndex = null, int? rows = null)
         {
              TelefonoesCollection objTelefonoesCol = null;

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

                   
                      switch (param)
                      {
                          case "clienteClienteid":
                              command.Parameters.AddWithValue("@clienteClienteid", paramValue);
                              break;
                          default:
                              break;
                      }

                      using (SqlDataAdapter da = new SqlDataAdapter(command))
                      {
                          DataTable dt = new DataTable();
                          da.Fill(dt);

                          if (dt != null)
                          {
                              if (dt.Rows.Count > 0)
                              {
                                  objTelefonoesCol = new TelefonoesCollection();

                                  foreach (DataRow dr in dt.Rows)
                                  {
                                      Telefonoes objTelefonoes = CreateTelefonoesFromDataRowShared(dr);
                                      objTelefonoesCol.Add(objTelefonoes);
                                  }
                              }
                          }
                      }
                  }
              }

              return objTelefonoesCol;
         }

         public static int Insert(Telefonoes objTelefonoes)
         {
             string storedProcName = "[dbo].[Telefonoes_Insert]";
             return InsertUpdate(objTelefonoes, false, storedProcName);
         }

        
         public static void Update(Telefonoes objTelefonoes)
         {
             string storedProcName = "[dbo].[Telefonoes_Update]";
             InsertUpdate(objTelefonoes, true, storedProcName);
         }

         private static int InsertUpdate(Telefonoes objTelefonoes, bool isUpdate, string storedProcName)
         {
              int newlyCreatedTelefonoID = objTelefonoes.TelefonoID;

              object numero = objTelefonoes.Numero;
              object clienteClienteid = objTelefonoes.ClienteClienteid;

              if (String.IsNullOrEmpty(objTelefonoes.Numero))
                  numero = System.DBNull.Value;

              if (objTelefonoes.ClienteClienteid == null)
                  clienteClienteid = System.DBNull.Value;

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                    
                      if (isUpdate)
                      {
                        
                          command.Parameters.AddWithValue("@telefonoID", objTelefonoes.TelefonoID);
                      }

                      command.Parameters.AddWithValue("@numero", numero);
                      command.Parameters.AddWithValue("@principal", objTelefonoes.Principal);
                      command.Parameters.AddWithValue("@clienteClienteid", clienteClienteid);

                      if (isUpdate)
                          command.ExecuteNonQuery();
                      else
                          newlyCreatedTelefonoID = (int)command.ExecuteScalar();
                  }
              }

              return newlyCreatedTelefonoID;
         }

       
         public static void Delete(int telefonoID)
         {
              string storedProcName = "[dbo].[Telefonoes_Delete]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                    
                      command.Parameters.AddWithValue("@telefonoID", telefonoID);

                  
                      command.ExecuteNonQuery();
                  }
              }
         }

        
         private static void AddSearchCommandParamsShared(SqlCommand command, int? telefonoID, string numero, bool? principal, int? clienteClienteid)
         {
              if(telefonoID != null)
                  command.Parameters.AddWithValue("@telefonoID", telefonoID);
              else
                  command.Parameters.AddWithValue("@telefonoID", System.DBNull.Value);

              if(!String.IsNullOrEmpty(numero))
                  command.Parameters.AddWithValue("@numero", numero);
              else
                  command.Parameters.AddWithValue("@numero", System.DBNull.Value);

              if(principal != null)
                  command.Parameters.AddWithValue("@principal", principal);
              else
                  command.Parameters.AddWithValue("@principal", System.DBNull.Value);

              if(clienteClienteid != null)
                  command.Parameters.AddWithValue("@clienteClienteid", clienteClienteid);
              else
                  command.Parameters.AddWithValue("@clienteClienteid", System.DBNull.Value);

         }

        
         private static Telefonoes CreateTelefonoesFromDataRowShared(DataRow dr)
         {
             Telefonoes objTelefonoes = new Telefonoes();

             objTelefonoes.TelefonoID = (int)dr["TelefonoID"];

             if (dr["Numero"] != System.DBNull.Value)
                 objTelefonoes.Numero = dr["Numero"].ToString();
             else
                 objTelefonoes.Numero = null;
             objTelefonoes.Principal = (bool)dr["Principal"];

             if (dr["ClienteClienteid"] != System.DBNull.Value)
                 objTelefonoes.ClienteClienteid = (int)dr["ClienteClienteid"];
             else
                 objTelefonoes.ClienteClienteid = null;


             return objTelefonoes;
         }
     }
}
