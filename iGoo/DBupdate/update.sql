IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'CMS_Users' AND COLUMN_NAME = 'SendMail')
    ALTER TABLE [dbo].[CMS_Users] 
		ADD [SendMail] INT DEFAULT 1
GO
UPDATE CMS_Users SET SendMail=1 WHERE SendMail IS NULL
GO
IF NOT EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'CMS_Users' AND COLUMN_NAME = 'TaxNumber')
BEGIN    
	ALTER TABLE cms_users
		ADD TaxNumber NVARCHAR(50)
END
GO
/*
IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'CMS_Marketting')
CREATE TABLE [dbo].[CMS_Marketting](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Title] [nvarchar](150) NULL,
	[Status] [int] NULL,
	[Content] [nvarchar](450) NULL,
	[DateCreated] [datetime] NULL,
	[Expired] [datetime] NULL,
 CONSTRAINT [PK_Marketting] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'CMS_MarkettingLogs')
CREATE TABLE [dbo].[CMS_MarkettingLogs](
	[ID] [uniqueidentifier] NOT NULL,
	[MarkettingId] [uniqueidentifier] NULL,
	[ReceivedEmail] [nvarchar](80) NULL,
	[SendDate] [datetime] NULL,
 CONSTRAINT [PK_CMS_MarkettingLogs] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'CMS_BonusPoint')
CREATE TABLE [dbo].[CMS_BonusPoint](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NULL,
	[DateCreated] [datetime] NULL,
	[ExpiredDate] [datetime] NULL,
	[CostConversion] [decimal](18, 2) NULL,
	[MinimumPoint] [int] NULL,
	[RegisteredPoint] [int] NULL,
	[Status] [int] NULL,
	[ReferPoint] [int] NULL,
 CONSTRAINT [PK_CMS_BonusPoint] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[CMS_BonusPoint] ADD  CONSTRAINT [DF_CMS_BonusPoint_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO

ALTER TABLE [dbo].[CMS_BonusPoint] ADD  CONSTRAINT [DF_CMS_BonusPoint_ExpiredDate]  DEFAULT (getdate()) FOR [ExpiredDate]
GO

ALTER TABLE [dbo].[CMS_BonusPoint] ADD  CONSTRAINT [DF_CMS_BonusPoint_CostConversion]  DEFAULT ((0)) FOR [CostConversion]
GO

ALTER TABLE [dbo].[CMS_BonusPoint] ADD  CONSTRAINT [DF_CMS_BonusPoint_MinimumPoint]  DEFAULT ((0)) FOR [MinimumPoint]
GO

ALTER TABLE [dbo].[CMS_BonusPoint] ADD  CONSTRAINT [DF_CMS_BonusPoint_RegisteredPoint]  DEFAULT ((0)) FOR [RegisteredPoint]
GO

ALTER TABLE [dbo].[CMS_BonusPoint] ADD  CONSTRAINT [DF_CMS_BonusPoint_Status]  DEFAULT ((1)) FOR [Status]
GO

ALTER TABLE [dbo].[CMS_BonusPoint] ADD  CONSTRAINT [DF_CMS_BonusPoint_ReferPoint]  DEFAULT ((0)) FOR [ReferPoint]
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'CMS_BonusPointLogs')
CREATE TABLE [dbo].[CMS_BonusPointLogs](
	[ID] [uniqueidentifier] NOT NULL,
	[BonusPointId] [uniqueidentifier] NULL,
	[DateCreated] [datetime] NULL,
	[OrderId] [uniqueidentifier] NULL,
	[UserId] [uniqueidentifier] NULL,
	[CostPoint] [int] NULL,
	[RegisteredPoint] [int] NULL,
	[ReferPoint] [int] NULL,
 CONSTRAINT [PK_CMS_BonusPointLogs] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[CMS_BonusPointLogs] ADD  CONSTRAINT [DF_CMS_BonusPointLogs_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO

ALTER TABLE [dbo].[CMS_BonusPointLogs] ADD  CONSTRAINT [DF_CMS_BonusPointLogs_CostPoint]  DEFAULT ((0)) FOR [CostPoint]
GO

ALTER TABLE [dbo].[CMS_BonusPointLogs] ADD  CONSTRAINT [DF_Table_1_RegisteredPoit]  DEFAULT ((0)) FOR [RegisteredPoint]
GO

ALTER TABLE [dbo].[CMS_BonusPointLogs] ADD  CONSTRAINT [DF_CMS_BonusPointLogs_ReferPoint]  DEFAULT ((0)) FOR [ReferPoint]
GO

--get data for chart
/*SELECT [Year], [Quarter], [Month], [Week], InventoryID, SUM(TotalPrice) TotalPrice FROM
(SELECT DATEPART(ww,Created) [Week], MONTH([Created]) [Month], DATEPART(qq,Created) [Quarter], YEAR(Created) [Year], InventoryID, TotalPrice  FROM CMS_Orders) AS O
GROUP BY [Year], [Quarter], [Month], [Week], InventoryID
ORDER BY [Year], [Quarter], [Month], [Week], InventoryID*/

SELECT [Year], [Month], [InventoryID], SUM(TotalPrice) TotalPrice FROM
(SELECT DATEPART(ww,Created) [Week], MONTH([Created]) [Month], DATEPART(qq,Created) [Quarter], YEAR(Created) [Year], InventoryID, TotalPrice  FROM CMS_Orders) AS O
GROUP BY [Year], [Month], InventoryID
ORDER BY [Year], [Month], InventoryID

-- get week/month/quarter of year from CMS_Orders table

/****** Object:  StoredProcedure [dbo].[sp_Report_DoanhThu_CN]    Script Date: 06/09/2015 22:24:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_Report_GetDateFromOrders]	
	@iReturnType int, -- 1. return week/year, 2. return Month/year, 3. return quarter/year
	@sFromDate VARCHAR(50),
	@sToDate VARCHAR(50)
AS
SET NOCOUNT ON
BEGIN
	DECLARE @Sql nvarchar(max)
	DECLARE @SqlCon nvarchar(max)
	set @SqlCon= ' SELECT DATEPART(ww,o.[Created]) [Week], MONTH(o.[Created]) [Month], DATEPART(qq,o.[Created]) [Quarter], YEAR(o.[Created]) [Year] FROM CMS_Orders AS o WHERE 1=1 '
	-- BEGIN Where condition	
	IF(@sFromDate IS NOT NULL)
		SET @SqlCon = @SqlCon + ' AND o.[Created] >=  convert(datetime,@sFromDate,103) '
	IF(@sToDate IS NOT NULL)
	begin
		SET @SqlCon = @SqlCon + ' AND o.[Created] <= convert(datetime,@sToDate,103) '	
	end				
	-- END Where condition
	
	SET @Sql = ' '	
	-- BEGIN Where	
	
	IF(@iReturnType = 1)
	BEGIN
		SET @Sql = @Sql + ' SELECT DISTINCT d.[Week], d.[Year] FROM ( ' + @SqlCon + ' ) AS d ORDER BY d.[Year], d.[Week]'
	END	
	ELSE IF(@iReturnType = 2)
	BEGIN
		SET @Sql = @Sql + ' SELECT DISTINCT d.[Month], d.[Year] FROM ( ' + @SqlCon + ' ) AS d ORDER BY d.[Year], d.[Month]'
	END
	ELSE IF(@iReturnType = 3)
	BEGIN
		SET @Sql = @Sql + ' SELECT DISTINCT d.[Quarter], d.[Year] FROM ( ' + @SqlCon + ' ) AS d ORDER BY d.[Year], d.[Quarter]'
	END
	
	PRINT @Sql
	EXEC SP_EXECUTESQL  @Sql
end
*/
/****** Object:  StoredProcedure [dbo].[sp_CMS_Orders_SelectOrdersByCustomerId] Script Date: 06/19/2015 22:38:28 ******/
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_CMS_Orders_SelectOrdersByCustomerId')
	DROP PROC sp_CMS_Orders_SelectOrdersByCustomerId
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_CMS_Orders_SelectOrdersByCustomerId]
	@UserId uniqueidentifier
AS
SET NOCOUNT ON
BEGIN
	SELECT ROW_NUMBER() OVER (ORDER BY [o].[OrderID]) AS [RowNum], [o].[OrderID], [o].[OrderCode], CONVERT(NVARCHAR(20), [o].[Created], 103) AS [Created], [o].[TotalPrice], 
		CASE [o].[Status] 
			WHEN 0 THEN N'Chờ xử lý'
			WHEN 1 THEN N'Đang xử lý'
			WHEN 2 THEN N'Hoàn tất'
			WHEN 3 THEN N'Chờ giao hàng'
			WHEN 4 THEN N'Huỷ bỏ'
			WHEN 5 THEN N'Chờ vào sổ'
			WHEN 6 THEN N'Đang giao hàng'
			ELSE N'Không xác định' 
		END AS [Status]
	FROM [dbo].[CMS_Orders] AS [o]
	WHERE [o].[UserID]=@UserId
END

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_CMS_Users_Update')
	DROP PROC sp_CMS_Users_Update
GO
CREATE PROCEDURE [dbo].[sp_CMS_Users_Update]
	@guidUserID uniqueidentifier,
	@guidGroupID uniqueidentifier,
	@sFullName nvarchar(200),
	@sUserName varchar(200),
	@sPassword varchar(200),
	@iGender int,
	@sEmail varchar(200),
	@sAddress nvarchar(200),
	@sPhone nvarchar(200),
	@sPhone1 nvarchar(200),
	@sPhone2 nvarchar(200),
	@daBrithday datetime,
	@sImage nvarchar(500),
	@sGoogleID varchar(50),
	@sPermission varchar(500),
	@sSignature nvarchar(500),
	@iStatus int,
	@daCreated datetime,
	@iViews int,
	@iTotalAnswer int,
	@daDateLogin datetime,
	@sFollow varchar(max),
	@iSendMail int,
	@sTaxNumber nvarchar(50)
AS
SET NOCOUNT ON
-- UPDATE an existing row in the table.
UPDATE [dbo].[CMS_Users]
SET 
	[GroupID] = ISNULL(@guidGroupID,[GroupID]),
	[FullName] = ISNULL(@sFullName,[FullName]),
	[UserName] = ISNULL(@sUserName,[UserName]),
	[Password] = ISNULL(@sPassword,[Password]),
	[Gender] = ISNULL(@iGender,[Gender]),
	[Email] = ISNULL(@sEmail,[Email]),
	[Address] = ISNULL(@sAddress,[Address]),
	[Phone] = ISNULL(@sPhone,[Phone]),
	[Phone1] = ISNULL(@sPhone1,[Phone1]),
	[Phone2] = ISNULL(@sPhone2,[Phone2]),
	[Brithday] = ISNULL(@daBrithday,[Brithday]),
	[Image] = ISNULL(@sImage,[Image]),
	[GoogleID] = ISNULL(@sGoogleID,[GoogleID]),
	[Permission] = ISNULL(@sPermission,[Permission]),
	[Signature] = ISNULL(@sSignature,[Signature]),
	[Status] = ISNULL(@iStatus,[Status]),
	[Created] = ISNULL(@daCreated,[Created]),
	[Views] = ISNULL(@iViews,[Views]),
	[TotalAnswer] = ISNULL(@iTotalAnswer,[TotalAnswer]),
	[DateLogin] = ISNULL(@daDateLogin,[DateLogin]),
	[Follow] = ISNULL(@sFollow,[Follow]),
	[SendMail] = ISNULL(@iSendMail,1),
	[TaxNumber] = @sTaxNumber 
WHERE
	[UserID] = @guidUserID
GO


IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_CMS_Users_Insert')
	DROP PROC sp_CMS_Users_Insert
GO
CREATE PROCEDURE [dbo].[sp_CMS_Users_Insert]
	@guidUserID uniqueidentifier,
	@guidGroupID uniqueidentifier,
	@sFullName nvarchar(200),
	@sUserName varchar(200),
	@sPassword varchar(200),
	@iGender int,
	@sEmail varchar(200),
	@sAddress nvarchar(200),
	@sPhone nvarchar(200),
	@sPhone1 nvarchar(200),
	@sPhone2 nvarchar(200),
	@daBrithday datetime,
	@sImage nvarchar(500),
	@sGoogleID varchar(50),
	@sPermission varchar(500),
	@sSignature nvarchar(500),
	@iStatus int,
	@daCreated datetime,
	@iViews int,
	@iTotalAnswer int,
	@daDateLogin datetime,
	@sFollow varchar(max),
	@iSendMail int,
	@sTaxNumber nvarchar(50)
AS
SET NOCOUNT ON
-- INSERT a new row in the table.
INSERT [dbo].[CMS_Users]
(
	[UserID],
	[GroupID],
	[FullName],
	[UserName],
	[Password],
	[Gender],
	[Email],
	[Address],
	[Phone],
	[Phone1],
	[Phone2],
	[Brithday],
	[Image],
	[GoogleID],
	[Permission],
	[Signature],
	[Status],
	[Created],
	[Views],
	[TotalAnswer],
	[DateLogin],
	[Follow],
	[SendMail],
	[TaxNumber] 
)
VALUES
(
	@guidUserID,
	@guidGroupID,
	@sFullName,
	@sUserName,
	@sPassword,
	@iGender,
	@sEmail,
	@sAddress,
	@sPhone,
	@sPhone1,
	@sPhone2,
	@daBrithday,
	@sImage,
	@sGoogleID,
	@sPermission,
	@sSignature,
	@iStatus,
	@daCreated,
	@iViews,
	@iTotalAnswer,
	@daDateLogin,
	@sFollow,
	@iSendMail,
	@sTaxNumber 
)
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_CMS_Users_SelectOne')
	DROP PROC sp_CMS_Users_SelectOne
GO
CREATE PROCEDURE [dbo].[sp_CMS_Users_SelectOne]
	@guidUserID uniqueidentifier
AS
SET NOCOUNT ON
-- SELECT an existing row from the table.
SELECT
	[UserID],
	[GroupID],
	[FullName],
	[UserName],
	[Password],
	[Gender],
	[Email],
	[Address],
	[Phone],
	CONVERT(VARCHAR(24),[Brithday],103)[Brithday],
	[Image],
	[GoogleID],
	[Permission],
	[Signature],
	[Status],
	[Created],
	[Views],
	[TotalAnswer],
	[DateLogin],
	[Follow],
	[SendMail],
	[TaxNumber] 
FROM [dbo].[CMS_Users]
WHERE
	[UserID] = @guidUserID
GO

-- bao cao doanh thu
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_CMS_Orders_SelectSaleReport')
	DROP PROC sp_CMS_Orders_SelectSaleReport
GO
CREATE PROCEDURE [dbo].[sp_CMS_Orders_SelectSaleReport]
	@SaleID UNIQUEIDENTIFIER,
	@InvID  UNIQUEIDENTIFIER,
	@ShipperID  UNIQUEIDENTIFIER,
	@CusClassID  UNIQUEIDENTIFIER,
	@sSendDate VARCHAR(50),
	@sToDate VARCHAR(50),
	@iTypeBuy INT
