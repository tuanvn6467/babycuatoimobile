using System;
using System.Data;
using iGoo.Classes;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
namespace iGoo.Areas.Webcms.Models
{
    public class NVGHViewModel : clsCMS_Shippers
    {
        public override DataTable SelectAll()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[sp_CMS_Shippers_SelectAll]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("CMS_Shippers");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@PageIndex", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, PageIndex));
                cmdToExecute.Parameters.Add(new SqlParameter("@PageSize", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, PageSize));
                cmdToExecute.Parameters.Add(new SqlParameter("@OrderBy", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, OrderBy));
                cmdToExecute.Parameters.Add(new SqlParameter("@sFullName", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, FullName));
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
                throw new Exception("ShipperViewModel::SelectAll::Error occured.", ex);
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

       
        //Select all CMS_Inventory
        public DataTable SelectOptimize()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[sp_CMS_Shippers_Optimize]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("CMS_Shippers");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@sFullName", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, FullName));

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
                throw new Exception("clsCMS_Shipper::SelectOptimize::Error occured.", ex);
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

        public DataTable SelectProvince()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[sp_CMS_Shippers_SelectProvince]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("CMS_Provinces");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
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
                throw new Exception("clsCMS_Shipper::SelectOptimize::Error occured.", ex);
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


        public DataTable SelectDistrictByProvinceID(int ProvinceID)
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[sp_CMS_Shippers_SelectDistrictByProvinceID]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("CMS_Districts");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@sProvinceID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, ProvinceID));
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
                throw new Exception("clsCMS_Shipper::SelectOptimize::Error occured.", ex);
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