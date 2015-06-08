using System;
using System.Data;
using iGoo.Classes;
using System.Data.SqlTypes;
using System.Data.SqlClient;

namespace iGoo.Models
{
    public class OrderViewModel : clsCMS_Orders
    {

        public DataTable SelectOrderDetailsByOrderID()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[sp_OrderDetail_ByOrderID]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("CMS_OrderDetails");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@guidOrderID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, OrderID));
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
                throw new Exception("OrderViewModel::SelectOrderDetailsByOrderID::Error occured.", ex);
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

        public DataTable SelectByUserID()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[sp_CMS_Orders_SelectByUserID]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("CMS_Orders");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@guidUserID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, UserID));

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
                throw new Exception("OrderViewModel::sp_CMS_Orders_SelectByUserID::Error occured.", ex);
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
            cmdToExecute.CommandText = "dbo.[sp_CMS_Orders_Insert]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@guidOrderID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, OrderID));
                cmdToExecute.Parameters.Add(new SqlParameter("@guidUserID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, UserID));
                cmdToExecute.Parameters.Add(new SqlParameter("@sFullName", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, FullName));
                cmdToExecute.Parameters.Add(new SqlParameter("@sAddress", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, Address));
                cmdToExecute.Parameters.Add(new SqlParameter("@sEmail", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, Email));
                cmdToExecute.Parameters.Add(new SqlParameter("@sPhone", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, Phone));
                cmdToExecute.Parameters.Add(new SqlParameter("@sRequest", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, Request));
                cmdToExecute.Parameters.Add(new SqlParameter("@dcTotalPrice", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 18, 2, "", DataRowVersion.Proposed, TotalPrice));
                cmdToExecute.Parameters.Add(new SqlParameter("@sVoucher", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, Voucher));
                cmdToExecute.Parameters.Add(new SqlParameter("@iStatus", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, Status));
                cmdToExecute.Parameters.Add(new SqlParameter("@daCreated", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, Created));
                cmdToExecute.Parameters.Add(new SqlParameter("@sComment", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, new SqlString("")));
                cmdToExecute.Parameters.Add(new SqlParameter("@iTax", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, 0));
                cmdToExecute.Parameters.Add(new SqlParameter("@dcTransportFee", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 18, 2, "", DataRowVersion.Proposed, 0));
                cmdToExecute.Parameters.Add(new SqlParameter("@dcDiscount", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 18, 2, "", DataRowVersion.Proposed, 0));
                cmdToExecute.Parameters.Add(new SqlParameter("@iDistrict", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, DistrictID));
                cmdToExecute.Parameters.Add(new SqlParameter("@iProvince", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, ProvinceID));
                cmdToExecute.Parameters.Add(new SqlParameter("@guidPaymentID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, new SqlGuid("3ffd9820-ad96-4637-9f71-74845b9bd529")));
                cmdToExecute.Parameters.Add(new SqlParameter("@guidCusClassID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, CusClassID));
                cmdToExecute.Parameters.Add(new SqlParameter("@guidSaleID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, SqlGuid.Null));
                cmdToExecute.Parameters.Add(new SqlParameter("@guidInvID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, InventoryID));
                cmdToExecute.Parameters.Add(new SqlParameter("@iOType", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, OType));
                cmdToExecute.Parameters.Add(new SqlParameter("@dcPrePayment", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 18, 2, "", DataRowVersion.Proposed, PrePayment));

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
                throw new Exception("OrderViewModel::Insert::Error occured.", ex);
            }
            finally
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
            }
        }

    }
}
