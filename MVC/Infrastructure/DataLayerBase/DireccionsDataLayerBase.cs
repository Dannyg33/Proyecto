using System;
using System.Data;
using System.Data.SqlClient;
using MVC.BusinessObject;
using System.Configuration;

namespace MVC.DataLayer.Base
{
    
     public class DireccionsDataLayerBase
     {
       
         public DireccionsDataLayerBase()
         {
         }

        
         public static Direccions SelectByPrimaryKey(int direccionId)
         {
              Direccions objDireccions = null;
              string storedProcName = "[dbo].[Direccions_SelectByPrimaryKey]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                     
                      command.Parameters.AddWithValue("@direccionId", direccionId);

                      using (SqlDataAdapter da = new SqlDataAdapter(command))
                      {
                          DataTable dt = new DataTable();
                          da.Fill(dt);

                          if (dt != null)
                          {
                              if (dt.Rows.Count > 0)
                              {
                                  objDireccions = CreateDireccionsFromDataRowShared(dt.Rows[0]);
                              }
                          }
                      }
                  }
              }

              return objDireccions;
         }

        
         public static int GetRecordCount()
         {
             return GetRecordCountShared("[dbo].[Direccions_GetRecordCount]", null, null, true, null);
         }

         
         public static int GetRecordCountByClienteClienteid(int clienteClienteid)
         {
             return GetRecordCountShared("[dbo].[Direccions_GetRecordCountByClienteClienteid]", "clienteClienteid", clienteClienteid, true, null);
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

       
         public static int GetRecordCountDynamicWhere(int? direccionId, string calle1, string calle2, string numeroCasa, string canton, string provincia, bool? principal, int? clienteClienteid)
         {
              int recordCount = 0;
              string storedProcName = "[dbo].[Direccions_GetRecordCountWhereDynamic]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                     
                      AddSearchCommandParamsShared(command, direccionId, calle1, calle2, numeroCasa, canton, provincia, principal, clienteClienteid);

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

        
         public static DireccionsCollection SelectSkipAndTake(string sortByExpression, int startRowIndex, int rows)
         {
             return SelectShared("[dbo].[Direccions_SelectSkipAndTake]", null, null, true, null, sortByExpression, startRowIndex, rows);
         }

        
         public static DireccionsCollection SelectSkipAndTakeByClienteClienteid(string sortByExpression, int startRowIndex, int rows, int clienteClienteid)
         {
             return SelectShared("[dbo].[Direccions_SelectSkipAndTakeByClienteClienteid]", "clienteClienteid", clienteClienteid, true, null, sortByExpression, startRowIndex, rows);
         }

         
         public static DireccionsCollection SelectSkipAndTakeDynamicWhere(int? direccionId, string calle1, string calle2, string numeroCasa, string canton, string provincia, bool? principal, int? clienteClienteid, string sortByExpression, int startRowIndex, int rows)
         {
              DireccionsCollection objDireccionsCol = null;
              string storedProcName = "[dbo].[Direccions_SelectSkipAndTakeWhereDynamic]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      
                      command.Parameters.AddWithValue("@start", startRowIndex);
                      command.Parameters.AddWithValue("@numberOfRows", rows);
                      command.Parameters.AddWithValue("@sortByExpression", sortByExpression);

                    
                      AddSearchCommandParamsShared(command, direccionId, calle1, calle2, numeroCasa, canton, provincia, principal, clienteClienteid);

                      using (SqlDataAdapter da = new SqlDataAdapter(command))
                      {
                          DataTable dt = new DataTable();
                          da.Fill(dt);

                          if (dt != null)
                          {
                              if (dt.Rows.Count > 0)
                              {
                                  objDireccionsCol = new DireccionsCollection();

                                  foreach (DataRow dr in dt.Rows)
                                  {
                                      Direccions objDireccions = CreateDireccionsFromDataRowShared(dr);
                                      objDireccionsCol.Add(objDireccions);
                                  }
                              }
                          }
                      }
                  }
              }

              return objDireccionsCol;
         }

        
         public static DireccionsCollection SelectAll()
         {
             return SelectShared("[dbo].[Direccions_SelectAll]", String.Empty, null);
         }

      
         public static DireccionsCollection SelectAllDynamicWhere(int? direccionId, string calle1, string calle2, string numeroCasa, string canton, string provincia, bool? principal, int? clienteClienteid)
         {
              DireccionsCollection objDireccionsCol = null;
              string storedProcName = "[dbo].[Direccions_SelectAllWhereDynamic]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                   
                      AddSearchCommandParamsShared(command, direccionId, calle1, calle2, numeroCasa, canton, provincia, principal, clienteClienteid);

                      using (SqlDataAdapter da = new SqlDataAdapter(command))
                      {
                          DataTable dt = new DataTable();
                          da.Fill(dt);

                          if (dt != null)
                          {
                              if (dt.Rows.Count > 0)
                              {
                                  objDireccionsCol = new DireccionsCollection();

                                  foreach (DataRow dr in dt.Rows)
                                  {
                                      Direccions objDireccions = CreateDireccionsFromDataRowShared(dr);
                                      objDireccionsCol.Add(objDireccions);
                                  }
                              }
                          }
                      }
                  }
              }

              return objDireccionsCol;
         }

        
         public static DireccionsCollection SelectDireccionsCollectionByClienteClienteid(int clienteId)
         {
             return SelectShared("[dbo].[Direccions_SelectAllByClienteClienteid]", "clienteClienteid", clienteId);
         }

        
         public static DireccionsCollection SelectDireccionsDropDownListData()
         {
              DireccionsCollection objDireccionsCol = null;
              string storedProcName = "[dbo].[Direccions_SelectDropDownListData]";

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
                                  objDireccionsCol = new DireccionsCollection();

                                  foreach (DataRow dr in dt.Rows)
                                  {
                                      Direccions objDireccions = new Direccions();
                                      objDireccions.DireccionId = (int)dr["DireccionId"];

                                      if (dr["Calle1"] != System.DBNull.Value)
                                          objDireccions.Calle1 = (string)(dr["Calle1"]);
                                      else
                                          objDireccions.Calle1 = null;

                                      objDireccionsCol.Add(objDireccions);
                                  }
                              }
                          }
                      }
                  }
              }

              return objDireccionsCol;
         }

         public static DireccionsCollection SelectShared(string storedProcName, string param, object paramValue, bool isUseStoredProc = true, string dynamicSqlScript = null, string sortByExpression = null, int? startRowIndex = null, int? rows = null)
         {
              DireccionsCollection objDireccionsCol = null;

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
                                  objDireccionsCol = new DireccionsCollection();

                                  foreach (DataRow dr in dt.Rows)
                                  {
                                      Direccions objDireccions = CreateDireccionsFromDataRowShared(dr);
                                      objDireccionsCol.Add(objDireccions);
                                  }
                              }
                          }
                      }
                  }
              }

              return objDireccionsCol;
         }

       
         public static int Insert(Direccions objDireccions)
         {
             string storedProcName = "[dbo].[Direccions_Insert]";
             return InsertUpdate(objDireccions, false, storedProcName);
         }

       
         public static void Update(Direccions objDireccions)
         {
             string storedProcName = "[dbo].[Direccions_Update]";
             InsertUpdate(objDireccions, true, storedProcName);
         }

         private static int InsertUpdate(Direccions objDireccions, bool isUpdate, string storedProcName)
         {
              int newlyCreatedDireccionId = objDireccions.DireccionId;

              object calle1 = objDireccions.Calle1;
              object calle2 = objDireccions.Calle2;
              object numeroCasa = objDireccions.NumeroCasa;
              object canton = objDireccions.Canton;
              object provincia = objDireccions.Provincia;
              object clienteClienteid = objDireccions.ClienteClienteid;

              if (String.IsNullOrEmpty(objDireccions.Calle1))
                  calle1 = System.DBNull.Value;

              if (String.IsNullOrEmpty(objDireccions.Calle2))
                  calle2 = System.DBNull.Value;

              if (String.IsNullOrEmpty(objDireccions.NumeroCasa))
                  numeroCasa = System.DBNull.Value;

              if (String.IsNullOrEmpty(objDireccions.Canton))
                  canton = System.DBNull.Value;

              if (String.IsNullOrEmpty(objDireccions.Provincia))
                  provincia = System.DBNull.Value;

              if (objDireccions.ClienteClienteid == null)
                  clienteClienteid = System.DBNull.Value;

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                    
                      if (isUpdate)
                      {
                
                          command.Parameters.AddWithValue("@direccionId", objDireccions.DireccionId);
                      }

                      command.Parameters.AddWithValue("@calle1", calle1);
                      command.Parameters.AddWithValue("@calle2", calle2);
                      command.Parameters.AddWithValue("@numeroCasa", numeroCasa);
                      command.Parameters.AddWithValue("@canton", canton);
                      command.Parameters.AddWithValue("@provincia", provincia);
                      command.Parameters.AddWithValue("@principal", objDireccions.Principal);
                      command.Parameters.AddWithValue("@clienteClienteid", clienteClienteid);

                      if (isUpdate)
                          command.ExecuteNonQuery();
                      else
                          newlyCreatedDireccionId = (int)command.ExecuteScalar();
                  }
              }

              return newlyCreatedDireccionId;
         }

      
         public static void Delete(int direccionId)
         {
              string storedProcName = "[dbo].[Direccions_Delete]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

               
                      command.Parameters.AddWithValue("@direccionId", direccionId);

              
                      command.ExecuteNonQuery();
                  }
              }
         }

         private static void AddSearchCommandParamsShared(SqlCommand command, int? direccionId, string calle1, string calle2, string numeroCasa, string canton, string provincia, bool? principal, int? clienteClienteid)
         {
              if(direccionId != null)
                  command.Parameters.AddWithValue("@direccionId", direccionId);
              else
                  command.Parameters.AddWithValue("@direccionId", System.DBNull.Value);

              if(!String.IsNullOrEmpty(calle1))
                  command.Parameters.AddWithValue("@calle1", calle1);
              else
                  command.Parameters.AddWithValue("@calle1", System.DBNull.Value);

              if(!String.IsNullOrEmpty(calle2))
                  command.Parameters.AddWithValue("@calle2", calle2);
              else
                  command.Parameters.AddWithValue("@calle2", System.DBNull.Value);

              if(!String.IsNullOrEmpty(numeroCasa))
                  command.Parameters.AddWithValue("@numeroCasa", numeroCasa);
              else
                  command.Parameters.AddWithValue("@numeroCasa", System.DBNull.Value);

              if(!String.IsNullOrEmpty(canton))
                  command.Parameters.AddWithValue("@canton", canton);
              else
                  command.Parameters.AddWithValue("@canton", System.DBNull.Value);

              if(!String.IsNullOrEmpty(provincia))
                  command.Parameters.AddWithValue("@provincia", provincia);
              else
                  command.Parameters.AddWithValue("@provincia", System.DBNull.Value);

              if(principal != null)
                  command.Parameters.AddWithValue("@principal", principal);
              else
                  command.Parameters.AddWithValue("@principal", System.DBNull.Value);

              if(clienteClienteid != null)
                  command.Parameters.AddWithValue("@clienteClienteid", clienteClienteid);
              else
                  command.Parameters.AddWithValue("@clienteClienteid", System.DBNull.Value);

         }

     
         private static Direccions CreateDireccionsFromDataRowShared(DataRow dr)
         {
             Direccions objDireccions = new Direccions();

             objDireccions.DireccionId = (int)dr["DireccionId"];

             if (dr["Calle1"] != System.DBNull.Value)
                 objDireccions.Calle1 = dr["Calle1"].ToString();
             else
                 objDireccions.Calle1 = null;

             if (dr["Calle2"] != System.DBNull.Value)
                 objDireccions.Calle2 = dr["Calle2"].ToString();
             else
                 objDireccions.Calle2 = null;

             if (dr["NumeroCasa"] != System.DBNull.Value)
                 objDireccions.NumeroCasa = dr["NumeroCasa"].ToString();
             else
                 objDireccions.NumeroCasa = null;

             if (dr["Canton"] != System.DBNull.Value)
                 objDireccions.Canton = dr["Canton"].ToString();
             else
                 objDireccions.Canton = null;

             if (dr["Provincia"] != System.DBNull.Value)
                 objDireccions.Provincia = dr["Provincia"].ToString();
             else
                 objDireccions.Provincia = null;
             objDireccions.Principal = (bool)dr["Principal"];

             if (dr["ClienteClienteid"] != System.DBNull.Value)
                 objDireccions.ClienteClienteid = (int)dr["ClienteClienteid"];
             else
                 objDireccions.ClienteClienteid = null;


             return objDireccions;
         }
     }
}
