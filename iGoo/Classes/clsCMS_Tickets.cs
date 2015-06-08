using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;

namespace iGoo.Classes
{
	/// <summary>
    /// Loc Ngoc Son - loc.ngoc.son@gmail.com
    /// Purpose: Data Access class for the table 'clsCMS_Tickets'.
	/// </summary>
	public class clsCMS_Tickets : clsDBInteractionBase
	{
		/// <summary>
		/// Purpose: Class constructor.
		/// </summary>
        public clsCMS_Tickets()
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
            cmdToExecute.CommandText = "dbo.[sp_CMS_Tickets_Insert]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;

			// Use base class' connection object
			cmdToExecute.Connection = _mainConnection;

			try
			{
                cmdToExecute.Parameters.Add(new SqlParameter("@guidTicketID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _orderID));
                cmdToExecute.Parameters.Add(new SqlParameter("@guidUserID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _userID));
                cmdToExecute.Parameters.Add(new SqlParameter("@guidShipperID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _shipperId));
                cmdToExecute.Parameters.Add(new SqlParameter("@guidOrderID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _orderID));
                cmdToExecute.Parameters.Add(new SqlParameter("@iTicketType", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _ticketType));
                cmdToExecute.Parameters.Add(new SqlParameter("@daCreateDate", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _createDate));
                cmdToExecute.Parameters.Add(new SqlParameter("@daCompleteDate", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _completeDate));
                cmdToExecute.Parameters.Add(new SqlParameter("@sComment", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 2, "", DataRowVersion.Proposed, _comment));
                cmdToExecute.Parameters.Add(new SqlParameter("@iDistrictID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _districtID));
                cmdToExecute.Parameters.Add(new SqlParameter("@iProvinceID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _provinceID));
                
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
                throw new Exception("clsCMS_Tickets::Insert::Error occured.", ex);
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
            cmdToExecute.CommandText = "dbo.[sp_CMS_Tickets_Update]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;

			// Use base class' connection object
			cmdToExecute.Connection = _mainConnection;
			try
			{
                cmdToExecute.Parameters.Add(new SqlParameter("@guidUserID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _userID));
                cmdToExecute.Parameters.Add(new SqlParameter("@guidShipperID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _shipperId));
                cmdToExecute.Parameters.Add(new SqlParameter("@guidOrderID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _orderID));
                cmdToExecute.Parameters.Add(new SqlParameter("@TicketType", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _ticketType));
                cmdToExecute.Parameters.Add(new SqlParameter("@daCreateDate", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _createDate));
                cmdToExecute.Parameters.Add(new SqlParameter("@daCompleteDate", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _completeDate));
                cmdToExecute.Parameters.Add(new SqlParameter("@sComment", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _comment));
                cmdToExecute.Parameters.Add(new SqlParameter("@iDistrictID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _districtID));
                cmdToExecute.Parameters.Add(new SqlParameter("@iProvinceID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _provinceID));
                
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
                throw new Exception("clsCMS_Tickets::Update::Error occured.", ex);
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
            cmdToExecute.CommandText = "dbo.[sp_CMS_Tickets_Delete]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;

			// Use base class' connection object
			cmdToExecute.Connection = _mainConnection;

			try
			{
                cmdToExecute.Parameters.Add(new SqlParameter("@guidTicketID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _ticketId));

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
                throw new Exception("clsCMS_Tickets::Delete::Error occured.", ex);
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
            cmdToExecute.CommandText = "dbo.[sp_CMS_Tickets_SelectOne]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("clsCMS_Tickets");
			SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

			// Use base class' connection object
			cmdToExecute.Connection = _mainConnection;

			try
			{
                cmdToExecute.Parameters.Add(new SqlParameter("@guidTicketID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _ticketId));

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
                throw new Exception("clsCMS_Tickets::SelectOne::Error occured.", ex);
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
            cmdToExecute.CommandText = "dbo.[sp_CMS_Tickets_SelectAll]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("clsCMS_Tickets");
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
                throw new Exception("clsCMS_Tickets::SelectAll::Error occured.", ex);
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

	    private SqlGuid _ticketId = SqlGuid.Null;
        public SqlGuid TicketID
	    {
	        get
	        {
	            return _ticketId;
	        }
	        set
	        {
	            _ticketId = value;
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

        private SqlGuid _shipperId = SqlGuid.Null;

	    public SqlGuid ShipperID
	    {
	        get
	        {
                return _shipperId;
	        }
	        set
	        {
                _shipperId =value;
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

	    private SqlInt32 _ticketType = SqlInt32.Null;

        public SqlInt32 TicketType
	    {
            get
            {
                return _ticketType;
            }
            set
            {
                _ticketType = value;
            }
	    }

	    private SqlDateTime _createDate = SqlDateTime.Null;

	    public SqlDateTime CreateDate
	    {
	        get
	        {
	            return _createDate;
	        }
	        set
	        {
                _createDate =value;
	        }
	    }

	    private SqlDateTime _completeDate = SqlDateTime.Null;

	    public SqlDateTime CompleteDate
	    {
	        get
	        {
                return _completeDate;
	        }
	        set
	        {
                _completeDate = value;
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

	    private SqlInt32 _districtID = SqlInt32.Null;

	    public SqlInt32 DistrictID
	    {
	        get
	        {
                return _districtID;
	        }
	        set
	        {
                _districtID = value;
	        }
	    }

	    private SqlInt32 _provinceID = SqlInt32.Null;

	    public SqlInt32 ProvinceID
	    {
	        get
	        {
                return _provinceID;
	        }
	        set
	        {
                _provinceID = value;
	        }
	    }
		#endregion
	}
}
