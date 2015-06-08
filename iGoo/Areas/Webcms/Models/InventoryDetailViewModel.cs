using System;
using System.Data;
using iGoo.Classes;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;


namespace iGoo.Areas.Webcms.Models
{
    public class InventoryDetailViewModel:clsCMS_InventoryDetails
    {
        public override DataTable SelectAll()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[sp_CMS_InventoryDetails_SelectAll]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("InventoryDetails");
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
                cmdToExecute.Parameters.Add(new SqlParameter("@sProductName", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, ProductName));
                cmdToExecute.Parameters.Add(new SqlParameter("@iStatus", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, Status));
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


        public override bool Insert()
    {
        SqlCommand cmdToExecute = new SqlCommand();
        cmdToExecute.CommandText = "dbo.[sp_CMS_InventoryDetails_InsertFull]";
        cmdToExecute.CommandType = CommandType.StoredProcedure;
        DataTable toReturn = new DataTable("InventoryDetails");
        SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

        // Use base class' connection object
        cmdToExecute.Connection = _mainConnection;

        try
        {
            cmdToExecute.Parameters.Add(new SqlParameter("@guidInventoryDetailID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, InventoryDetailID));
            cmdToExecute.Parameters.Add(new SqlParameter("@guidProductID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, ProductID));
            cmdToExecute.Parameters.Add(new SqlParameter("@guidInventoryID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, InventoryID));
            cmdToExecute.Parameters.Add(new SqlParameter("@iQuantity", SqlDbType.Int, 16, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, Quantity));
            cmdToExecute.Parameters.Add(new SqlParameter("@iBrokenQuantity", SqlDbType.Int, 16, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, BrokenQuantity));
            cmdToExecute.Parameters.Add(new SqlParameter("@guidUserID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, UserID));
            cmdToExecute.Parameters.Add(new SqlParameter("@iChangeType", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, ChangeType));
            cmdToExecute.Parameters.Add(new SqlParameter("@sDescription", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, Description));

            cmdToExecute.Parameters.Add(new SqlParameter("@iSoChungTu", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, SoChungTu));
            cmdToExecute.Parameters.Add(new SqlParameter("@sGhiChu", SqlDbType.NVarChar, 400, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, GhiChu));
            cmdToExecute.Parameters.Add(new SqlParameter("@iKieu", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, Kieu));
            cmdToExecute.Parameters.Add(new SqlParameter("@guidKhoNhap", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, KhoNhap)); 
  
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
            cmdToExecute.ExecuteNonQuery();
            return true;
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
        public bool CheckProductCode()
    {
        SqlCommand cmdToExecute = new SqlCommand();
        cmdToExecute.CommandText = "dbo.[sp_CMS_Product_CheckProductCode]";
        cmdToExecute.CommandType = CommandType.StoredProcedure;
        DataTable toReturn = new DataTable("ProductDetails");
        SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

        // Use base class' connection object
        cmdToExecute.Connection = _mainConnection;

        try
        {
            cmdToExecute.Parameters.Add(new SqlParameter("@sProductCode", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, ProductCode));
               
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

            if(toReturn.Rows.Count>0)
                return true;
            else
                return false;
        }
        catch (Exception ex)
        {
            // some error occured. Bubble it to caller and encapsulate Exception object
            throw new Exception("InventoryDetailViewModel::CheckProductCode::Error occured.", ex);
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

    
   
    public class ImportProduct
    {
        public String ProductCode { get; set; }
        public Int16 Quantity { get; set; }
        public Int16 BrokenQuantity { get; set; }
    }

    public class ExportProduct
    {
        public String MaSP { get; set; }
        public Int32 SoLuong { get; set; }
        
    }
}