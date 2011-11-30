
select * into raif from employeetotask
select * into user_temp from User

delete from EmployeeToTask
insert into EmployeeToTask (task_id, user_id) select task_id, employee_id from raif
delete from UserLoginInfo
insert into UserLoginInfo 
	(companyId,
	 LoginName,
	 Password,
	 UserRoles,
	 UserType )
select Companyid,loginName,password,userroles,usertype from user_temp