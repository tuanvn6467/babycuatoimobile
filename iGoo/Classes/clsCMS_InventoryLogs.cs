using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;

namespace iGoo.Classes
{
    /// <summary>
    /// Dang Ngoc Hung - hungdn@ptit.edu.vn
    /// Purpose: Data Access class for the table 'CMS_InventoryLogs'.
    /// </summary>
    public class clsCMS_InventoryLogs : clsDBInteractionBase
    {
           /// <summary>
		/// Purpose: Class constructor.
		/// </summary>
        public clsCMS_InventoryLogs()
		{
			// Nothing for now.
		}

        public override DataTable SelectAll()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[sp_CMS_InventoryLog_SelectAll]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("CMS_InventoryLogs");
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
                throw new Exception("CMS_InventoryLogs::SelectAll::Error occured.", ex);
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

        private SqlGuid _CategoryID = SqlGuid.Null;

        public SqlGuid CategoryID
        {
            get { return _CategoryID; }
            set { _CategoryID = value; }
        }


        private SqlString _ProductName = SqlString.Null;

        public SqlString ProductName
        {
            get { return _ProductName; }
            set { _ProductName = value; }
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

        private SqlString _StartDate = SqlString.Null;

        public SqlString StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        private SqlString _EndDate = SqlString.Null;

        public SqlString EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }

        private SqlInt32 _iDescription = SqlInt32.Null;

        public SqlInt32 iDescription
        {
            get { return _iDescription; }
            set { _iDescription = value; }
        }

        private SqlString _SoChungTu = SqlString.Null;

        public SqlString SoChungTu
        {
            get { return _SoChungTu; }
            set { _SoChungTu = value; }
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
}