USE [SouthWestTradersDB]
GO

/****** Object:  Table [dbo].[Order]    Script Date: 2022/04/25 09:18:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Order](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[CreatedDateUTC] [datetime] NOT NULL,
	[Quantity] [int] NOT NULL,
	[OrderStateId] [int] NOT NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_OrderState] FOREIGN KEY([OrderStateId])
REFERENCES [dbo].[OrderState] ([OrderStateId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_OrderState]
GO

ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Product]
GO

