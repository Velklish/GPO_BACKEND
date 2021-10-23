-- Таблица MAP

INSERT INTO geom_schema.map(map_id, url_name, project, name, description, main, geom_id) VALUES
(1234, 'test_map', 'project.qgs', 'Тестовая карта', 'Это тестовая карта', false, null);

-- Таблица LAYERS

INSERT INTO geom_schema.layer(layer_id, layer_table_name) VALUES 
(2234, 'buildings'), (2245, 'rivers'), (2267, 'lakes');



--Пользователи

INSERT INTO user_schema.user_table(user_id, email, password, name) VALUES
(100, 'petya@mail.com', '12345', 'Петр'),
(101, 'viktor@mail.com', '12345', 'Виктор'),
(102, 'anya@mail.com', '12345', 'Анна'),
(103, 'egor@mail.com', '12345', 'Егор');

-- Роли

INSERT INTO user_schema.role_table(role_id, name) VALUES 
(200, 'Озерщик'), 
(201, 'Строитель'), 
(202, 'Всезнающий');

INSERT INTO user_schema.user_role(role_id, user_id) VALUES
(200, 100),
(201, 101),
(200, 101),
(202, 103);

-- Привилегии

INSERT INTO user_schema.privilege(privilege_id, object_type, privilege_type, object_id) VALUES
(300, 'MAP', 'READ', 1234),
(301, 'LAYER', 'READ', 2234),
(302, 'LAYER', 'READ', 2245),
(303, 'LAYER', 'READ', 2267),
(304, 'LAYER', 'WRITE', 2234),
(305, 'LAYER', 'WRITE', 2245),
(306, 'LAYER', 'WRITE', 2267);

INSERT INTO user_schema.role_privilege(role_id, privilege_id) VALUES
(200, 300),
(201, 300),
(202, 300),

(200, 303),
(201, 304),
(202, 304),
(202, 305),
(202, 306);
