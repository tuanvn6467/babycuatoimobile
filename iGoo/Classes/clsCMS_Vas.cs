using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;

namespace iGoo.Classes
{
	/// <summary>
    /// Loc Ngoc Son - Loc.Ngoc.Son@gmail.com
	/// Purpose: Data Access class for the table 'CMS_Vas'.
	/// </summary>
	public class clsCMS_Vas1 : clsDBInteractionBase
	{
		/// <summary>
		/// Purpose: Class constructor.
		/// </summary>
		public clsCMS_Vas1()
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
			cmdToExecute.CommandText = "dbo.[sp_CMS_VAS_Insert]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;

			// Use base class' connection object
			cmdToExecute.Connection = _mainConnection;

			try
			{
				cmdToExecute.Parameters.Add(new SqlParameter("@Id", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _id));
				cmdToExecute.Parameters.Add(new SqlParameter("@Code", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _code));
				cmdToExecute.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 150, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _name));
				cmdToExecute.Parameters.Add(new SqlParameter("@Price", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 18, 2, "", DataRowVersion.Proposed, _price));
                cmdToExecute.Parameters.Add(new SqlParameter("@Status", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _status));
                cmdToExecute.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar, 250, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _description));

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
				//_id = (SqlInt64)cmdToExecute.Parameters["@lID"].Value;
				return true;
			}
			catch(Exception ex)
			{
				// some error occured. Bubble it to caller and encapsulate Exception object
				throw new Exception("clsCMS_VAS::Insert::Error occured.", ex);
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
			cmdToExecute.CommandText = "dbo.[sp_CMS_VAS_Update]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;

			// Use base class' connection object
			cmdToExecute.Connection = _mainConnection;
			try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@Id", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _id));
                cmdToExecute.Parameters.Add(new SqlParameter("@Code", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _code));
                cmdToExecute.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 150, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _name));
                cmdToExecute.Parameters.Add(new SqlParameter("@Price", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 18, 2, "", DataRowVersion.Proposed, _price));
                cmdToExecute.Parameters.Add(new SqlParameter("@Status", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _status));
                cmdToExecute.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar, 250, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _description));
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
				throw new Exception("clsCMS_VAS::Update::Error occured.", ex);
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
			cmdToExecute.CommandText = "dbo.[sp_CMS_VAS_Delete]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;

			// Use base class' connection object
			cmdToExecute.Connection = _mainConnection;

			try
			{
				cmdToExecute.Parameters.Add(new SqlParameter("@Id", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _id));

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
				throw new Exception("clsCMS_VAS::Delete::Error occured.", ex);
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
			cmdToExecute.CommandText = "dbo.[sp_CMS_VAS_SelectOne]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			DataTable toReturn = new DataTable("CMS_VAS");
			SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

			// Use base class' connection object
			cmdToExecute.Connection = _mainConnection;

			try
			{
				cmdToExecute.Parameters.Add(new SqlParameter("@Id", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _id));

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
				throw new Exception("clsCMS_VAS::SelectOne::Error occured.", ex);
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
			cmdToExecute.CommandText = "dbo.[sp_CMS_VAS_SelectAll]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			DataTable toReturn = new DataTable("CMS_VAS");
			SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

			// Use base class' connection object
			cmdToExecute.Connection = _mainConnection;

			try
			{
                cmdToExecute.Parameters.Add(new SqlParameter("@PageIndex", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, PageIndex));
                cmdToExecute.Parameters.Add(new SqlParameter("@PageSize", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, PageSize));
                cmdToExecute.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, Name));
                cmdToExecute.Parameters.Add(new SqlParameter("@Status", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, Status));
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
			get
			{
				return _id;
			}
			set
			{
				_id = value;
			}
		}


		private SqlString _code = SqlString.Null;
		public SqlString Code
		{
			get
			{
                return _code;
			}
			set
			{
                _code = value;
			}
		}


		private SqlString _name = SqlString.Null;
		public SqlString Name
		{
			get
			{
				return _name;
			}
			set
			{
				_name = value;
			}
		}

        private SqlDecimal _price = SqlDecimal.Null;
        public SqlDecimal Price
		{
			get
			{
				return _price;
			}
			set
			{
				_price = value;
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

        private SqlString _description = SqlString.Null;
        public SqlString Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }

		#endregion
	}
}
