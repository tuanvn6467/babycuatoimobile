using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;

namespace iGoo.Classes
{
    
    public class clsCMS_Shippers : clsDBInteractionBase 
    {
        /// <summary>
		/// Purpose: Class constructor.
		/// </summary>
        public clsCMS_Shippers()
		{
			// Nothing for now.
		}

        #region "Class Property Declarations"
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

        private SqlGuid _ShipperID = SqlGuid.Null;

        public SqlGuid ShipperID
        {
            get { return _ShipperID; }
            set { _ShipperID = value; }
        }

        private SqlString _ShipperCode = SqlString.Null;

        public SqlString ShipperCode
        {
            get { return _ShipperCode; }
            set { _ShipperCode = value; }
        }        
        
        private SqlString _FullName = SqlString.Null;

        public SqlString FullName
        {
            get { return _FullName; }
            set { _FullName = value; }
        }

        private SqlString _Phone = SqlString.Null;

        public SqlString Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }

        private SqlInt32 _Gender = SqlInt32.Null;

        public SqlInt32 Gender
        {
            get { return _Gender; }
            set { _Gender = value; }
        }

        private SqlString _Address = SqlString.Null;

        public SqlString Address
        {
            get { return _Address; }
            set { _Address = value; }
        }

        private SqlString _Email = SqlString.Null;

        public SqlString Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        private SqlString _Info = SqlString.Null;

        public SqlString Info
        {
            get { return _Info; }
            set { _Info = value; }
        }

        private SqlInt32 _Status = SqlInt32.Null;

        public SqlInt32 Status
        {
            get { return _Status; }
            set { _Status = value; }
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



        #endregion

        /// <summary>
        /// Purpose: Insert method. This method will insert one new row into the database.
        /// </summary>
        /// <returns>True if succeeded, otherwise an Exception is thrown. </returns>
        /// <remarks>
        /// </remarks>
        public override bool Insert()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[sp_CMS_Shippers_Insert]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@guidShipperID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _ShipperID));
                cmdToExecute.Parameters.Add(new SqlParameter("@sShipperCode", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _ShipperCode));
                cmdToExecute.Parameters.Add(new SqlParameter("@sFullName", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _FullName));
                cmdToExecute.Parameters.Add(new SqlParameter("@sPhone", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _Phone));
                cmdToExecute.Parameters.Add(new SqlParameter("@iGender", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _Gender));
                cmdToExecute.Parameters.Add(new SqlParameter("@sAddress", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _Address));
                cmdToExecute.Parameters.Add(new SqlParameter("@sEmail", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _Email));
                cmdToExecute.Parameters.Add(new SqlParameter("@sInfo", SqlDbType.NVarChar, 500, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _Info));
                cmdToExecute.Parameters.Add(new SqlParameter("@iStatus", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _Status));
                cmdToExecute.Parameters.Add(new SqlParameter("@daCreated", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _created));
                              
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
                throw new Exception("clsCMS_Shippers::Insert::Error occured.", ex);
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
            cmdToExecute.CommandText = "dbo.[sp_CMS_Shippers_Update]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@guidShipperID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _ShipperID));
                cmdToExecute.Parameters.Add(new SqlParameter("@sShipperCode", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _ShipperCode));
                cmdToExecute.Parameters.Add(new SqlParameter("@sFullName", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _FullName));
                cmdToExecute.Parameters.Add(new SqlParameter("@sPhone", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _Phone));
                cmdToExecute.Parameters.Add(new SqlParameter("@iGender", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _Gender));
                cmdToExecute.Parameters.Add(new SqlParameter("@sAddress", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _Address));
                cmdToExecute.Parameters.Add(new SqlParameter("@sEmail", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _Email));
                cmdToExecute.Parameters.Add(new SqlParameter("@sInfo", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _Info));
                cmdToExecute.Parameters.Add(new SqlParameter("@iStatus", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _Status));
                cmdToExecute.Parameters.Add(new SqlParameter("@daCreated", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _created));
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
                throw new Exception("clsCMS_Shippers::Update::Error occured.", ex);
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
            cmdToExecute.CommandText = "dbo.[sp_CMS_Shippers_Delete]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@guidShipperID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _ShipperID));

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
                throw new Exception("clsCMS_Shippers::Delete::Error occured.", ex);
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
            cmdToExecute.CommandText = "dbo.[sp_CMS_Shippers_SelectOne]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("CMS_Shippers");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@guidShipperID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _ShipperID));

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
                throw new Exception("clsCMS_Shippers::SelectOne::Error occured.", ex);
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
            cmdToExecute.CommandText = "dbo.[sp_CMS_Shippers_SelectAll]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("CMS_Shippers");
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
                throw new Exception("clsCMS_Shippers::SelectAll::Error occured.", ex);
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