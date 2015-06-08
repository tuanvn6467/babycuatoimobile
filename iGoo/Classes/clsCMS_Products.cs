using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;

namespace iGoo.Classes
{
	/// <summary>
	/// Nguyen Thanh Binh - Thanhbinh101287@gmail.com
	/// Purpose: Data Access class for the table 'CMS_Products'.
	/// </summary>
	public class clsCMS_Products : clsDBInteractionBase
	{
		/// <summary>
		/// Purpose: Class constructor.
		/// </summary>
		public clsCMS_Products()
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
			cmdToExecute.CommandText = "dbo.[sp_CMS_Products_Insert]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;

			// Use base class' connection object
			cmdToExecute.Connection = _mainConnection;

			try
			{
				cmdToExecute.Parameters.Add(new SqlParameter("@guidProductID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _productID));
				cmdToExecute.Parameters.Add(new SqlParameter("@guidCategoryID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _categoryID));
				cmdToExecute.Parameters.Add(new SqlParameter("@guidUserID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _userID));
				cmdToExecute.Parameters.Add(new SqlParameter("@sTitle", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _title));
				cmdToExecute.Parameters.Add(new SqlParameter("@sSEOName", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _sEOName));
				cmdToExecute.Parameters.Add(new SqlParameter("@sImage", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _image));
				cmdToExecute.Parameters.Add(new SqlParameter("@sBrief", SqlDbType.NVarChar, 500, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _brief));
				cmdToExecute.Parameters.Add(new SqlParameter("@sContent", SqlDbType.NVarChar, -1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _content));
				cmdToExecute.Parameters.Add(new SqlParameter("@sMetaTitle", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _metaTitle));
				cmdToExecute.Parameters.Add(new SqlParameter("@sMetaKeyword", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _metaKeyword));
				cmdToExecute.Parameters.Add(new SqlParameter("@sMetaDescription", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _metaDescription));
				cmdToExecute.Parameters.Add(new SqlParameter("@sTags", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _tags));
				cmdToExecute.Parameters.Add(new SqlParameter("@sTagsTitle", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _tagsTitle));
				cmdToExecute.Parameters.Add(new SqlParameter("@sRelated", SqlDbType.NVarChar, -1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _related));
				cmdToExecute.Parameters.Add(new SqlParameter("@sType", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _type));
				cmdToExecute.Parameters.Add(new SqlParameter("@iStatus", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _status));
				cmdToExecute.Parameters.Add(new SqlParameter("@iVisitor", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _visitor));
				cmdToExecute.Parameters.Add(new SqlParameter("@sSlideImage", SqlDbType.NVarChar, -1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _slideImage));
				cmdToExecute.Parameters.Add(new SqlParameter("@daCreated", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _created));
				cmdToExecute.Parameters.Add(new SqlParameter("@daModified", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _modified));
				cmdToExecute.Parameters.Add(new SqlParameter("@iTotalComment", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _totalComment));
				cmdToExecute.Parameters.Add(new SqlParameter("@sSKU", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _sKU));
				cmdToExecute.Parameters.Add(new SqlParameter("@iQuantity", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _quantity));
				cmdToExecute.Parameters.Add(new SqlParameter("@dcImportPrice", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 18, 2, "", DataRowVersion.Proposed, _importPrice));
				cmdToExecute.Parameters.Add(new SqlParameter("@dcRealPrice", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 18, 2, "", DataRowVersion.Proposed, _realPrice));
				cmdToExecute.Parameters.Add(new SqlParameter("@dcSalePrice", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 18, 2, "", DataRowVersion.Proposed, _salePrice));
				cmdToExecute.Parameters.Add(new SqlParameter("@iDiscountPercent", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _discountPercent));
				cmdToExecute.Parameters.Add(new SqlParameter("@sWeight", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _weight));
				cmdToExecute.Parameters.Add(new SqlParameter("@sModel", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _model));
				cmdToExecute.Parameters.Add(new SqlParameter("@sFilter", SqlDbType.VarChar, -1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _filter));
				cmdToExecute.Parameters.Add(new SqlParameter("@guidManufactureID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _manufactureID));
				cmdToExecute.Parameters.Add(new SqlParameter("@sProductAttribute", SqlDbType.NVarChar, -1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _productAttribute));
				cmdToExecute.Parameters.Add(new SqlParameter("@sPromotion", SqlDbType.NVarChar, -1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _promotion));
				cmdToExecute.Parameters.Add(new SqlParameter("@sParameter", SqlDbType.NVarChar, -1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _parameter));
				cmdToExecute.Parameters.Add(new SqlParameter("@sTransportFee", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _transportFee));
				cmdToExecute.Parameters.Add(new SqlParameter("@guidPollID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _pollID));
				cmdToExecute.Parameters.Add(new SqlParameter("@iOrder", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _order));

                cmdToExecute.Parameters.Add(new SqlParameter("@dcSalePriceDealer", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 18, 2, "", DataRowVersion.Proposed, _salePriceDealer));
                cmdToExecute.Parameters.Add(new SqlParameter("@dcSalePriceHCM", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 18, 2, "", DataRowVersion.Proposed, _salePriceHCM));
                cmdToExecute.Parameters.Add(new SqlParameter("@dcSalePriceDealerHCM", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 18, 2, "", DataRowVersion.Proposed, _salePriceDealerHCM));
                cmdToExecute.Parameters.Add(new SqlParameter("@sBarcode", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _productBarcode));
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
				throw new Exception("clsCMS_Products::Insert::Error occured.", ex);
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
			cmdToExecute.CommandText = "dbo.[sp_CMS_Products_Update]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;

			// Use base class' connection object
			cmdToExecute.Connection = _mainConnection;

			try
			{
				cmdToExecute.Parameters.Add(new SqlParameter("@guidProductID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _productID));
				cmdToExecute.Parameters.Add(new SqlParameter("@guidCategoryID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _categoryID));
				cmdToExecute.Parameters.Add(new SqlParameter("@guidUserID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _userID));
				cmdToExecute.Parameters.Add(new SqlParameter("@sTitle", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _title));
				cmdToExecute.Parameters.Add(new SqlParameter("@sSEOName", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _sEOName));
				cmdToExecute.Parameters.Add(new SqlParameter("@sImage", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _image));
				cmdToExecute.Parameters.Add(new SqlParameter("@sBrief", SqlDbType.NVarChar, 500, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _brief));
				cmdToExecute.Parameters.Add(new SqlParameter("@sContent", SqlDbType.NVarChar, -1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _content));
				cmdToExecute.Parameters.Add(new SqlParameter("@sMetaTitle", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _metaTitle));
				cmdToExecute.Parameters.Add(new SqlParameter("@sMetaKeyword", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _metaKeyword));
				cmdToExecute.Parameters.Add(new SqlParameter("@sMetaDescription", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _metaDescription));
				cmdToExecute.Parameters.Add(new SqlParameter("@sTags", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _tags));
				cmdToExecute.Parameters.Add(new SqlParameter("@sTagsTitle", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _tagsTitle));
				cmdToExecute.Parameters.Add(new SqlParameter("@sRelated", SqlDbType.NVarChar, -1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _related));
				cmdToExecute.Parameters.Add(new SqlParameter("@sType", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _type));
				cmdToExecute.Parameters.Add(new SqlParameter("@iStatus", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _status));
				cmdToExecute.Parameters.Add(new SqlParameter("@iVisitor", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _visitor));
				cmdToExecute.Parameters.Add(new SqlParameter("@sSlideImage", SqlDbType.NVarChar, -1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _slideImage));
				cmdToExecute.Parameters.Add(new SqlParameter("@daCreated", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _created));
				cmdToExecute.Parameters.Add(new SqlParameter("@daModified", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _modified));
				cmdToExecute.Parameters.Add(new SqlParameter("@iTotalComment", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _totalComment));
				cmdToExecute.Parameters.Add(new SqlParameter("@sSKU", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _sKU));
				cmdToExecute.Parameters.Add(new SqlParameter("@iQuantity", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _quantity));
				cmdToExecute.Parameters.Add(new SqlParameter("@dcSalePrice", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 18, 2, "", DataRowVersion.Proposed, _salePrice));
				cmdToExecute.Parameters.Add(new SqlParameter("@iDiscountPercent", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _discountPercent));
				cmdToExecute.Parameters.Add(new SqlParameter("@sWeight", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _weight));
				cmdToExecute.Parameters.Add(new SqlParameter("@sModel", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _model));
				cmdToExecute.Parameters.Add(new SqlParameter("@sFilter", SqlDbType.VarChar, -1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _filter));
				cmdToExecute.Parameters.Add(new SqlParameter("@guidManufactureID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _manufactureID));
				cmdToExecute.Parameters.Add(new SqlParameter("@sProductAttribute", SqlDbType.NVarChar, -1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _productAttribute));
				cmdToExecute.Parameters.Add(new SqlParameter("@sPromotion", SqlDbType.NVarChar, -1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _promotion));
				cmdToExecute.Parameters.Add(new SqlParameter("@sParameter", SqlDbType.NVarChar, -1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _parameter));
				cmdToExecute.Parameters.Add(new SqlParameter("@sTransportFee", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _transportFee));
				cmdToExecute.Parameters.Add(new SqlParameter("@guidPollID", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _pollID));
				cmdToExecute.Parameters.Add(new SqlParameter("@iOrder", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _order));

                cmdToExecute.Parameters.Add(new SqlParameter("@dcSalePriceDealer", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 18, 2, "", DataRowVersion.Proposed, _salePriceDealer));
                cmdToExecute.Parameters.Add(new SqlParameter("@dcSalePriceHCM", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 18, 2, "", DataRowVersion.Proposed, _salePriceHCM));
                cmdToExecute.Parameters.Add(new SqlParameter("@dcSalePriceDealerHCM", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 18, 2, "", DataRowVersion.Proposed, _salePriceDealerHCM));
                cmdToExecute.Parameters.Add(new SqlParameter("@dcSalePriceCN3", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 18, 2, "", DataRowVersion.Proposed, _salePriceCN3));
                cmdToExecute.Parameters.Add(new SqlParameter("@dcSalePriceDealerCN3", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 18, 2, "", DataRowVersion.Proposed, _salePriceDealerCN3));
                cmdToExecute.Parameters.Add(new SqlParameter("@sBarcode", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _productBarcode));

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
				throw new Exception("clsCMS_Products::Update::Error occured.", ex);
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
			cmdToExecute.CommandText = "dbo.[sp_CMS_Products_Delete]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;

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
				cmdToExecute.ExecuteNonQuery();
				return true;
			}
			catch(Exception ex)
			{
				// some error occured. Bubble it to caller and encapsulate Exception object
				throw new Exception("clsCMS_Products::Delete::Error occured.", ex);
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
			cmdToExecute.CommandText = "dbo.[sp_CMS_Products_SelectOne]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			DataTable toReturn = new DataTable("CMS_Products");
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
				throw new Exception("clsCMS_Products::SelectOne::Error occured.", ex);
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
			cmdToExecute.CommandText = "dbo.[sp_CMS_Products_SelectAll]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			DataTable toReturn = new DataTable("CMS_Products");
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
				throw new Exception("clsCMS_Products::SelectAll::Error occured.", ex);
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
        public bool UpdatePrice()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[sp_CMS_Products_UpdatePrice]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@sSKU", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _sKU));
                cmdToExecute.Parameters.Add(new SqlParameter("@dcSalePrice", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 18, 2, "", DataRowVersion.Proposed, _salePrice));
                cmdToExecute.Parameters.Add(new SqlParameter("@dcSalePriceDealer", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 18, 2, "", DataRowVersion.Proposed, _salePriceDealer));
                cmdToExecute.Parameters.Add(new SqlParameter("@dcSalePriceHCM", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 18, 2, "", DataRowVersion.Proposed, _salePriceHCM));
                cmdToExecute.Parameters.Add(new SqlParameter("@dcSalePriceDealerHCM", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 18, 2, "", DataRowVersion.Proposed, _salePriceDealerHCM));
                cmdToExecute.Parameters.Add(new SqlParameter("@dcSalePriceCN3", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 18, 2, "", DataRowVersion.Proposed, _salePriceCN3));
                cmdToExecute.Parameters.Add(new SqlParameter("@dcSalePriceDealerCN3", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 18, 2, "", DataRowVersion.Proposed, _salePriceDealerCN3));
                cmdToExecute.Parameters.Add(new SqlParameter("@iHN", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _hn));
                cmdToExecute.Parameters.Add(new SqlParameter("@iHN1", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _hn1));
                cmdToExecute.Parameters.Add(new SqlParameter("@iHCM", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _hcm));
                cmdToExecute.Parameters.Add(new SqlParameter("@iHCM1", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _hcm1));
                cmdToExecute.Parameters.Add(new SqlParameter("@iCN3", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _cn3));
                cmdToExecute.Parameters.Add(new SqlParameter("@iCN31", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _cn31));
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
                throw new Exception("clsCMS_Products::UpdatePrice::Error occured.", ex);
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

        public bool CheckProductCode()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[sp_CMS_Product_CheckProductCode]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("CMS_Products");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;
            bool bResult = false;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@sProductCode", SqlDbType.VarChar, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _sKU));

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
                if (toReturn.Rows.Count > 0)
                    bResult = true;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("clsCMS_Products::CheckProductCode::Error occured.", ex);
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
            return bResult;
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


		private SqlString _sEOName = SqlString.Null;
		public SqlString SEOName
		{
			get
			{
				return _sEOName;
			}
			set
			{
				_sEOName = value;
			}
		}


		private SqlString _image = SqlString.Null;
		public SqlString Image
		{
			get
			{
				return _image;
			}
			set
			{
				_image = value;
			}
		}


		private SqlString _brief = SqlString.Null;
		public SqlString Brief
		{
			get
			{
				return _brief;
			}
			set
			{
				_brief = value;
			}
		}


		private SqlString _content = SqlString.Null;
		public SqlString Content
		{
			get
			{
				return _content;
			}
			set
			{
				_content = value;
			}
		}


		private SqlString _metaTitle = SqlString.Null;
		public SqlString MetaTitle
		{
			get
			{
				return _metaTitle;
			}
			set
			{
				_metaTitle = value;
			}
		}


		private SqlString _metaKeyword = SqlString.Null;
		public SqlString MetaKeyword
		{
			get
			{
				return _metaKeyword;
			}
			set
			{
				_metaKeyword = value;
			}
		}


		private SqlString _metaDescription = SqlString.Null;
		public SqlString MetaDescription
		{
			get
			{
				return _metaDescription;
			}
			set
			{
				_metaDescription = value;
			}
		}


		private SqlString _tags = SqlString.Null;
		public SqlString Tags
		{
			get
			{
				return _tags;
			}
			set
			{
				_tags = value;
			}
		}


		private SqlString _tagsTitle = SqlString.Null;
		public SqlString TagsTitle
		{
			get
			{
				return _tagsTitle;
			}
			set
			{
				_tagsTitle = value;
			}
		}


		private SqlString _related = SqlString.Null;
		public SqlString Related
		{
			get
			{
				return _related;
			}
			set
			{
				_related = value;
			}
		}


		private SqlString _type = SqlString.Null;
		public SqlString Type
		{
			get
			{
				return _type;
			}
			set
			{
				_type = value;
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


		private SqlInt32 _visitor = SqlInt32.Null;
		public SqlInt32 Visitor
		{
			get
			{
				return _visitor;
			}
			set
			{
				_visitor = value;
			}
		}


		private SqlString _slideImage = SqlString.Null;
		public SqlString SlideImage
		{
			get
			{
				return _slideImage;
			}
			set
			{
				_slideImage = value;
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


		private SqlDateTime _modified = SqlDateTime.Null;
		public SqlDateTime Modified
		{
			get
			{
				return _modified;
			}
			set
			{
				_modified = value;
			}
		}


		private SqlInt32 _totalComment = SqlInt32.Null;
		public SqlInt32 TotalComment
		{
			get
			{
				return _totalComment;
			}
			set
			{
				_totalComment = value;
			}
		}


		private SqlString _sKU = SqlString.Null;
		public SqlString SKU
		{
			get
			{
				return _sKU;
			}
			set
			{
				_sKU = value;
			}
		}


		private SqlInt32 _quantity = SqlInt32.Null;
		public SqlInt32 Quantity
		{
			get
			{
				return _quantity;
			}
			set
			{
				_quantity = value;
			}
		}


		private SqlDecimal _importPrice = SqlDecimal.Null;
		public SqlDecimal ImportPrice
		{
			get
			{
				return _importPrice;
			}
			set
			{
				_importPrice = value;
			}
		}


		private SqlDecimal _realPrice = SqlDecimal.Null;
		public SqlDecimal RealPrice
		{
			get
			{
				return _realPrice;
			}
			set
			{
				_realPrice = value;
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

        private SqlDecimal _salePriceDealer = SqlDecimal.Null;
        public SqlDecimal SalePriceDealer
        {
            get
            {
                return _salePriceDealer;
            }
            set
            {
                _salePriceDealer = value;
            }
        }

        private SqlDecimal _salePriceHCM = SqlDecimal.Null;
        public SqlDecimal SalePriceHCM
        {
            get
            {
                return _salePriceHCM;
            }
            set
            {
                _salePriceHCM = value;
            }
        }

        private SqlInt32 _customerType = SqlInt32.Null;
        public SqlInt32 CustomerType
        {
            get
            {
                return _customerType;
            }
            set
            {
                _customerType = value;
            }
        }

        private SqlDecimal _salePriceDealerHCM = SqlDecimal.Null;
        public SqlDecimal SalePriceDealerHCM
        {
            get
            {
                return _salePriceDealerHCM;
            }
            set
            {
                _salePriceDealerHCM = value;
            }
        }
        private SqlString _productBarcode = SqlString.Null;
        public SqlString ProductBarcode
        {
            get
            {
                return _productBarcode;
            }
            set
            {
                _productBarcode = value;
            }
        }


		private SqlInt32 _discountPercent = SqlInt32.Null;
		public SqlInt32 DiscountPercent
		{
			get
			{
				return _discountPercent;
			}
			set
			{
				_discountPercent = value;
			}
		}


		private SqlString _weight = SqlString.Null;
		public SqlString Weight
		{
			get
			{
				return _weight;
			}
			set
			{
				_weight = value;
			}
		}


		private SqlString _model = SqlString.Null;
		public SqlString Model
		{
			get
			{
				return _model;
			}
			set
			{
				_model = value;
			}
		}


		private SqlString _filter = SqlString.Null;
		public SqlString Filter
		{
			get
			{
				return _filter;
			}
			set
			{
				_filter = value;
			}
		}


		private SqlGuid _manufactureID = SqlGuid.Null;
		public SqlGuid ManufactureID
		{
			get
			{
				return _manufactureID;
			}
			set
			{
				_manufactureID = value;
			}
		}


		private SqlString _productAttribute = SqlString.Null;
		public SqlString ProductAttribute
		{
			get
			{
				return _productAttribute;
			}
			set
			{
				_productAttribute = value;
			}
		}


		private SqlString _promotion = SqlString.Null;
		public SqlString Promotion
		{
			get
			{
				return _promotion;
			}
			set
			{
				_promotion = value;
			}
		}


		private SqlString _parameter = SqlString.Null;
		public SqlString Parameter
		{
			get
			{
				return _parameter;
			}
			set
			{
				_parameter = value;
			}
		}


		private SqlString _transportFee = SqlString.Null;
		public SqlString TransportFee
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


		private SqlGuid _pollID = SqlGuid.Null;
		public SqlGuid PollID
		{
			get
			{
				return _pollID;
			}
			set
			{
				_pollID = value;
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

        private SqlInt32 _hn = SqlInt32.Null;
        public SqlInt32 HN
        {
            get
            {
                return _hn;
            }
            set
            {
                _hn = value;
            }
        }
        private SqlInt32 _hn1 = SqlInt32.Null;
        public SqlInt32 HN1
        {
            get
            {
                return _hn1;
            }
            set
            {
                _hn1 = value;
            }
        }
        private SqlInt32 _hcm = SqlInt32.Null;
        public SqlInt32 HCM
        {
            get
            {
                return _hcm;
            }
            set
            {
                _hcm = value;
            }
        }
        private SqlInt32 _hcm1 = SqlInt32.Null;
        public SqlInt32 HCM1
        {
            get
            {
                return _hcm1;
            }
            set
            {
                _hcm1 = value;
            }
        }
        private SqlDecimal _salePriceCN3 = SqlDecimal.Null;
        public SqlDecimal SalePriceCN3
        {
            get
            {
                return _salePriceCN3;
            }
            set
            {
                _salePriceCN3 = value;
            }
        }
        private SqlDecimal _salePriceDealerCN3 = SqlDecimal.Null;
        public SqlDecimal SalePriceDealerCN3
        {
            get
            {
                return _salePriceDealerCN3;
            }
            set
            {
                _salePriceDealerCN3 = value;
            }
        }
        private SqlInt32 _cn3 = SqlInt32.Null;
        public SqlInt32 CN3
        {
            get
            {
                return _cn3;
            }
            set
            {
                _cn3 = value;
            }
        }
        private SqlInt32 _cn31 = SqlInt32.Null;
        public SqlInt32 CN31
        {
            get
            {
                return _cn31;
            }
            set
            {
                _cn31 = value;
            }
        }
		#endregion
	}
}
