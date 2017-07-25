using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;

namespace iGoo.Classes
{
	/// <summary>
	/// Son Ngoc Loc - locngocson@gmail.com
    /// Purpose: Data Access class for the table 'CMS_InvProductPrice'.
	/// </summary>
	public class clsCMS_InvProductPrice : clsDBInteractionBase
	{
		/// <summary>
		/// Purpose: Class constructor.
		/// </summary>
        public clsCMS_InvProductPrice()
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
			cmdToExecute.CommandText = "dbo.[sp_CMS_InvProductPriceInsert]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;

			// Use base class' connection object
			cmdToExecute.Connection = _mainConnection;

			try
			{
                cmdToExecute.Parameters.Add(new SqlParameter("@ID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _id));
                cmdToExecute.Parameters.Add(new SqlParameter("@InventoryID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _inventoryID));
				cmdToExecute.Parameters.Add(new SqlParameter("@ProductID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _productID));
				cmdToExecute.Parameters.Add(new SqlParameter("@SaleTypeID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, Guid.Empty));
				cmdToExecute.Parameters.Add(new SqlParameter("@SalePrice", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 18, 2, "", DataRowVersion.Proposed, _salePrice));
                cmdToExecute.Parameters.Add(new SqlParameter("@DealerPrice", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 18, 2, "", DataRowVersion.Proposed, _dealerPrice));
                cmdToExecute.Parameters.Add(new SqlParameter("@UpdatedDate", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DateTime.Now));
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
				throw new Exception("clsCMS_InvProductPrice::Insert::Error occured.", ex);
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
			cmdToExecute.CommandText = "dbo.[sp_CMS_InvProductPriceUpdate]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;

			// Use base class' connection object
			cmdToExecute.Connection = _mainConnection;

			try
			{
                cmdToExecute.Parameters.Add(new SqlParameter("@ID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _id));
                cmdToExecute.Parameters.Add(new SqlParameter("@InventoryID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _inventoryID));
                cmdToExecute.Parameters.Add(new SqlParameter("@ProductID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _productID));
                cmdToExecute.Parameters.Add(new SqlParameter("@SaleTypeID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, Guid.Empty));
                cmdToExecute.Parameters.Add(new SqlParameter("@SalePrice", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 18, 2, "", DataRowVersion.Proposed, _salePrice));
                cmdToExecute.Parameters.Add(new SqlParameter("@DealerPrice", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 18, 2, "", DataRowVersion.Proposed, _dealerPrice));
                cmdToExecute.Parameters.Add(new SqlParameter("@UpdatedDate", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DateTime.Now));
                cmdToExecute.Parameters.Add(new SqlParameter("@iStatus", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _status));
                cmdToExecute.Parameters.Add(new SqlParameter("@iOrder", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _order));

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
				throw new Exception("clsCMS_InvProductPrice::Update::Error occured.", ex);
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
        /// 
        /// </summary>
        /// <returns></returns>
		public DataTable SelectAllPriceByProductId()
		{
			SqlCommand	cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[sp_CMS_InvProductPrice_SelectAllPriceByProductId]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("CMS_InvProductPrice");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

			// Use base class' connection object
			cmdToExecute.Connection = _mainConnection;

			try
			{
                cmdToExecute.Parameters.Add(new SqlParameter("@guidProductID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _productID));

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
				throw new Exception("clsCMS_InvProductPrice::Update::Error occured.", ex);
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
			cmdToExecute.CommandText = "dbo.[sp_CMS_InvProductPriceDelete]";
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
				throw new Exception("clsCMS_InvProductPrice::Delete::Error occured.", ex);
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
			get
			{
				return _id;
			}
			set
			{
				_id = value;
			}
		}

		private SqlGuid _productID = SqlGuid.Null;
		public SqlGuid ProductID
		{
			get
			{
				return _productID;
			}
			set
			{
				_productID = value;
			}
		}

        private SqlGuid _inventoryID = SqlGuid.Null;
        public SqlGuid InventoryID
        {
            get
            {
                return _inventoryID;
            }
            set
            {
                _inventoryID = value;
            }
        }

        private SqlGuid _saleTypeID = SqlGuid.Null;
        public SqlGuid SaleTypeID
        {
            get
            {
                return _saleTypeID;
            }
            set
            {
                _saleTypeID = value;
            }
        }

		private SqlDecimal _salePrice = SqlDecimal.Null;
		public SqlDecimal SalePrice
		{
			get
			{
				return _salePrice;
			}
			set
			{
				_salePrice = value;
			}
		}

        private SqlDecimal _dealerPrice = SqlDecimal.Null;
        public SqlDecimal DealerPrice
        {
            get
            {
                return _dealerPrice;
            }
            set
            {
                _dealerPrice = value;
            }
        }

        private SqlDateTime _updatedDate = SqlDateTime.Null;
        public SqlDateTime UpdatedDate
        {
            get
            {
                return _updatedDate;
            }
            set
            {
                _updatedDate = value;
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

        private SqlInt32 _order = SqlInt32.Null;
        public SqlInt32 Order
        {
            get
            {
                return _order;
            }
            set
            {
                _order = value;
            }
        }
		#endregion
	}
}
