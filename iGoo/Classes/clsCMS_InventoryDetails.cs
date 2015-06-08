using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;

namespace iGoo.Classes
{
    /// <summary>
    /// Dang Ngoc Hung - hungdn@ptit.edu.vn
    /// Purpose: Data Access class for the table 'CMS_InventoryDetails'.
    /// </summary>
    public class clsCMS_InventoryDetails:clsDBInteractionBase
    {
          /// <summary>
		/// Purpose: Class constructor.
		/// </summary>
        public clsCMS_InventoryDetails()
		{
			// Nothing for now.
		}


        /// <summary>
        /// Purpose: Insert method. This method will insert one new row into the database.
        /// </summary>
        /// <returns>True if succeeded, otherwise an Exception is thrown. </returns>
        /// <remarks>
        /// </remarks>
        public override bool Insert()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[sp_CMS_InventoryDetails_Insert]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@guidInventoryDetailID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _InventoryDetailID));
                cmdToExecute.Parameters.Add(new SqlParameter("@guidProductID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _ProductID));
                cmdToExecute.Parameters.Add(new SqlParameter("@guidInventoryID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _InventoryID));
                cmdToExecute.Parameters.Add(new SqlParameter("@iQuantity", SqlDbType.Int, 16, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _Quantity));
                cmdToExecute.Parameters.Add(new SqlParameter("@iBrokenQuantity", SqlDbType.Int, 16, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _BrokenQuantity));

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
                throw new Exception("CMS_InventoryDetails::Insert::Error occured.", ex);
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


        /// <summary>
        /// Purpose: Update method. This method will Update one existing row in the database.
        /// </summary>
        /// <returns>True if succeeded, otherwise an Exception is thrown. </returns>
        /// <remarks>
        /// </remarks>
        public override bool Update()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[sp_CMS_InventoryDetails_Update]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@guidInventoryDetailID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _InventoryDetailID));
                cmdToExecute.Parameters.Add(new SqlParameter("@guidProductID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _ProductID));
                cmdToExecute.Parameters.Add(new SqlParameter("@guidInventoryID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _InventoryID));
                cmdToExecute.Parameters.Add(new SqlParameter("@iQuantity", SqlDbType.Int, 16, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _Quantity));
                cmdToExecute.Parameters.Add(new SqlParameter("@iBrokenQuantity", SqlDbType.Int, 16, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _BrokenQuantity));

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
                throw new Exception("CMS_InventoryDetails::Insert::Error occured.", ex);
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
        /// <summary>
        /// Purpose: Change method. This method will Update one existing row in the database.
        /// </summary>
        /// <returns>True if succeeded, otherwise an Exception is thrown. </returns>
        /// <remarks>
        /// </remarks>
        public bool Change()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[sp_CMS_InventoryDetails_Change]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@guidInventoryDetailID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _InventoryDetailID));
                cmdToExecute.Parameters.Add(new SqlParameter("@guidProductID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _ProductID));
                cmdToExecute.Parameters.Add(new SqlParameter("@guidInventoryID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _InventoryID));
                cmdToExecute.Parameters.Add(new SqlParameter("@iChangeType", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _ChangeType));
                cmdToExecute.Parameters.Add(new SqlParameter("@iQuantityChange", SqlDbType.Int, 16, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _QuantityChange));
                cmdToExecute.Parameters.Add(new SqlParameter("@iBrokenQuantityChange", SqlDbType.Int, 16, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _BrokenQuantityChange));
                cmdToExecute.Parameters.Add(new SqlParameter("@guidUserID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _UserID));
                cmdToExecute.Parameters.Add(new SqlParameter("@sDescription", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _Description));
                cmdToExecute.Parameters.Add(new SqlParameter("@guidRefID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _RefID));

                cmdToExecute.Parameters.Add(new SqlParameter("@iSoChungTu", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _SoChungTu));
                cmdToExecute.Parameters.Add(new SqlParameter("@sGhiChu", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _GhiChu));
                cmdToExecute.Parameters.Add(new SqlParameter("@iKieu", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _Kieu));
                cmdToExecute.Parameters.Add(new SqlParameter("@guidKhoNhap", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _KhoNhap));

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
                throw new Exception("CMS_InventoryDetails::Insert::Error occured.", ex);
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


        public bool ImportChange()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[sp_CMS_InventoryDetails_ImportChange]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@guidProductCode", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _ProductCode));
                cmdToExecute.Parameters.Add(new SqlParameter("@guidInventoryID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _InventoryID));
                cmdToExecute.Parameters.Add(new SqlParameter("@iChangeType", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _ChangeType));
                cmdToExecute.Parameters.Add(new SqlParameter("@iQuantityChange", SqlDbType.Int, 16, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _QuantityChange));
                cmdToExecute.Parameters.Add(new SqlParameter("@iBrokenQuantityChange", SqlDbType.Int, 16, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _BrokenQuantityChange));
                cmdToExecute.Parameters.Add(new SqlParameter("@sSoChungTu", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _SoChungTu));
                cmdToExecute.Parameters.Add(new SqlParameter("@guidUserID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _UserID));
                cmdToExecute.Parameters.Add(new SqlParameter("@sDescription", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _Description));

                cmdToExecute.Parameters.Add(new SqlParameter("@sKhoXuat", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _InventoryCodeXuat));
                cmdToExecute.Parameters.Add(new SqlParameter("@sKhoNhap", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _InventoryCodeNhap));
                

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
                throw new Exception("CMS_InventoryDetails::Insert::Error occured.", ex);
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

        /// <summary>
        /// Purpose: Delete method. This method will Delete one existing row in the database, based on the Primary Key.
        /// </summary>
        /// <returns>True if succeeded, otherwise an Exception is thrown. </returns>
        /// <remarks>
        /// </remarks>
        public override bool Delete()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[sp_CMS_InventoryDetails_Delete]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@guidInventoryDetailID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _InventoryDetailID));

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
                throw new Exception("CMS_InventoryDetails::Delete::Error occured.", ex);
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

        /// <summary>
        /// Purpose: Select method. This method will Select one existing row from the database, based on the Primary Key.
        /// </summary>
        /// <returns>DataTable object if succeeded, otherwise an Exception is thrown. </returns>
        /// <remarks>
        /// Will fill all properties corresponding with a field in the table with the value of the row selected.
        /// </remarks>
        public override DataTable SelectOne()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[sp_CMS_InventoryDetails_SelectOne]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("CMS_InventoryDetails");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@guidInventoryDetailID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _InventoryDetailID));

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
                throw new Exception("CMS_InventoryDetails::SelectOne::Error occured.", ex);
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
        /// <summary>
        /// Purpose: SelectAll method. This method will Select all rows from the table.
        /// </summary>
        /// <returns>DataTable object if succeeded, otherwise an Exception is thrown. </returns>
        /// <remarks>
        /// </remarks>
        public override DataTable SelectAll()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[sp_CMS_InventoryDetails_SelectAll]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("CMS_InventoryDetails");
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
                throw new Exception("CMS_InventoryDetails::SelectAll::Error occured.", ex);
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

        //sonln added
        public DataTable SelectOne1()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[sp_CMS_InventoryDetails_SelectOne1]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("CMS_InventoryDetails");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@guidInventoryID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _InventoryID));
                cmdToExecute.Parameters.Add(new SqlParameter("@guidProductID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _ProductID));

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
                throw new Exception("CMS_InventoryDetails::SelectOne::Error occured.", ex);
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


        #region Class Property Declarations
        private SqlInt32 _pageIndex = SqlInt32.Null;
        public SqlInt32 PageIndex
        {
            get
            {
                return _pageIndex;
            }
            set
            {
                _pageIndex = value;
            }
        }
        private SqlInt32 _pageSize = SqlInt32.Null;
        public SqlInt32 PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = value;
            }
        }
        private SqlInt32 _totalRows = SqlInt32.Null;
        public SqlInt32 TotalRows
        {
            get
            {
                return _totalRows;
            }
            set
            {
                _totalRows = value;
            }
        }
        private SqlString _orderBy = SqlString.Null;
        public SqlString OrderBy
        {
            get
            {
                return _orderBy;
            }
            set
            {
                _orderBy = value;
            }
        }

        private SqlGuid _InventoryDetailID = SqlGuid.Null;

        public SqlGuid InventoryDetailID
        {
            get { return _InventoryDetailID; }
            set { _InventoryDetailID = value; }
        }

        private SqlGuid _ProductID = SqlGuid.Null;

        public SqlGuid ProductID
        {
            get { return _ProductID; }
            set { _ProductID = value; }
        }

        private SqlGuid _InventoryID = SqlGuid.Null;

        public SqlGuid InventoryID
        {
            get { return _InventoryID; }
            set { _InventoryID = value; }
        }

        private SqlString _InventoryCodeXuat = SqlString.Null;

        public SqlString InventoryCodeXuat
        {
            get { return _InventoryCodeXuat; }
            set { _InventoryCodeXuat = value; }
        }

        private SqlString _InventoryCodeNhap = SqlString.Null;

        public SqlString InventoryCodeNhap
        {
            get { return _InventoryCodeNhap; }
            set { _InventoryCodeNhap = value; }
        }

        private SqlGuid _CategoryID = SqlGuid.Null;

        public SqlGuid CategoryID
        {
            get { return _CategoryID; }
            set { _CategoryID = value; }
        }
        private SqlInt32 _Status = SqlInt32.Null;

        public SqlInt32 Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        private SqlString _ProductCode = SqlString.Null;

        public SqlString ProductCode
        {
            get { return _ProductCode; }
            set { _ProductCode = value; }
        }

        private SqlString _ProductName = SqlString.Null;

        public SqlString ProductName
        {
            get { return _ProductName; }
            set { _ProductName = value; }
        }
        private SqlInt32 _Quantity = SqlInt32.Null;

        public SqlInt32 Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        private SqlInt32 _BrokenQuantity = SqlInt32.Null;

        public SqlInt32 BrokenQuantity
        {
            get { return _BrokenQuantity; }
            set { _BrokenQuantity = value; }
        }

        private SqlGuid _UserID = SqlGuid.Null;

        public SqlGuid UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        private SqlInt32 _ChangeType = SqlInt32.Null;

        public SqlInt32 ChangeType
        {
            get { return _ChangeType; }
            set { _ChangeType = value; }
        }

        private SqlInt32 _BrokenQuantityChange = SqlInt32.Null;

        public SqlInt32 BrokenQuantityChange
        {
            get { return _BrokenQuantityChange; }
            set { _BrokenQuantityChange = value; }
        }

        private SqlInt32 _QuantityChange = SqlInt32.Null;

        public SqlInt32 QuantityChange
        {
            get { return _QuantityChange; }
            set { _QuantityChange = value; }
        }

        private SqlString _Description = SqlString.Null;

        public SqlString Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        private SqlGuid _RefID = SqlGuid.Null;

        public SqlGuid RefID
        {
            get { return _RefID; }
            set { _RefID = value; }
        }

        private SqlGuid _KhoNhap = SqlGuid.Null;

        public SqlGuid KhoNhap
        {
            get { return _KhoNhap; }
            set { _KhoNhap = value; }
        }

        private SqlString _SoChungTu = SqlString.Null;

        public SqlString SoChungTu
        {
            get { return _SoChungTu; }
            set { _SoChungTu = value; }
        }

        private SqlString _GhiChu = SqlString.Null;

        public SqlString GhiChu
        {
            get { return _GhiChu; }
            set { _GhiChu = value; }
        }

        private SqlInt32 _Kieu = SqlInt32.Null;

        public SqlInt32 Kieu
        {
            get { return _Kieu; }
            set { _Kieu = value; }
        }

        private SqlInt32 _TrangThai = SqlInt32.Null;

        public SqlInt32 TrangThai
        {
            get { return _TrangThai; }
            set { _TrangThai = value; }
        }


        private SqlGuid _ChungLoai = SqlGuid.Null;
        public SqlGuid ChungLoai
        {
            get
            {
                return _ChungLoai;
            }
            set
            {
                _ChungLoai = value;
            }
        }
        #endregion
    }
        public class InventoryDetailExport
        {
            public string STT { get; set; }
            public string SKU { get; set; }
            public string Title { get; set; }
            public string Quantity { get; set; }
            public string BrokenQuantity { get; set; }
            public string InventoryName { get; set; }

        }
        public class BaoCaoDoanhThu_Export
        {
            public string STT { get; set; }
            public string MaDonHang { get; set; }
            public string Ngay { get; set; }
            public Double TongTien { get; set; }
            public Int32 SoLuong { get; set; }
            public string KhachHang { get; set; }
            public string NhanVien { get; set; }
        }
        public class BaoCaoDoanhThu_BySP_Export
        {
            public string STT { get; set; }
            public string MaSanPham { get; set; }
            public Int32 SoLuong { get; set; }
            public Double TongTien { get; set; }
        }

        public class BaoCaoDoanhThu_ByNVBH_Export
        {
            public string STT { get; set; }
            public string TenNV { get; set; }
            public Int32 OLBuon { get; set; }
            public Int32 SRBuon { get; set; }
            public Double DTOLBuon { get; set; }
            public Double DTSRBuon { get; set; }
            public Int32 OLLe { get; set; }
            public Int32 SRLe { get; set; }
            public Double DTOLLe { get; set; }
            public Double DTSRLe { get; set; }            
        }

        public class BaoCaoDoanhThu_BySP_Export2
        {
            public string STT { get; set; }
            public string MaSP { get; set; }
            public string ChungLoai { get; set; }
            public Int32 OLBuon { get; set; }
            public Int32 SRBuon { get; set; }
            public Double DTOLBuon { get; set; }
            public Double DTSRBuon { get; set; }
            public Int32 OLLe { get; set; }
            public Int32 SRLe { get; set; }
            public Double DTOLLe { get; set; }
            public Double DTSRLe { get; set; }
        }
        public class InventoryLogs_SelectAll_Export
        {
            public string STT { get; set; }
            public string NgayThucHien { get; set; }
            public string MaSP { get; set; }
            public string TenSP { get; set; }
            public string HinhThuc { get; set; }
            public string KhoXuat { get; set; }
            public string KhoNhan { get; set; }
            public Int32 SoLuong { get; set; }
            public Int32 TonDau { get; set; }
            public Int32 TonCuoi { get; set; }
            public string TinhTrang { get; set; }
            public string ChungTu { get; set; }
            public string NguoiThucHien { get; set; }
        }


    
}