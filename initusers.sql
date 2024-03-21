CREATE ROLE admindb WITH LOGIN PASSWORD 'admin_password';

GRANT CONNECT ON DATABASE reasn TO admindb;
GRANT USAGE ON SCHEMA public TO admindb;
GRANT INSERT, SELECT, UPDATE, DELETE ON ALL TABLES IN SCHEMA public TO admindb;
REVOKE ALL ON event_parameter, event_tag, user_interest  FROM admindb;


CREATE ROLE userdb WITH LOGIN PASSWORD 'user_password';

GRANT CONNECT ON DATABASE reasn TO userdb;
GRANT SELECT ON ALL TABLES IN SCHEMA public TO userdb;
GRANT SELECT, UPDATE, INSERT, DELETE ON comment, event, address, interest TO userdb;
GRANT SELECT, UPDATE, INSERT ("name", surname, username, "password", email, phone) ON "user" TO userdb

