#!/bin/bash


set -e


psql -v ON_ERROR_STOP=1 --username "$POSTGRES_USER" --dbname "$POSTGRES_DB" <<-EOSQL

	CREATE USER "$POSTGRES_ADMIN_USER" WITH LOGIN PASSWORD '$POSTGRES_ADMIN_PASSWORD';
	GRANT CONNECT ON DATABASE $POSTGRES_DB TO $POSTGRES_ADMIN_USER;
	GRANT INSERT, SELECT, UPDATE, DELETE ON ALL TABLES IN SCHEMA events_schema TO $POSTGRES_ADMIN_USER;
	GRANT INSERT, SELECT, UPDATE, DELETE ON ALL TABLES IN SCHEMA users_schema TO $POSTGRES_ADMIN_USER;
	GRANT INSERT, SELECT, UPDATE, DELETE ON ALL TABLES IN SCHEMA general_schema TO $POSTGRES_ADMIN_USER;
	GRANT CREATE ON SCHEMA events_schema TO $POSTGRES_ADMIN_USER;
	GRANT CREATE ON SCHEMA users_schema TO $POSTGRES_ADMIN_USER;
	GRANT CREATE ON SCHEMA general_schema TO $POSTGRES_ADMIN_USER;


	CREATE USER "$POSTGRES_CLIENT_USER" WITH LOGIN PASSWORD '$POSTGRES_CLIENT_PASSWORD';
        GRANT CONNECT ON DATABASE $POSTGRES_DB TO $POSTGRES_CLIENT_USER;
        GRANT SELECT ON ALL TABLES IN SCHEMA events_schema TO $POSTGRES_CLIENT_USER;
	GRANT SELECT ON ALL TABLES IN SCHEMA general_schema TO $POSTGRES_CLIENT_USER;
        GRANT SELECT, UPDATE, INSERT, DELETE ON ALL TABLES IN SCHEMA users_schema TO $POSTGRES_CLIENT_USER;
	GRANT SELECT, UPDATE, INSERT ON events_schema.event, events_schema.tag, events_schema.parameter, general_schema.status, general_schema.image TO $POSTGRES_CLIENT_USER;

EOSQL




