using System;
using System.Data;
using System.Data.SqlClient;
using MVC.BusinessObject;
using System.Configuration;

namespace MVC.DataLayer.Base
{
   
     public class CorreosDataLayerBase
     {
         public CorreosDataLayerBase()
         {
         }
         public static Correos SelectByPrimaryKey(int correoID)
         {
              Correos objCorreos = null;
              string storedProcName = "[dbo].[Correos_SelectByPrimaryKey]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

             
                      command.Parameters.AddWithValue("@correoID", correoID);

                      using (SqlDataAdapter da = new SqlDataAdapter(command))
                      {
                          DataTable dt = new DataTable();
                          da.Fill(dt);

                          if (dt != null)
                          {
                              if (dt.Rows.Count > 0)
                              {
                                  objCorreos = CreateCorreosFromDataRowShared(dt.Rows[0]);
                              }
                          }
                      }
                  }
              }

              return objCorreos;
         }

         
         public static int GetRecordCount()
         {
             return GetRecordCountShared("[dbo].[Correos_GetRecordCount]", null, null, true, null);
         }

         
         public static int GetRecordCountByClienteClienteid(int clienteClienteid)
         {
             return GetRecordCountShared("[dbo].[Correos_GetRecordCountByClienteClienteid]", "clienteClienteid", clienteClienteid, true, null);
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

         
         public static int GetRecordCountDynamicWhere(int? correoID, string mail, bool? principal, int? clienteClienteid)
         {
              int recordCount = 0;
              string storedProcName = "[dbo].[Correos_GetRecordCountWhereDynamic]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                    
                      AddSearchCommandParamsShared(command, correoID, mail, principal, clienteClienteid);

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

         
         public static CorreosCollection SelectSkipAndTake(string sortByExpression, int startRowIndex, int rows)
         {
             return SelectShared("[dbo].[Correos_SelectSkipAndTake]", null, null, true, null, sortByExpression, startRowIndex, rows);
         }

         
         public static CorreosCollection SelectSkipAndTakeByClienteClienteid(string sortByExpression, int startRowIndex, int rows, int clienteClienteid)
         {
             return SelectShared("[dbo].[Correos_SelectSkipAndTakeByClienteClienteid]", "clienteClienteid", clienteClienteid, true, null, sortByExpression, startRowIndex, rows);
         }

         
         public static CorreosCollection SelectSkipAndTakeDynamicWhere(int? correoID, string mail, bool? principal, int? clienteClienteid, string sortByExpression, int startRowIndex, int rows)
         {
              CorreosCollection objCorreosCol = null;
              string storedProcName = "[dbo].[Correos_SelectSkipAndTakeWhereDynamic]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                    
                      command.Parameters.AddWithValue("@start", startRowIndex);
                      command.Parameters.AddWithValue("@numberOfRows", rows);
                      command.Parameters.AddWithValue("@sortByExpression", sortByExpression);

                     
                      AddSearchCommandParamsShared(command, correoID, mail, principal, clienteClienteid);

                      using (SqlDataAdapter da = new SqlDataAdapter(command))
                      {
                          DataTable dt = new DataTable();
                          da.Fill(dt);

                          if (dt != null)
                          {
                              if (dt.Rows.Count > 0)
                              {
                                  objCorreosCol = new CorreosCollection();

                                  foreach (DataRow dr in dt.Rows)
                                  {
                                      Correos objCorreos = CreateCorreosFromDataRowShared(dr);
                                      objCorreosCol.Add(objCorreos);
                                  }
                              }
                          }
                      }
                  }
              }

              return objCorreosCol;
         }

         
         public static CorreosCollection SelectAll()
         {
             return SelectShared("[dbo].[Correos_SelectAll]", String.Empty, null);
         }

         
         public static CorreosCollection SelectAllDynamicWhere(int? correoID, string mail, bool? principal, int? clienteClienteid)
         {
              CorreosCollection objCorreosCol = null;
              string storedProcName = "[dbo].[Correos_SelectAllWhereDynamic]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                    
                      AddSearchCommandParamsShared(command, correoID, mail, principal, clienteClienteid);

                      using (SqlDataAdapter da = new SqlDataAdapter(command))
                      {
                          DataTable dt = new DataTable();
                          da.Fill(dt);

                          if (dt != null)
                          {
                              if (dt.Rows.Count > 0)
                              {
                                  objCorreosCol = new CorreosCollection();

                                  foreach (DataRow dr in dt.Rows)
                                  {
                                      Correos objCorreos = CreateCorreosFromDataRowShared(dr);
                                      objCorreosCol.Add(objCorreos);
                                  }
                              }
                          }
                      }
                  }
              }

              return objCorreosCol;
         }

         
         public static CorreosCollection SelectCorreosCollectionByClienteClienteid(int clienteId)
         {
             return SelectShared("[dbo].[Correos_SelectAllByClienteClienteid]", "clienteClienteid", clienteId);
         }

         
         public static CorreosCollection SelectCorreosDropDownListData()
         {
              CorreosCollection objCorreosCol = null;
              string storedProcName = "[dbo].[Correos_SelectDropDownListData]";

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
                                  objCorreosCol = new CorreosCollection();

                                  foreach (DataRow dr in dt.Rows)
                                  {
                                      Correos objCorreos = new Correos();
                                      objCorreos.CorreoID = (int)dr["CorreoID"];

                                      if (dr["Mail"] != System.DBNull.Value)
                                          objCorreos.Mail = (string)(dr["Mail"]);
                                      else
                                          objCorreos.Mail = null;

                                      objCorreosCol.Add(objCorreos);
                                  }
                              }
                          }
                      }
                  }
              }

              return objCorreosCol;
         }

         public static CorreosCollection SelectShared(string storedProcName, string param, object paramValue, bool isUseStoredProc = true, string dynamicSqlScript = null, string sortByExpression = null, int? startRowIndex = null, int? rows = null)
         {
              CorreosCollection objCorreosCol = null;

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
                                  objCorreosCol = new CorreosCollection();

                                  foreach (DataRow dr in dt.Rows)
                                  {
                                      Correos objCorreos = CreateCorreosFromDataRowShared(dr);
                                      objCorreosCol.Add(objCorreos);
                                  }
                              }
                          }
                      }
                  }
              }

              return objCorreosCol;
         }

        
         public static int Insert(Correos objCorreos)
         {
             string storedProcName = "[dbo].[Correos_Insert]";
             return InsertUpdate(objCorreos, false, storedProcName);
         }

         public static void Update(Correos objCorreos)
         {
             string storedProcName = "[dbo].[Correos_Update]";
             InsertUpdate(objCorreos, true, storedProcName);
         }

         private static int InsertUpdate(Correos objCorreos, bool isUpdate, string storedProcName)
         {
              int newlyCreatedCorreoID = objCorreos.CorreoID;

              object mail = objCorreos.Mail;
              object clienteClienteid = objCorreos.ClienteClienteid;

              if (String.IsNullOrEmpty(objCorreos.Mail))
                  mail = System.DBNull.Value;

              if (objCorreos.ClienteClienteid == null)
                  clienteClienteid = System.DBNull.Value;

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;
                    
                      if (isUpdate)
                      {
                    
                          command.Parameters.AddWithValue("@correoID", objCorreos.CorreoID);
                      }

                      command.Parameters.AddWithValue("@mail", mail);
                      command.Parameters.AddWithValue("@principal", objCorreos.Principal);
                      command.Parameters.AddWithValue("@clienteClienteid", clienteClienteid);

                      if (isUpdate)
                          command.ExecuteNonQuery();
                      else
                          newlyCreatedCorreoID = (int)command.ExecuteScalar();
                  }
              }

              return newlyCreatedCorreoID;
         }

        
         public static void Delete(int correoID)
         {
              string storedProcName = "[dbo].[Correos_Delete]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                   
                      command.Parameters.AddWithValue("@correoID", correoID);

                     
                      command.ExecuteNonQuery();
                  }
              }
         }

         private static void AddSearchCommandParamsShared(SqlCommand command, int? correoID, string mail, bool? principal, int? clienteClienteid)
         {
              if(correoID != null)
                  command.Parameters.AddWithValue("@correoID", correoID);
              else
                  command.Parameters.AddWithValue("@correoID", System.DBNull.Value);

              if(!String.IsNullOrEmpty(mail))
                  command.Parameters.AddWithValue("@mail", mail);
              else
                  command.Parameters.AddWithValue("@mail", System.DBNull.Value);

              if(principal != null)
                  command.Parameters.AddWithValue("@principal", principal);
              else
                  command.Parameters.AddWithValue("@principal", System.DBNull.Value);

              if(clienteClienteid != null)
                  command.Parameters.AddWithValue("@clienteClienteid", clienteClienteid);
              else
                  command.Parameters.AddWithValue("@clienteClienteid", System.DBNull.Value);

         }

       
         private static Correos CreateCorreosFromDataRowShared(DataRow dr)
         {
             Correos objCorreos = new Correos();

             objCorreos.CorreoID = (int)dr["CorreoID"];

             if (dr["Mail"] != System.DBNull.Value)
                 objCorreos.Mail = dr["Mail"].ToString();
             else
                 objCorreos.Mail = null;
             objCorreos.Principal = (bool)dr["Principal"];

             if (dr["ClienteClienteid"] != System.DBNull.Value)
                 objCorreos.ClienteClienteid = (int)dr["ClienteClienteid"];
             else
                 objCorreos.ClienteClienteid = null;


             return objCorreos;
         }
     }
}
