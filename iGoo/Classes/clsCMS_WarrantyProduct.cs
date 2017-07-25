using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;

namespace iGoo.Classes
{
	/// <summary>
    /// Loc Ngoc Son - Loc.Ngoc.Son@gmail.com
    /// Purpose: Data Access class for the table 'clsCMS_WarrantyProduct'.
	/// </summary>
	public class clsCMS_WarrantyProduct : clsDBInteractionBase
	{
		/// <summary>
		/// Purpose: Class constructor.
		/// </summary>
        public clsCMS_WarrantyProduct()
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
            cmdToExecute.CommandText = "dbo.[sp_CMS_WarrantyProductInsert]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;

			// Use base class' connection object
			cmdToExecute.Connection = _mainConnection;

			try
			{
				cmdToExecute.Parameters.Add(new SqlParameter("@ID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _id));
                cmdToExecute.Parameters.Add(new SqlParameter("@WarrantyID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _warrantyID));
                cmdToExecute.Parameters.Add(new SqlParameter("@ProductID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _productID));
				cmdToExecute.Parameters.Add(new SqlParameter("@ProductName", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _productName));
                cmdToExecute.Parameters.Add(new SqlParameter("@PurchaseDate", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _purchaseDate));
                cmdToExecute.Parameters.Add(new SqlParameter("@WarrantyDate", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _warrantyDate));
                cmdToExecute.Parameters.Add(new SqlParameter("@ReturnDate", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _returnDate));
                cmdToExecute.Parameters.Add(new SqlParameter("@WarrantyStatus", SqlDbType.NVarChar, 250, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _warrantyStatus));
                cmdToExecute.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar, 250, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _description));
                cmdToExecute.Parameters.Add(new SqlParameter("@WarrantyUserID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _warrantyUserID));
                cmdToExecute.Parameters.Add(new SqlParameter("@FixerID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _fixerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@Price", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 18, 2, "", DataRowVersion.Proposed, _price));
                cmdToExecute.Parameters.Add(new SqlParameter("@Quantity", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _quantity));
                if (_mainConnectionIsCreatedLocal)
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
				//_id = (SqlInt64)cmdToExecute.Parameters["@lID"].Value;
				return true;
			}
			catch(Exception ex)
			{
				// some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("clsCMS_WarrantyProduct::Insert::Error occured.", ex);
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
            cmdToExecute.CommandText = "dbo.[sp_CMS_WarrantyProductUpdate]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;

			// Use base class' connection object
			cmdToExecute.Connection = _mainConnection;
			try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@ID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _id));
                cmdToExecute.Parameters.Add(new SqlParameter("@WarrantyID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _warrantyID));
                cmdToExecute.Parameters.Add(new SqlParameter("@ProductID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _productID));
                cmdToExecute.Parameters.Add(new SqlParameter("@ProductName", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _productName));
                cmdToExecute.Parameters.Add(new SqlParameter("@PurchaseDate", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _purchaseDate));
                cmdToExecute.Parameters.Add(new SqlParameter("@WarrantyDate", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _warrantyDate));
                cmdToExecute.Parameters.Add(new SqlParameter("@ReturnDate", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _returnDate));
                cmdToExecute.Parameters.Add(new SqlParameter("@WarrantyStatus", SqlDbType.NVarChar, 250, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _warrantyStatus));
                cmdToExecute.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar, 250, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _description));
                cmdToExecute.Parameters.Add(new SqlParameter("@WarrantyUserID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _warrantyUserID));
                cmdToExecute.Parameters.Add(new SqlParameter("@FixerID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _fixerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@Price", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 18, 2, "", DataRowVersion.Proposed, _price));
                cmdToExecute.Parameters.Add(new SqlParameter("@Quantity", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _quantity));
                if (_mainConnectionIsCreatedLocal)
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
                throw new Exception("clsCMS_WarrantyProduct::Update::Error occured.", ex);
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
            cmdToExecute.CommandText = "dbo.[sp_CMS_WarrantyProductDelete]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;

			// Use base class' connection object
			cmdToExecute.Connection = _mainConnection;

			try
			{
				cmdToExecute.Parameters.Add(new SqlParameter("@ID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _id));

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
                throw new Exception("clsCMS_WarrantyProduct::Delete::Error occured.", ex);
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
            cmdToExecute.CommandText = "dbo.[sp_CMS_WarrantyProductSelect]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("CMS_WarrantyProduct");
			SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

			// Use base class' connection object
			cmdToExecute.Connection = _mainConnection;

			try
			{
				cmdToExecute.Parameters.Add(new SqlParameter("@ID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _id));

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
                throw new Exception("clsCMS_WarrantyProduct::SelectOne::Error occured.", ex);
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
		public DataTable SelectByWarrantyId()
		{
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[sp_CMS_WarrantyProductSelectByWarrantyId]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("CMS_WarrantyProduct");
			SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

			// Use base class' connection object
			cmdToExecute.Connection = _mainConnection;

			try
			{
                cmdToExecute.Parameters.Add(new SqlParameter("@WarrantyId", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _warrantyID));
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
				throw new Exception("clsCMS_VAS::SelectAll::Error occured.", ex);
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

        private SqlGuid _id = SqlGuid.Null;

	    public SqlGuid ID
	    {
	        get { return _id; } 
            set { _id = value; }
	    }

        private SqlGuid _warrantyID = SqlGuid.Null;
        public SqlGuid WarrantyID { get { return _warrantyID; } set { _warrantyID = value; } }  

        private SqlGuid _productID = SqlGuid.Null;
        public SqlGuid ProductID { get { return _productID; } set { _productID = value; } }  

        private SqlString _productName = SqlString.Null;
        public SqlString ProductName { get { return _productName; } set { _productName = value; } }  

        private SqlDateTime _purchaseDate = SqlDateTime.Null;
        public SqlDateTime PurchaseDate { get { return _purchaseDate; } set { _purchaseDate = value; } }  

        private SqlDateTime _warrantyDate = SqlDateTime.Null;
        public SqlDateTime WarrantyDate { get { return _warrantyDate; } set { _warrantyDate = value; } }

        private SqlString _warrantyStatus = SqlString.Null;
        public SqlString WarrantyStatus { get { return _warrantyStatus; } set { _warrantyStatus = value; } }

        private SqlString _description = SqlString.Null;
        public SqlString Description { get { return _description; } set { _description = value; } }

        private SqlGuid _warrantyUserID = SqlGuid.Null;
        public SqlGuid WarrantyUserID { get { return _warrantyUserID; } set { _warrantyUserID = value; } }

        private SqlGuid _fixerID = SqlGuid.Null;
        public SqlGuid FixerID { get { return _fixerID; } set { _fixerID = value; } }

        private SqlDateTime _returnDate = SqlDateTime.Null;
        public SqlDateTime ReturnDate { get { return _returnDate; } set { _returnDate = value; } }

        private SqlDecimal _price = SqlDecimal.Null;
        public SqlDecimal Price { get { return _price; } set { _price = value; } }

        private SqlInt32 _quantity = SqlInt32.Null;
        public SqlInt32 Quantity { get { return _quantity; } set { _quantity = value; } }
		#endregion
	}
}
