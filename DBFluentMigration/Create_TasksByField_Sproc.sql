
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE TasksByField 
	@FieldId int 
AS
BEGIN
	SET NOCOUNT ON;
select t.ScheduledDate,
t.StartTime,
t.EndTime,
t.ActualTimeSpent,
t.Complete,
p.name as ProductName,
t.QuantityUsed
 from task as t left join InventoryProduct as ip on ip.entityid = t.InventoryProduct_id
 left join baseproduct as p on ip.Product_id = p.entityid
where t.field_id = @FieldId
END
GO
