
create table sys_users
(
   [user_id] uniqueidentifier not null,
   user_title nvarchar(100) null,
   user_login_name nvarchar(50) null,
   user_skype nvarchar(50) null,
   user_phone nvarchar(50) null,
   user_email nvarchar(50) null,
   user_description nvarchar(max) null,
   user_active_flag bit not null,
   user_admin_flag bit not null,
   user_password uniqueidentifier null,
   user_modified_dt datetime null,
   primary key([user_id])



)
go
create table sys_sessions
(
   session_id      uniqueidentifier not null,
   session_user_id uniqueidentifier not null,
   session_expired_dt datetime not null,
   session_created_dt datetime not null,
   primary key(session_id)
)
go
  insert into sys_users(user_id,user_login_name,user_title,user_admin_flag,user_active_flag,user_password,user_modified_dt)
  values('{3842cac4-b9a0-8223-0dcc-509a6f75849b}','Admin','Sys Admin',1,1,'{3842cac4-b9a0-8223-0dcc-509a6f75849b}',GetDate())

go
  update sys_users set user_active_flag = 1 , user_password = '{3842cac4-b9a0-8223-0dcc-509a6f75849b}'
go
create table projects
(
  project_id uniqueidentifier not null,
  project_created_id uniqueidentifier not null,
  project_created_dt datetime not null,
  project_modified_id uniqueidentifier not null,
  project_modified_dt datetime not null,
  project_code nvarchar(50),
  project_title nvarchar(100),
  project_description nvarchar(max),
  primary key(project_id)
)
go


create table pbs
(
  pbs_id        uniqueidentifier not null,
  pbs_root_id   uniqueidentifier not null,
  pbs_parent_id uniqueidentifier not null,
  pbs_owner_id  uniqueidentifier  null,
  
  pbs_index int not null,
  pbs_level int not null,
  pbs_tech_id   int null,

  pbs_created_id uniqueidentifier not null,
  pbs_created_dt datetime not null,
  pbs_modified_id uniqueidentifier not null,
  pbs_modified_dt datetime not null,
  pbs_code nvarchar(50),
  pbs_title nvarchar(100),
  pbs_description nvarchar(max),
  primary key(pbs_id)
)
go 
create table tech
(
    tech_id int not null,
	tech_code nvarchar(50),
	tech_name nvarchar(100),
	primary key (tech_id)

)