-- ================================================
-- Template generated from Template Explorer using:
-- Create Trigger (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- See additional Create Trigger templates for more
-- examples of different Trigger statements.
--
-- This block of comments will not be included in
-- the definition of the function.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sergey
-- Create date: 18.05.22
-- Description:	Check the possibility of payment before inserting
-- =============================================
CREATE TRIGGER dbo.TriggerPayment 
   ON  dbo.Transactions 
   AFTER INSERT
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @order_price decimal;
	DECLARE @order_sumPayment decimal;
	DECLARE @income_balance decimal;
	DECLARE @payment_sum decimal;

	SET @order_price = (SELECT Orders.Sum FROM Orders 
		LEFT JOIN inserted ON Orders.Id = inserted.OrderId
		WHERE Orders.Id = inserted.OrderId);

	SET @order_sumPayment = (SELECT Orders.SumPay FROM Orders 
		LEFT JOIN inserted ON Orders.Id = inserted.OrderId
		WHERE Orders.Id = inserted.OrderId);

	SET @income_balance = (SELECT Incomes.Remainder FROM Incomes 
		LEFT JOIN inserted ON Incomes.Id = inserted.IncomeId
		WHERE Incomes.Id = inserted.IncomeId);

	SET @payment_sum = (SELECT inserted.Sum FROM inserted);

		IF (@order_sumPayment + @payment_sum <= @order_price)
			UPDATE Orders
			SET Orders.SumPay = @order_sumPayment + @payment_sum
			FROM inserted 
			WHERE Orders.Id = inserted.OrderId;
		ELSE
		BEGIN
			ROLLBACK TRANSACTION;
			RAISERROR ('The payment amount exceeds the amount required to pay for the order', 16, 1);
			RETURN;
		END			

		IF (@income_balance - @payment_sum >= 0)
			UPDATE Incomes
			SET Incomes.Remainder = @income_balance - @payment_sum
			FROM inserted
			WHERE Incomes.Id = inserted.IncomeId;
		ELSE
		BEGIN 
			ROLLBACK TRANSACTION;
			RAISERROR ('Insufficient account balance to pay', 16, 1);
			RETURN;
		END		
END
GO