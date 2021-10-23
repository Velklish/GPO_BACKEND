drop function if exists proc_schema.get_codes(attribute_ids int[]);
create or replace function proc_schema.get_codes(attribute_ids int[]) returns varchar
as
$$
declare
    result varchar := '';
    i integer;
    id integer;
begin
    i := 0;
    foreach id IN ARRAY attribute_ids LOOP
        result := concat(result, ', v_', i, '.value AS ', (SELECT code FROM attr_schema.attribute WHERE attribute_id = id));
        i:= i + 1;
    end loop;
    result := concat(result, ' ');
    return result;
END;
$$ LANGUAGE plpgsql;

drop function if exists proc_schema.get_attributes_id(entity_type_id_param int);
create or replace function proc_schema.get_attributes_id(entity_type_id_param int) returns setof int
as
$$
declare
    rec RECORD;
begin
    for rec in SELECT attr.attribute_id
        FROM attr_schema.attribute AS attr
        WHERE attr.entity_type_id = entity_type_id_param
    LOOP
        RETURN NEXT rec.attribute_id;
    END LOOP;
    return;
end;
$$ LANGUAGE plpgsql;


drop function if exists proc_schema.get_values_join(int);
create or replace function proc_schema.get_values_join(count int) returns varchar
as
$$
declare
    result varchar := '';
    rec RECORD;
    i integer;
begin
    i := 0;
    FOR i IN 0 .. count - 1
    LOOP
        result := concat(result, 'INNER JOIN attr_schema.attribute AS attr_', i, ' ON e.entity_type_id = attr_', i, '.entity_type_id ' ||
                                  'INNER JOIN attr_schema.value AS v_', i, ' ON e.entity_id = v_', i, '.entity_id AND v_', i, '.attribute_id = attr_', i, '.attribute_id ');
    end loop;
    return result;
end;
$$ LANGUAGE plpgsql;


drop function if exists proc_schema.create_geom_view(entity_type_id_param int);
create or replace function proc_schema.create_geom_view(entity_type_id_param int) returns varchar AS $$
    declare
        view_ddl text;
        geom_table text;
        attributes_id int[];
        attr_id int;
        i int;
BEGIN
    geom_table = (SELECT layer_table_name FROM attr_schema.entity_type
              WHERE entity_type_id = entity_type_id_param);

    attributes_id = ARRAY(SELECT * FROM proc_schema.get_attributes_id(entity_type_id_param));

    execute 'DROP VIEW IF EXISTS geom_schema.' || geom_table || '_view';

    view_ddl = 'CREATE VIEW geom_schema.' || geom_table || '_view AS ' ||
    'SELECT id, geom' || proc_schema.get_codes(attributes_id) ||
    'FROM geom_schema.' || geom_table ||
          ' AS geom_table ' ||
          'INNER JOIN attr_schema.entity AS e ON geom_table.id=e.geom_id ' ||
          'INNER JOIN attr_schema.entity_type AS et ON e.entity_type_id=et.entity_type_id ' ||
          (select * from proc_schema.get_values_join(array_length(attributes_id, 1))) ||
          'WHERE et.entity_type_id = ' || entity_type_id_param || ' ';

    i := 0;
    FOREACH attr_id IN ARRAY attributes_id LOOP
        view_ddl := concat(view_ddl, ' AND attr_' || i || '.attribute_id = ' || attr_id || ' ');
        i := i + 1;
    end loop;
    view_ddl := concat (view_ddl, ' ;');
    execute view_ddl;
    return view_ddl;
END;
$$ language plpgsql;


drop function if exists proc_schema.get_geom_id(int);
create or replace function proc_schema.get_geom_id(entity_type_id_param int) returns int[]
as
$$
declare
    result int[];
begin
    EXECUTE 'SELECT array(SELECT id FROM geom_schema.' ||
            (SELECT layer_table_name FROM attr_schema.entity_type WHERE entity_type_id = entity_type_id_param)
                || ' );'  INTO result;
    return result;
end;
$$ LANGUAGE plpgsql;
