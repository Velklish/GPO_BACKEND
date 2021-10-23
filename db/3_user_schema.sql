create schema if not exists user_schema;

drop table if exists user_schema.user_table;

drop table if exists user_schema.role_table;

drop table if exists user_schema.privilege;

drop table if exists user_schema.user_role;

drop table if exists user_schema.role_privilege;


create table user_schema.user_table
(
	user_id serial
		constraint user_pk primary key,
	email VARCHAR(256) not null,
	password VARCHAR(256) not null,
	name VARCHAR(256) not null,
	admin boolean not null default false
);

INSERT INTO user_schema.user_table(email, password, name, admin) VALUES
('admin@mail.com', 'admin', 'Администратор', true);


create table user_schema.role_table 
(
	role_id serial
		constraint role_pk primary key,
	name VARCHAR(256) not null
);


create table user_schema.privilege
(
	privilege_id serial
		constraint privilege_pk primary key,
	object_type VARCHAR(256) not null,
	privilege_type VARCHAR(256) not null,
	object_id integer
);


create table user_schema.user_role 
(
    user_role_id serial
		constraint user_role_pk primary key,
	user_id integer not null,
	role_id integer not null,
	FOREIGN KEY(user_id) references user_schema.user_table(user_id),
	foreign key(role_id) references user_schema.role_table(role_id)
);

create table user_schema.role_privilege 
(
    role_privilege_id serial
		constraint role_privilege_pk primary key,
	privilege_id integer not null,
	role_id integer not null,
	FOREIGN KEY(privilege_id) references user_schema.privilege(privilege_id),
	foreign key(role_id) references user_schema.role_table(role_id)
);
