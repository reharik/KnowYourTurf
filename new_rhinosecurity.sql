



    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKE58BBFF82B7CDCD3]') AND parent_object_id = OBJECT_ID('security_Operations'))
alter table security_Operations  drop constraint FKE58BBFF82B7CDCD3


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKEA223C4C71C937C7]') AND parent_object_id = OBJECT_ID('security_Permissions'))
alter table security_Permissions  drop constraint FKEA223C4C71C937C7


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKEA223C4CFC8C2B95]') AND parent_object_id = OBJECT_ID('security_Permissions'))
alter table security_Permissions  drop constraint FKEA223C4CFC8C2B95


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKEA223C4C2EE8F612]') AND parent_object_id = OBJECT_ID('security_Permissions'))
alter table security_Permissions  drop constraint FKEA223C4C2EE8F612


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKEA223C4C6C8EC3A5]') AND parent_object_id = OBJECT_ID('security_Permissions'))
alter table security_Permissions  drop constraint FKEA223C4C6C8EC3A5


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKEC3AF233D0CB87D0]') AND parent_object_id = OBJECT_ID('security_UsersGroups'))
alter table security_UsersGroups  drop constraint FKEC3AF233D0CB87D0


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK7817F27AA6C99102]') AND parent_object_id = OBJECT_ID('security_UsersToUsersGroups'))
alter table security_UsersToUsersGroups  drop constraint FK7817F27AA6C99102


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK7817F27A1238D4D4]') AND parent_object_id = OBJECT_ID('security_UsersToUsersGroups'))
alter table security_UsersToUsersGroups  drop constraint FK7817F27A1238D4D4


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK69A3B61FA860AB70]') AND parent_object_id = OBJECT_ID('security_UsersGroupsHierarchy'))
alter table security_UsersGroupsHierarchy  drop constraint FK69A3B61FA860AB70


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK69A3B61FA87BAE50]') AND parent_object_id = OBJECT_ID('security_UsersGroupsHierarchy'))
alter table security_UsersGroupsHierarchy  drop constraint FK69A3B61FA87BAE50


    if exists (select * from dbo.sysobjects where id = object_id(N'security_Operations') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table security_Operations

    if exists (select * from dbo.sysobjects where id = object_id(N'security_Permissions') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table security_Permissions

    if exists (select * from dbo.sysobjects where id = object_id(N'security_UsersGroups') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table security_UsersGroups

    if exists (select * from dbo.sysobjects where id = object_id(N'security_UsersToUsersGroups') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table security_UsersToUsersGroups

    if exists (select * from dbo.sysobjects where id = object_id(N'security_UsersGroupsHierarchy') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table security_UsersGroupsHierarchy


    create table security_Operations (
        EntityId bigint identity(1,1),
       Name NVARCHAR(255) not null unique,
       Comment NVARCHAR(1000) null,
       Parent bigint null,
       primary key (EntityId)
    )

    create table security_Permissions (
        EntityId bigint identity(1,1),
       Allow BIT not null,
       Level INT not null,
       Operation bigint not null,
       [User] bigint null,
       UsersGroup bigint null,
       primary key (EntityId),
       [Description] NVARCHAR(1000) null,
    )

    create table security_UsersGroups (
        EntityId bigint identity(1,1),
       Name NVARCHAR(255) not null unique,
       Parent bigint null,
       primary key (EntityId),
       [Description] NVARCHAR(1000) null,
    )

    create table security_UsersToUsersGroups (
        GroupId bigint not null,
       UserId bigint not null,
       primary key (GroupId, UserId)
    )

    create table security_UsersGroupsHierarchy (
        ParentGroup bigint not null,
       ChildGroup bigint not null,
       primary key (ChildGroup, ParentGroup)
    )

    alter table security_Operations 
        add constraint FKE58BBFF82B7CDCD3 
        foreign key (Parent) 
        references security_Operations

    alter table security_Permissions 
        add constraint FKEA223C4C71C937C7 
        foreign key (Operation) 
        references security_Operations

    alter table security_Permissions 
        add constraint FKEA223C4CFC8C2B95 
        foreign key ([User]) 
        references [User]

    alter table security_Permissions 
        add constraint FKEA223C4C2EE8F612 
        foreign key (UsersGroup) 
        references security_UsersGroups

    alter table security_UsersGroups 
        add constraint FKEC3AF233D0CB87D0 
        foreign key (Parent) 
        references security_UsersGroups

    alter table security_UsersToUsersGroups 
        add constraint FK7817F27AA6C99102 
        foreign key (UserId) 
        references [User]

    alter table security_UsersToUsersGroups 
        add constraint FK7817F27A1238D4D4 
        foreign key (GroupId) 
        references security_UsersGroups

    alter table security_UsersGroupsHierarchy 
        add constraint FK69A3B61FA860AB70 
        foreign key (ChildGroup) 
        references security_UsersGroups

    alter table security_UsersGroupsHierarchy 
        add constraint FK69A3B61FA87BAE50 
        foreign key (ParentGroup) 
        references security_UsersGroups

