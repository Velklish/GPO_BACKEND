drop table if exists geom_schema.map;
 
create table geom_schema.map
(
	map_id serial
		constraint map_pk primary key,
	url_name VARCHAR(256) not null,
	project VARCHAR(256) not null,
	name VARCHAR(256) not null,
	description TEXT,
	main boolean not null default false,
	geom_id integer
)
