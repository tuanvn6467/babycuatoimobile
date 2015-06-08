using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;

namespace iGoo.Classes
{
    /// <summary>
    /// Dang Ngoc Hung - hungdn@ptit.edu.vn
    /// Purpose: Data Access class for the table 'CMS_Inventory'.
    /// </summary>
    public class clsCMS_Inventory : clsDBInteractionBase
    {
        /// <summary>
		/// Purpose: Class constructor.
		/// </summary>
        public clsCMS_Inventory()
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

        private SqlGuid _InventoryID = SqlGuid.Null;

        public SqlGuid InventoryID
        {
            get { return _InventoryID; }
            set { _InventoryID = value; }
        }

        private SqlGuid _userID = SqlGuid.Null;

        public SqlGuid UserID
        {
            get { return _userID; }
            set { _userID = value; }
        }

        private SqlString _InventoryName = SqlString.Null;

        public SqlString InventoryName
        {
            get { return _InventoryName; }
            set { _InventoryName = value; }
        }
        private SqlString _InventoryDeputy = SqlString.Null;

        public SqlString InventoryDeputy
        {
            get { return _InventoryDeputy; }
            set { _InventoryDeputy = value; }
        }
        private SqlString _InventoryDesc = SqlString.Null;

        public SqlString InventoryDesc
        {
            get { return _InventoryDesc; }
            set { _InventoryDesc = value; }
        }


        private SqlInt32 _Status = SqlInt32.Null;

        public SqlInt32 Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        
        private SqlString _CityArr = SqlString.Null;

        public SqlString CityArr
        {
            get { return _CityArr; }
            set { _CityArr = value; }
        }

        private SqlString _InventoryCode = SqlString.Null;

        public SqlString InventoryCode
        {
            get { return _InventoryCode; }
            set { _InventoryCode = value; }
        }
        private SqlString _InventoryLocation = SqlString.Null;

        public SqlString InventoryLocation
        {
            get { return _InventoryLocation; }
            set { _InventoryLocation = value; }
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
            cmdToExecute.CommandText = "dbo.[sp_CMS_Inventory_Insert]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@guidInventoryID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _InventoryID));
                cmdToExecute.Parameters.Add(new SqlParameter("@sInventoryName", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _InventoryName));
                cmdToExecute.Parameters.Add(new SqlParameter("@sInventoryDeputy", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _InventoryDeputy));
                cmdToExecute.Parameters.Add(new SqlParameter("@sInventoryDesc", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _InventoryDesc));
                cmdToExecute.Parameters.Add(new SqlParameter("@iStatus", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _Status));
                cmdToExecute.Parameters.Add(new SqlParameter("@sCityArr", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _CityArr));
                cmdToExecute.Parameters.Add(new SqlParameter("@sInventoryCode", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _InventoryCode));
                cmdToExecute.Parameters.Add(new SqlParameter("@sInventoryLocation", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _InventoryLocation));
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
                throw new Exception("clsCMS_Inventory::Insert::Error occured.", ex);
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
            cmdToExecute.CommandText = "dbo.[sp_CMS_Inventory_Update]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@guidInventoryID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _InventoryID));
                cmdToExecute.Parameters.Add(new SqlParameter("@sInventoryName", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _InventoryName));
                cmdToExecute.Parameters.Add(new SqlParameter("@sInventoryDeputy", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _InventoryDeputy));
                cmdToExecute.Parameters.Add(new SqlParameter("@sInventoryDesc", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _InventoryDesc));
                cmdToExecute.Parameters.Add(new SqlParameter("@iStatus", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _Status));
                cmdToExecute.Parameters.Add(new SqlParameter("@sCityArr", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _CityArr));
                cmdToExecute.Parameters.Add(new SqlParameter("@sInventoryCode", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _InventoryCode));
                cmdToExecute.Parameters.Add(new SqlParameter("@sInventoryLocation", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _InventoryLocation));
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
                throw new Exception("clsCMS_Inventory::Update::Error occured.", ex);
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
            cmdToExecute.CommandText = "dbo.[sp_CMS_Inventory_Delete]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@guidInventoryID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _InventoryID));

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
                throw new Exception("clsCMS_Inventory::Delete::Error occured.", ex);
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
            cmdToExecute.CommandText = "dbo.[sp_CMS_Inventory_SelectOne]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("CMS_Inventory");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@guidInventoryID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _InventoryID));

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
                throw new Exception("clsCMS_Products::SelectOne::Error occured.", ex);
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
            cmdToExecute.CommandText = "dbo.[sp_CMS_Inventory_SelectAll]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("CMS_Inventory");
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
                throw new Exception("clsCMS_Inventory::SelectAll::Error occured.", ex);
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