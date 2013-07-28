SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
alter PROCEDURE [dbo].TDAReport 
	@StartDate	DateTime,
	@EndDate	DateTime,
	@ClientId	int
AS
BEGIN
	SET NOCOUNT ON;
select 

c.Name as BusinessName,
c.[Address],
c.Address2,
c.City,
c.[State],
c.ZipCode,
t.scheduleddate as [Date],
t.StartTime,
f.Name as FieldName,
w.WindSpeed,
w.WindDirection,
w.HighTemperature,
p.Name as ProductName,
t.TargetPest,
t.RatePerUnit,
t.SprayPermitNumber,
t.ApplicationRequestedBy,
f.Size as AreaSprayed,
t.QuantityUsed,
chem.EPARegNumber,
STUFF((SELECT ', ' + em.FirstName +' '+ em.LastName + '( ' + em.LicenseNumber + ' )'
	FROM [User] as em
		left join EmployeeToTask as emtt on em.entityid = emtt.User_id 
		where emtt.Task_id = t.entityid and em.LicenseNumber IS NOT NULL
		FOR XML PATH('')), 1, 1, '') as LicensedEmployees,
STUFF((SELECT ', ' + em.FirstName +' '+ em.LastName 
	FROM [User] as em
		left join EmployeeToTask as emtt on em.entityid = emtt.User_id 
		where emtt.Task_id = t.entityid and em.LicenseNumber IS NULL
		FOR XML PATH('')), 1, 1, '') as UnLicensedEmployees,
STUFF((SELECT ', ' + eq.name + ' ( ' + eq.ID + ' )'
	FROM [Equipment] as eq
		left join EquipmentToTask as eqtt on eq.entityid = eqtt.Equipment_id 
		where eqtt.Task_id = t.entityid 
		FOR XML PATH('')), 1, 1, '') as Equipment
 from 
task as t inner join weather as w on CAST(t.scheduleddate as date) = CAST(w.[date] as date)
inner join field as f on t.field_id = f.entityid
inner join inventoryproduct as ip on t.inventoryproduct_id = ip.entityid
inner join baseproduct as p on ip.product_id = p.entityid
inner join Chemical as chem on p.entityId = chem.baseproduct_id
inner join client as c on t.clientid = c.entityId

where p.[InstantiatingType] = 'Chemical'
AND c.EntityId = @clientId
AND  @StartDate < = CAST(t.ScheduledDate as Date)
AND  @EndDate >= CAST(t.ScheduledDate as DATE )
AND t.Complete = 1

END  