AS
SET NOCOUNT ON
BEGIN
	DECLARE @Sql nvarchar(max)
	
	DECLARE @Parameter nvarchar(500)
	SET @Parameter =N'@SaleID UNIQUEIDENTIFIER, @InvID UNIQUEIDENTIFIER, @ShipperID UNIQUEIDENTIFIER, @CusClassID UNIQUEIDENTIFIER, @sSendDate VARCHAR(50), @sToDate VARCHAR(50), @iTypeBuy INT'
	SET @Sql = 'SELECT [t1].[TotalRows], [t1].[RowNumber],
				[t1].[OrderID], CONVERT(VARCHAR(10),[t1].[OrderSend], 103) [OrderSend], CONVERT(VARCHAR(5),[t1].[OrderSend],108) [OrderSendTime], [t1].[PrePayment], [t1].[FullName], [t1].[TotalPrice], [t1].[Address], 
				[t1].[Phone], ISNULL([inv].[InventoryName],N''Chưa có'') AS [InventoryName] , [u].fullname as nhanvien, [t1].giaohang giaohang, [cus].CusClassName, [t1].[Comment]
				FROM (SELECT TotalRows = COUNT(*) OVER(), ROW_NUMBER() OVER (ORDER BY [OrderSend] DESC) AS [RowNumber], [t0].[OrderSend], [t0].[OrderID], [t0].[OrderCode], [t0].[FullName], [t0].[TotalPrice], [t0].[Created], [t0].[Status], [t0].[Address], [t0].[Phone], [t0].[InventoryID], [t0].[SaleID], tic.giaohang,  [t0].[CusClassID], [t0].[PrePayment], [t0].[Comment]
						FROM [dbo].[CMS_Orders] AS [t0] 
						LEFT JOIN (SELECT t.orderid, t.shipperid, s.fullname AS giaohang FROM [CMS_Tickets] t, cms_shippers AS s WHERE t.shipperid = s.shipperid AND t.tickettype=0 AND t.status=0) AS [tic] ON [t0].[OrderID] = [tic].[OrderID] WHERE (1=1) AND [t0].[Status]=2 '
	-- BEGIN Where
	IF(@InvID IS NOT NULL)
		SET @Sql = @Sql + ' AND [t0].[InventoryID] = @InvID '
	IF(@CusClassID IS NOT NULL)
		SET @Sql = @Sql + ' AND [t0].[CusClassID] = @CusClassID '
	IF(@SaleID IS NOT NULL)
		SET @Sql = @Sql + ' AND [t0].[SaleID] = @SaleID '
	IF(@iTypeBuy IS NOT NULL)
	BEGIN
		IF(@iTypeBuy = 1)
			SET @Sql = @Sql + ' AND [t0].[TypeBuy] = @iTypeBuy '
		ELSE
			SET @Sql = @Sql + ' AND [t0].[TypeBuy] IN (2,3) '
	END
	IF(@ShipperID IS NOT NULL)
		SET @Sql = @Sql + ' AND [tic].[ShipperID] = @ShipperID '
	IF(@sSendDate IS NOT NULL)
	BEGIN
		SET @Sql = @Sql + ' AND [t0].[OrderSend] >= CONVERT(datetime,@sSendDate, 103) '
	END
	IF(@sToDate IS NOT NULL)
	BEGIN
		SET @Sql = @Sql + ' AND [t0].[OrderSend] <= CONVERT(datetime,@sToDate, 103) '
	END
	-- END Where
	SET @Sql = @Sql + ') AS [t1] '
	SET @Sql = @Sql + ' LEFT JOIN [CMS_Inventory] AS [inv] ON [t1].[InventoryID] = [inv].[InventoryID] '
	SET @Sql = @Sql + ' LEFT JOIN [CMS_CustomerClass] AS [cus] ON [t1].[CusClassID] = [cus].[CusClassID] '
	SET @Sql = @Sql + ' LEFT JOIN [ADM_Users] AS [u] ON [t1].[SaleID] = [u].[UserID] '
		
	EXEC SP_EXECUTESQL @Sql, @Parameter, @SaleID, @InvID, @ShipperID, @CusClassID, @sSendDate, @sToDate, @iTypeBuy
	--print @Sql
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_CMS_OrderDetails_SelectBySaleReport')
	DROP PROC sp_CMS_OrderDetails_SelectBySaleReport
GO
CREATE PROCEDURE [dbo].[sp_CMS_OrderDetails_SelectBySaleReport]
	@SaleID UNIQUEIDENTIFIER,
	@InvID  UNIQUEIDENTIFIER,
	@ShipperID  UNIQUEIDENTIFIER,
	@CusClassID  UNIQUEIDENTIFIER,
	@sSendDate VARCHAR(50),
	@sToDate VARCHAR(50),
	@iTypeBuy INT
AS
SET NOCOUNT ON
BEGIN
	DECLARE @Sql nvarchar(max)
	DECLARE @SqlVas nvarchar(max)
	DECLARE @tSql nvarchar(max)
	
	DECLARE @Parameter nvarchar(500)
	SET @Parameter =N'@SaleID UNIQUEIDENTIFIER, @InvID UNIQUEIDENTIFIER, @ShipperID UNIQUEIDENTIFIER, @CusClassID UNIQUEIDENTIFIER, @sSendDate VARCHAR(50), @sToDate VARCHAR(50), @iTypeBuy INT'
	SET @Sql = 'SELECT [od].[OrderID], [p].[SKU], [p].[Title], [ca].[Name] AS [CategoryName], [p].[Type] AS [ProductType], [p].[SalePrice], [p].[SalePriceDealer], [p].[SalePriceHCM], [p].[SalePriceDealerHCM], [p].[SalePriceCN3], [p].[SalePriceDealerCN3], [od].[Quantity], 
	CASE
        WHEN [od].[Discount]<100 THEN
                ((ISNULL([od].[Price],0) * (100 - ISNULL([od].[Discount],0))) / 100)
         ELSE (ISNULL([od].[Price],0) - ISNULL([od].[Discount],0))
    END AS [Price], 
	CASE
        WHEN [od].[Discount]<100 THEN
                [od].[Quantity] * ((ISNULL([od].[Price],0) * (100 - ISNULL([od].[Discount],0))) / 100)
         ELSE [od].[Quantity] * (ISNULL([od].[Price],0) - ISNULL([od].[Discount],0))
    END AS [Cash]    
	FROM [dbo].[CMS_OrderDetails] AS [od]
	INNER JOIN [dbo].[CMS_Products] AS [p] ON [od].[ProductID] = [p].[ProductID]
	INNER JOIN [dbo].[CMS_Categories] AS [ca] ON [p].[CategoryID] = [ca].[CategoryID]
	WHERE [od].[OrderID] IN ( '
	SET @SqlVas = 'SELECT ov.OrderId, v.Code AS SKU, v.Name AS Title, N''VAS'', N''VAS'', 0, 0, 0, 0, 0, 0, COUNT(ov.OrderId) Quantity, ov.Price, 
					COUNT(ov.OrderId)*SUM(ov.Price) AS Cash FROM CMS_OrderVAS ov
					INNER JOIN CMS_VAS v ON ov.VasId = v.Id
					WHERE ov.OrderId IN ( '
	SET @tSql = 'SELECT [t0].[OrderID] FROM [dbo].[CMS_Orders] AS [t0] WHERE [t0].[Status]=2 '
	-- BEGIN Where
	IF(@InvID IS NOT NULL)
		SET @tSql = @tSql + ' AND [t0].[InventoryID] = @InvID '
	IF(@CusClassID IS NOT NULL)
		SET @tSql = @tSql + ' AND [t0].[CusClassID] = @CusClassID '
	IF(@SaleID IS NOT NULL)
		SET @tSql = @tSql + ' AND [t0].[SaleID] = @SaleID '
	IF(@iTypeBuy IS NOT NULL)
	BEGIN
		IF(@iTypeBuy = 1)
			SET @tSql = @tSql + ' AND [t0].[TypeBuy] = @iTypeBuy '
		ELSE
			SET @tSql = @tSql + ' AND [t0].[TypeBuy] IN (2,3) '
	END
	IF(@ShipperID IS NOT NULL)
		SET @tSql = @tSql + ' AND [tic].[ShipperID] = @ShipperID '	
	IF(@sSendDate IS NOT NULL)
	BEGIN
		SET @tSql = @tSql + ' AND [t0].[OrderSend] >= CONVERT(datetime,@sSendDate, 103) '
	END
	IF(@sToDate IS NOT NULL)
	BEGIN
		SET @tSql = @tSql + ' AND [t0].[OrderSend] <= CONVERT(datetime,@sToDate, 103) '
	END
	-- END Where
	SET @Sql = @Sql + @tSql + ')';
	SET @SqlVas = @SqlVas + @tSql + ') GROUP BY ov.OrderId, v.Code,v.Name,ov.Price';
	SET @Sql = @Sql + ' UNION ALL ' + @SqlVas;
	EXEC SP_EXECUTESQL @Sql, @Parameter, @SaleID, @InvID, @ShipperID, @CusClassID, @sSendDate, @sToDate, @iTypeBuy
	--PRINT @Sql
END
GO

/* Get user logs from ADM_Logs*/
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_ADM_Logs_SelectAll')
	DROP PROC sp_ADM_Logs_SelectAll
GO
CREATE PROCEDURE [dbo].[sp_ADM_Logs_SelectAll]
	@PageIndex INT,
	@PageSize INT,
	@OrderBy NVARCHAR(100),
	@sUserName NVARCHAR(100),
	@daFromDate DATETIME,
	@daToDate DATETIME,
	@sActionType NVARCHAR(200),
	@sForm NVARCHAR (100)
AS
SET NOCOUNT ON
BEGIN
	DECLARE @Sql nvarchar(max)
	DECLARE @Parameter nvarchar(500)
	SET @Parameter =N'@PageIndex int, @PageSize int, @OrderBy varchar(100), @sUserName NVARCHAR(100), @daFromDate DATETIME, @daToDate DATETIME, @sActionType NVARCHAR(200), @sForm NVARCHAR (100)'
-- SELECT all rows from the table.
SET @Sql = 'SELECT TOP (@PageIndex * @PageSize) [t1].[TotalRows], [t1].[RowNumer],
				[t1].[ActionType], [t1].[UserName], [t1].[UserID], CONVERT(NVARCHAR(10), [t1].[Created], 103) [Created], [t1].[Comment], [t1].[Form]
				FROM (SELECT TotalRows = COUNT(*) OVER(), ROW_NUMBER() OVER (ORDER BY [t0].[Created] DESC) AS [RowNumer], 
				[t0].[ActionType], [t0].[UserName], [t0].[UserID], [t0].[Created], [t0].[Comment], [t0].[Form] 
				FROM [dbo].[ADM_Logs] AS [t0] WHERE 1=1 '
-- BEGIN Where
	IF(@sUserName IS NOT NULL)
	BEGIN
		SET @sUserName='%' + @sUserName + '%'
		SET @Sql = @Sql + ' AND [t0].[UserName] LIKE @sUserName '
	END
	IF(@daFromDate IS NOT NULL)
		SET @Sql = @Sql + ' AND [t0].[Created] >= CONVERT(DATETIME, @daFromDate, 103) '
	
	IF(@daToDate IS NOT NULL)
		SET @Sql = @Sql + ' AND [t0].[Created] <= CONVERT(DATETIME, @daToDate, 103) '
		
	IF(@sActionType IS NOT NULL)
		SET @Sql = @Sql + ' AND [t0].[ActionType] = @sActionType '	
	IF(@sForm IS NOT NULL)
		SET @Sql = @Sql + ' AND [t0].[Form] = @sForm '
	-- END Where
	
	SET @Sql = @Sql + ') AS [t1] WHERE [t1].[RowNumer] >= ((@PageIndex - 1) * @PageSize + 1)  AND [t1].[RowNumer] <= (@PageIndex * @PageSize)'
	--select @Sql
	EXEC SP_EXECUTESQL @Sql, @Parameter, @PageIndex, @PageSize, @OrderBy, @sUserName, @daFromDate, @daToDate, @sActionType, @sForm
END
GO

IF NOT EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'CMS_MemberChildren')
CREATE TABLE [dbo].[CMS_MemberChildren](
	[ChildID] [uniqueidentifier] NOT NULL,
	[ParentID] [uniqueidentifier] NOT NULL,
	[ChildName] [nvarchar](100) NULL,
	[BirthDate] [date] NULL,
	[Sex] [bit] NULL 
)
GO
IF OBJECT_ID('[dbo].[sp_CMS_MemberChildrenSelect]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[sp_CMS_MemberChildrenSelect] 
END 
GO
CREATE PROC [dbo].[sp_CMS_MemberChildrenSelect] 
    @ParentID uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT ROW_NUMBER() OVER (ORDER BY [ChildID]) AS [RowNum], [ChildID], [ChildName], CONVERT(NVARCHAR(10), [BirthDate], 103) AS [BirthDate], [Sex] 
	FROM   [dbo].[CMS_MemberChildren] 
	WHERE  ([ParentID] = @ParentID OR @ParentID IS NULL) 

	COMMIT
GO
IF OBJECT_ID('[dbo].[sp_CMS_MemberChildrenInsert]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[sp_CMS_MemberChildrenInsert] 
END 
GO
CREATE PROC [dbo].[sp_CMS_MemberChildrenInsert] 
    @ChildID uniqueidentifier,
    @ParentID uniqueidentifier,
    @ChildName nvarchar(100),
    @BirthDate date,
    @Sex bit
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[CMS_MemberChildren] ([ChildID], [ParentID], [ChildName], [BirthDate], [Sex])
	SELECT @ChildID, @ParentID, @ChildName, @BirthDate, @Sex
	
	-- Begin Return Select 
	SELECT [ChildID], [ParentID], [ChildName], [BirthDate], [Sex]
	FROM   [dbo].[CMS_MemberChildren]
	WHERE  [ChildID] = @ChildID
	-- End Return Select 
               
	COMMIT
GO
IF OBJECT_ID('[dbo].[sp_CMS_MemberChildrenUpdate]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[sp_CMS_MemberChildrenUpdate] 
END 
GO
CREATE PROC [dbo].[sp_CMS_MemberChildrenUpdate] 
    @ChildID uniqueidentifier,
    @ParentID uniqueidentifier,
    @ChildName nvarchar(100),
    @BirthDate date,
    @Sex bit
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[CMS_MemberChildren]
	SET    [ParentID] = @ParentID, [ChildName] = @ChildName, [BirthDate] = @BirthDate, [Sex] = @Sex
	WHERE  [ChildID] = @ChildID
	
	-- Begin Return Select 
	SELECT [ChildID], [ParentID], [ChildName], [BirthDate], [Sex]
	FROM   [dbo].[CMS_MemberChildren]
	WHERE  [ChildID] = @ChildID	
	-- End Return Select 

	COMMIT
GO
IF OBJECT_ID('[dbo].[sp_CMS_MemberChildrenDelete]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[sp_CMS_MemberChildrenDelete] 
END 
GO
CREATE PROC [dbo].[sp_CMS_MemberChildrenDelete] 
    @ChildID uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[CMS_MemberChildren]
	WHERE  [ChildID] = @ChildID

	COMMIT
GO

IF OBJECT_ID('[dbo].[sp_CMS_Users_SelectByPhone]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[sp_CMS_Users_SelectByPhone] 
END 
GO
CREATE PROCEDURE [dbo].[sp_CMS_Users_SelectByPhone]
	@sPhone nvarchar(30)
AS
SET NOCOUNT ON
BEGIN
	DECLARE @strPhone nvarchar(30)
	SET @strPhone = '%' + @sPhone + '%'
	SELECT [UserID], [FullName], [Address], [Email], [Phone] FROM [dbo].[CMS_Users] WHERE [Phone] like @strPhone
	--IF(@sPhone IS NOT NULL) AND NOT EXISTS (SELECT DISTINCT [Phone] FROM [dbo].[CMS_Users])
	--	BEGIN
	--		SET @strPhone = @sPhone + '%'		
	--		SELECT [UserID], [FullName], [Address], [Email], [Phone] FROM [dbo].[CMS_Users] WHERE [Phone] like @strPhone
	--		UNION 
	--		SELECT [UserID],[FullName], [Address] ,[Email] ,[Phone] FROM [dbo].[CMS_Orders] WHERE [Phone] NOT IN(SELECT DISTINCT [Phone] FROM [dbo].[CMS_Users]) AND [Phone] like @strPhone
	--	END
	--ELSE
	--	BEGIN
	--		SET @strPhone = @sPhone + '%'
	--		SELECT [UserID],[FullName], [Address] ,[Email] ,[Phone] FROM [dbo].[CMS_Orders] WHERE [Phone] like @strPhone
	--	END
END
GO

IF OBJECT_ID('[dbo].[sp_CMS_Orders_Update_By_OrderDetail]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[sp_CMS_Orders_Update_By_OrderDetail] 
END 
GO
CREATE PROCEDURE [dbo].[sp_CMS_Orders_Update_By_OrderDetail]
	@guidOrderID uniqueidentifier,
	@guidSaleID uniqueidentifier,
	@guidUserID uniqueidentifier,
	@sFullName nvarchar(200),
	@sAddress nvarchar(200),
	@sEmail nvarchar(200),
	@sPhone nvarchar(200),
	@sRequest nvarchar(1000),
	@dcTotalPrice numeric(18, 2),
	@sComment nvarchar(1000),
	@iTax int,
	@dcTransportFee numeric(18, 2),
	@dcDiscount numeric(18, 2),
	@iStatus int,
	@iDistrict int,
	@iProvince int,
	@guidPaymentID uniqueidentifier,
	@daOrderSend datetime,
	@guidCusClassID uniqueidentifier,
	@guidInvID uniqueidentifier,
	@sBookNumber nvarchar(50),
	@iPageNUmber int,
	@dcPrePayment decimal(18,2)
AS
SET NOCOUNT ON
BEGIN
	--DECLARE @totalPrice int = 0
	DECLARE @stt int
	SELECT @stt = [Status] FROM [dbo].[CMS_Orders] WHERE [OrderID] = @guidOrderID
	IF @stt != 2 AND @stt != 6 AND @stt != 4
	BEGIN
		IF @iStatus = 2 -- 2 la trang thai hoan tat
			BEGIN
				UPDATE [dbo].[CMS_Orders] 
				SET 
					[SaleID] = ISNULL(@guidSaleID,[SaleID]),
					[UserID] = ISNULL(@guidUserID,[UserID]), 
					[FullName] = ISNULL(@sFullName,[FullName]),
					[Address] = ISNULL(@sAddress,[Address]),
					[Email] = ISNULL(@sEmail,[Email]),
					[Phone] = ISNULL(@sPhone,[Phone]),
					[Request] = ISNULL(@sRequest,[Request]),
					[Comment] = ISNULL(@sComment,[Comment]),
					[TotalPrice] = ISNULL(@dcTotalPrice,[TotalPrice]),
					[Tax] = ISNULL(@iTax,[Tax]),
					[TransportFee] = ISNULL(@dcTransportFee,[TransportFee]), 
					[Discount] = ISNULL(@dcDiscount,[Discount]),
					[Status] = ISNULL(@iStatus,[Status]),
					[DistrictId] = ISNULL(@iDistrict, [DistrictId]),
					[ProvinceId] = ISNULL(@iProvince, [ProvinceId]),
					[PaymentID] = @guidPaymentID,
					[CusClassId] = @guidCusClassID,
					[InventoryID] = ISNULL(@guidInvID,[InventoryID]),
					[OrderSend] = ISNULL(@daOrderSend,[OrderSend]),
					[PageNumber] = ISNULL(@iPageNUmber, [PageNumber]),
					[BookNumber] = ISNULL(@sBookNumber, [BookNumber]),
					[PrePayment] = ISNULL(@dcPrePayment, [PrePayment])
				WHERE [OrderID] = @guidOrderID
				EXEC sp_CMS_Orders_Update_Inventory @guidOrderID,@guidCusClassID,@guidInvID
			END
		ELSE
			BEGIN
				UPDATE [dbo].[CMS_Orders] 
				SET 
					[SaleID] = ISNULL(@guidSaleID,[SaleID]),
					[UserID] = ISNULL(@guidUserID,[UserID]),  
					[FullName] = ISNULL(@sFullName,[FullName]),
					[Address] = ISNULL(@sAddress,[Address]),
					[Email] = ISNULL(@sEmail,[Email]),
					[Phone] = ISNULL(@sPhone,[Phone]),
					[Request] = ISNULL(@sRequest,[Request]),
					[Comment] = ISNULL(@sComment,[Comment]),
					[TotalPrice] = ISNULL(@dcTotalPrice,[TotalPrice]),
					[Tax] = ISNULL(@iTax,[Tax]),
					[TransportFee] = ISNULL(@dcTransportFee,[TransportFee]), 
					[Discount] = ISNULL(@dcDiscount,[Discount]),
					[Status] = ISNULL(@iStatus,[Status]),
					[DistrictId] = ISNULL(@iDistrict, [DistrictId]),
					[ProvinceId] = ISNULL(@iProvince, [ProvinceId]),
					[PaymentID] = ISNULL(@guidPaymentID,[PaymentID]),
					[CusClassId] = ISNULL(@guidCusClassID,[CusClassId]),
					[InventoryID] = ISNULL(@guidInvID,[InventoryID]),
					[OrderSend] = ISNULL(@daOrderSend,[OrderSend]),
					[PageNumber] = ISNULL(@iPageNUmber, [PageNumber]),
					[BookNumber] = ISNULL(@sBookNumber, [BookNumber]),
					[PrePayment] = ISNULL(@dcPrePayment, [PrePayment])
				WHERE [OrderID] = @guidOrderID
				EXEC sp_CMS_Orders_Update_Inventory @guidOrderID,@guidCusClassID,@guidInvID
			END
	END
END
GO

IF OBJECT_ID('[dbo].[sp_CMS_Products_SelectProductsByCustomerId]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[sp_CMS_Products_SelectProductsByCustomerId] 
END 
GO
CREATE PROCEDURE [dbo].[sp_CMS_Products_SelectProductsByCustomerId]
	@guidUserID uniqueidentifier
AS
SET NOCOUNT ON
BEGIN
	SELECT ROW_NUMBER() OVER (ORDER BY [t0].[TITLE] DESC) AS [RowNum], [t0].[ProductID], [t0].[SKU], [t0].[Title], [t0].[Quantity], [t0].[Price]
		FROM (SELECT [od].[ProductID], [p].[SKU], [p].[Title], SUM([od].[Quantity]) AS [Quantity], [od].[Price] FROM [dbo].[CMS_OrderDetails] AS [od]
		INNER JOIN [dbo].[CMS_Products] AS [p] ON [od].[ProductID] = [p].[ProductID]
		INNER JOIN [dbo].[CMS_Orders] AS [o] ON [od].[OrderID] = [o].[OrderID] AND [o].[UserID]=@guidUserID
		GROUP BY [od].[ProductID], [p].[SKU], [p].[Title], [od].[Price]) AS [t0]
	ORDER BY [t0].[Title]
END

/* ALTER TABLE [dbo].[ADM_Logs] to display Unicode*/
ALTER TABLE [dbo].[ADM_Logs] ALTER COLUMN [ActionType] [nvarchar](20) NULL
GO
ALTER TABLE [dbo].[ADM_Logs] ALTER COLUMN [UserName] [nvarchar](200) NULL
GO
ALTER TABLE [dbo].[ADM_Logs] ALTER COLUMN [Form] [nvarchar](200) NULL
GO


IF OBJECT_ID('[dbo].[sp_CMS_Orders_Update_By_OrderDetail]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[sp_CMS_Orders_Update_By_OrderDetail] 
END 
GO
CREATE  PROCEDURE [dbo].[sp_CMS_Orders_Update_By_OrderDetail]
	@guidOrderID uniqueidentifier,
	@guidSaleID uniqueidentifier,
	@guidUserID uniqueidentifier,
	@sFullName nvarchar(200),
	@sAddress nvarchar(200),
	@sEmail nvarchar(200),
	@sPhone nvarchar(200),
	@sRequest nvarchar(1000),
	@dcTotalPrice numeric(18, 2),
	@sComment nvarchar(1000),
	@iTax int,
	@dcTransportFee numeric(18, 2),
	@dcDiscount numeric(18, 2),
	@iStatus int,
	@iDistrict int,
	@iProvince int,
	@guidPaymentID uniqueidentifier,
	@daOrderSend datetime,
	@guidCusClassID uniqueidentifier,
	@guidInvID uniqueidentifier,
	@sBookNumber nvarchar(50),
	@iPageNUmber int,
	@dcPrePayment decimal(18,2),
	@isDataChanged int
AS
SET NOCOUNT ON
BEGIN
	DECLARE @stt int
	SELECT @stt = [Status] FROM [dbo].[CMS_Orders] WHERE [OrderID] = @guidOrderID
	IF @stt != 2 AND @stt != 6 AND @stt != 4
	BEGIN
		IF @iStatus = 2 -- 2 la trang thai hoan tat
			BEGIN
				UPDATE [dbo].[CMS_Orders] 
				SET 
					[SaleID] = ISNULL(@guidSaleID,[SaleID]), 
					[FullName] = ISNULL(@sFullName,[FullName]),
					[Address] = ISNULL(@sAddress,[Address]),
					[Email] = ISNULL(@sEmail,[Email]),
					[Phone] = ISNULL(@sPhone,[Phone]),
					[Request] = ISNULL(@sRequest,[Request]),
					[Comment] = ISNULL(@sComment,[Comment]),
					[TotalPrice] = ISNULL(@dcTotalPrice,[TotalPrice]),
					[Tax] = ISNULL(@iTax,[Tax]),
					[TransportFee] = ISNULL(@dcTransportFee,[TransportFee]), 
					[Discount] = ISNULL(@dcDiscount,[Discount]),
					[Status] = ISNULL(@iStatus,[Status]),
					[DistrictId] = ISNULL(@iDistrict, [DistrictId]),
					[ProvinceId] = ISNULL(@iProvince, [ProvinceId]),
					[PaymentID] = @guidPaymentID,
					[CusClassId] = @guidCusClassID,
					[InventoryID] = ISNULL(@guidInvID,[InventoryID]),
					[OrderSend] = ISNULL(@daOrderSend,[OrderSend]),
					[PageNumber] = ISNULL(@iPageNUmber, [PageNumber]),
					[BookNumber] = ISNULL(@sBookNumber, [BookNumber]),
					[PrePayment] = ISNULL(@dcPrePayment, [PrePayment])
				WHERE [OrderID] = @guidOrderID
				EXEC sp_CMS_Orders_Update_Inventory @guidOrderID,@guidCusClassID,@guidInvID,@isDataChanged
			END
		ELSE
			BEGIN
				UPDATE [dbo].[CMS_Orders] 
				SET 
					[SaleID] = ISNULL(@guidSaleID,[SaleID]), 
					[FullName] = ISNULL(@sFullName,[FullName]),
					[Address] = ISNULL(@sAddress,[Address]),
					[Email] = ISNULL(@sEmail,[Email]),
					[Phone] = ISNULL(@sPhone,[Phone]),
					[Request] = ISNULL(@sRequest,[Request]),
					[Comment] = ISNULL(@sComment,[Comment]),
					[TotalPrice] = ISNULL(@dcTotalPrice,[TotalPrice]),
					[Tax] = ISNULL(@iTax,[Tax]),
					[TransportFee] = ISNULL(@dcTransportFee,[TransportFee]), 
					[Discount] = ISNULL(@dcDiscount,[Discount]),
					[Status] = ISNULL(@iStatus,[Status]),
					[DistrictId] = ISNULL(@iDistrict, [DistrictId]),
					[ProvinceId] = ISNULL(@iProvince, [ProvinceId]),
					[PaymentID] = ISNULL(@guidPaymentID,[PaymentID]),
					[CusClassId] = ISNULL(@guidCusClassID,[CusClassId]),
					[InventoryID] = ISNULL(@guidInvID,[InventoryID]),
					[OrderSend] = ISNULL(@daOrderSend,[OrderSend]),
					[PageNumber] = ISNULL(@iPageNUmber, [PageNumber]),
					[BookNumber] = ISNULL(@sBookNumber, [BookNumber]),
					[PrePayment] = ISNULL(@dcPrePayment, [PrePayment])
				WHERE [OrderID] = @guidOrderID
				EXEC sp_CMS_Orders_Update_Inventory @guidOrderID,@guidCusClassID,@guidInvID,@isDataChanged
			END
	END
END
GO

--06-Sep-15
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_CMS_Users_SelectAll]') AND type in (N'P', N'PC'))
BEGIN 
DROP PROCEDURE [dbo].[sp_CMS_Users_SelectAll]
END
GO
CREATE PROCEDURE [dbo].[sp_CMS_Users_SelectAll]
	@PageIndex int,
	@PageSize int,
	@OrderBy varchar(100),
	@guidGroupID uniqueidentifier,
	@sUserName nvarchar(200),
	@iStatus int
AS
SET NOCOUNT ON
BEGIN
	DECLARE @Sql nvarchar(max)
	DECLARE @Parameter nvarchar(500)
	SET @Parameter =N'@PageIndex int, @PageSize int, @OrderBy varchar(100), @guidGroupID uniqueidentifier, @sUserName nvarchar(200), @iStatus int'
	SET @Sql = 'SELECT TOP (@PageIndex * @PageSize) [t1].[TotalRows],
				[t1].[UserID],[t1].[GroupID], [t1].[UserName],[t1].[FullName], [t1].[Email], [t1].[Address], [t1].[Phone], [t1].[Brithday], [t1].[GoogleID] , [t1].[Status], [t1].[Views]
				FROM (SELECT TotalRows = COUNT(*) OVER(), '
	-- BEGIN Order by
	SET @Sql = @Sql + CASE (@OrderBy)
		WHEN 'USERNAME DESC' THEN ' ROW_NUMBER() OVER (ORDER BY [t0].[UserName] DESC) AS [RowNumer], '
		WHEN 'USERNAME ASC' THEN ' ROW_NUMBER() OVER (ORDER BY [t0].[UserName]) AS [RowNumer], '
		WHEN 'FULLNAME DESC' THEN ' ROW_NUMBER() OVER (ORDER BY [t0].[FullName] DESC) AS [RowNumer], '
		WHEN 'FULLNAME ASC' THEN ' ROW_NUMBER() OVER (ORDER BY [t0].[FullName]) AS [RowNumer], '
		WHEN 'EMAIL DESC' THEN ' ROW_NUMBER() OVER (ORDER BY [t0].[EMAIL] DESC) AS [RowNumer], '
		WHEN 'EMAIL ASC' THEN ' ROW_NUMBER() OVER (ORDER BY [t0].[EMAIL]) AS [RowNumer], '
		WHEN 'VIEWS DESC' THEN ' ROW_NUMBER() OVER (ORDER BY [t0].[VIEWS] DESC) AS [RowNumer], '
		WHEN 'VIEWS ASC' THEN ' ROW_NUMBER() OVER (ORDER BY [t0].[VIEWS]) AS [RowNumer], '
		ELSE ' ROW_NUMBER() OVER (ORDER BY [t0].[Created] DESC) AS [RowNumer], '
	END
	-- END Order by
	SET @Sql = @Sql + '	[t0].[UserID],[t0].[GroupID], [t0].[UserName],[t0].[FullName], [t0].[Email], [t0].[Address], [t0].[Phone], [t0].[Brithday], [t0].[GoogleID], [t0].[Status], [t0].[Views] 
						FROM [dbo].[CMS_Users] AS [t0] WHERE 1=1 '
	-- BEGIN Where
	IF(@sUserName IS NOT NULL)
	BEGIN
		SET @sUserName='%' + @sUserName + '%'
		SET @Sql = @Sql + ' AND ([t0].[UserName] LIKE @sUserName OR [t0].[Email] LIKE @sUserName OR [t0].[UserID] LIKE  @sUserName OR [t0].[Phone] LIKE  @sUserName) '
	END
	IF(@guidGroupID IS NOT NULL)
		SET @Sql = @Sql + ' AND [t0].[GroupID] = @guidGroupID '
	IF(@iStatus IS NOT NULL)
		SET @Sql = @Sql + ' AND [t0].[Status] = @iStatus '
	-- END Where
	SET @Sql = @Sql + ') AS [t1] WHERE [t1].[RowNumer] >= ((@PageIndex - 1) * @PageSize + 1)  AND [t1].[RowNumer] <= (@PageIndex * @PageSize)'
	--select @Sql
	EXEC SP_EXECUTESQL @Sql, @Parameter, @PageIndex, @PageSize, @OrderBy, @guidGroupID, @sUserName, @iStatus
END
GO

--06-Sep-15
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_CMS_Orders_SelectOptimize]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_CMS_Orders_SelectOptimize]
GO
CREATE PROCEDURE [dbo].[sp_CMS_Orders_SelectOptimize]
	@PageIndex int,
	@PageSize int,
	@OrderBy varchar(100),
	@sOrderCode nvarchar(100),
	@iProvinceID INT,
	@iDistrictID INT,
	@guidShipperID uniqueidentifier,
	@iStatus INT,
	@sFromDate VARCHAR(50),
	@sToDate VARCHAR(50),
	@guidUserID uniqueidentifier
	
AS
SET NOCOUNT ON
BEGIN
	DECLARE @Sql nvarchar(max)
	
	DECLARE @Parameter nvarchar(500)
	SET @Parameter =N'@PageIndex int, @PageSize int, @OrderBy varchar(100),  @sOrderCode nvarchar(100),@iProvinceID INT,@iDistrictID INT,@guidShipperID uniqueidentifier, @iStatus int,@sFromDate VARCHAR(50),@sToDate VARCHAR(50),@guidUserID uniqueidentifier'
	SET @Sql = 'SELECT TOP (@PageIndex * @PageSize) [t1].[TotalRows],
				[t1].[OrderID], [t1].[OrderCode], [t1].[FullName], [t1].[TotalPrice], CONVERT ( VARCHAR( 16 ),[t1].[Created], 103 ) AS Created, [t1].[Status],t1.[SumOrder],[t1].[Request],[t1].[InventoryID], [t1].address,
				(select InventoryName from [dbo].[CMS_Inventory] where InventoryID =  [t1].[InventoryID]) as InventoryName,
				(SELECT TOP 1 a.FullName FROM dbo.CMS_Shippers a,dbo.CMS_Tickets b WHERE a.ShipperID = b.ShipperID
				AND b.OrderID = [t1].[OrderID]) as ShipperName								
				FROM (SELECT TotalRows = COUNT(*) OVER(), '
				--- AND b.STATUS = 1 AND b.OrderID = [t1].[OrderID]) as ShipperName	
				--- status  1 dang thuc hien 0 da ket thuc --- type : 1 thanh cong 0 or # : that bai 
	-- BEGIN Order by
	SET @Sql = @Sql + CASE (@OrderBy)
		WHEN 'ORDERCODE DESC' THEN ' ROW_NUMBER() OVER (ORDER BY [ORDERCODE] DESC) AS [RowNumer], '
		WHEN 'ORDERCODE ASC' THEN ' ROW_NUMBER() OVER (ORDER BY [ORDERCODE]) AS [RowNumer], '
		WHEN 'FULLNAME DESC' THEN ' ROW_NUMBER() OVER (ORDER BY [FULLNAME] DESC) AS [RowNumer], '
		WHEN 'FULLNAME ASC' THEN ' ROW_NUMBER() OVER (ORDER BY [FULLNAME]) AS [RowNumer], '
		WHEN 'TOTALPRICE DESC' THEN ' ROW_NUMBER() OVER (ORDER BY [TOTALPRICE] DESC) AS [RowNumer], '
		WHEN 'TOTALPRICE ASC' THEN ' ROW_NUMBER() OVER (ORDER BY [TOTALPRICE]) AS [RowNumer], '
		WHEN 'Created ASC' THEN ' ROW_NUMBER() OVER (ORDER BY [Created]) AS [RowNumer], '
		ELSE ' ROW_NUMBER() OVER (ORDER BY [Created] DESC) AS [RowNumer], '
	END
	-- END Order by
	
	--SET @Sql = @Sql + '	[t0].[OrderID], [t0].[OrderCode], [t0].[FullName],([t0].[TotalPrice]+ [t0].[TransportFee]-[t0].[Discount]) AS TotalPrice, [t0].[Created], [t0].[Status],cod.[SumOrder],[t0].[Request],
	--					[t0].[InventoryID], [t0].address
	--					FROM [dbo].[CMS_Orders] AS [t0] ,(select t.OrderID  ,STUFF((
 --       select  '', '' + t2.Title + '' ( '' +  CAST(t1.Quantity AS NVARCHAR(100)) + '' )''
 --       from CMS_OrderDetails t1,dbo.CMS_Products t2
 --       where t1.ProductID = t2.ProductID
 --       and       t1.OrderID = t.OrderID
 --       for xml path(''''), type).value(''.'', ''nvarchar(max)''), 1, 1, '''') [Sumorder]  
 --       from CMS_OrderDetails t group by t.OrderID ) AS cod  WHERE [t0].OrderID = cod.OrderID '
    
    --28Jan15
    SET @Sql = @Sql + '	[t0].[OrderID], [t0].[OrderCode], [t0].[FullName],[t0].[TotalPrice] AS TotalPrice, [t0].[Created], [t0].[Status],cod.[SumOrder],[t0].[Request],
						[t0].[InventoryID], [t0].address
						FROM [dbo].[CMS_Orders] AS [t0] ,(select t.OrderID  ,STUFF((
        select  '', '' + t2.Title + '' ( '' +  CAST(t1.Quantity AS NVARCHAR(100)) + '' )''
        from CMS_OrderDetails t1,dbo.CMS_Products t2
        where t1.ProductID = t2.ProductID
        and       t1.OrderID = t.OrderID
        for xml path(''''), type).value(''.'', ''nvarchar(max)''), 1, 1, '''') [Sumorder]  
        from CMS_OrderDetails t group by t.OrderID ) AS cod  WHERE [t0].OrderID = cod.OrderID '    
	-- BEGIN Where
	IF(@sOrderCode IS NOT NULL)
	BEGIN
		SET @sOrderCode='%'+@sOrderCode+'%'
		SET @Sql = @Sql + ' AND ( [t0].[OrderCode] LIKE @sOrderCode or [t0].[FullName] LIKE @sOrderCode or [t0].[Address] LIKE @sOrderCode or [t0].[Phone] LIKE @sOrderCode) '
	END
	IF(@iStatus IS NOT NULL)
		SET @Sql = @Sql + ' AND [t0].[Status] = @iStatus '
	IF(@iProvinceID IS NOT NULL)
		SET @Sql = @Sql + ' AND [t0].[ProvinceID] = @iProvinceID '	
	IF(@iDistrictID IS NOT NULL)
		SET @Sql = @Sql + ' AND [t0].[DistrictID] = @iDistrictID '
	IF(@guidShipperID IS NOT NULL)
	BEGIN
		SET @Sql = @Sql + ' AND (SELECT COUNT(*) FROM dbo.CMS_Tickets b WHERE b.OrderID = [t0].[OrderID] and b.status =1 AND b.ShipperID = @guidShipperID)  >0 '
	END		
	
	IF(@sFromDate IS NOT NULL)
	begin
	if (@iStatus =6)--Dang giao hang
		SET @Sql = @Sql + ' AND (SELECT COUNT(*) FROM dbo.CMS_Tickets b WHERE b.OrderID = [t0].[OrderID] and b.status =1 AND b.CreateDate >= convert(datetime,@sFromDate,103) )  >0 '
	else if (@iStatus  in (2,3,4))
		SET @Sql = @Sql + ' AND (SELECT COUNT(*) FROM dbo.CMS_Tickets b WHERE b.OrderID = [t0].[OrderID] AND b.CompleteDate >= convert(datetime,@sFromDate,103) )  >0 '
	else
		SET @Sql = @Sql + ' AND [t0].[Ordersend] >=  convert(datetime,@sFromDate,103) '
	end
	IF(@sToDate IS NOT NULL)
	begin
		set @sToDate = @sToDate + ' 23:59:59'
		if (@iStatus =6) --dang giao hang
			SET @Sql = @Sql + ' AND (SELECT COUNT(*) FROM dbo.CMS_Tickets b WHERE b.OrderID = [t0].[OrderID] AND b.CreateDate <= convert(datetime,@sToDate,103) )  >0 '
		else if (@iStatus  in (2,3,4))
			SET @Sql = @Sql + ' AND (SELECT COUNT(*) FROM dbo.CMS_Tickets b WHERE b.OrderID = [t0].[OrderID] AND b.CompleteDate <= convert(datetime,@sToDate,103) )  >0 '
		else
			SET @Sql = @Sql + ' AND [t0].[Ordersend] <= convert(datetime,@sToDate,103) '			
	end
	IF(@guidUserID IS NOT NULL)
	BEGIN
		SET @Sql = @Sql + ' AND [t0].[InventoryID] in (select [inventoryId] from dbo.ADM_InvUsers where [userId] = @guidUserID) '
	END		
	-- END Where
	SET @Sql = @Sql + ') AS [t1] WHERE [t1].[RowNumer] >= ((@PageIndex - 1) * @PageSize + 1)  AND [t1].[RowNumer] <= (@PageIndex * @PageSize)'
	print @Sql 
	EXEC SP_EXECUTESQL @Sql, @Parameter, @PageIndex, @PageSize, @OrderBy, @sOrderCode,@iProvinceID,@iDistrictID,@guidShipperID, @iStatus, @sFromDate,@sToDate,@guidUserID
END
GO

--06-Sep-15
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_CMS_Orders_SelectAll]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_CMS_Orders_SelectAll]
GO
CREATE PROCEDURE [dbo].[sp_CMS_Orders_SelectAll]
	@PageIndex int,
	@PageSize int,
	@OrderBy varchar(100),
	@sOrderCode nvarchar(100),
	@iStatus int,
	@iProvince int,
	@iDistrict int,
	@iOType int,
	@guidSaleID uniqueidentifier,
	
	@SaleID uniqueidentifier,
	@InvID  uniqueidentifier,
	@ShipperID  uniqueidentifier,
	@CusID  uniqueidentifier,
	
	@sFromDate VARCHAR(50),
	@sToDate VARCHAR(50)
AS
SET NOCOUNT ON
BEGIN
	DECLARE @Sql nvarchar(max)
	
	DECLARE @Parameter nvarchar(500)
	SET @Parameter =N'@PageIndex int, @PageSize int, @OrderBy varchar(100),  @sOrderCode nvarchar(100), @iStatus int, @iProvince int, @iDistrict int, @iOType int, @guidSaleID uniqueidentifier, @SaleID uniqueidentifier,@InvID uniqueidentifier,@ShipperID  uniqueidentifier,	@sFromDate VARCHAR(50),@sToDate VARCHAR(50),@CusID  uniqueidentifier'
	SET @Sql = 'SELECT TOP (@PageIndex * @PageSize) [t1].[TotalRows],
				[t1].[OrderID], [t1].[OrderCode], [t1].[FullName], ([t1].[TotalPrice]) AS [TotalPrice], 
				CONVERT ( VARCHAR( 16 ),[t1].[Created], 103 ) AS Created, CONVERT(VARCHAR(5),[t1].[Created],108) AS CreatedTime, [t1].[Status], [t1].[Address], 
				[t1].[Phone], ISNULL([inv].[InventoryName],''Chưa có'') AS [InventoryName] , u.fullname as nhanvien, t1.giaohang giaohang, [cus].CusClassName
				FROM (SELECT TotalRows = COUNT(*) OVER(), ' -- + ISNULL([ov].[SumVas],0)
	-- BEGIN Order by
	SET @Sql = @Sql + CASE (@OrderBy)
		WHEN 'ORDERCODE DESC' THEN ' ROW_NUMBER() OVER (ORDER BY [ORDERCODE] DESC) AS [RowNumer], '
		WHEN 'ORDERCODE ASC' THEN ' ROW_NUMBER() OVER (ORDER BY [ORDERCODE]) AS [RowNumer], '
		WHEN 'FULLNAME DESC' THEN ' ROW_NUMBER() OVER (ORDER BY [FULLNAME] DESC) AS [RowNumer], '
		WHEN 'FULLNAME ASC' THEN ' ROW_NUMBER() OVER (ORDER BY [FULLNAME]) AS [RowNumer], '
		WHEN 'TOTALPRICE DESC' THEN ' ROW_NUMBER() OVER (ORDER BY [TOTALPRICE] DESC) AS [RowNumer], '
		WHEN 'TOTALPRICE ASC' THEN ' ROW_NUMBER() OVER (ORDER BY [TOTALPRICE]) AS [RowNumer], '
		WHEN 'Created ASC' THEN ' ROW_NUMBER() OVER (ORDER BY [Created]) AS [RowNumer], '
		ELSE ' ROW_NUMBER() OVER (ORDER BY [Created] DESC) AS [RowNumer], '
	END
	-- END Order by
	SET @Sql = @Sql + '	[t0].[OrderID], [t0].[OrderCode], [t0].[FullName],([t0].[TotalPrice] ) AS [TotalPrice], [t0].[Created], [t0].[Status], [t0].[Address], [t0].[Phone], [t0].[InventoryID], [t0].[SaleID], tic.giaohang,  [t0].[CusClassID]
						FROM [dbo].[CMS_Orders] AS [t0] 
						LEFT JOIN (select t.orderid,t.shipperid, s.fullname as giaohang from [CMS_Tickets] t, cms_shippers s where t.shipperid = s.shipperid and t.tickettype=0) AS [tic] ON [t0].[OrderID] = [tic].[OrderID]
						WHERE (1=1) AND [t0].[InventoryID] IN (SELECT [inventoryId] FROM [ADM_InvUsers] WHERE [userId] = @guidSaleID) '
	-- BEGIN Where
	IF(@iOType = 1)
		SET @Sql = @Sql + ' AND [t0].[TypeBuy]=1 '
	ELSE
		SET @Sql = @Sql + ' AND [t0].[TypeBuy] IN (2,3) '
	IF(@sOrderCode IS NOT NULL)
	BEGIN
		SET @sOrderCode= '%'+@sOrderCode+'%'
		SET @Sql = @Sql + ' AND ([t0].[Phone] LIKE @sOrderCode OR [t0].[FullName] LIKE @sOrderCode OR [t0].[Address] LIKE @sOrderCode OR [t0].[OrderCode] LIKE @sOrderCode) '
	END
	IF(@InvID IS NOT NULL)
		SET @Sql = @Sql + ' AND [t0].[InventoryID] = @InvID '
	IF(@CusID IS NOT NULL)
		SET @Sql = @Sql + ' AND [t0].[CusClassID] = @CusID '
	IF(@SaleID IS NOT NULL)
		SET @Sql = @Sql + ' AND [t0].[SaleID] = @SaleID '
	IF(@ShipperID IS NOT NULL)
		SET @Sql = @Sql + ' AND [tic].[ShipperID] = @ShipperID '
	
	IF(@sFromDate IS NOT NULL)
		begin
			if (@iStatus =2)
				SET @Sql = @Sql + ' AND [t0].[OrderSend] >=  convert(datetime,@sFromDate,103) '
			else
				SET @Sql = @Sql + ' AND [t0].[Created] >=  convert(datetime,@sFromDate,103) '				
		end
	IF (@sFromDate IS NULL and @iStatus =2)
		begin
			set @sFromDate = CAST(DATEADD(DAY,-DAY(GETDATE())+1, CAST(GETDATE() AS DATE)) AS DATETIME);
			SET @Sql = @Sql + ' AND [t0].[OrderSend] >= convert(datetime, @sFromDate, 103) '
		end
	IF(@sToDate IS NOT NULL)
		begin
			if (@iStatus =2)
				SET @Sql = @Sql + ' AND [t0].[OrderSend] <= convert(datetime,@sToDate,103) '	
			else
				begin
					--set @sToDate = @sToDate + ' 23:59:59'
					SET @Sql = @Sql + ' AND [t0].[Created] <= convert(datetime,@sToDate,103) '	
				end				
		end		
	
	IF(@iStatus IS NOT NULL)
		SET @Sql = @Sql + ' AND [t0].[Status] = @iStatus '
	IF(@iProvince > 0)
		SET @Sql = @Sql + ' AND [t0].[ProvinceID] = @iProvince '
	IF(@iDistrict > 0)
		SET @Sql = @Sql + ' AND [t0].[DistrictID] = @iDistrict '
	-- END Where
	SET @Sql = @Sql + ') AS [t1] LEFT JOIN (SELECT [OrderId],SUM([Price]) AS [SumVas] from [dbo].[CMS_OrderVAS] group by [OrderId]) AS [OV] ON [t1].[OrderID] = [OV].[OrderID] '
	SET @Sql = @Sql + ' LEFT JOIN [CMS_Inventory] AS [inv] ON [t1].[InventoryID] = [inv].[InventoryID] '
	SET @Sql = @Sql + ' LEFT JOIN [CMS_CustomerClass] AS [cus] ON [t1].[CusClassID] = [cus].[CusClassID] '
	SET @Sql = @Sql + ' LEFT JOIN [ADM_Users] AS [u] ON [t1].[SaleID] = [u].[UserID] '
	--SET @Sql = @Sql + ' LEFT JOIN (select t.orderid,t.shipperid, s.fullname from [CMS_Tickets] t, cms_shippers s where t.shipperid = s.shipperid) AS [tic] ON [t1].[OrderID] = [tic].[OrderID] '
	SET @Sql = @Sql + 'WHERE [t1].[RowNumer] >= ((@PageIndex - 1) * @PageSize + 1)  AND [t1].[RowNumer] <= (@PageIndex * @PageSize)'
	--select @Sql
	
	EXEC SP_EXECUTESQL @Sql, @Parameter, @PageIndex, @PageSize, @OrderBy, @sOrderCode, @iStatus, @iProvince , @iDistrict, @iOType, @guidSaleID, @SaleID, @InvID, @ShipperID  ,	@sFromDate ,@sToDate, @CusID
print @Sql
END
GO

/****** Object:  Table [dbo].[CMS_InvProductPrice] ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CMS_InvProductPrice](
	[ID] [uniqueidentifier] NOT NULL,
	[InventoryID] [uniqueidentifier] NULL,
	[ProductID] [uniqueidentifier] NULL,
	[SaleTypeID] [uniqueidentifier] NULL,
	[SalePrice] [decimal](18, 2) NULL,
	[DealerPrice] [decimal](18, 2) NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_CMS_InvProductPrice] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[CMS_InvProductPrice] ADD  CONSTRAINT [DF_CMS_InvProductPrice_ID]  DEFAULT (newid()) FOR [ID]
GO

ALTER TABLE [dbo].[CMS_InvProductPrice] ADD  CONSTRAINT [DF_CMS_InvProductPrice_UpdatedDate]  DEFAULT (getdate()) FOR [UpdatedDate]
GO

IF OBJECT_ID('[dbo].[sp_CMS_InvProductPriceSelect]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[sp_CMS_InvProductPriceSelect] 
END 
GO
CREATE PROC [dbo].[sp_CMS_InvProductPriceSelect] 
    @ID UNIQUEIDENTIFIER
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [ID], [InventoryID], [ProductID], [SaleTypeID], [SalePrice], [DealerPrice], [UpdatedDate] 
	FROM   [dbo].[CMS_InvProductPrice] 
	WHERE  ([ID] = @ID OR @ID IS NULL) 

	COMMIT
GO

IF OBJECT_ID('[dbo].[sp_CMS_InvProductPriceInsert]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[sp_CMS_InvProductPriceInsert] 
END 
GO
CREATE PROC [dbo].[sp_CMS_InvProductPriceInsert] 
    @ID uniqueidentifier,
    @InventoryID uniqueidentifier,
    @ProductID uniqueidentifier,
    @SaleTypeID uniqueidentifier,
    @SalePrice decimal(18, 2),
    @UpdatedDate datetime,
    @DealerPrice decimal(18, 2)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[CMS_InvProductPrice] ([ID], [InventoryID], [ProductID], [SaleTypeID], [SalePrice], [DealerPrice], [UpdatedDate])
	SELECT @ID, @InventoryID, @ProductID, @SaleTypeID, @SalePrice, @DealerPrice, @UpdatedDate
	
	-- Begin Return Select 
	SELECT [ID], [InventoryID], [ProductID], [SaleTypeID], [SalePrice], [DealerPrice], [UpdatedDate]
	FROM   [dbo].[CMS_InvProductPrice]
	WHERE  [ID] = @ID
	-- End Return Select 
               
	COMMIT
GO

IF OBJECT_ID('[dbo].[sp_CMS_InvProductPriceDelete]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[sp_CMS_InvProductPriceDelete] 
END 
GO
CREATE PROC [dbo].[sp_CMS_InvProductPriceDelete] 
    @ID uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[CMS_InvProductPrice]
	WHERE  [ID] = @ID

	COMMIT
GO

ALTER PROCEDURE [dbo].[sp_CMS_Products_SelectAll]
	@PageIndex int,
	@PageSize int,
	@OrderBy varchar(100),
	@guidCategoryID uniqueidentifier,
	@sType varchar(100),
	@sTitle nvarchar(200),
	@iStatus int,
	@guidInventoryID uniqueidentifier
AS
SET NOCOUNT ON
BEGIN
	DECLARE @Sql nvarchar(max)
	
	DECLARE @Parameter nvarchar(500)
	SET @Parameter =N'@PageIndex int, @PageSize int, @OrderBy varchar(100), @guidCategoryID uniqueidentifier, @sType varchar(100), @sTitle nvarchar(200),@iStatus int, @guidInventoryID uniqueidentifier'
	SET @Sql = 'SELECT TOP (@PageIndex * @PageSize) [t1].[TotalRows],
				[t1].[ProductID], [t1].[Name], [t1].[Order],[t1].[Title], [t1].[Quantity], [t1].[RealPrice], [t1].[Promotion], [t1].[DiscountPercent] ,[t1].[SalePrice],[t1].[Type], [t1].[Status], [t1].[SKU],[t1].[SalePriceDealer] FROM (SELECT TotalRows = COUNT(*) OVER(), '
	-- BEGIN Order by
	SET @Sql = @Sql + CASE (@OrderBy)
		WHEN 'TITLE DESC' THEN ' ROW_NUMBER() OVER (ORDER BY [t0].[TITLE] DESC) AS [RowNumer], '
		WHEN 'TITLE ASC' THEN ' ROW_NUMBER() OVER (ORDER BY [t0].[TITLE]) AS [RowNumer], '
		WHEN 'QUANTITY DESC' THEN ' ROW_NUMBER() OVER (ORDER BY [t0].[QUANTITY] DESC) AS [RowNumer], '
		WHEN 'QUANTITY ASC' THEN ' ROW_NUMBER() OVER (ORDER BY [t0].[QUANTITY]) AS [RowNumer], '
		WHEN 'SALEPRICE DESC' THEN ' ROW_NUMBER() OVER (ORDER BY [t0].[SALEPRICE] DESC) AS [RowNumer], '
		WHEN 'SALEPRICE ASC' THEN ' ROW_NUMBER() OVER (ORDER BY [t0].[SALEPRICE]) AS [RowNumer], '
		WHEN 'ORDER DESC' THEN ' ROW_NUMBER() OVER (ORDER BY [t0].[Order] DESC) AS [RowNumer], '
		WHEN 'ORDER ASC' THEN ' ROW_NUMBER() OVER (ORDER BY [t0].[Order]) AS [RowNumer], '
		ELSE ' ROW_NUMBER() OVER (ORDER BY [t0].[Created] DESC) AS [RowNumer], '
	END
	-- END Order by 
	SET @Sql = @Sql + '	[t0].[ProductID], [ca].[Name], [t0].[Order],[t0].[Title], [t0].[Quantity], [t0].[RealPrice], [t0].[Promotion], [t0].[DiscountPercent] ,[t0].[Type], [t0].[Status], [t0].[SKU],ISNULL([ipp].[SalePrice],0) [SalePrice],ISNULL([ipp].[DealerPrice],0) [SalePriceDealer]
						FROM [dbo].[CMS_Products] AS [t0]
						INNER JOIN [dbo].[CMS_Categories] AS [ca ] ON [t0].[CategoryID] = [ca].[CategoryID] 
						LEFT JOIN (SELECT * FROM [dbo].[CMS_InvProductPrice] WHERE [InventoryId]=@guidInventoryID) AS [ipp] ON [t0].ProductID = [ipp].[ProductId] 
						WHERE 1=1 '
	-- BEGIN Where
	IF(@sTitle IS NOT NULL)
	BEGIN
		SET @Sql = @Sql + ' AND ([t0].[SKU] LIKE @sTitle OR [t0].[ProductBarcode] LIKE @sTitle '
		SET @sTitle='%'+@sTitle+'%'
		SET @Sql = @Sql + ' OR [t0].[Title] LIKE @sTitle) '
	END
	IF(@sType IS NOT NULL)
	BEGIN
		SET @sType='%' + @sType + '%'
		SET @Sql = @Sql + ' AND [t0].[Type] LIKE @sType '
	END
	IF(@guidCategoryID IS NOT NULL)
		SET @Sql = @Sql + ' AND [t0].[CategoryID] = @guidCategoryID '
	IF(@iStatus IS NOT NULL)
		SET @Sql = @Sql + ' AND [t0].[Status] = @iStatus '
	-- END Where
	SET @Sql = @Sql + ') AS [t1] WHERE [t1].[RowNumer] >= ((@PageIndex - 1) * @PageSize + 1)  AND [t1].[RowNumer] <= (@PageIndex * @PageSize)'
	--select @Sql
	print @Sql
	EXEC SP_EXECUTESQL @Sql, @Parameter, @PageIndex, @PageSize, @OrderBy, @guidCategoryID, @sType, @sTitle, @iStatus, @guidInventoryID
END
GO

ALTER PROCEDURE [dbo].[sp_CMS_Products_SelectAll3]	
	@sTitle nvarchar(200),
	@guidInventoryID uniqueidentifier
AS
SET NOCOUNT ON
BEGIN
	SET @sTitle = '%'+@sTitle+'%'
	SELECT [p].[ProductID] AS [productid], [Title] AS [title], [SKU] AS [sku], [Image] AS [image], 
			ISNULL([ipp].[SalePrice],0) AS [saleprice], ISNULL([ipp].[DealerPrice],0) [salepricedealer],
			ISNULL([ivd].[Quantity],0) AS [quantity] 
		FROM [CMS_Products] AS [p]
		LEFT JOIN [CMS_InventoryDetails] AS [ivd] ON [p].[ProductID] = [ivd].[ProductID] AND [ivd].[InventoryID] = @guidInventoryID
		LEFT JOIN [CMS_InvProductPrice] AS [ipp] ON [p].[ProductID] = [ipp].[ProductID] AND [ipp].[InventoryID] = @guidInventoryID
		WHERE [SKU] LIKE @sTitle
END
GO

ALTER PROCEDURE [dbo].[sp_CMS_Products_SelectByList]
	@List varchar(max)
AS
SET NOCOUNT ON
BEGIN
	SELECT TOP 100 [p].[Title], [p].[Image], [p].[MetaTitle], [p].[SEOName], ISNULL([pp].[SalePrice],0) AS [SalePrice]
	FROM [dbo].[CMS_Products] AS [p]
	LEFT JOIN [dbo].[CMS_InvProductPrice] AS [pp] ON [p].[ProductID] = [pp].[ProductID] AND [pp].[InventoryID] = '0C80DCD0-5D8E-4041-ACAF-2CF7C2916162'
	WHERE [p].[ProductID] IN (SELECT [Data] FROM [dbo].[Split](@List, ','))
END
GO

ALTER PROCEDURE [dbo].[sp_CMS_Products_SelectByTags]
	@sTags nvarchar(200)
AS
SET NOCOUNT ON
BEGIN
	SELECT TOP 20 [p].[ProductID], [p].[Title], ISNULL([pp].[SalePrice],0) AS [SalePrice], [p].[Image], [p].[MetaTitle], [p].[SEOName], [p].[Status],[p].[Tags], 
				[p].[Created], [p].[MetaKeyword],[p].[MetaTitle], [p].[Brief], [p].[TotalComment]
	FROM [dbo].[CMS_Products] AS [p]
	LEFT JOIN [dbo].[CMS_InvProductPrice] AS [pp] ON [p].[ProductID] = [pp].[ProductID] AND [pp].[InventoryID] = '0C80DCD0-5D8E-4041-ACAF-2CF7C2916162'
	WHERE [p].[Tags] like  '%' + @sTags + '%'
END
GO

ALTER PROCEDURE [dbo].[sp_CMS_Products_SelectByType]
	@sType varchar(100),
	@Top int
AS
SET NOCOUNT ON
BEGIN
SELECT TOP (@Top) [p].[ProductID], [p].[SKU],[p].[Brief], [p].[Title], [p].[SEOName],[p].[Type], [p].[ImportPrice], ISNULL([pp].[SalePrice],0) AS [SalePrice], [p].[RealPrice],
		[p].[MetaTitle],[p].[DiscountPercent],[p].[Image],[p].[Promotion], [p].[Brief], [p].[Visitor], [p].[Created], [p].[TotalComment]
	FROM [dbo].[CMS_Products] AS [p]
	LEFT JOIN [dbo].[CMS_InvProductPrice] AS [pp] ON [p].[ProductID] = [pp].[ProductID] AND [pp].[InventoryID] = '0C80DCD0-5D8E-4041-ACAF-2CF7C2916162'
	WHERE [p].[Status] = 1 AND [p].[Type] like '%' + @sType + '%'
	ORDER BY [p].[Order], [p].[Created] DESC
END
GO

ALTER PROCEDURE [dbo].[sp_CMS_Products_SelectByCategory]
	@PageIndex int,
	@PageSize int,
	@OrderBy varchar(100),
	@sTitle nvarchar(200),
	@ManufactureCode varchar(100),
	@FilterCode varchar(100),
	@PriceFrom numeric(18,2),
	@PriceTo numeric(18,2),
	@Type varchar(100),
	@guidCategoryID uniqueidentifier
AS
SET NOCOUNT ON
BEGIN
	DECLARE @Sql nvarchar(max)
	
	DECLARE @Parameter nvarchar(500)
	DECLARE @ManufactureID uniqueidentifier
	
	CREATE TABLE #CATE(CategoryID uniqueidentifier)
	INSERT INTO #CATE EXECUTE [dbo].[sp_CMS_Categories_SelectAllChild] @guidCategoryID

	SET @Parameter =N'@PageIndex int, @PageSize int, @OrderBy varchar(100), @sTitle nvarchar(200), @ManufactureCode varchar(100), @FilterCode varchar(100), @PriceFrom numeric(18,2), @PriceTo numeric(18,2), @Type varchar(100), @guidCategoryID uniqueidentifier '
	SET @Sql = 'SELECT TOP (@PageIndex * @PageSize) [t1].[TotalRows],
		[t1].[ProductID], [t1].[Title], [t1].[SKU],[t1].[Image], [t1].[DiscountPercent],[t1].[SalePriceHCM],[t1].[SalePrice], [t1].[MetaTitle], [t1].[MetaKeyword], 
		[t1].[SEOName], [t1].[Status], [t1].[Created], [t1].[Brief],[t1].[TotalComment], [t1].[Modified], [t1].[Tags],
		[t1].[RealPrice], [t1].[Promotion] ,[t1].[Type]
	FROM (SELECT TotalRows = COUNT(*) OVER(), '
	SET @Sql = @Sql + CASE (@OrderBy)
		WHEN 'SALEPRICE ASC' THEN ' ROW_NUMBER() OVER (ORDER BY [SALEPRICE]) AS [RowNumer], '
		WHEN 'SALEPRICE DESC' THEN ' ROW_NUMBER() OVER (ORDER BY [SALEPRICE] DESC) AS [RowNumer], '
		WHEN 'Created ASC' THEN ' ROW_NUMBER() OVER (ORDER BY [Created]) AS [RowNumer], '
		ELSE ' ROW_NUMBER() OVER (ORDER BY [Order],[Created] DESC) AS [RowNumer], '
	END
	-- END Order by
	SET @Sql = @Sql + ' [t0].[ProductID], [t0].[Title], [t0].[SKU],[t0].[Image], [t0].[DiscountPercent],[t0].[SalePriceHCM],ISNULL([pp].[SalePrice],0) AS [SalePrice], [t0].[MetaTitle],
					[t0].[MetaKeyword], [t0].[SEOName], [t0].[Status], [t0].[Created], [t0].[Brief],[t0].[TotalComment], 
					[t0].[Modified], [t0].[Tags], [t0].[RealPrice], [t0].[Promotion] ,[t0].[Type]
		FROM [dbo].[CMS_Products] AS [t0]
		LEFT JOIN [dbo].[CMS_InvProductPrice] AS [pp] ON [t0].[ProductID] = [pp].[ProductID] AND [pp].[InventoryID] = ''0C80DCD0-5D8E-4041-ACAF-2CF7C2916162''
		WHERE [t0].[Status] = 1 '
	IF(@sTitle IS NOT NULL)
	BEGIN
		SET @Sql = @Sql + ' AND ([t0].[SKU] = @sTitle '
		SET @sTitle='%' + @sTitle + '%'
		SET @Sql = @Sql + ' OR [t0].[Title] LIKE @sTitle)'
		--SET @Sql = @Sql + ' AND (FREETEXT([t0].[Title], @sTitle) OR [t0].[SKU] = @sTitle)'
	END
	IF(@PriceFrom IS NOT NULL)
	BEGIN
		SET @Sql = @Sql + ' AND [t0].[SalePrice] >= @PriceFrom '
	END
	IF(@PriceTo IS NOT NULL)
	BEGIN
		SET @Sql = @Sql + ' AND [t0].[Type] <= @PriceTo '
	END
	IF(@Type IS NOT NULL AND @Type != 'DISCOUNT')
	BEGIN
		SET @Type='%'+@Type+'%'
		SET @Sql = @Sql + ' AND [t0].[Type] LIKE @Type '
	END
	IF(@Type IS NOT NULL AND @Type = 'DISCOUNT')
	BEGIN
		SET @Sql = @Sql + ' AND [t0].[DiscountPercent] > 0 '
	END
	IF(@FilterCode IS NOT NULL)
	BEGIN
		SET @FilterCode='%'+@FilterCode+'%'
		SET @Sql = @Sql + ' AND [t0].[Filter] LIKE @FilterCode '
	END
	IF @ManufactureCode IS NOT NULL
	BEGIN
		SELECT TOP 1 @ManufactureID = [AttributeID] FROM [dbo].[CMS_Attributes] WHERE [Code] = @ManufactureCode 
		SET @Sql = @Sql + ' AND [t0].[ManufactureID] = @ManufactureID '
	END
	IF(@guidCategoryID IS NOT NULL)
	BEGIN
		--SET @Sql = @Sql + ' AND [t0].[CategoryID] = @guidCategoryID '
		SET @Sql = @Sql + ' AND ([t0].[CategoryID] = @guidCategoryID OR [t0].[CategoryID] IN(SELECT [CategoryID] FROM #CATE)) '
	END
		
	SET @Sql = @Sql + ') AS [t1] WHERE [t1].[RowNumer] >= ((@PageIndex - 1) * @PageSize + 1)  AND [t1].[RowNumer] <= (@PageIndex * @PageSize)'
	--select @Sql
	EXEC SP_EXECUTESQL @Sql, @Parameter, @PageIndex, @PageSize, @OrderBy, @sTitle, @ManufactureCode, @FilterCode, @PriceFrom, @PriceTo, @Type, @guidCategoryID
END
GO
--------------------


--sonln 2016/03/02
--Add phone1 and phone2 to CMS_Users table
IF NOT EXISTS (select 1 from sys.columns where name='Phone1' and object_id = OBJECT_ID('CMS_Users'))
ALTER TABLE CMS_Users
	ADD Phone1 NVARCHAR(20) NULL
GO

IF NOT EXISTS (select 1 from sys.columns where name='Phone2' and object_id = OBJECT_ID('CMS_Users'))
ALTER TABLE CMS_Users
	ADD Phone2 NVARCHAR(20) NULL
GO

--edit
ALTER PROCEDURE [dbo].[sp_CMS_Users_SelectAll]
	@PageIndex int,
	@PageSize int,
	@OrderBy varchar(100),
	@guidGroupID uniqueidentifier,
	@sUserName nvarchar(200),
	@iStatus int
AS
BEGIN
	DECLARE @Sql nvarchar(max)
	DECLARE @Parameter nvarchar(500)
	SET @Parameter =N'@PageIndex int, @PageSize int, @OrderBy varchar(100), @guidGroupID uniqueidentifier, @sUserName nvarchar(200), @iStatus int'
	SET @Sql = 'SELECT TOP (@PageIndex * @PageSize) [t1].[TotalRows],
				[t1].[UserID],[t1].[GroupID], [t1].[UserName],[t1].[FullName], [t1].[Email], [t1].[Address], [t1].[Phone], [t1].[Phone1], [t1].[Phone2], [t1].[Brithday], [t1].[GoogleID] , [t1].[Status], [t1].[Views]
				FROM (SELECT TotalRows = COUNT(*) OVER(), '
	-- BEGIN Order by
	SET @Sql = @Sql + CASE (@OrderBy)
		WHEN 'USERNAME DESC' THEN ' ROW_NUMBER() OVER (ORDER BY [t0].[UserName] DESC) AS [RowNumer], '
		WHEN 'USERNAME ASC' THEN ' ROW_NUMBER() OVER (ORDER BY [t0].[UserName]) AS [RowNumer], '
		WHEN 'FULLNAME DESC' THEN ' ROW_NUMBER() OVER (ORDER BY [t0].[FullName] DESC) AS [RowNumer], '
		WHEN 'FULLNAME ASC' THEN ' ROW_NUMBER() OVER (ORDER BY [t0].[FullName]) AS [RowNumer], '
		WHEN 'EMAIL DESC' THEN ' ROW_NUMBER() OVER (ORDER BY [t0].[EMAIL] DESC) AS [RowNumer], '
		WHEN 'EMAIL ASC' THEN ' ROW_NUMBER() OVER (ORDER BY [t0].[EMAIL]) AS [RowNumer], '
		WHEN 'VIEWS DESC' THEN ' ROW_NUMBER() OVER (ORDER BY [t0].[VIEWS] DESC) AS [RowNumer], '
		WHEN 'VIEWS ASC' THEN ' ROW_NUMBER() OVER (ORDER BY [t0].[VIEWS]) AS [RowNumer], '
		ELSE ' ROW_NUMBER() OVER (ORDER BY [t0].[Created] DESC) AS [RowNumer], '
	END
	-- END Order by
	SET @Sql = @Sql + '	[t0].[UserID],[t0].[GroupID], [t0].[UserName],[t0].[FullName], [t0].[Email], [t0].[Address], [t0].[Phone], [t0].[Phone1], [t0].[Phone2], [t0].[Brithday], [t0].[GoogleID], [t0].[Status], [t0].[Views] 
						FROM [dbo].[CMS_Users] AS [t0] WHERE 1=1 '
	-- BEGIN Where
	IF(@sUserName IS NOT NULL)
	BEGIN
		SET @sUserName='%' + @sUserName + '%'
		SET @Sql = @Sql + ' AND ([t0].[UserName] LIKE @sUserName OR [t0].[Email] LIKE @sUserName OR [t0].[UserID] LIKE  @sUserName ) '
	END
	IF(@guidGroupID IS NOT NULL)
		SET @Sql = @Sql + ' AND [t0].[GroupID] = @guidGroupID '
	IF(@iStatus IS NOT NULL)
		SET @Sql = @Sql + ' AND [t0].[Status] = @iStatus '
	ELSE
		SET @Sql = @Sql + ' AND [t0].[Status] > 0 '
	-- END Where
	SET @Sql = @Sql + ') AS [t1] WHERE [t1].[RowNumer] >= ((@PageIndex - 1) * @PageSize + 1)  AND [t1].[RowNumer] <= (@PageIndex * @PageSize)'
	--select @Sql
	EXEC SP_EXECUTESQL @Sql, @Parameter, @PageIndex, @PageSize, @OrderBy, @guidGroupID, @sUserName, @iStatus
END
GO
--edit
ALTER PROCEDURE [dbo].[sp_CMS_Users_SelectAllWithOrder]
	@PageIndex int,
	@PageSize int,
	@OrderBy varchar(100),
	@guidGroupID uniqueidentifier,
	@sUserName nvarchar(200),
	@iStatus int
AS
SET NOCOUNT ON
BEGIN
	DECLARE @Sql nvarchar(max)
	DECLARE @Parameter nvarchar(500)
	SET @Parameter =N'@PageIndex int, @PageSize int, @OrderBy varchar(100), @guidGroupID uniqueidentifier, @sUserName nvarchar(200), @iStatus int'
	SET @Sql = 'SELECT TOP (@PageIndex * @PageSize) [t1].[TotalRows],
				[t1].[UserID],[t1].[GroupID], [t1].[UserName],[t1].[FullName], [t1].[Email], [t1].[Address], [t1].[Phone], [t1].[Phone1], [t1].[Phone2], [t1].[Brithday], [t1].[GoogleID] , [t1].[Status], [t1].[Views], ISNULL([uo].[SumOrders],0) AS [SumOrders]
				FROM (SELECT TotalRows = COUNT(*) OVER(), '
	-- BEGIN Order by
	SET @Sql = @Sql + CASE (@OrderBy)
		WHEN 'USERNAME DESC' THEN ' ROW_NUMBER() OVER (ORDER BY [t0].[UserName] DESC) AS [RowNumer], '
		WHEN 'USERNAME ASC' THEN ' ROW_NUMBER() OVER (ORDER BY [t0].[UserName]) AS [RowNumer], '
		WHEN 'FULLNAME DESC' THEN ' ROW_NUMBER() OVER (ORDER BY [t0].[FullName] DESC) AS [RowNumer], '
		WHEN 'FULLNAME ASC' THEN ' ROW_NUMBER() OVER (ORDER BY [t0].[FullName]) AS [RowNumer], '
		WHEN 'EMAIL DESC' THEN ' ROW_NUMBER() OVER (ORDER BY [t0].[EMAIL] DESC) AS [RowNumer], '
		WHEN 'EMAIL ASC' THEN ' ROW_NUMBER() OVER (ORDER BY [t0].[EMAIL]) AS [RowNumer], '
		WHEN 'VIEWS DESC' THEN ' ROW_NUMBER() OVER (ORDER BY [t0].[VIEWS] DESC) AS [RowNumer], '
		WHEN 'VIEWS ASC' THEN ' ROW_NUMBER() OVER (ORDER BY [t0].[VIEWS]) AS [RowNumer], '
		ELSE ' ROW_NUMBER() OVER (ORDER BY [t0].[Created] DESC) AS [RowNumer], '
	END
	-- END Order by
	SET @Sql = @Sql + '	[t0].[UserID],[t0].[GroupID], [t0].[UserName],[t0].[FullName], [t0].[Email], [t0].[Address], [t0].[Phone], [t0].[Phone1], [t0].[Phone2], [t0].[Brithday], [t0].[GoogleID], [t0].[Status], [t0].[Views] 
						FROM [dbo].[CMS_Users] AS [t0] WHERE 1=1 '
	-- BEGIN Where
	IF(@sUserName IS NOT NULL)
	BEGIN
		SET @sUserName='%' + @sUserName + '%'
		SET @Sql = @Sql + ' AND ([t0].[UserName] LIKE @sUserName OR [t0].[Email] LIKE @sUserName OR [t0].[UserID] LIKE  @sUserName ) '
	END
	IF(@guidGroupID IS NOT NULL)
		SET @Sql = @Sql + ' AND [t0].[GroupID] = @guidGroupID '
	IF(@iStatus IS NOT NULL)
		SET @Sql = @Sql + ' AND [t0].[Status] = @iStatus '
	ELSE
		SET @Sql = @Sql + ' AND [t0].[Status] > 0 '
	-- END Where
	SET @Sql = @Sql + ') AS [t1] 
	LEFT JOIN (SELECT [UserID], SUM([TotalPrice]) AS [SumOrders] FROM [dbo].[CMS_Orders] WHERE [UserID] IS NOT NULL GROUP BY [UserID]) AS [uo] ON [t1].[UserID] = [uo].[UserID]
	WHERE [t1].[RowNumer] >= ((@PageIndex - 1) * @PageSize + 1)  AND [t1].[RowNumer] <= (@PageIndex * @PageSize)'
	print @Sql
	EXEC SP_EXECUTESQL @Sql, @Parameter, @PageIndex, @PageSize, @OrderBy, @guidGroupID, @sUserName, @iStatus
END
GO
--edit
ALTER PROCEDURE [dbo].[sp_CMS_Users_Insert]
	@guidUserID uniqueidentifier,
	@guidGroupID uniqueidentifier,
	@sFullName nvarchar(200),
	@sUserName varchar(200),
	@sPassword varchar(200),
	@iGender int,
	@sEmail varchar(200),
	@sAddress nvarchar(200),
	@sPhone nvarchar(200),
	@sPhone1 nvarchar(200),
	@sPhone2 nvarchar(200),
	@daBrithday datetime,
	@sImage nvarchar(500),
	@sGoogleID varchar(50),
	@sPermission varchar(500),
	@sSignature nvarchar(500),
	@iStatus int,
	@daCreated datetime,
	@iViews int,
	@iTotalAnswer int,
	@daDateLogin datetime,
	@sFollow varchar(max),
	@iSendMail int,
	@sTaxNumber nvarchar(50)
AS
SET NOCOUNT ON
-- INSERT a new row in the table.
INSERT [dbo].[CMS_Users]
(
	[UserID],
	[GroupID],
	[FullName],
	[UserName],
	[Password],
	[Gender],
	[Email],
	[Address],
	[Phone],
	[Phone1],
	[Phone2],
	[Brithday],
	[Image],
	[GoogleID],
	[Permission],
	[Signature],
	[Status],
	[Created],
	[Views],
	[TotalAnswer],
	[DateLogin],
	[Follow],
	[SendMail],
	[TaxNumber] 
)
VALUES
(
	@guidUserID,
	@guidGroupID,
	@sFullName,
	@sUserName,
	@sPassword,
	@iGender,
	@sEmail,
	@sAddress,
	@sPhone,
	@sPhone1,
	@sPhone2,
	@daBrithday,
	@sImage,
	@sGoogleID,
	@sPermission,
	@sSignature,
	@iStatus,
	@daCreated,
	@iViews,
	@iTotalAnswer,
	@daDateLogin,
	@sFollow,
	@iSendMail,
	@sTaxNumber 
)
GO
--edit
ALTER PROCEDURE [dbo].[sp_CMS_Users_Update]
	@guidUserID uniqueidentifier,
	@guidGroupID uniqueidentifier,
	@sFullName nvarchar(200),
	@sUserName varchar(200),
	@sPassword varchar(200),
	@iGender int,
	@sEmail varchar(200),
	@sAddress nvarchar(200),
	@sPhone nvarchar(200),
	@sPhone1 nvarchar(200),
	@sPhone2 nvarchar(200),
	@daBrithday datetime,
	@sImage nvarchar(500),
	@sGoogleID varchar(50),
	@sPermission varchar(500),
	@sSignature nvarchar(500),
	@iStatus int,
	@daCreated datetime,
	@iViews int,
	@iTotalAnswer int,
	@daDateLogin datetime,
	@sFollow varchar(max),
	@iSendMail int,
	@sTaxNumber nvarchar(50)
AS
SET NOCOUNT ON
-- UPDATE an existing row in the table.
UPDATE [dbo].[CMS_Users]
SET 
	[GroupID] = ISNULL(@guidGroupID,[GroupID]),
	[FullName] = ISNULL(@sFullName,[FullName]),
	[UserName] = ISNULL(@sUserName,[UserName]),
	[Password] = ISNULL(@sPassword,[Password]),
	[Gender] = ISNULL(@iGender,[Gender]),
	[Email] = ISNULL(@sEmail,[Email]),
	[Address] = ISNULL(@sAddress,[Address]),
	[Phone] = ISNULL(@sPhone,[Phone]),
	[Phone1] = ISNULL(@sPhone1,[Phone1]),
	[Phone2] = ISNULL(@sPhone2,[Phone2]),
	[Brithday] = ISNULL(@daBrithday,[Brithday]),
	[Image] = ISNULL(@sImage,[Image]),
	[GoogleID] = ISNULL(@sGoogleID,[GoogleID]),
	[Permission] = ISNULL(@sPermission,[Permission]),
	[Signature] = ISNULL(@sSignature,[Signature]),
	[Status] = ISNULL(@iStatus,[Status]),
	[Created] = ISNULL(@daCreated,[Created]),
	[Views] = ISNULL(@iViews,[Views]),
	[TotalAnswer] = ISNULL(@iTotalAnswer,[TotalAnswer]),
	[DateLogin] = ISNULL(@daDateLogin,[DateLogin]),
	[Follow] = ISNULL(@sFollow,[Follow]),
	[SendMail] = ISNULL(@iSendMail,1),
	[TaxNumber] = @sTaxNumber 
WHERE
	[UserID] = @guidUserID
GO
--edit 
ALTER PROCEDURE [dbo].[sp_CMS_Users_SelectOneWEmailLogic]
	@sEmail varchar(200)
AS
SET NOCOUNT ON
-- SELECT an existing row from the table.
SELECT
	[UserID],
	[GroupID],
	[FullName],
	[UserName],
	[Password],
	[Gender],
	[Email],
	[Address],
	[Phone],
	[Phone1],
	[Phone2],
	[Brithday],
	[Image],
	[GoogleID],
	[Permission],
	[Signature],
	[Status],
	[Created],
	[Views],
	[TotalAnswer],
	[DateLogin],
	[Follow]
FROM [dbo].[CMS_Users]
WHERE
	[Email] = @sEmail
GO
--edit
ALTER PROCEDURE [dbo].[sp_CMS_Users_SelectOneWUserNameLogic]
	@sUserName varchar(200)
AS
SET NOCOUNT ON
-- SELECT an existing row from the table.
SELECT
	[UserID],
	[GroupID],
	[FullName],
	[UserName],
	[Password],
	[Gender],
	[Email],
	[Address],
	[Phone],
	[Phone1],
	[Phone2],
	[Brithday],
	[Image],
	[GoogleID],
	[Permission],
	[Signature],
	[Status],
	[Created],
	[Views],
	[TotalAnswer],
	[DateLogin],
	[Follow]
FROM [dbo].[CMS_Users]
WHERE
	[UserName] = @sUserName
GO
-------------------------
ALTER PROCEDURE [dbo].[sp_CMS_Users_SelectOne]
	@guidUserID uniqueidentifier
AS
SET NOCOUNT ON
SELECT
	[UserID],
	[GroupID],
	[FullName],
	[UserName],
	[Password],
	[Gender],
	[Email],
	[Address],
	[Phone],
	[Phone1],
	[Phone2],
	[Brithday],
	[Image],
	[GoogleID],
	[Permission],
	[Signature],
	[Status],
	[Created],
	[Views],
	[TotalAnswer],
	[DateLogin],
	[Follow],
	[SendMail],
	[TaxNumber]
FROM [dbo].[CMS_Users]
WHERE
	[UserID] = @guidUserID

/****** Create:  Table [dbo].[CMS_Warranty] ******/

CREATE TABLE [dbo].[CMS_Warranty](
	[ID] [uniqueidentifier] NOT NULL,
	[UserID] [uniqueidentifier] NULL,
	[Phone] [nvarchar](50) NULL,
	[Name] [nvarchar](50) NULL,
	[DateCreated] [datetime] NULL,
	[DateComplete] [datetime] NULL,
	[Status] [int] NULL,
 CONSTRAINT [PK_CMS_Warranty] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[CMS_Warranty] ADD  CONSTRAINT [DF_CMS_Warranty_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO

IF OBJECT_ID('[dbo].[sp_CMS_WarrantyDelete]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[sp_CMS_WarrantyDelete] 
END 
GO
CREATE PROC [dbo].[sp_CMS_WarrantyDelete] 
    @ID uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	DELETE
	FROM   [dbo].[CMS_WarrantyProduct]
	WHERE  [WarrantyID] = @ID
	
	DELETE
	FROM   [dbo].[CMS_Warranty]
	WHERE  [ID] = @ID

	COMMIT
GO

--khanhhq
IF NOT EXISTS (select 1 from sys.columns where name='Phone1' and object_id = OBJECT_ID('CMS_Orders'))
ALTER TABLE CMS_Orders
	ADD Phone1 NVARCHAR(20) NULL
Go

IF NOT EXISTS (select 1 from sys.columns where name='Phone2' and object_id = OBJECT_ID('CMS_Orders'))
ALTER TABLE CMS_Orders
	ADD Phone2 NVARCHAR(20) NULL
Go

ALTER PROCEDURE [dbo].[sp_CMS_Orders_Update_By_OrderDetail]
	@guidOrderID uniqueidentifier,
	@guidSaleID uniqueidentifier,
	@guidUserID uniqueidentifier,
	@sFullName nvarchar(200),
	@sAddress nvarchar(200),
	@sEmail nvarchar(200),
	@sPhone nvarchar(200),
	@sPhone1 nvarchar(200),
	@sPhone2 nvarchar(200),
	@sRequest nvarchar(1000),
	@dcTotalPrice numeric(18, 2),
	@sComment nvarchar(1000),
	@iTax int,
	@dcTransportFee numeric(18, 2),
	@dcDiscount numeric(18, 2),
	@iStatus int,
	@iDistrict int,
	@iProvince int,
	@guidPaymentID uniqueidentifier,
	@daOrderSend datetime,
	@guidCusClassID uniqueidentifier,
	@guidInvID uniqueidentifier,
	@sBookNumber nvarchar(50),
	@iPageNUmber int,
	@dcPrePayment decimal(18,2),
	@isDataChanged int
AS
SET NOCOUNT ON
BEGIN
	DECLARE @stt int
	SELECT @stt = [Status] FROM [dbo].[CMS_Orders] WHERE [OrderID] = @guidOrderID
	IF @stt != 2 AND @stt != 6 AND @stt != 4
	BEGIN
		IF @iStatus = 2 -- 2 la trang thai hoan tat
			BEGIN
				UPDATE [dbo].[CMS_Orders] 
				SET 
					[SaleID] = ISNULL(@guidSaleID,[SaleID]), 
					[FullName] = ISNULL(@sFullName,[FullName]),
					[Address] = ISNULL(@sAddress,[Address]),
					[Email] = ISNULL(@sEmail,[Email]),
					[Phone] = ISNULL(@sPhone,[Phone]),
					[Phone1] = ISNULL(@sPhone1,[Phone1]),
					[Phone2] = ISNULL(@sPhone2,[Phone2]),
					[Request] = ISNULL(@sRequest,[Request]),
					[Comment] = ISNULL(@sComment,[Comment]),
					[TotalPrice] = ISNULL(@dcTotalPrice,[TotalPrice]),
					[Tax] = ISNULL(@iTax,[Tax]),
					[TransportFee] = ISNULL(@dcTransportFee,[TransportFee]), 
					[Discount] = ISNULL(@dcDiscount,[Discount]),
					[Status] = ISNULL(@iStatus,[Status]),
					[DistrictId] = ISNULL(@iDistrict, [DistrictId]),
					[ProvinceId] = ISNULL(@iProvince, [ProvinceId]),
					[PaymentID] = @guidPaymentID,
					[CusClassId] = @guidCusClassID,
					[InventoryID] = ISNULL(@guidInvID,[InventoryID]),
					[OrderSend] = ISNULL(@daOrderSend,[OrderSend]),
					[PageNumber] = ISNULL(@iPageNUmber, [PageNumber]),
					[BookNumber] = ISNULL(@sBookNumber, [BookNumber]),
					[PrePayment] = ISNULL(@dcPrePayment, [PrePayment])
				WHERE [OrderID] = @guidOrderID
				EXEC sp_CMS_Orders_Update_Inventory @guidOrderID,@guidCusClassID,@guidInvID,@isDataChanged
			END
		ELSE
			BEGIN
				UPDATE [dbo].[CMS_Orders] 
				SET 
					[SaleID] = ISNULL(@guidSaleID,[SaleID]), 
					[FullName] = ISNULL(@sFullName,[FullName]),
					[Address] = ISNULL(@sAddress,[Address]),
					[Email] = ISNULL(@sEmail,[Email]),
					[Phone] = ISNULL(@sPhone,[Phone]),
					[Phone1] = ISNULL(@sPhone1,[Phone1]),
					[Phone2] = ISNULL(@sPhone2,[Phone2]),
					[Request] = ISNULL(@sRequest,[Request]),
					[Comment] = ISNULL(@sComment,[Comment]),
					[TotalPrice] = ISNULL(@dcTotalPrice,[TotalPrice]),
					[Tax] = ISNULL(@iTax,[Tax]),
					[TransportFee] = ISNULL(@dcTransportFee,[TransportFee]), 
					[Discount] = ISNULL(@dcDiscount,[Discount]),
					[Status] = ISNULL(@iStatus,[Status]),
					[DistrictId] = ISNULL(@iDistrict, [DistrictId]),
					[ProvinceId] = ISNULL(@iProvince, [ProvinceId]),
					[PaymentID] = ISNULL(@guidPaymentID,[PaymentID]),
					[CusClassId] = ISNULL(@guidCusClassID,[CusClassId]),
					[InventoryID] = ISNULL(@guidInvID,[InventoryID]),
					[OrderSend] = ISNULL(@daOrderSend,[OrderSend]),
					[PageNumber] = ISNULL(@iPageNUmber, [PageNumber]),
					[BookNumber] = ISNULL(@sBookNumber, [BookNumber]),
					[PrePayment] = ISNULL(@dcPrePayment, [PrePayment])
				WHERE [OrderID] = @guidOrderID
				EXEC sp_CMS_Orders_Update_Inventory @guidOrderID,@guidCusClassID,@guidInvID,@isDataChanged
			END
	END
END
Go
ALTER PROCEDURE [dbo].[sp_CMS_Orders_SelectOne]
	@guidOrderID uniqueidentifier
AS
SET NOCOUNT ON
	SELECT [od].[OrderID], [od].[UserID],[od].[FullName] FullNameTo, [od].[Address] AddressTo, [od].[Email] EmailTo, 
				[od].[PhoneTo] PhoneTo, [od].[Phone1To] Phone1To,[od].[Phone2To] Phone2To, [od].[SaleID], [od].[OrderCode], [od].[FullName], [od].[Address], [od].[Email], [od].[Phone], [od].[Phone1], [od].[Phone2], 
				[od].[Request], od.[Comment], od.[TotalPrice], od.[Tax], od.[TransportFee], od.[Discount], 
				--od.[TotalPrice] + od.[Tax] + od.[TransportFee] - od.[Discount] AS [Cash] , 
				ISNULL([od].[totalPrice],0) AS [Cash] , 
				[od].[Voucher], od.[Status], [od].[Created], od.[OrderSend], od.[PaymentID], [od].[ProvinceId], [od].[DistrictId], [od].[CusClassId], [od].[InventoryID], 
				[od].[TypeBuy], [od].[PrePayment], [od].[PageNumber], [od].[BookNumber],
				ISNULL([od].[totalPrice],0) AS [totalPirce]
			FROM 
			(SELECT
					od.[OrderID], od.[UserID],u.[FullName] FullNameTo, u.[Address] AddressTo, u.[Email] EmailTo, 
					u.[Phone] PhoneTo, u.[Phone1] Phone1To, u.[Phone2] Phone2To, od.[SaleID], od.[OrderCode], od.[FullName], od.[Address], od.[Email], od.[Phone], od.[Phone1], od.[Phone2], 
					od.[Request], od.[Comment], od.[TotalPrice], od.[Tax], od.[TransportFee], od.[Discount], 
					--od.[TotalPrice] + od.[Tax] + od.[TransportFee] - od.[Discount] AS [Cash] , 
					od.[TotalPrice] AS [Cash] , 
					od.[Voucher], od.[Status], od.[Created], od.[OrderSend], od.[PaymentID], [od].[ProvinceId], 
					[od].[DistrictId], [od].[CusClassId], [od].[InventoryID], 
					[od].[TypeBuy], ISNULL([od].[PrePayment],0) [PrePayment], [od].[PageNumber], [od].[BookNumber]
				FROM [dbo].[CMS_Orders] [od] LEFT JOIN [dbo].[CMS_Users] u ON od.[UserID] = u.[UserID]
				WHERE  od.[OrderID] = @guidOrderID) AS [od]
				--LEFT JOIN(SELECT [OrderID],SUM([totalPirce]) AS [totalPirce] FROM
				--	(SELECT od.[OrderID],[totalPirce] = CASE 
				--											WHEN isnull([od].[Discount],0)>=100 THEN([od].Price-isnull(od.Discount,0))*[od].Quantity
				--											WHEN isnull([od].[Discount],0)<100 THEN(([od].Price/100)*(100-isnull([od].Discount,0)))*[od].Quantity
				--										END
				--FROM [dbo].[CMS_OrderDetails] AS [od] WHERE [od].[OrderID] = @guidOrderID) AS [od] GROUP BY [OrderID]) AS [tb]				
			--ON [od].[OrderID] =[tb].[OrderID] 
			LEFT JOIN (SELECT top 1 @guidOrderID AS [OrderId], 0 AS [SumVas] FROM [dbo].[CMS_OrderVAS]) AS [vas]
			ON [od].[OrderID] = [vas].[OrderId]
GO
-- Fix update product price in Order
ALTER PROCEDURE [dbo].[sp_CMS_Products_ProductInOrder]
	@sTitle nvarchar(4000),
	@guidInvID uniqueidentifier,
	@guidCusID uniqueidentifier
AS
SET NOCOUNT ON
BEGIN


	SELECT p.[ProductID], p.[Title], p.[SEOName], p.[Image], 
			CASE
				WHEN @guidCusID='29ed76bb-b3a9-420f-aa49-ad96538465fc' THEN [ipp].[SalePrice]
				WHEN @guidCusID='d4cc298e-2e7a-4d6a-bd41-e4291cf6d929' THEN [ipp].[DealerPrice]
				ELSE [ipp].[SalePrice]
			END AS [SalePrice]   
		FROM [dbo].[CMS_Products] AS [p]
		LEFT JOIN (SELECT [ProductID], ISNULL([SalePrice],0) AS [SalePrice], ISNULL([DealerPrice], 0) AS [DealerPrice] FROM [dbo].[CMS_InvProductPrice] WHERE [InventoryID] = @guidInvID) AS [ipp] ON [p].[ProductID] = [ipp].[ProductID] 
	WHERE p.[ProductID] IN (SELECT [Data] FROM [dbo].[Split](@sTitle, ',')) 
END

Go

--Khanh sua lai gia lay theo dung gia cua chi nhanh (15Mar2016)
IF EXISTS (SELECT * FROM sys .objects WHERE type = 'P' AND name = 'sp_CMS_InvProductPrice_MigrateData' )
        DROP PROC sp_CMS_InvProductPrice_MigrateData
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------
CREATE PROCEDURE [dbo].[sp_CMS_InvProductPrice_MigrateData]       
AS
BEGIN TRAN
        TRUNCATE TABLE [dbo].[CMS_InvProductPrice]
        -- ha noi
        INSERT INTO [dbo].[CMS_InvProductPrice]
                        ([InventoryID]
                        ,[ProductID]
                        ,[SaleTypeID]
                        ,[SalePrice]
                        ,[DealerPrice])
                        (SELECT N'0C80DCD0-5D8E-4041-ACAF-2CF7C2916162' AS InventoryId, [p]. ProductID, null, [p]. [SalePrice], [p] .[SalePriceDealer] FROM CMS_Products AS [p])

        -- ho chi minh
        INSERT INTO [dbo].[CMS_InvProductPrice]
                        ([InventoryID]
                        ,[ProductID]
                        ,[SaleTypeID]
                        ,[SalePrice]
                        ,[DealerPrice])
                        (SELECT N'6197D743-0B16-4BCF-8048-3803AE3FDCD8' AS InventoryId, [p]. ProductID, null, [p]. [SalePriceHCM], [p] .[SalePriceDealerHCM] FROM CMS_Products AS [p])

        -- chi nhanh 3
        INSERT INTO [dbo].[CMS_InvProductPrice]
                        ([InventoryID]
                        ,[ProductID]
                        ,[SaleTypeID]
                        ,[SalePrice]
                        ,[DealerPrice])
                        (SELECT N'665F2362-FE8A-4169-9513-0109340F3C0B' AS InventoryId, [p]. ProductID, null, [p]. [SalePriceCN3], [p] .[SalePriceDealerCN3] FROM CMS_Products AS [p])
COMMIT
GO
--Khanh sua lai hien thi giá các chi nhánh trên web
ALTER PROCEDURE [dbo].[sp_CMS_Products_SelectBySEOName]
	@sSEOName nvarchar(200)
AS
SET NOCOUNT ON
BEGIN
	--SELECT TOP 1 n.*, c.[MetaTitle] CateMetaTitle, c.[SEOName] CateSeoName, c.[Name] CateName, 
	--		c.[SEOName] CateSEOName, c.[SEOTags], c.[Code] CodeCate , n.[TotalComment] 
	--FROM [dbo].[CMS_Products] n, [dbo].[CMS_Categories] c
	--WHERE n.[CategoryID] =c.[CategoryID] 
	--	AND n.[SEOName] = @sSEOName
	
	-- KhanhHQ sua lai de hien thi gia tren web cho cac chi nhanh (15Mar2016): khi them 1 chi nhanh moi phai them tai day
	SELECT TOP 1 n.*, c.[MetaTitle] CateMetaTitle, c.[SEOName] CateSeoName, c.[Name] CateName, 
			c.[SEOName] CateSEOName, c.[SEOTags], c.[Code] CodeCate , n.[TotalComment], t.*  
	FROM [dbo].[CMS_Products] n left join 
	(select *
from
(
  select inventoryid, SalePrice, ProductID
  from CMS_InvProductPrice
) d
pivot
(
  sum(SalePrice)
  for inventoryid in ([0C80DCD0-5D8E-4041-ACAF-2CF7C2916162], [6197D743-0B16-4BCF-8048-3803AE3FDCD8], [665F2362-FE8A-4169-9513-0109340F3C0B])
) piv) t on n.ProductID= t.ProductID
	
	, [dbo].[CMS_Categories] c
	WHERE n.[CategoryID] =c.[CategoryID] 
		AND n.[SEOName] = @sSEOName
END
Go

--Export Excel: edit to get export data  SonLN
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_CMS_Products_SelectAll2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_CMS_Products_SelectAll2]
GO
CREATE PROCEDURE [dbo].[sp_CMS_Products_SelectAll2]
	@PageIndex int,
	@PageSize int,
	@OrderBy varchar(100),
	@guidCategoryID uniqueidentifier,
	@guidInventoryID uniqueidentifier,
	@sType varchar(100),
	@sTitle nvarchar(200)
AS
SET NOCOUNT ON
BEGIN
	DECLARE @Sql nvarchar(max)
	
	DECLARE @Parameter nvarchar(500)
	SET @Parameter =N'@PageIndex int, @PageSize int, @OrderBy varchar(100), @guidCategoryID uniqueidentifier, @guidInventoryID uniqueidentifier, @sType varchar(100), @sTitle nvarchar(200)'
	SET @Sql = 'SELECT TOP (@PageIndex * @PageSize) [t1].[TotalRows],
				[t1].[ProductID], [t1].[CategoryID], [t1].[Name], [t1].[NameAttribute], [t1].[Order],[t1].[Title],[t1].[SKU], [t1].[SalePrice],[t1].[SalePriceDealer]
				FROM (SELECT TotalRows = COUNT(*) OVER(), '
	-- BEGIN Order by
	SET @Sql = @Sql + CASE (@OrderBy)
		WHEN 'TITLE DESC' THEN ' ROW_NUMBER() OVER (ORDER BY [t0].[TITLE] DESC) AS [RowNumer], '
		WHEN 'TITLE ASC' THEN ' ROW_NUMBER() OVER (ORDER BY [t0].[TITLE]) AS [RowNumer], '
		ELSE ' ROW_NUMBER() OVER (ORDER BY [t0].[Created] DESC) AS [RowNumer], '
	END
	-- END Order by 
	SET @Sql = @Sql + '	[t0].[ProductID], ca.[CategoryID], ISNULL(ca.[Name],'''') [Name], ISNULL(att.[Name],'''') AS NameAttribute, [t0].[Order], [t0].[Title], [t0].[SKU], ISNULL([ipp].[SalePrice],0) [SalePrice],ISNULL([ipp].[DealerPrice],0) [SalePriceDealer]
						FROM [dbo].[CMS_Products] AS [t0]
						LEFT JOIN [dbo].[CMS_Categories] AS ca ON [t0].[CategoryID] = ca.[CategoryID]
						LEFT JOIN [dbo].[CMS_Attributes] AS att ON [t0].[ManufactureID] = [att].[AttributeID]
						LEFT JOIN [dbo].[CMS_InvProductPrice] AS [ipp] ON [t0].ProductID = [ipp].[ProductId] 
						WHERE 1=1 '
	-- BEGIN Where
	IF(@sTitle IS NOT NULL)
	BEGIN
		SET @Sql = @Sql + ' AND ([t0].[SKU] LIKE @sTitle OR [t0].[ProductBarcode] LIKE @sTitle '
		SET @sTitle='%'+@sTitle+'%'
		SET @Sql = @Sql + ' OR [t0].[Title] LIKE @sTitle) '
	END
	IF(@sType IS NOT NULL)
	BEGIN
		SET @sType='%' + @sType + '%'
		SET @Sql = @Sql + ' AND [t0].[Type] LIKE @sType '
	END
	IF(@guidCategoryID IS NOT NULL)
		SET @Sql = @Sql + ' AND ca.[CategoryID] = @guidCategoryID '
	
	IF(@guidInventoryID IS NOT NULL)
		SET @Sql = @Sql + ' AND [ipp].[InventoryID] = @guidInventoryID '	
		 
	-- END Where
	SET @Sql = @Sql + ') AS [t1] WHERE [t1].[RowNumer] >= ((@PageIndex - 1) * @PageSize + 1)  AND [t1].[RowNumer] <= (@PageIndex * @PageSize)'
	--select @Sql
	print @Sql
	EXEC SP_EXECUTESQL @Sql, @Parameter, @PageIndex, @PageSize, @OrderBy, @guidCategoryID, @guidInventoryID, @sType, @sTitle
END
GO

--Import Excel: edit to get import from excel file SonLN
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_CMS_Products_SelectAll2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_CMS_Products_SelectAll2]
GO
CREATE PROCEDURE [dbo].[sp_CMS_Products_UpdatePrice]
	@guidInventoryID uniqueidentifier,
	@guidProductID uniqueidentifier,
	@sSKU varchar(100),
	@dcSalePrice numeric(18, 2),
	@dcSalePriceDealer numeric(18, 2),
	@iHN int,
	@iHN1 int
AS
SET NOCOUNT ON
-- UPDATE an existing row in the table.
	IF EXISTS (SELECT 1 FROM CMS_InvProductPrice WHERE InventoryID = @guidInventoryID AND ProductID = @guidProductID )
	BEGIN
		IF @iHN = 1
			UPDATE CMS_InvProductPrice SET [SalePrice] = @dcSalePrice
			WHERE InventoryID = @guidInventoryID AND ProductID = @guidProductID 
		
		IF @iHN1 = 1
			UPDATE CMS_InvProductPrice SET [DealerPrice] = @dcSalePriceDealer 
			WHERE InventoryID = @guidInventoryID AND ProductID = @guidProductID 
	END
	ELSE
	BEGIN
		INSERT INTO CMS_InvProductPrice VALUES (NEWID(), @guidInventoryID, @guidProductID, NULL, @dcSalePrice, GETDATE(), @dcSalePriceDealer)
	END	
GO

-- 18/03/2016 sonln: complete edit info of Product Warranty
IF OBJECT_ID('[dbo].[sp_CMS_WarrantyProductSelect]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[sp_CMS_WarrantyProductSelect] 
END 
GO
CREATE PROC [dbo].[sp_CMS_WarrantyProductSelect] 
    @ID UNIQUEIDENTIFIER
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [ID], [WarrantyID], [ProductID], [ProductName], CONVERT(NVARCHAR(10), [PurchaseDate], 103) AS [PurchaseDate], [WarrantyDate], [WarrantyStatus], [Description], [WarrantyUserID], [FixerID], [ReturnDate] 
	FROM   [dbo].[CMS_WarrantyProduct] 
	WHERE  ([ID] = @ID OR @ID IS NULL) 

	COMMIT
GO
--edit
ALTER PROCEDURE [dbo].[sp_CMS_Users_SelectOne]
	@guidUserID uniqueidentifier
AS
SET NOCOUNT ON
SELECT
	[UserID],
	[GroupID],
	[FullName],
	[UserName],
	[Password],
	ISNULL([Gender],0) AS [Gender],
	[Email],
	[Address],
	[Phone],
	[Phone1],
	[Phone2],
	[Brithday],
	[Image],
	[GoogleID],
	[Permission],
	[Signature],
	[Status],
	[Created],
	[Views],
	[TotalAnswer],
	[DateLogin],
	[Follow],	
	ISNULL([SendMail],0) AS [SendMail],
	[TaxNumber]
FROM [dbo].[CMS_Users]
WHERE
	[UserID] = @guidUserID
GO

--update link userid to order after create acc
IF OBJECT_ID('[dbo].[sp_CMS_Orders_Insert]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[sp_CMS_Orders_Insert] 
END 
GO
CREATE PROCEDURE [dbo].[sp_CMS_Orders_Insert]
	@guidOrderID uniqueidentifier,
	@guidSaleID uniqueidentifier,
	@guidUserID uniqueidentifier,
	@sFullName nvarchar(200),
	@sAddress nvarchar(200),
	@sEmail nvarchar(200),
	@sPhone nvarchar(200),
	@sPhone1 nvarchar(200),
	@sPhone2 nvarchar(200),
	@sRequest nvarchar(1000),
	@dcTotalPrice numeric(18, 2),
	@sComment nvarchar(1000),
	@iTax int,
	@dcTransportFee numeric(18, 2),
	@dcDiscount numeric(18, 2),
	@sVoucher varchar(100),
	@iStatus int,
	@iDistrict int,
	@iProvince int,
	@guidPaymentID uniqueidentifier,
	@daCreated datetime,
	@guidCusClassID uniqueidentifier,
	@guidInvID uniqueidentifier,	
	@iOType int,
	@dcPrePayment numeric(18, 2)
AS
SET NOCOUNT ON
BEGIN
	INSERT [dbo].[CMS_Orders]([OrderID],[SaleID],[UserID],[FullName],[Address],[Email],
						[Phone],[Request],[TotalPrice],[Tax],[TransportFee],[Comment],
						[Discount],[Voucher],[Status],[Created], [DistrictId], [ProvinceId], [PaymentID], [CusClassID], [InventoryID], [TypeBuy], [PrePayment] ) 
						
	VALUES(@guidOrderID,@guidSaleID,@guidUserID,@sFullName,@sAddress,@sEmail,
				@sPhone,@sRequest,@dcTotalPrice, @iTax, @dcTransportFee, @sComment,
				@dcDiscount, @sVoucher,@iStatus,CONVERT(DATETIME,@daCreated),@iDistrict,@iProvince, @guidPaymentID, @guidCusClassID, @guidInvID, @iOType, @dcPrePayment )
				
				
	UPDATE [dbo].[CMS_Orders] SET [OrderCode] = 'RO-'+replace(convert(varchar, getdate(),103),'/','')+'-' + convert(varchar,[ID])
	WHERE [OrderID] = @guidOrderID
	
	
	IF LEN(@sEmail)=0
		SET @sEmail = @sPhone
	IF NOT EXISTS (SELECT 1 FROM CMS_Users WHERE Phone=@sPhone)
	BEGIN
		DECLARE @GUID_Value UNIQUEIDENTIFIER;
		SET @GUID_Value = NEWID();
		DECLARE @dCreated DATETIME;
		SET @dCreated = GETDATE();
		EXEC [dbo].[sp_CMS_Users_Insert] @GUID_Value,null,@sFullName,@sPhone,N'CF-72-4C-61-16-15-30-85-4E-1E-8E-45-6A-67-55-A4',null,@sEmail,@sAddress,@sPhone,@sPhone1,@sPhone2,null,null,null,null,null,1,@dCreated,0,0,null,null,1,null
	
		--update UserID to Order
		UPDATE [dbo].[CMS_Orders] SET [UserID] = @GUID_Value
		WHERE [OrderID] = @guidOrderID
	END	
	--ELSE IF EXISTS (SELECT 1 FROM CMS_Users WHERE Phone=@sPhone) AND (@guidUserID IS NULL OR @guidUserID = '')
	ELSE IF EXISTS (SELECT 1 FROM CMS_Users WHERE Phone=@sPhone) AND (@guidUserID IS NULL)
	BEGIN
		--update UserID to Order
		UPDATE [dbo].[CMS_Orders] SET [UserID] = (SELECT [UserID] FROM [dbo].[CMS_Users] WHERE [Phone]=@sPhone)
		WHERE [OrderID] = @guidOrderID
	END	
END
GO

ALTER PROCEDURE [dbo].[sp_CMS_Orders_SelectByOrderCode]
	@sOrderCode VARCHAR(100)
AS
SET NOCOUNT ON
	
	SELECT *, ROW_NUMBER() OVER(ORDER BY OrderCode DESC) AS Row
  FROM (
	select	o.FullName, o.Phone, o.Address, o.OrderCode, p.SKU, p.Title, DVT = CASE p.Quantity
	WHEN 1 THEN N'Cái'
	WHEN 2 THEN N'Lô'
	else N'Cái' END, 
		od.Quantity, od.Price, o.TotalPrice, 
		[dbo].[sp_ConvertNumberToText4](CAST(o.TotalPrice as int)) as PriceText,
		o.PageNumber,o.PrePayment, od.Discount,u.fullname as nhanvien, inv.InventoryCode, o.Comment, inv.InventoryDesc,  WarrantyPeriod = CASE p.WarrantyPeriod
	WHEN NULL THEN ''
	WHEN 0 THEN ''
	else CONVERT(varchar, p.WarrantyPeriod) + N' tháng' END
	FROM [dbo].[CMS_Orders] o 
	LEFT JOIN [dbo].[CMS_OrderDetails] od ON o.[OrderID] = od.[OrderID]
	LEFT JOIN [dbo].[CMS_Products] p ON od.[ProductID] = p.[ProductID]	
	LEFT JOIN [dbo].[ADM_Users] u ON o.[SaleID] = u.[UserID]	
	LEFT JOIN [dbo].[cms_inventory] inv ON o.[InventoryID] = inv.[InventoryID]
	WHERE  o.[OrderCode] = @sOrderCode
	union all
SELECT 
		null FullName, null, null, o.OrderCode, a.Code, a.Name Title, '', 
		1 Quantity, a.Price, null, 
		null,		
		null,null, null	, null,null, o.Comment, null, ''
	FROM [dbo].[CMS_Orders] o 
	inner join (select ov.OrderId, vas.Name, ov.Price, vas.Code from CMS_OrderVAS ov, CMS_VAS vas where ov.VasId=vas.Id) a on a.OrderId=o.[OrderID]
	WHERE  o.[OrderCode] = @sOrderCode
	) a;
Go
ALTER PROCEDURE [dbo].[sp_CMS_Products_SelectAll]
	@PageIndex int,
	@PageSize int,
	@OrderBy varchar(100),
	@guidCategoryID uniqueidentifier,
	@sType varchar(100),
	@sTitle nvarchar(200),
	@iStatus int,
	@guidInventoryID uniqueidentifier
AS
SET NOCOUNT ON
BEGIN
	DECLARE @Sql nvarchar(max)
	
	DECLARE @Parameter nvarchar(500)
	SET @Parameter =N'@PageIndex int, @PageSize int, @OrderBy varchar(100), @guidCategoryID uniqueidentifier, @sType varchar(100), @sTitle nvarchar(200),@iStatus int, @guidInventoryID uniqueidentifier'
	SET @Sql = 'SELECT TOP (@PageIndex * @PageSize) [t1].[TotalRows],
				[t1].[ProductID], [t1].[Name], [t1].[Order],[t1].[Title], [t1].[Quantity], [t1].[RealPrice], [t1].[Promotion], [t1].[DiscountPercent] ,[t1].[SalePrice],[t1].[Type], [t1].[Status], [t1].[SKU],[t1].[SalePriceDealer] FROM (SELECT TotalRows = COUNT(*) OVER(), '
	-- BEGIN Order by
	SET @Sql = @Sql + CASE (@OrderBy)
		WHEN 'TITLE DESC' THEN ' ROW_NUMBER() OVER (ORDER BY [t0].[TITLE] DESC) AS [RowNumer], '
		WHEN 'TITLE ASC' THEN ' ROW_NUMBER() OVER (ORDER BY [t0].[TITLE]) AS [RowNumer], '
		WHEN 'QUANTITY DESC' THEN ' ROW_NUMBER() OVER (ORDER BY [t0].[QUANTITY] DESC) AS [RowNumer], '
		WHEN 'QUANTITY ASC' THEN ' ROW_NUMBER() OVER (ORDER BY [t0].[QUANTITY]) AS [RowNumer], '
		WHEN 'SALEPRICE DESC' THEN ' ROW_NUMBER() OVER (ORDER BY [t0].[SALEPRICE] DESC) AS [RowNumer], '
		WHEN 'SALEPRICE ASC' THEN ' ROW_NUMBER() OVER (ORDER BY [t0].[SALEPRICE]) AS [RowNumer], '
		WHEN 'ORDER DESC' THEN ' ROW_NUMBER() OVER (ORDER BY [t0].[Order] DESC) AS [RowNumer], '
		WHEN 'ORDER ASC' THEN ' ROW_NUMBER() OVER (ORDER BY [t0].[Order]) AS [RowNumer], '
		ELSE ' ROW_NUMBER() OVER (ORDER BY [t0].[Created] DESC) AS [RowNumer], '
	END
	-- END Order by 
	SET @Sql = @Sql + '	[t0].[ProductID], [ca].[Name], [t0].[Order],[t0].[Title], [t0].[Quantity], [t0].[RealPrice], [t0].[Promotion], [t0].[DiscountPercent] ,[t0].[Type], [t0].[Status], [t0].[SKU],ISNULL([ipp].[SalePrice],0) [SalePrice],ISNULL([ipp].[DealerPrice],0) [SalePriceDealer], ISNULL([t0].[WarrantyPeriod],0) [WarrantyPeriod]
						FROM [dbo].[CMS_Products] AS [t0]
						INNER JOIN [dbo].[CMS_Categories] AS [ca ] ON [t0].[CategoryID] = [ca].[CategoryID] 
						LEFT JOIN (SELECT * FROM [dbo].[CMS_InvProductPrice] WHERE [InventoryId]=@guidInventoryID) AS [ipp] ON [t0].ProductID = [ipp].[ProductId] 
						WHERE 1=1 '
	-- BEGIN Where
	IF(@sTitle IS NOT NULL)
	BEGIN
		SET @Sql = @Sql + ' AND ([t0].[SKU] LIKE @sTitle OR [t0].[ProductBarcode] LIKE @sTitle '
		SET @sTitle='%'+@sTitle+'%'
		SET @Sql = @Sql + ' OR [t0].[Title] LIKE @sTitle) '
	END
	IF(@sType IS NOT NULL)
	BEGIN
		SET @sType='%' + @sType + '%'
		SET @Sql = @Sql + ' AND [t0].[Type] LIKE @sType '
	END
	IF(@guidCategoryID IS NOT NULL)
		SET @Sql = @Sql + ' AND [t0].[CategoryID] = @guidCategoryID '
	IF(@iStatus IS NOT NULL)
		SET @Sql = @Sql + ' AND [t0].[Status] = @iStatus '
	-- END Where
	SET @Sql = @Sql + ') AS [t1] WHERE [t1].[RowNumer] >= ((@PageIndex - 1) * @PageSize + 1)  AND [t1].[RowNumer] <= (@PageIndex * @PageSize)'
	--select @Sql
	print @Sql
	EXEC SP_EXECUTESQL @Sql, @Parameter, @PageIndex, @PageSize, @OrderBy, @guidCategoryID, @sType, @sTitle, @iStatus, @guidInventoryID
END
;
Go
ALTER PROCEDURE [dbo].[sp_CMS_Products_SelectOne]
	@guidProductID uniqueidentifier
AS
SET NOCOUNT ON
-- SELECT an existing row from the table.
SELECT
	[ProductID],
	[CategoryID],
	[UserID],
	[Title],
	[SEOName],
	[Image],
	[Brief],
	[Content],
	[MetaTitle],
	[MetaKeyword],
	[MetaDescription],
	[Tags],
	[TagsTitle],
	[Related],
	[Type],
	[Status],
	[Visitor],
	[SlideImage],
	[Created],
	[Modified],
	[TotalComment],
	[SKU],
	[Quantity],
	[ImportPrice],
	[RealPrice],
	ISNULL([SalePrice],0) [SalePrice],
	[DiscountPercent],
	[Weight],
	[Model],
	[Filter],
	[ManufactureID],
	[ProductAttribute],
	[Promotion],
	[Parameter],
	[TransportFee],
	[PollID],
	[Order],
	[WarrantyPeriod],
	ISNULL([SalePriceDealer],0) [SalePriceDealer],
	ISNULL([SalePriceHCM],0) [SalePriceHCM],
	ISNULL([SalePriceDealerHCM],0) [SalePriceDealerHCM],
	ISNULL([SalePriceCN3],0) [SalePriceCN3],
	ISNULL([SalePriceDealerCN3],0) [SalePriceDealerCN3],
	[ProductBarcode]
FROM [dbo].[CMS_Products]
WHERE
	[ProductID] = @guidProductID
;
Go
ALTER PROCEDURE [dbo].[sp_CMS_Products_Update]
	@guidProductID uniqueidentifier,
	@guidCategoryID uniqueidentifier,
	@guidUserID uniqueidentifier,
	@sTitle nvarchar(200),
	@sSEOName nvarchar(200),
	@sImage nvarchar(200),
	@sBrief nvarchar(500),
	@sContent nvarchar(max),
	@sMetaTitle nvarchar(200),
	@sMetaKeyword nvarchar(200),
	@sMetaDescription nvarchar(200),
	@sTags nvarchar(200),
	@sTagsTitle nvarchar(200),
	@sRelated nvarchar(max),
	@sType varchar(100),
	@iStatus int,
	@iVisitor int,
	@sSlideImage nvarchar(max),
	@daCreated datetime,
	@daModified datetime,
	@iTotalComment int,
	@sSKU varchar(100),
	@iQuantity int,
	-- @dcImportPrice numeric(18, 2),
	-- @dcRealPrice numeric(18, 2),
	@dcSalePrice numeric(18, 2),
	@dcSalePriceDealer numeric(18, 2),
	@dcSalePriceHCM numeric(18, 2),
	@dcSalePriceDealerHCM numeric(18, 2),
	@dcSalePriceCN3 numeric(18, 2),
	@dcSalePriceDealerCN3 numeric(18, 2),
	@sBarcode varchar(100),
	@iDiscountPercent int,
	@sWeight nvarchar(100),
	@sModel nvarchar(100),
	@sFilter varchar(max),
	@guidManufactureID uniqueidentifier,
	@sProductAttribute nvarchar(max),
	@sPromotion nvarchar(max),
	@sParameter nvarchar(max),
	@sTransportFee nvarchar(100),
	@guidPollID uniqueidentifier,
	@iOrder int,
	@iWarrantyPeriod int
AS
SET NOCOUNT ON
-- UPDATE an existing row in the table.
UPDATE [dbo].[CMS_Products]
SET [CategoryID] = ISNULL(@guidCategoryID,[CategoryID]), [UserID] = ISNULL(@guidUserID,[UserID]), [Title] = ISNULL(@sTitle,[Title]), 
	[SEOName] = ISNULL(@sSEOName,[SEOName]), [Image] = ISNULL(@sImage,[Image]), [Brief] = ISNULL(@sBrief,[Brief]), 
	[Content] = ISNULL(@sContent,[Content]), [MetaTitle] = ISNULL(@sMetaTitle,[MetaTitle]), 
	[MetaKeyword] = ISNULL(@sMetaKeyword,[MetaKeyword]), [MetaDescription] = ISNULL(@sMetaDescription,[MetaDescription]), 
	[Tags] = ISNULL(@sTags,[Tags]), [TagsTitle] = ISNULL(@sTagsTitle,[TagsTitle]), [Related] = ISNULL(@sRelated,[Related]), 
	[Type] = ISNULL(@sType,[Type]), [Status] = ISNULL(@iStatus,[Status]), [Visitor] = ISNULL(@iVisitor,[Visitor]), 
	[SlideImage] = ISNULL(@sSlideImage,[SlideImage]), [Created] = ISNULL(@daCreated,[Created]), 
	[Modified] = ISNULL(@daModified,[Modified]), [TotalComment] = ISNULL(@iTotalComment,[TotalComment]), 
	[SKU] = ISNULL(@sSKU,[SKU]), [Quantity] = ISNULL(@iQuantity,[Quantity]), [SalePrice] = ISNULL(@dcSalePrice,[SalePrice]), 
	[DiscountPercent] = ISNULL(@iDiscountPercent,[DiscountPercent]), [Weight] = ISNULL(@sWeight,[Weight]),
	[Model] = ISNULL(@sModel,[Model]), [Filter] = ISNULL(@sFilter,[Filter]), [ManufactureID] = ISNULL(@guidManufactureID,[ManufactureID]), 
	[ProductAttribute] = ISNULL(@sProductAttribute,[ProductAttribute]), [Promotion] = ISNULL(@sPromotion,[Promotion]), 
	[Parameter] = ISNULL(@sParameter,[Parameter]), [TransportFee] = ISNULL(@sTransportFee,[TransportFee]), 
	[PollID] = @guidPollID,[Order] = ISNULL(@iOrder,[Order]),
	[SalePriceDealer] = ISNULL(@dcSalePriceDealer,[SalePriceDealer]),[SalePriceDealerHCM] = ISNULL(@dcSalePriceDealerHCM,
	[SalePriceDealerHCM]),[SalePriceHCM] = ISNULL(@dcSalePriceHCM,[SalePriceHCM]), [SalePriceCN3]  = ISNULL(@dcSalePriceCN3,[SalePriceCN3]), 
	[SalePriceDealerCN3] = ISNULL(@dcSalePriceDealerCN3,[SalePriceDealerCN3]), [ProductBarcode] = ISNULL(@sBarcode,[ProductBarcode]),
	[WarrantyPeriod] = ISNULL(@iWarrantyPeriod,[WarrantyPeriod])
WHERE
	[ProductID] = @guidProductID
;
Go

ALTER TABLE [dbo].[CMS_Products] ADD DEFAULT ((0)) FOR [WarrantyPeriod];
Go

ALTER PROCEDURE [dbo].[sp_CMS_Products_Insert]
	@guidProductID uniqueidentifier,
	@guidCategoryID uniqueidentifier,
	@guidUserID uniqueidentifier,
	@sTitle nvarchar(200),
	@sSEOName nvarchar(200),
	@sImage nvarchar(200),
	@sBrief nvarchar(500),
	@sContent nvarchar(max),
	@sMetaTitle nvarchar(200),
	@sMetaKeyword nvarchar(200),
	@sMetaDescription nvarchar(200),
	@sTags nvarchar(200),
	@sTagsTitle nvarchar(200),
	@sRelated nvarchar(max),
	@sType varchar(100),
	@iStatus int,
	@iVisitor int,
	@sSlideImage nvarchar(max),
	@daCreated datetime,
	@daModified datetime,
	@iTotalComment int,
	@sSKU varchar(100),
	@iQuantity int,
	@dcImportPrice numeric(18, 2),
	@dcRealPrice numeric(18, 2),
	@dcSalePrice numeric(18, 2),
	@dcSalePriceDealer numeric(18, 2),
	@dcSalePriceHCM numeric(18, 2),
	@dcSalePriceDealerHCM numeric(18, 2),
	@sBarcode varchar(100),
	@iDiscountPercent int,
	@sWeight nvarchar(100),
	@sModel nvarchar(100),
	@sFilter varchar(max),
	@guidManufactureID uniqueidentifier,
	@sProductAttribute nvarchar(max),
	@sPromotion nvarchar(max),
	@sParameter nvarchar(max),
	@sTransportFee nvarchar(100),
	@guidPollID uniqueidentifier,
	@iOrder int,
	@iWarrantyPeriod int
AS
SET NOCOUNT ON
-- INSERT a new row in the table.
IF @guidProductID is null
SET @guidProductID = newid()

INSERT [dbo].[CMS_Products]
(
	[ProductID],
	[CategoryID],
	[UserID],
	[Title],
	[SEOName],
	[Image],
	[Brief],
	[Content],
	[MetaTitle],
	[MetaKeyword],
	[MetaDescription],
	[Tags],
	[TagsTitle],
	[Related],
	[Type],
	[Status],
	[Visitor],
	[SlideImage],
	[Created],
	[Modified],
	[TotalComment],
	[SKU],
	[Quantity],
	[ImportPrice],
	[RealPrice],
	[SalePrice],
	[SalePriceDealer],
	[SalePriceHCM],
	[SalePriceDealerHCM],
	[DiscountPercent],
	[Weight],
	[Model],
	[Filter],
	[ManufactureID],
	[ProductAttribute],
	[Promotion],
	[Parameter],
	[TransportFee],
	[PollID],
	[Order],
	[ProductBarcode],
	[WarrantyPeriod]
)
VALUES
(
	@guidProductID,
	@guidCategoryID,
	@guidUserID,
	@sTitle,
	@sSEOName,
	@sImage,
	@sBrief,
	@sContent,
	@sMetaTitle,
	@sMetaKeyword,
	@sMetaDescription,
	@sTags,
	@sTagsTitle,
	@sRelated,
	@sType,
	@iStatus,
	@iVisitor,
	@sSlideImage,
	@daCreated,
	@daModified,
	@iTotalComment,
	@sSKU,
	@iQuantity,
	@dcImportPrice,
	@dcRealPrice,
	@dcSalePrice,
	@dcSalePriceDealer,
	@dcSalePriceHCM,
	@dcSalePriceDealerHCM,
	@iDiscountPercent,
	@sWeight,
	@sModel,
	@sFilter,
	@guidManufactureID,
	@sProductAttribute,
	@sPromotion,
	@sParameter,
	@sTransportFee,
	@guidPollID,
	@iOrder,
	@sBarcode,
	@iWarrantyPeriod
)

DECLARE cur CURSOR FORWARD_ONLY FOR
select  inventoryid
 from dbo.CMS_Inventory where status=1

OPEN cur
declare @invid uniqueidentifier
FETCH NEXT FROM cur into @invid
 WHILE @@FETCH_STATUS = 0
    BEGIN    
        INSERT INTO [dbo].[CMS_InventoryDetails] 
           ([InventoryDetailID]
           ,[ProductID]
           ,[InventoryID]
		   ,[Quantity]
           ,[BrokenQuantity])
		VALUES
		(
			newid(),
			@guidProductID,
			@invid,
			0,	
			0
		)
		INSERT INTO [dbo].[CMS_InvProductPrice]
                        ([InventoryID]
                        ,[ProductID]
                        ,[SaleTypeID]
                        ,[SalePrice]
                        ,[DealerPrice]) 
		VALUES
		(
			@invid,
			@guidProductID,
			null,
			0,	
			0
		)                         
		fetch next from cur into @invid
    END
CLOSE cur
DEALLOCATE cur
Go
-- sp_CMS_InvProductPrice_SelectAllPriceByProductId 
IF OBJECT_ID('[dbo].[sp_CMS_InvProductPrice_SelectAllPriceByProductId]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[sp_CMS_InvProductPrice_SelectAllPriceByProductId] 
END 
GO
CREATE PROCEDURE [dbo].[sp_CMS_InvProductPrice_SelectAllPriceByProductId]
	@guidProductID uniqueidentifier
AS
SET NOCOUNT ON
BEGIN
	SELECT i.InventoryID, i.InventoryName, @guidProductID AS ProductID, ISNULL(ipp.SalePrice, 0) AS [SalePrice], ISNULL(ipp.DealerPrice, 0) AS DealerPrice FROM dbo.CMS_Inventory AS i
	LEFT JOIN (SELECT * FROM dbo.CMS_InvProductPrice WHERE ProductID=@guidProductID) AS ipp ON i.InventoryID = ipp.InventoryID
	WHERE i.Status=1
END
GO

IF OBJECT_ID('[dbo].[sp_CMS_InvProductPriceUpdate]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[sp_CMS_InvProductPriceUpdate] 
END 
GO
CREATE PROC [dbo].[sp_CMS_InvProductPriceUpdate] 
    @ID uniqueidentifier,
    @InventoryID uniqueidentifier,
    @ProductID uniqueidentifier,
    @SaleTypeID uniqueidentifier,
    @SalePrice decimal(18, 2),
    @UpdatedDate datetime,
    @DealerPrice decimal(18, 2),
    @iStatus int,
    @iOrder int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	IF(@SalePrice IS NULL)
		SET @SalePrice = 0
	IF(@DealerPrice IS NULL)
		SET @DealerPrice = 0		
	IF EXISTS (SELECT 1 FROM [dbo].[CMS_InvProductPrice] WHERE [InventoryID] = @InventoryID AND [ProductID] = @ProductID)
	BEGIN
		UPDATE [dbo].[CMS_InvProductPrice]
		SET    [SalePrice] = @SalePrice, [DealerPrice] = @DealerPrice, [UpdatedDate] = @UpdatedDate
		WHERE  [InventoryID] = @InventoryID AND [ProductID] = @ProductID
	END
	ELSE
	BEGIN
		INSERT INTO [dbo].[CMS_InvProductPrice] ([ID], [InventoryID], [ProductID], [SaleTypeID], [SalePrice], [DealerPrice], [UpdatedDate])
		SELECT @ID, @InventoryID, @ProductID, @SaleTypeID, @SalePrice, @DealerPrice, @UpdatedDate
	END
	--Update Products
	UPDATE [dbo].[CMS_Products]
SET [Status] = ISNULL(@iStatus,[Status]),	
	[Order] = ISNULL(@iOrder,[Order])	
WHERE
	[ProductID] = @ProductID
	-- Begin Return Select 
	SELECT [ID], [InventoryID], [ProductID], [SaleTypeID], [SalePrice], [DealerPrice], [UpdatedDate]
	FROM   [dbo].[CMS_InvProductPrice]
	WHERE  [InventoryID] = @InventoryID AND [ProductID] = @ProductID	
	-- End Return Select 

	COMMIT
GO

--2016/04/27: sonln update sp_CMS_Orders_Update_Inventory
IF OBJECT_ID('[dbo].[sp_CMS_Orders_Update_Inventory]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[sp_CMS_Orders_Update_Inventory] 
END 
GO
CREATE PROCEDURE [dbo].[sp_CMS_Orders_Update_Inventory]
	@guidOrderID uniqueidentifier,
	@guidCusClassID uniqueidentifier,
	@guidInvID uniqueidentifier,
	@isDataChanged int
AS
SET NOCOUNT ON
BEGIN
	DECLARE @totalPrice int = 0
	DECLARE @SPrice varchar(100)
	IF @isDataChanged = 1
	BEGIN		
		IF @guidCusClassID = '29ED76BB-B3A9-420F-AA49-AD96538465FC'
			UPDATE [od] SET [od].[Price] = (SELECT ISNULL([SalePrice],0) AS [SalePrice] FROM [CMS_InvProductPrice] AS [p] WHERE [p].[ProductID] = [od].[ProductID] AND [p].[InventoryID] = @guidInvID)
				FROM [CMS_OrderDetails] AS [od] WHERE [od].[OrderID] = @guidOrderID
		ELSE IF @guidCusClassID = 'D4CC298E-2E7A-4D6A-BD41-E4291CF6D929'
			UPDATE [od] SET [od].[Price] = (SELECT ISNULL([DealerPrice],0) AS [SalePrice] FROM [CMS_InvProductPrice] AS [p] WHERE [p].[ProductID] = [od].[ProductID] AND [p].[InventoryID] = @guidInvID)
				FROM [CMS_OrderDetails] AS [od] WHERE [od].[OrderID] = @guidOrderID
			
	
		SELECT @totalPrice = SUM(tb.totalPirce) FROM (SELECT totalPirce = CASE 
													WHEN ISNULL([od].[Discount],0)>=100 THEN(ISNULL([od].Price,0)-ISNULL(od.Discount,0))*[od].Quantity
													WHEN ISNULL([od].[Discount],0)<100 THEN((ISNULL([od].Price,0)/100)*(100-ISNULL([od].Discount,0)))*[od].Quantity
													END
													FROM [dbo].[CMS_OrderDetails] od WHERE od.[OrderID] = @guidOrderID) AS [tb]
		SELECT @totalPrice = @totalPrice - [ds].[Discount] FROM (SELECT [Discount] FROM [CMS_Orders] WHERE [OrderID] = @guidOrderID) AS [ds]
		IF EXISTS(SELECT * FROM [CMS_OrderVAS] WHERE [OrderId] = @guidOrderID)
			SELECT @totalPrice = @totalPrice + v.sumVas FROM (SELECT SUM(ISNULL([Price],0)) AS [sumVas] FROM [CMS_OrderVAS] WHERE [OrderId] = @guidOrderID) AS [v]
		UPDATE [CMS_Orders] SET [TotalPrice] = @totalPrice, [InventoryID] = @guidInvID, [CusClassId] = @guidCusClassID  WHERE [OrderID] = @guidOrderID 
	END
END
GO

--Thong ke don hang dang giao
IF OBJECT_ID('[dbo].[sp_ReportTK_GiaoHang]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[sp_ReportTK_GiaoHang] 
END 
GO
CREATE PROCEDURE [dbo].[sp_ReportTK_GiaoHang]
	@SaleID UNIQUEIDENTIFIER,
	@InvID  UNIQUEIDENTIFIER,
	@ShipperID  UNIQUEIDENTIFIER,
	@CusClassID  UNIQUEIDENTIFIER,
	@sSendDate VARCHAR(50),
	@sToDate VARCHAR(50),
	@iTypeBuy INT
AS
SET NOCOUNT ON
BEGIN
	DECLARE @Sql nvarchar(max)
	
	DECLARE @Parameter nvarchar(500)
	SET @Parameter =N'@SaleID UNIQUEIDENTIFIER, @InvID UNIQUEIDENTIFIER, @ShipperID UNIQUEIDENTIFIER, @CusClassID UNIQUEIDENTIFIER, @sSendDate VARCHAR(50), @sToDate VARCHAR(50), @iTypeBuy INT'
	SET @Sql = 'SELECT [t1].[TotalRows], [t1].[RowNumber],
				[t1].[OrderID], [OrderCode], CONVERT(VARCHAR(10),[t1].[OrderSend], 103) [OrderSend], CONVERT(VARCHAR(5),[t1].[OrderSend],108) [OrderSendTime], [t1].[PrePayment], [t1].[FullName], [t1].[TotalPrice], [t1].[Address], 
				[t1].[Phone], ISNULL([inv].[InventoryName],N''Chưa có'') AS [InventoryName] , [u].fullname as nhanvien, [t1].giaohang giaohang, [cus].CusClassName, [t1].[Comment], [t1].TienConLai
				FROM (SELECT TotalRows = COUNT(*) OVER(), ROW_NUMBER() OVER (ORDER BY [OrderSend] DESC) AS [RowNumber], [t0].[OrderSend], [t0].[OrderID], [t0].[OrderCode], [t0].[FullName], [t0].[TotalPrice], [t0].[Created], [t0].[Status], [t0].[Address], [t0].[Phone], [t0].[InventoryID], [t0].[SaleID], tic.giaohang,  [t0].[CusClassID], [t0].[PrePayment], [t0].[Comment], ([t0].[TotalPrice] - [t0].[PrePayment]) TienConLai
						FROM [dbo].[CMS_Orders] AS [t0] 
						LEFT JOIN (SELECT t.orderid, t.shipperid, s.fullname AS giaohang FROM [CMS_Tickets] t, cms_shippers AS s WHERE t.shipperid = s.shipperid AND t.tickettype=0 AND t.status=0) AS [tic] ON [t0].[OrderID] = [tic].[OrderID] WHERE (1=1) AND [t0].[Status]=6 '
	-- BEGIN Where
	IF(@InvID IS NOT NULL)
		SET @Sql = @Sql + ' AND [t0].[InventoryID] = @InvID '
	IF(@CusClassID IS NOT NULL)
		SET @Sql = @Sql + ' AND [t0].[CusClassID] = @CusClassID '
	IF(@SaleID IS NOT NULL)
		SET @Sql = @Sql + ' AND [t0].[SaleID] = @SaleID '
	IF(@iTypeBuy IS NOT NULL)
	BEGIN
		IF(@iTypeBuy = 1)
			SET @Sql = @Sql + ' AND [t0].[TypeBuy] = @iTypeBuy '
		ELSE
			SET @Sql = @Sql + ' AND [t0].[TypeBuy] IN (2,3) '
	END
	IF(@ShipperID IS NOT NULL)
		SET @Sql = @Sql + ' AND [tic].[ShipperID] = @ShipperID '
	IF(@sSendDate IS NOT NULL)
	BEGIN
		SET @Sql = @Sql + ' AND [t0].[OrderSend] >= CONVERT(datetime,@sSendDate, 103) '
	END
	IF(@sToDate IS NOT NULL)
	BEGIN
		SET @Sql = @Sql + ' AND [t0].[OrderSend] <= CONVERT(datetime,@sToDate, 103) '
	END
	-- END Where
	SET @Sql = @Sql + ') AS [t1] '
	SET @Sql = @Sql + ' LEFT JOIN [CMS_Inventory] AS [inv] ON [t1].[InventoryID] = [inv].[InventoryID] '
	SET @Sql = @Sql + ' LEFT JOIN [CMS_CustomerClass] AS [cus] ON [t1].[CusClassID] = [cus].[CusClassID] '
	SET @Sql = @Sql + ' LEFT JOIN [ADM_Users] AS [u] ON [t1].[SaleID] = [u].[UserID] '
	-- Order by 	
	SET @Sql = @Sql + ' order by [inv].[InventoryName], [t1].[OrderSend] '
	EXEC SP_EXECUTESQL @Sql, @Parameter, @SaleID, @InvID, @ShipperID, @CusClassID, @sSendDate, @sToDate, @iTypeBuy
	--print @Sql
END
GO
--In thong ke don hang dang giao
IF OBJECT_ID('[dbo].[sp_ReportTK_GiaoHangChiTiet]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[sp_ReportTK_GiaoHangChiTiet] 
END 
GO
CREATE PROCEDURE [dbo].[sp_ReportTK_GiaoHangChiTiet]
	@SaleID UNIQUEIDENTIFIER,
	@InvID  UNIQUEIDENTIFIER,
	@ShipperID  UNIQUEIDENTIFIER,
	@CusClassID  UNIQUEIDENTIFIER,
	@sSendDate VARCHAR(50),
	@sToDate VARCHAR(50),
	@iTypeBuy INT
AS
SET NOCOUNT ON
BEGIN
	DECLARE @Sql nvarchar(max)
	DECLARE @SqlVas nvarchar(max)
	DECLARE @tSql nvarchar(max)
	
	DECLARE @Parameter nvarchar(500)
	SET @Parameter =N'@SaleID UNIQUEIDENTIFIER, @InvID UNIQUEIDENTIFIER, @ShipperID UNIQUEIDENTIFIER, @CusClassID UNIQUEIDENTIFIER, @sSendDate VARCHAR(50), @sToDate VARCHAR(50), @iTypeBuy INT'
	SET @Sql = 'SELECT [od].[OrderID], [p].[SKU], [p].[Title], [ca].[Name] AS [CategoryName], [p].[Type] AS [ProductType], [p].[SalePrice], [p].[SalePriceDealer], [p].[SalePriceHCM], [p].[SalePriceDealerHCM], [p].[SalePriceCN3], [p].[SalePriceDealerCN3], [od].[Quantity], 
	CASE
        WHEN [od].[Discount]<100 THEN
                ((ISNULL([od].[Price],0) * (100 - ISNULL([od].[Discount],0))) / 100)
         ELSE (ISNULL([od].[Price],0) - ISNULL([od].[Discount],0))
    END AS [Price], 
	CASE
        WHEN [od].[Discount]<100 THEN
                [od].[Quantity] * ((ISNULL([od].[Price],0) * (100 - ISNULL([od].[Discount],0))) / 100)
         ELSE [od].[Quantity] * (ISNULL([od].[Price],0) - ISNULL([od].[Discount],0))
    END AS [Cash]    
	FROM [dbo].[CMS_OrderDetails] AS [od]
	INNER JOIN [dbo].[CMS_Products] AS [p] ON [od].[ProductID] = [p].[ProductID]
	INNER JOIN [dbo].[CMS_Categories] AS [ca] ON [p].[CategoryID] = [ca].[CategoryID]
	LEFT JOIN (SELECT t.orderid, t.shipperid, s.fullname AS giaohang FROM [CMS_Tickets] t, cms_shippers AS s WHERE t.shipperid = s.shipperid AND t.tickettype=0 AND t.status=0) AS [tic] ON [od].[OrderID] = [tic].[OrderID] 
	WHERE [od].[OrderID] IN ( '
	SET @SqlVas = 'SELECT ov.OrderId, v.Code AS SKU, v.Name AS Title, N''VAS'', N''VAS'', 0, 0, 0, 0, 0, 0, COUNT(ov.OrderId) Quantity, ov.Price, 
					COUNT(ov.OrderId)*SUM(ov.Price) AS Cash FROM CMS_OrderVAS ov
					INNER JOIN CMS_VAS v ON ov.VasId = v.Id
					LEFT JOIN (SELECT t.orderid, t.shipperid, s.fullname AS giaohang FROM [CMS_Tickets] t, cms_shippers AS s WHERE t.shipperid = s.shipperid AND t.tickettype=0 AND t.status=0) AS [tic] ON [ov].[OrderID] = [tic].[OrderID] 
					WHERE ov.OrderId IN ( '
	SET @tSql = 'SELECT [t0].[OrderID] FROM [dbo].[CMS_Orders] AS [t0] WHERE [t0].[Status]=6 '
	-- BEGIN Where
	IF(@InvID IS NOT NULL)
		SET @tSql = @tSql + ' AND [t0].[InventoryID] = @InvID '
	IF(@CusClassID IS NOT NULL)
		SET @tSql = @tSql + ' AND [t0].[CusClassID] = @CusClassID '
	IF(@SaleID IS NOT NULL)
		SET @tSql = @tSql + ' AND [t0].[SaleID] = @SaleID '
	IF(@iTypeBuy IS NOT NULL)
	BEGIN
		IF(@iTypeBuy = 1)
			SET @tSql = @tSql + ' AND [t0].[TypeBuy] = @iTypeBuy '
		ELSE
			SET @tSql = @tSql + ' AND [t0].[TypeBuy] IN (2,3) '
	END
	IF(@ShipperID IS NOT NULL)
		SET @tSql = @tSql + ' AND [tic].[ShipperID] = @ShipperID '	
	IF(@sSendDate IS NOT NULL)
	BEGIN
		SET @tSql = @tSql + ' AND [t0].[OrderSend] >= CONVERT(datetime,@sSendDate, 103) '
	END
	IF(@sToDate IS NOT NULL)
	BEGIN
		SET @tSql = @tSql + ' AND [t0].[OrderSend] <= CONVERT(datetime,@sToDate, 103) '
	END
	-- END Where
	SET @Sql = @Sql + @tSql + ')';
	SET @SqlVas = @SqlVas + @tSql + ') GROUP BY ov.OrderId, v.Code,v.Name,ov.Price';
	SET @Sql = @Sql + ' UNION ALL ' + @SqlVas;
	EXEC SP_EXECUTESQL @Sql, @Parameter, @SaleID, @InvID, @ShipperID, @CusClassID, @sSendDate, @sToDate, @iTypeBuy
	PRINT @Sql
END
GO

--sonln 20160628
/****** Create:  Table [dbo].[CMS_WarrantyProduct] ******/
CREATE TABLE [dbo].[CMS_WarrantyProduct](
	[ID] [uniqueidentifier] NOT NULL,
	[WarrantyID] [uniqueidentifier] NULL,
	[ProductID] [uniqueidentifier] NULL,
	[ProductName] [nvarchar](250) NULL,
	[PurchaseDate] [datetime] NULL,
	[WarrantyDate] [datetime] NULL,
	[WarrantyStatus] [nvarchar](350) NULL,
	[Price] [DECIMAL](18,2) NULL,
	[Quantity] [INT] NULL,
	[Description] [nvarchar](350) NULL,
	[WarrantyUserID] [uniqueidentifier] NULL,
	[FixerID] [uniqueidentifier] NULL,
	[ReturnDate] [datetime] NULL,
 CONSTRAINT [PK_CMS_WarrantyProduct] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

IF NOT EXISTS (select 1 from sys.columns where name='Price' and object_id = OBJECT_ID('CMS_WarrantyProduct'))
	ALTER TABLE [dbo].[CMS_WarrantyProduct] ADD [Price] [decimal](18,2) NULL
GO
IF NOT EXISTS (select 1 from sys.columns where name='Quantity' and object_id = OBJECT_ID('CMS_WarrantyProduct'))
	ALTER TABLE [dbo].[CMS_WarrantyProduct] ADD [Quantity] [int] NULL
GO

IF OBJECT_ID('[dbo].[sp_CMS_WarrantyProductInsert]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[sp_CMS_WarrantyProductInsert] 
END 
GO
CREATE PROC [dbo].[sp_CMS_WarrantyProductInsert] 
    @ID uniqueidentifier,
    @WarrantyID uniqueidentifier,
    @ProductID uniqueidentifier,
    @ProductName nvarchar(250),
    @PurchaseDate datetime,
    @WarrantyDate datetime,
    @WarrantyStatus nvarchar(350),
	@Price DECIMAL(18,2),
	@Quantity INT,
    @Description nvarchar(350),
    @WarrantyUserID uniqueidentifier,
    @FixerID uniqueidentifier,
    @ReturnDate datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[CMS_WarrantyProduct] ([ID], [WarrantyID], [ProductID], [ProductName], [PurchaseDate], [WarrantyDate], [WarrantyStatus], 	[Price], [Quantity], [Description], [WarrantyUserID], [FixerID], [ReturnDate])
	SELECT @ID, @WarrantyID, @ProductID, @ProductName, @PurchaseDate, @WarrantyDate, @WarrantyStatus, @Price, @Quantity, @Description, @WarrantyUserID, @FixerID, @ReturnDate
	
	-- Begin Return Select <- do not remove
	SELECT [ID], [WarrantyID], [ProductID], [ProductName], [PurchaseDate], [WarrantyDate], [WarrantyStatus], [Price], [Quantity], [Description], [WarrantyUserID], [FixerID], [ReturnDate]
	FROM   [dbo].[CMS_WarrantyProduct]
	WHERE  [ID] = @ID
	-- End Return Select <- do not remove
               
	COMMIT
GO
IF OBJECT_ID('[dbo].[sp_CMS_WarrantyProductUpdate]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[sp_CMS_WarrantyProductUpdate] 
END 
GO
CREATE PROC [dbo].[sp_CMS_WarrantyProductUpdate] 
    @ID uniqueidentifier,
    @WarrantyID uniqueidentifier,
    @ProductID uniqueidentifier,
    @ProductName nvarchar(250),
    @PurchaseDate datetime,
    @WarrantyDate datetime,
    @WarrantyStatus nvarchar(350),
	@Price DECIMAL(18,2),
	@Quantity INT,
    @Description nvarchar(350),
    @WarrantyUserID uniqueidentifier,
    @FixerID uniqueidentifier,
    @ReturnDate datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[CMS_WarrantyProduct]
	SET    [WarrantyID] = @WarrantyID, [ProductID] = @ProductID, [ProductName] = @ProductName, [PurchaseDate] = @PurchaseDate, [WarrantyDate] = @WarrantyDate, [WarrantyStatus] = @WarrantyStatus,
		[Price] = @Price, [Quantity]=@Quantity, [Description] = @Description, [WarrantyUserID] = @WarrantyUserID, [FixerID] = @FixerID, [ReturnDate] = @ReturnDate
	WHERE  [ID] = @ID
	
	-- Begin Return Select <- do not remove
	SELECT [ID], [WarrantyID], [ProductID], [ProductName], [PurchaseDate], [WarrantyDate], [WarrantyStatus], Price, Quantity, [Description], [WarrantyUserID], [FixerID], [ReturnDate]
	FROM   [dbo].[CMS_WarrantyProduct]
	WHERE  [ID] = @ID	
	-- End Return Select <- do not remove

	COMMIT
GO

IF OBJECT_ID('[dbo].[sp_CMS_WarrantyProductDelete]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[sp_CMS_WarrantyProductDelete] 
END 
GO
CREATE PROC [dbo].[sp_CMS_WarrantyProductDelete] 
    @ID uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[CMS_WarrantyProduct]
	WHERE  [ID] = @ID

	COMMIT
GO
IF OBJECT_ID('[dbo].[sp_CMS_WarrantyProductSelectByWarrantyId]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[sp_CMS_WarrantyProductSelectByWarrantyId] 
END 
GO
CREATE PROC [dbo].[sp_CMS_WarrantyProductSelectByWarrantyId] 
    @WarrantyId UNIQUEIDENTIFIER
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT ROW_NUMBER() OVER (ORDER BY wp.[ID]) AS [RowNum], wp.[ID], [WarrantyID], [ProductID], [ProductName], CONVERT(NVARCHAR(10), w.[DateCreated], 103) AS [WarrantyDate], [WarrantyStatus], ISNULL([wp].[Price],0) AS [Price], ISNULL([wp].[Quantity], 0) AS [Quantity],(ISNULL([wp].[Price],0) * ISNULL([wp].[Quantity], 0)) AS [TotalCost], [Description], [WarrantyUserID], [FixerID], CONVERT(NVARCHAR(10), w.[DateComplete], 103) AS [ReturnDate] , 
CONVERT(NVARCHAR(10), [PurchaseDate], 103) AS [PurchaseDate], u1.FullName AS CreatedUser, u2.FullName AS FixerName
	FROM   [dbo].[CMS_WarrantyProduct] AS wp
	INNER JOIN [dbo].[CMS_Warranty] AS w ON wp.WarrantyID = w.ID
	LEFT JOIN [dbo].[ADM_Users] AS u1 ON wp.WarrantyUserID = u1.UserID
	LEFT JOIN [dbo].[ADM_Users] AS u2 ON wp.FixerID = u2.UserID
	WHERE ([WarrantyID] = @WarrantyID) 

	COMMIT
GO

IF OBJECT_ID('[dbo].[sp_CMS_Products_GetSuggestWarrantyProduct]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[sp_CMS_Products_GetSuggestWarrantyProduct] 
END 
GO
CREATE PROCEDURE [dbo].[sp_CMS_Products_GetSuggestWarrantyProduct]	
	@sTitle nvarchar(200)
AS
SET NOCOUNT ON
BEGIN
	SET @sTitle = '%'+@sTitle+'%'
	SELECT [p].[ProductID] AS [productid], [Title] AS [title]
		FROM [CMS_Products] AS [p]
		WHERE [SKU] LIKE @sTitle OR [Title] LIKE @sTitle
END
GO

IF OBJECT_ID('[dbo].[sp_CMS_WarrantyInsert]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[sp_CMS_WarrantyInsert] 
END 
GO
CREATE PROC [dbo].[sp_CMS_WarrantyInsert] 
    @ID uniqueidentifier,
    @UserID uniqueidentifier,
    @Phone nvarchar(50),
    @Name nvarchar(50),
    @DateCreated datetime,
    @Status int,
    @Address nvarchar(200),
    @Email nvarchar(200)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[CMS_Warranty] ([ID], [UserID], [Phone], [Name], [DateCreated], [Status], [Address], [Email])
	SELECT @ID, @UserID, @Phone, @Name, @DateCreated, @Status, @Address, @Email
	
	-- Begin Return Select <- do not remove
	SELECT [ID], [UserID], [Phone], [Name], [DateCreated], [DateComplete], [Status], [Address], [Email]
	FROM   [dbo].[CMS_Warranty]
	WHERE  [ID] = @ID
	-- End Return Select <- do not remove
               
	COMMIT
GO

IF OBJECT_ID('[dbo].[sp_CMS_WarrantyUpdate]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[sp_CMS_WarrantyUpdate] 
END 
GO
CREATE PROC [dbo].[sp_CMS_WarrantyUpdate] 
    @ID uniqueidentifier,
    @UserID uniqueidentifier,
    @Phone nvarchar(50),
    @Name nvarchar(50),
    @DateComplete datetime,
    @Status int,
    @Address nvarchar(200),
    @Email nvarchar(200)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[CMS_Warranty]
	SET    [UserID] = @UserID, [Phone] = @Phone, [Name] = @Name, [DateComplete] = @DateComplete, [Status] = @Status, [Address] = @Address, [Email] = @Email
	WHERE  [ID] = @ID
	
	-- Begin Return Select <- do not remove
	SELECT [ID], [UserID], [Phone], [Name], [DateCreated], [DateComplete], [Status], [Address], [Email]
	FROM   [dbo].[CMS_Warranty]
	WHERE  [ID] = @ID	
	-- End Return Select <- do not remove

	COMMIT
GO

--Fix Warranty tickets list issue
IF OBJECT_ID('[dbo].[sp_CMS_WarrantySelect]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[sp_CMS_WarrantySelect] 
END 
GO
CREATE PROC [dbo].[sp_CMS_WarrantySelect] 
    @ID UNIQUEIDENTIFIER
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [ID], [UserID], [Phone], [Name], CONVERT(NVARCHAR(10), [DateCreated], 103) AS [DateCreated], CONVERT(NVARCHAR(10), [DateComplete], 103) AS [DateComplete], [Status],
		CASE [Status] 
			WHEN 1 THEN N'Nhận bảo hành'
			WHEN 2 THEN N'Chờ bảo hành'
			WHEN 3 THEN N'Đang bảo hành'
			WHEN 4 THEN N'Đã bảo hành'
			WHEN 5 THEN N'Đang trả'
			WHEN 6 THEN N'Đã trả'
		END	AS [StatusName]
	FROM   [dbo].[CMS_Warranty] 
	WHERE  ([ID] = @ID OR @ID IS NULL) 
	ORDER BY [DateCreated] DESC

	COMMIT
GO


IF OBJECT_ID('[dbo].[sp_CMS_WarrantySelectByPhone]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[sp_CMS_WarrantySelectByPhone] 
END 
GO
CREATE PROC [dbo].[sp_CMS_WarrantySelectByPhone] 
    @sPhone NVARCHAR(50)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [ID], [UserID], [Phone], [Name], CONVERT(NVARCHAR(10), [DateCreated], 103) AS [DateCreated], CONVERT(NVARCHAR(10), [DateComplete], 103) AS [DateComplete], [Status],
		CASE [Status] 
			WHEN 1 THEN N'Nhận bảo hành'
			WHEN 2 THEN N'Chờ bảo hành'
			WHEN 3 THEN N'Đang bảo hành'
			WHEN 4 THEN N'Đã bảo hành'
			WHEN 5 THEN N'Đang trả'
			WHEN 6 THEN N'Đã trả'
		END	AS [StatusName]
	FROM   [dbo].[CMS_Warranty] 
	WHERE  ([Phone] = @sPhone) 
	ORDER BY [DateCreated] DESC

	COMMIT
GO