create schema if not exists attr_schema;

create schema if not exists geom_schema;

create schema if not exists proc_schema;

drop table if exists attr_schema.entity_type cascade;

drop table if exists attr_schema.entity cascade;

drop table if exists attr_schema.attribute cascade;

drop table if exists attr_schema.value cascade;

drop type if exists attr_schema.data_type;

drop index if exists attr_schema.entity_type_layer_table_name_index;

drop index if exists attr_schema.value_entity_id_attribute_id_index;

create table attr_schema.entity_type
(
    entity_type_id    serial                            -- Первичный ключ
        constraint entity_type_pk
            primary key,
    name              varchar(256)         not null,    -- Название типа сущности, например "Трубы"
    layer_table_name  varchar(256)         not null,    -- Название таблицы-слоя, например "Truby"
    active            boolean default true not null,    -- Активный ли слой. Резервное поле
    UNIQUE (layer_table_name)
);

comment on table attr_schema.entity_type is 'Тип сущности - набор аттрибутов для объектов слоя';

create unique index  entity_type_layer_table_name_index
    on attr_schema.entity_type(layer_table_name);

create table attr_schema.entity
(
    entity_id      serial                       -- Первичный ключ
        constraint entity_pk
            primary key,
    entity_type_id int                          -- Тип сущности
        constraint entity_type_id_fk
            references attr_schema.entity_type
            on update cascade on delete cascade,
    geom_id        int not null                 -- id объекта
);

comment on table attr_schema.entity is 'Сущности - объекты';


create table attr_schema.attribute
(
    attribute_id   serial                           -- Первичный ключ
        constraint attribute_pk
            primary key,
    entity_type_id int             not null         -- Тип сущности
        constraint entity_type_id_fk
            references attr_schema.entity_type
            on update cascade on delete cascade,
    name           varchar(256)    not null,        -- Имя атрибута
    code           varchar(256)    not null,        -- Код атрибута, название его столбца в view
    order_num         int              not null,        -- Какой по порядку атрибут в entity_type
    value_type    varchar(64) not null,    -- Тип значения
    CHECK (value_type IN ('int', 'float', 'boolean', 'string'))
);

comment on table attr_schema.attribute is 'Атрибуты - свойства сущностей';

create table attr_schema.value
(
    value_id serial                         -- Первичный ключ
        constraint value_pk
            primary key,
    entity_id    int           not null     -- Сущность
        constraint entity_id_fk
            references attr_schema.entity
            on update cascade on delete cascade,
    attribute_id int           not null     -- Атрибут
        constraint attribute_id_fk
            references attr_schema.attribute
            on update cascade on delete cascade,
    value text default '' not null          -- Значение
);

comment on table attr_schema.value is 'Значения';

create unique index value_entity_id_attribute_id_index ON attr_schema.value(entity_id, attribute_id);
