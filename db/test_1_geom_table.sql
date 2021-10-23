create table geom_schema.buildings
(
	id serial
		constraint buildings_pk
			primary key,
	something text,
	geom text default 'Геометрия'
);

INSERT INTO geom_schema.buildings (something) VALUES
(1), (2), (3), (4), ('five'), (6), (7), ('fsgasg'), ('asdfasdfasfdasdf');


create table geom_schema.rivers
(
	id serial
		constraint rivers_pk
			primary key,
	something text,
	geom text default 'Геометрия'
);

INSERT INTO geom_schema.rivers (something) VALUES
(1), (2), (3), (4), ('five'), (6), (7), ('fsgasg'), ('asdfasdfasfdasdf');


create table geom_schema.lakes
(
	id serial
		constraint lakes_pk
			primary key,
	something text,
	geom text default 'Геометрия'
);

INSERT INTO geom_schema.lakes (something) VALUES
(1), (2), (3), (4), ('five'), (6), (7), ('fsgasg'), ('asdfasdfasfdasdf');
