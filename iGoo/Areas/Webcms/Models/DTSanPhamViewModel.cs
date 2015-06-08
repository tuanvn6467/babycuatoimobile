using System;
using System.Data;
using iGoo.Classes;
using System.Data.SqlTypes;
using System.Data.SqlClient;

namespace iGoo.Areas.Webcms.Models
{
    public class DTSanPhamViewModel : clsCMS_DTNhanVien
    {     

        public DataTable ReportDoanhThu_SanPham()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[sp_Report_DoanhThu_SKU]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("sp_Report_DoanhThu_SKU");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {                
                cmdToExecute.Parameters.Add(new SqlParameter("@sTitle", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, Title));
                cmdToExecute.Parameters.Add(new SqlParameter("@ChungLoai", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, ChungLoai));
                cmdToExecute.Parameters.Add(new SqlParameter("@TypeBuy", SqlDbType.Int, 32, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, TypeBuy));
                cmdToExecute.Parameters.Add(new SqlParameter("@CusClassID", SqlDbType.UniqueIdentifier, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, CusClassID));
                
                cmdToExecute.Parameters.Add(new SqlParameter("@sFromDate", SqlDbType.NVarChar, -1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, FromDate));
                cmdToExecute.Parameters.Add(new SqlParameter("@sToDate", SqlDbType.NVarChar, -1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, ToDate));
                cmdToExecute.Parameters.Add(new SqlParameter("@inv", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, InventoryID));
                                
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
                throw new Exception("DTGiaoHangViewModel::sp_Report_DoanhThu_SKU::Error occured.", ex);
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