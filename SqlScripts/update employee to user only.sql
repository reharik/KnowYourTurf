
select * into raif from employeetotask

delete from EmployeeToTask
insert into EmployeeToTask (task_id, user_id) select task_id, employee_id from raif