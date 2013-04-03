SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].TDAReport 
	@StartDate	DateTime,
	@EndDate	DateTime,
	@ClientId	int
AS
BEGIN
	SET NOCOUNT ON;
select 

c.Name as BusinessName,
c.ZipCode,
t.scheduleddate as [Date],
t.StartTime,
f.Name as FieldName,
w.WindSpeed,
w.HighTemperature,
p.Name as ProductName,
t.TargetPest,
t.RatePerUnit,
t.SprayPermitNumber,
f.Size as AreaSprayed,
t.QuantityUsed



 from 
task as t inner join weather as w on t.scheduleddate = w.[date]
inner join field as f on t.field_id = f.entityid
inner join inventoryproduct as ip on t.inventoryproduct_id = ip.entityid
inner join baseproduct as p on ip.product_id = p.entityid
inner join client as c on t.clientid = c.entityId

where p.[InstantiatingType] = 'Chemical'
AND c.EntityId = @clientId
AND  @StartDate < = CAST(t.ScheduledDate as Date)
AND  @EndDate >= CAST(t.ScheduledDate as DATE )

END  