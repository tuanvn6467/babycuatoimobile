using System;
using System.Data;
using iGoo.Classes;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;


namespace iGoo.Areas.Webcms.Models
{
    public class InventoryLogViewModel:clsCMS_InventoryLogs
    {
        public override DataTable SelectAll()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[sp_CMS_InventoryLogs_SelectAll]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("InventoryLogs");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@PageIndex", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, PageIndex));
                cmdToExecute.Parameters.Add(new SqlParameter("@PageSize", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, PageSize));
                cmdToExecute.Parameters.Add(new SqlParameter("@OrderBy", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, OrderBy));
                cmdToExecute.Parameters.Add(new SqlParameter("@guidInventoryID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, InventoryID));
                cmdToExecute.Parameters.Add(new SqlParameter("@guidCategoryID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, ChungLoai));
                cmdToExecute.Parameters.Add(new SqlParameter("@guidUserID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, UserID));
                cmdToExecute.Parameters.Add(new SqlParameter("@sProductName", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, SoChungTu));
                cmdToExecute.Parameters.Add(new SqlParameter("@ChangeType", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, ChangeType));
                cmdToExecute.Parameters.Add(new SqlParameter("@sStartDate", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, StartDate));
                cmdToExecute.Parameters.Add(new SqlParameter("@sEndDate", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, EndDate));
                cmdToExecute.Parameters.Add(new SqlParameter("@iDes", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, iDescription));

                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }

                // Execute query.
                adapter.Fill(toReturn);
                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("InventoryDetailViewModel::SelectAll::Error occured.", ex);
            }
            finally
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
                adapter.Dispose();
            }
        }

        public DataTable SelectAll_XK()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[sp_CMS_InventoryLogs_Xuat]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("InventoryLogs");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@PageIndex", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, PageIndex));
                cmdToExecute.Parameters.Add(new SqlParameter("@PageSize", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, PageSize));
                cmdToExecute.Parameters.Add(new SqlParameter("@OrderBy", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, OrderBy));
                cmdToExecute.Parameters.Add(new SqlParameter("@sSoChungTu", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, SoChungTu));
                cmdToExecute.Parameters.Add(new SqlParameter("@guidInventoryID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, InventoryID));
                cmdToExecute.Parameters.Add(new SqlParameter("@TrangThai", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, TrangThai));
                cmdToExecute.Parameters.Add(new SqlParameter("@sStartDate", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, StartDate));
                cmdToExecute.Parameters.Add(new SqlParameter("@sEndDate", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, EndDate));
                cmdToExecute.Parameters.Add(new SqlParameter("@sUserID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, UserID));                

                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }

                // Execute query.
                adapter.Fill(toReturn);
                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("InventoryDetailViewModel::SelectAll::Error occured.", ex);
            }
            finally
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
                adapter.Dispose();
            }
        }

        public DataTable SelectAll_Export()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[sp_CMS_InventoryLogs_SelectAll_Export]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("InventoryLogs");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@guidInventoryID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, InventoryID));
                cmdToExecute.Parameters.Add(new SqlParameter("@guidCategoryID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, ChungLoai));
                cmdToExecute.Parameters.Add(new SqlParameter("@guidUserID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, UserID));
                cmdToExecute.Parameters.Add(new SqlParameter("@sProductName", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, SoChungTu));
                cmdToExecute.Parameters.Add(new SqlParameter("@ChangeType", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, ChangeType));
                cmdToExecute.Parameters.Add(new SqlParameter("@sStartDate", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, StartDate));
                cmdToExecute.Parameters.Add(new SqlParameter("@sEndDate", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, EndDate));
                cmdToExecute.Parameters.Add(new SqlParameter("@iDes", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, iDescription));

                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }

                // Execute query.
                adapter.Fill(toReturn);
                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("InventoryDetailViewModel::SelectAll::Error occured.", ex);
            }
            finally
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
                adapter.Dispose();
            }
        }
    }
}