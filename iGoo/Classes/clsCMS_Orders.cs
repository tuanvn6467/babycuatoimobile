using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;

namespace iGoo.Classes
{
	/// <summary>
	/// Nguyen Thanh Binh - Thanhbinh101287@gmail.com
	/// Purpose: Data Access class for the table 'CMS_Orders'.
	/// </summary>
	public class clsCMS_Orders : clsDBInteractionBase
	{
		/// <summary>
		/// Purpose: Class constructor.
		/// </summary>
		public clsCMS_Orders()
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
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "dbo.[sp_CMS_Orders_Insert]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;

			// Use base class' connection object
			cmdToExecute.Connection = _mainConnection;

			try
			{
				cmdToExecute.Parameters.Add(new SqlParameter("@guidOrderID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _orderID));
				cmdToExecute.Parameters.Add(new SqlParameter("@guidUserID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _userID));
				cmdToExecute.Parameters.Add(new SqlParameter("@guidSaleID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _saleID));
				cmdToExecute.Parameters.Add(new SqlParameter("@guidPaymentID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _paymentID));
				//cmdToExecute.Parameters.Add(new SqlParameter("@sOrderCode", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _orderCode));
				cmdToExecute.Parameters.Add(new SqlParameter("@sFullName", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _fullName));
				cmdToExecute.Parameters.Add(new SqlParameter("@sAddress", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _address));
				cmdToExecute.Parameters.Add(new SqlParameter("@sEmail", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _email));
				cmdToExecute.Parameters.Add(new SqlParameter("@sPhone", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _phone));
                cmdToExecute.Parameters.Add(new SqlParameter("@sPhone1", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _phone1));
                cmdToExecute.Parameters.Add(new SqlParameter("@sPhone2", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _phone2));
                cmdToExecute.Parameters.Add(new SqlParameter("@sRequest", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _request));
                cmdToExecute.Parameters.Add(new SqlParameter("@dcTotalPrice", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 18, 2, "", DataRowVersion.Proposed, _totalPrice));
                cmdToExecute.Parameters.Add(new SqlParameter("@sComment", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _comment));
				cmdToExecute.Parameters.Add(new SqlParameter("@iTax", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _tax));
				cmdToExecute.Parameters.Add(new SqlParameter("@dcTransportFee", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 18, 2, "", DataRowVersion.Proposed, _transportFee));
				cmdToExecute.Parameters.Add(new SqlParameter("@dcDiscount", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 18, 2, "", DataRowVersion.Proposed, _discount));
				cmdToExecute.Parameters.Add(new SqlParameter("@sVoucher", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _voucher));
				cmdToExecute.Parameters.Add(new SqlParameter("@iStatus", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _status));
				cmdToExecute.Parameters.Add(new SqlParameter("@daCreated", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _created));
				//cmdToExecute.Parameters.Add(new SqlParameter("@daOrderSend", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _orderSend));
                cmdToExecute.Parameters.Add(new SqlParameter("@guidInvID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, InventoryID));
                cmdToExecute.Parameters.Add(new SqlParameter("@iDistrict", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _districtID));
                cmdToExecute.Parameters.Add(new SqlParameter("@iProvince", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _provinceID));
                cmdToExecute.Parameters.Add(new SqlParameter("@guidCusClassID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _cusClassID));
                cmdToExecute.Parameters.Add(new SqlParameter("@iOType", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _oType));
                cmdToExecute.Parameters.Add(new SqlParameter("@dcPrePayment", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 18, 2, "", DataRowVersion.Proposed, PrePayment));

				if(_mainConnectionIsCreatedLocal)
				{
					// Open connection.
					_mainConnection.Open();
				}
				else
				{
					if(_mainConnectionProvider.IsTransactionPending)
					{
						cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
					}
				}

				// Execute query.
				cmdToExecute.ExecuteNonQuery();
				//_iD = (SqlInt64)cmdToExecute.Parameters["@lID"].Value;
				return true;
			}
			catch(Exception ex)
			{
				// some error occured. Bubble it to caller and encapsulate Exception object
				throw new Exception("clsCMS_Orders::Insert::Error occured.", ex);
			}
			finally
			{
				if(_mainConnectionIsCreatedLocal)
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
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "dbo.[sp_CMS_Orders_Update]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;

			// Use base class' connection object
			cmdToExecute.Connection = _mainConnection;
			try
			{
				cmdToExecute.Parameters.Add(new SqlParameter("@guidOrderID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _orderID));
				cmdToExecute.Parameters.Add(new SqlParameter("@guidUserID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _userID));
				cmdToExecute.Parameters.Add(new SqlParameter("@guidSaleID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _saleID));
				cmdToExecute.Parameters.Add(new SqlParameter("@guidPaymentID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _paymentID));
				cmdToExecute.Parameters.Add(new SqlParameter("@sOrderCode", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _orderCode));
				cmdToExecute.Parameters.Add(new SqlParameter("@sFullName", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _fullName));
				cmdToExecute.Parameters.Add(new SqlParameter("@sAddress", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _address));
				cmdToExecute.Parameters.Add(new SqlParameter("@sEmail", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _email));
				cmdToExecute.Parameters.Add(new SqlParameter("@sPhone", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _phone));
				cmdToExecute.Parameters.Add(new SqlParameter("@sRequest", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _request));
				cmdToExecute.Parameters.Add(new SqlParameter("@sComment", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _comment));
				cmdToExecute.Parameters.Add(new SqlParameter("@dcTotalPrice", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 18, 2, "", DataRowVersion.Proposed, _totalPrice));
				cmdToExecute.Parameters.Add(new SqlParameter("@iTax", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _tax));
				cmdToExecute.Parameters.Add(new SqlParameter("@dcTransportFee", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 18, 2, "", DataRowVersion.Proposed, _transportFee));
				cmdToExecute.Parameters.Add(new SqlParameter("@dcDiscount", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 18, 2, "", DataRowVersion.Proposed, _discount));
				cmdToExecute.Parameters.Add(new SqlParameter("@sVoucher", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _voucher));
				cmdToExecute.Parameters.Add(new SqlParameter("@iStatus", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _status));
				cmdToExecute.Parameters.Add(new SqlParameter("@daCreated", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _created));
				cmdToExecute.Parameters.Add(new SqlParameter("@daOrderSend", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _orderSend));
                cmdToExecute.Parameters.Add(new SqlParameter("@dcPrePayment", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 18, 2, "", DataRowVersion.Proposed, PrePayment));
                //cmdToExecute.Parameters.Add(new SqlParameter("@iDistrict", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _status));
                //cmdToExecute.Parameters.Add(new SqlParameter("@iProvince", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _status));

				if(_mainConnectionIsCreatedLocal)
				{
					// Open connection.
					_mainConnection.Open();
				}
				else
				{
					if(_mainConnectionProvider.IsTransactionPending)
					{
						cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
					}
				}

				// Execute query.
				cmdToExecute.ExecuteNonQuery();
				return true;
			}
			catch(Exception ex)
			{
				// some error occured. Bubble it to caller and encapsulate Exception object
				throw new Exception("clsCMS_Orders::Update::Error occured.", ex);
			}
			finally
			{
				if(_mainConnectionIsCreatedLocal)
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
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "dbo.[sp_CMS_Orders_Delete]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;

			// Use base class' connection object
			cmdToExecute.Connection = _mainConnection;

			try
			{
				cmdToExecute.Parameters.Add(new SqlParameter("@guidOrderID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _orderID));

				if(_mainConnectionIsCreatedLocal)
				{
					// Open connection.
					_mainConnection.Open();
				}
				else
				{
					if(_mainConnectionProvider.IsTransactionPending)
					{
						cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
					}
				}

				// Execute query.
				cmdToExecute.ExecuteNonQuery();
				return true;
			}
			catch(Exception ex)
			{
				// some error occured. Bubble it to caller and encapsulate Exception object
				throw new Exception("clsCMS_Orders::Delete::Error occured.", ex);
			}
			finally
			{
				if(_mainConnectionIsCreatedLocal)
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
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "dbo.[sp_CMS_Orders_SelectOne]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			DataTable toReturn = new DataTable("CMS_Orders");
			SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

			// Use base class' connection object
			cmdToExecute.Connection = _mainConnection;

			try
			{
				cmdToExecute.Parameters.Add(new SqlParameter("@guidOrderID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _orderID));

				if(_mainConnectionIsCreatedLocal)
				{
					// Open connection.
					_mainConnection.Open();
				}
				else
				{
					if(_mainConnectionProvider.IsTransactionPending)
					{
						cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
					}
				}

				// Execute query.
				adapter.Fill(toReturn);
				return toReturn;
			}
			catch(Exception ex)
			{
				// some error occured. Bubble it to caller and encapsulate Exception object
				throw new Exception("clsCMS_Orders::SelectOne::Error occured.", ex);
			}
			finally
			{
				if(_mainConnectionIsCreatedLocal)
				{
					// Close connection.
					_mainConnection.Close();
				}
				cmdToExecute.Dispose();
				adapter.Dispose();
			}
		}


		/// <summary>
		/// Purpose: Select method for a unique field. This method will Select one row from the database, based on the unique field 'ID'
		/// </summary>
		/// <returns>DataTable object if succeeded, otherwise an Exception is thrown. </returns>
		/// <remarks>
		/// Will fill all properties corresponding with a field in the table with the value of the row selected.
		/// </remarks>
		public DataTable SelectOneWIDLogic()
		{
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "dbo.[sp_CMS_Orders_SelectOneWIDLogic]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			DataTable toReturn = new DataTable("CMS_Orders");
			SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

			// Use base class' connection object
			cmdToExecute.Connection = _mainConnection;

			try
			{
				cmdToExecute.Parameters.Add(new SqlParameter("@lID", SqlDbType.BigInt, 8, ParameterDirection.Input, false, 19, 0, "", DataRowVersion.Proposed, _iD));

				if(_mainConnectionIsCreatedLocal)
				{
					// Open connection.
					_mainConnection.Open();
				}
				else
				{
					if(_mainConnectionProvider.IsTransactionPending)
					{
						cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
					}
				}

				// Execute query.
				adapter.Fill(toReturn);
				return toReturn;
			}
			catch(Exception ex)
			{
				// some error occured. Bubble it to caller and encapsulate Exception object
				throw new Exception("clsCMS_Orders::SelectOneWIDLogic::Error occured.", ex);
			}
			finally
			{
				if(_mainConnectionIsCreatedLocal)
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
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "dbo.[sp_CMS_Orders_SelectAll]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			DataTable toReturn = new DataTable("CMS_Orders");
			SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

			// Use base class' connection object
			cmdToExecute.Connection = _mainConnection;

			try
			{

				if(_mainConnectionIsCreatedLocal)
				{
					// Open connection.
					_mainConnection.Open();
				}
				else
				{
					if(_mainConnectionProvider.IsTransactionPending)
					{
						cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
					}
				}

				// Execute query.
				adapter.Fill(toReturn);
				return toReturn;
			}
			catch(Exception ex)
			{
				// some error occured. Bubble it to caller and encapsulate Exception object
				throw new Exception("clsCMS_Orders::SelectAll::Error occured.", ex);
			}
			finally
			{
				if(_mainConnectionIsCreatedLocal)
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

        private SqlGuid _saleOrderID = SqlGuid.Null;
        public SqlGuid SaleOrderID
        {
            get
            {
                return _saleOrderID;
            }
            set
            {
                _saleOrderID = value;
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

        private SqlString _phone1 = SqlString.Null;
        public SqlString Phone1
        {
            get
            {
                return _phone1;
            }
            set
            {
                _phone1 = value;
            }
        }

        private SqlString _phone2 = SqlString.Null;
        public SqlString Phone2
        {
            get
            {
                return _phone2;
            }
            set
            {
                _phone2 = value;
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

        private SqlDecimal _prePayment = SqlDecimal.Null;
        public SqlDecimal PrePayment
        {
            get
            {
                return _prePayment;
            }
            set
            {
                _prePayment = value;
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
        private SqlString _CompleteDate = SqlString.Null;

        public SqlString CompleteDate
        {
            get { return _CompleteDate; }
            set { _CompleteDate = value; }
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

        private SqlInt32 _oType = SqlInt32.Null;

        public SqlInt32 OType
        {
            get { return _oType; }
            set { _oType = value; }
        }

        private SqlGuid _cusClassID = SqlGuid.Null;

        public SqlGuid CusClassID
        {
            get { return _cusClassID; }
            set { _cusClassID = value; }
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
        
        private SqlInt32 _pageNumber = SqlInt32.Null;
        public SqlInt32 PageNumber
        {
            get
            {
                return _pageNumber;
            }
            set
            {
                _pageNumber = value;
            }
        }
        private SqlString _bookNumber = SqlString.Null;

        public SqlString BookNumber
        {
            get { return _bookNumber; }
            set { _bookNumber = value; }
        }

        private SqlInt32 _order0 = SqlInt32.Null;
        public SqlInt32 Order_0
        {
            get
            {
                return _order0;
            }
            set
            {
                _order0 = value;
            }
        }

        private SqlInt32 _order1 = SqlInt32.Null;
        public SqlInt32 Order_1
        {
            get
            {
                return _order1;
            }
            set
            {
                _order1 = value;
            }
        }

        private SqlInt32 _order2 = SqlInt32.Null;
        public SqlInt32 Order_2
        {
            get
            {
                return _order2;
            }
            set
            {
                _order2 = value;
            }
        }

        private SqlInt32 _order3 = SqlInt32.Null;
        public SqlInt32 Order_3
        {
            get
            {
                return _order3;
            }
            set
            {
                _order3 = value;
            }
        }

        private SqlInt32 _order4 = SqlInt32.Null;
        public SqlInt32 Order_4
        {
            get
            {
                return _order4;
            }
            set
            {
                _order4 = value;
            }
        }

        private SqlInt32 _order5 = SqlInt32.Null;
        public SqlInt32 Order_5
        {
            get
            {
                return _order5;
            }
            set
            {
                _order5 = value;
            }
        }

        private SqlInt32 _order6 = SqlInt32.Null;
        public SqlInt32 Order_6
        {
            get
            {
                return _order6;
            }
            set
            {
                _order6 = value;
            }
        }

        private SqlInt32 _isDatachanged = 0;
        public SqlInt32 IsDatachanged
        {
            get
            {
                return _isDatachanged;
            }
            set
            {
                _isDatachanged = value;
            }
        }
		#endregion
	}
}
