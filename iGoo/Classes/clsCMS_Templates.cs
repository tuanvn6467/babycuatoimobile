using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;

namespace iGoo.Classes
{
	/// <summary>
    /// Loc Ngoc Son - loc.ngoc.son@gmail.com
	/// Purpose: Data Access class for the table 'CMS_Templates'.
	/// </summary>
	public class clsCMS_Templates : clsDBInteractionBase
	{
		/// <summary>
		/// Purpose: Class constructor.
		/// </summary>
        public clsCMS_Templates()
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
            cmdToExecute.CommandText = "dbo.[sp_CMS_Templates_Insert]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;

			// Use base class' connection object
			cmdToExecute.Connection = _mainConnection;

			try
			{
                cmdToExecute.Parameters.Add(new SqlParameter("@guidTemplateID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _templateId));
				cmdToExecute.Parameters.Add(new SqlParameter("@sName", SqlDbType.NVarChar, 500, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _name));
				cmdToExecute.Parameters.Add(new SqlParameter("@sDes", SqlDbType.NVarChar, 500, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _des));

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
                throw new Exception("clsCMS_Templates::Insert::Error occured.", ex);
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
            cmdToExecute.CommandText = "dbo.[sp_CMS_Templates_Update]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;

			// Use base class' connection object
			cmdToExecute.Connection = _mainConnection;

			try
			{
                cmdToExecute.Parameters.Add(new SqlParameter("@guidTemplateID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _templateId));
                cmdToExecute.Parameters.Add(new SqlParameter("@sName", SqlDbType.NVarChar, 500, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _name));
                cmdToExecute.Parameters.Add(new SqlParameter("@sDes", SqlDbType.NVarChar, 500, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _des));
				cmdToExecute.Parameters.Add(new SqlParameter("@iStatus", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _status));

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
                throw new Exception("clsCMS_Templates::Update::Error occured.", ex);
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

        public bool Update_Status()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[sp_CMS_Templates_Update_Status]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@guidTemplateID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _templateId));

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
                throw new Exception("clsCMS_Templates::Update_Status::Error occured.", ex);
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
			SqlCommand	cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[sp_CMS_Templates_Delete]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;

			// Use base class' connection object
			cmdToExecute.Connection = _mainConnection;

			try
			{
                cmdToExecute.Parameters.Add(new SqlParameter("@guidTemplateID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _templateId));

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
                throw new Exception("clsCMS_Templates::Delete::Error occured.", ex);
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
            cmdToExecute.CommandText = "dbo.[sp_CMS_Templates_SelectOne]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			DataTable toReturn = new DataTable("CMS_Templates");
			SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

			// Use base class' connection object
			cmdToExecute.Connection = _mainConnection;

			try
			{
                cmdToExecute.Parameters.Add(new SqlParameter("@guidTemplateID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _templateId));

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
                throw new Exception("clsCMS_Templates::SelectOne::Error occured.", ex);
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
            cmdToExecute.CommandText = "dbo.[sp_CMS_Templates_SelectAll]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			DataTable toReturn = new DataTable("CMS_Templates");
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
				throw new Exception("clsCMS_Templates::SelectAll::Error occured.", ex);
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

        public DataTable SelectMenuTemplate()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[sp_CMS_Templates_SelectMenu]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("CMS_Templates");
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
                throw new Exception("clsCMS_Templates::SelectMenuCMS_Template::Error occured.", ex);
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

        public DataTable SelectActive()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[sp_CMS_Templates_GetActive]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("CMS_Templates");
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
                throw new Exception("clsCMS_Templates::SelectActive::Error occured.", ex);
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
		
		private SqlGuid _templateId = SqlGuid.Null;
        public SqlGuid TemplateId
		{
			get
			{
                return _templateId;
			}
			set
			{
                _templateId = value;
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


		private SqlString _des = SqlString.Null;
		public SqlString Des
		{
			get
			{
                return _des;
			}
			set
			{
                _des = value;
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
		#endregion
	}
}
