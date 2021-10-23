drop table if exists geom_schema.layer;
 
create table geom_schema.layer
(
	layer_id serial
		constraint layer_pk primary key,
	layer_table_name VARCHAR(256) not null
)
