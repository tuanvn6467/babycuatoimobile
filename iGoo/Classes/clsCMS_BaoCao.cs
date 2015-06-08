using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;

namespace iGoo.Classes
{
    
    public class clsCMS_BaoCao : clsDBInteractionBase
    {
        /// <summary>
        /// Purpose: Class constructor.
        /// </summary>
        public clsCMS_BaoCao()
        {
            // Nothing for now.
        }

        public override DataTable SelectOne()
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
                throw new Exception("clsCMS_BaoCao::Report_DoanhThu::Error occured.", ex);
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

        private SqlString _title = SqlString.Null;
        public SqlString Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
            }
        }
        private SqlGuid _categoryID = SqlGuid.Null;
        public SqlGuid CategoryID
        {
            get
            {
                return _categoryID;
            }
            set
            {
                _categoryID = value;
            }
        }

        private SqlString _fromDate = SqlString.Null;
        public SqlString FromDate
        {
            get
            {
                return _fromDate;
            }
            set
            {
                _fromDate = value;
            }
        }
        private SqlString _toDate = SqlString.Null;
        public SqlString ToDate
        {
            get
            {
                return _toDate;
            }
            set
            {
                _toDate = value;
            }
        }

        private SqlGuid _NVBH = SqlGuid.Null;
        public SqlGuid NVBH
        {
            get
            {
                return _NVBH;
            }
            set
            {
                _NVBH = value;
            }
        }

        private SqlGuid _CusClassID = SqlGuid.Null;
        public SqlGuid CusClassID
        {
            get
            {
                return _CusClassID;
            }
            set
            {
                _CusClassID = value;
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


        private SqlGuid _orderID = SqlGuid.Null;
        public SqlGuid OrderID
        {
            get
            {
                return _orderID;
            }
            set
            {
                _orderID = value;
            }
        }


        private SqlGuid _userID = SqlGuid.Null;
        public SqlGuid UserID
        {
            get
            {
                return _userID;
            }
            set
            {
                _userID = value;
            }
        }


        private SqlGuid _saleID = SqlGuid.Null;
        public SqlGuid SaleID
        {
            get
            {
                return _saleID;
            }
            set
            {
                _saleID = value;
            }
        }


        private SqlGuid _paymentID = SqlGuid.Null;
        public SqlGuid PaymentID
        {
            get
            {
                return _paymentID;
            }
            set
            {
                _paymentID = value;
            }
        }


        private SqlInt64 _iD = SqlInt64.Null;
        public SqlInt64 ID
        {
            get
            {
                return _iD;
            }
            set
            {
                _iD = value;
            }
        }


        private SqlString _orderCode = SqlString.Null;
        public SqlString OrderCode
        {
            get
            {
                return _orderCode;
            }
            set
            {
                _orderCode = value;
            }
        }


        private SqlString _fullName = SqlString.Null;
        public SqlString FullName
        {
            get
            {
                return _fullName;
            }
            set
            {
                _fullName = value;
            }
        }


        private SqlString _address = SqlString.Null;
        public SqlString Address
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
            }
        }


        private SqlString _email = SqlString.Null;
        public SqlString Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
            }
        }


        private SqlString _phone = SqlString.Null;
        public SqlString Phone
        {
            get
            {
                return _phone;
            }
            set
            {
                _phone = value;
            }
        }


        private SqlString _request = SqlString.Null;
        public SqlString Request
        {
            get
            {
                return _request;
            }
            set
            {
                _request = value;
            }
        }


        private SqlString _comment = SqlString.Null;
        public SqlString Comment
        {
            get
            {
                return _comment;
            }
            set
            {
                _comment = value;
            }
        }


        private SqlDecimal _totalPrice = SqlDecimal.Null;
        public SqlDecimal TotalPrice
        {
            get
            {
                return _totalPrice;
            }
            set
            {
                _totalPrice = value;
            }
        }


        private SqlInt32 _tax = SqlInt32.Null;
        public SqlInt32 Tax
        {
            get
            {
                return _tax;
            }
            set
            {
                _tax = value;
            }
        }


        private SqlDecimal _transportFee = SqlDecimal.Null;
        public SqlDecimal TransportFee
        {
            get
            {
                return _transportFee;
            }
            set
            {
                _transportFee = value;
            }
        }


        private SqlDecimal _discount = SqlDecimal.Null;
        public SqlDecimal Discount
        {
            get
            {
                return _discount;
            }
            set
            {
                _discount = value;
            }
        }


        private SqlString _voucher = SqlString.Null;
        public SqlString Voucher
        {
            get
            {
                return _voucher;
            }
            set
            {
                _voucher = value;
            }
        }


        private SqlInt32 _status = SqlInt32.Null;
        public SqlInt32 Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
            }
        }


        private SqlDateTime _created = SqlDateTime.Null;
        public SqlDateTime Created
        {
            get
            {
                return _created;
            }
            set
            {
                _created = value;
            }
        }


        private SqlDateTime _orderSend = SqlDateTime.Null;
        public SqlDateTime OrderSend
        {
            get
            {
                return _orderSend;
            }
            set
            {
                _orderSend = value;
            }
        }
        private SqlInt32 _provinceID = SqlInt32.Null;

        public SqlInt32 ProvinceID
        {
            get { return _provinceID; }
            set { _provinceID = value; }
        }

        private SqlInt32 _districtID = SqlInt32.Null;

        public SqlInt32 DistrictID
        {
            get { return _districtID; }
            set { _districtID = value; }
        }



        private SqlGuid _InventoryID = SqlGuid.Null;

        public SqlGuid InventoryID
        {
            get { return _InventoryID; }
            set { _InventoryID = value; }
        }

        private SqlGuid _UserDelivery = SqlGuid.Null;

        public SqlGuid UserDelivery
        {
            get { return _UserDelivery; }
            set { _UserDelivery = value; }
        }

        private SqlGuid _TicketID = SqlGuid.Null;

        public SqlGuid TicketID
        {
            get { return _TicketID; }
            set { _TicketID = value; }
        }
        private SqlGuid _ShipperID = SqlGuid.Null;

        public SqlGuid ShipperID
        {
            get { return _ShipperID; }
            set { _ShipperID = value; }
        }
        private SqlInt16 _TicketType = SqlInt16.Null;

        public SqlInt16 TicketType
        {
            get { return _TicketType; }
            set { _TicketType = value; }
        }

        private SqlString _TicketComment = SqlString.Null;

        public SqlString TicketComment
        {
            get { return _TicketComment; }
            set { _TicketComment = value; }
        }
        private SqlInt16 _TicketStatus = SqlInt16.Null;

        public SqlInt16 TicketStatus
        {
            get { return _TicketStatus; }
            set { _TicketStatus = value; }
        }


        #endregion
    }
}
