using System;
using System.Data;
using iGoo.Classes;
using System.Data.SqlTypes;
using System.Data.SqlClient;

namespace iGoo.Areas.Webcms.Models
{
    public class BaoCaoViewModel : clsCMS_BaoCao
    {     

        public DataTable ReportDoanhThu()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[sp_Report_DoanhThu]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("Report_DoanhThu");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@PageIndex", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, PageIndex));
                cmdToExecute.Parameters.Add(new SqlParameter("@PageSize", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, PageSize));
                cmdToExecute.Parameters.Add(new SqlParameter("@OrderBy", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, OrderBy));
                cmdToExecute.Parameters.Add(new SqlParameter("@sTitle", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, Title));
                cmdToExecute.Parameters.Add(new SqlParameter("@guidCategoryID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, CategoryID));
                cmdToExecute.Parameters.Add(new SqlParameter("@sFromDate", SqlDbType.NVarChar, -1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, FromDate));
                cmdToExecute.Parameters.Add(new SqlParameter("@sToDate", SqlDbType.NVarChar, -1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, ToDate));
                cmdToExecute.Parameters.Add(new SqlParameter("@NVBH", SqlDbType.UniqueIdentifier, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, NVBH));
                cmdToExecute.Parameters.Add(new SqlParameter("@CusClassID", SqlDbType.UniqueIdentifier, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, CusClassID));
				
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
                throw new Exception("BaoCaoViewModel::sp_Report_DoanhThu::Error occured.", ex);
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

        public DataTable ReportDoanhThu_BySP()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[sp_Report_DoanhThu_BySP]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("sp_Report_DoanhThu_BySP");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@PageIndex", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, PageIndex));
                cmdToExecute.Parameters.Add(new SqlParameter("@PageSize", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, PageSize));
                cmdToExecute.Parameters.Add(new SqlParameter("@OrderBy", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, OrderBy));
                cmdToExecute.Parameters.Add(new SqlParameter("@sTitle", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, Title));
                cmdToExecute.Parameters.Add(new SqlParameter("@guidCategoryID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, CategoryID));
                cmdToExecute.Parameters.Add(new SqlParameter("@sFromDate", SqlDbType.NVarChar, -1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, FromDate));
                cmdToExecute.Parameters.Add(new SqlParameter("@sToDate", SqlDbType.NVarChar, -1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, ToDate));
                cmdToExecute.Parameters.Add(new SqlParameter("@NVBH", SqlDbType.UniqueIdentifier, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, NVBH));
                cmdToExecute.Parameters.Add(new SqlParameter("@CusClassID", SqlDbType.UniqueIdentifier, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, CusClassID));

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
                throw new Exception("BaoCaoViewModel::sp_Report_DoanhThu_BySP::Error occured.", ex);
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

        public DataTable ReportDoanhThu_ByDate()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[sp_Report_DoanhThu_ByDate]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("Report_DoanhThu_ByDate");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@PageIndex", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, PageIndex));
                cmdToExecute.Parameters.Add(new SqlParameter("@PageSize", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, PageSize));
                cmdToExecute.Parameters.Add(new SqlParameter("@OrderBy", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, OrderBy));
                cmdToExecute.Parameters.Add(new SqlParameter("@sFromDate", SqlDbType.NVarChar, -1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, FromDate));
                cmdToExecute.Parameters.Add(new SqlParameter("@sToDate", SqlDbType.NVarChar, -1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, ToDate));                
                cmdToExecute.Parameters.Add(new SqlParameter("@CusClassID", SqlDbType.UniqueIdentifier, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, CusClassID));

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
                throw new Exception("BaoCaoViewModel::sp_Report_DoanhThu_ByDate::Error occured.", ex);
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
       

        public DataTable Report_DoanhThu_Tong()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[sp_Report_DoanhThu_Tong]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("Report_DoanhThu_Tong");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@sTitle", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, Title));
                cmdToExecute.Parameters.Add(new SqlParameter("@guidCategoryID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, CategoryID));                
                cmdToExecute.Parameters.Add(new SqlParameter("@sFromDate", SqlDbType.NVarChar, -1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, FromDate));
                cmdToExecute.Parameters.Add(new SqlParameter("@sToDate", SqlDbType.NVarChar, -1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, ToDate));
                cmdToExecute.Parameters.Add(new SqlParameter("@NVBH", SqlDbType.UniqueIdentifier, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, NVBH));
                cmdToExecute.Parameters.Add(new SqlParameter("@CusClassID", SqlDbType.UniqueIdentifier, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, CusClassID));

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
                throw new Exception("BaoCaoViewModel::sp_Report_Tong_DoanhThu::Error occured.", ex);
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

        public DataTable Report_DoanhThu_BySP_Tong()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[sp_Report_DoanhThu_BySP_Tong]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("Report_DoanhThu_BySP_Tong");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@sTitle", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, Title));
                cmdToExecute.Parameters.Add(new SqlParameter("@guidCategoryID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, CategoryID));
                cmdToExecute.Parameters.Add(new SqlParameter("@sFromDate", SqlDbType.NVarChar, -1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, FromDate));
                cmdToExecute.Parameters.Add(new SqlParameter("@sToDate", SqlDbType.NVarChar, -1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, ToDate));
                cmdToExecute.Parameters.Add(new SqlParameter("@NVBH", SqlDbType.UniqueIdentifier, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, NVBH));
                cmdToExecute.Parameters.Add(new SqlParameter("@CusClassID", SqlDbType.UniqueIdentifier, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, CusClassID));

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
                throw new Exception("BaoCaoViewModel::sp_Report_Tong_DoanhThu::Error occured.", ex);
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