#!/bin/bash
set -e

psql -v ON_ERROR_STOP=1 --username "$POSTGRES_USER" --dbname "$POSTGRES_DB" <<-EOSQL
  CREATE USER "$POSTGRES_ADMIN_USER" WITH LOGIN PASSWORD '$POSTGRES_ADMIN_PASSWORD';
  GRANT CONNECT ON DATABASE $POSTGRES_DB TO $POSTGRES_ADMIN_USER;
  GRANT INSERT, SELECT, UPDATE, DELETE ON ALL TABLES IN SCHEMA events TO $POSTGRES_ADMIN_USER;
  GRANT INSERT, SELECT, UPDATE, DELETE ON ALL TABLES IN SCHEMA users TO $POSTGRES_ADMIN_USER;
  GRANT INSERT, SELECT, UPDATE, DELETE ON ALL TABLES IN SCHEMA common TO $POSTGRES_ADMIN_USER;
  GRANT CREATE ON SCHEMA events TO $POSTGRES_ADMIN_USER;
  GRANT CREATE ON SCHEMA users TO $POSTGRES_ADMIN_USER;
  GRANT CREATE ON SCHEMA common TO $POSTGRES_ADMIN_USER;
  
  CREATE USER "$POSTGRES_CLIENT_USER" WITH LOGIN PASSWORD '$POSTGRES_CLIENT_PASSWORD';
  GRANT CONNECT ON DATABASE $POSTGRES_DB TO $POSTGRES_CLIENT_USER;
  GRANT SELECT ON ALL TABLES IN SCHEMA events TO $POSTGRES_CLIENT_USER;
  GRANT SELECT ON ALL TABLES IN SCHEMA common TO $POSTGRES_CLIENT_USER;
  GRANT SELECT, UPDATE, INSERT, DELETE ON ALL TABLES IN SCHEMA users TO $POSTGRES_CLIENT_USER;
  GRANT UPDATE, INSERT ON events.event, events.tag, events.parameter, common.status, common.image TO $POSTGRES_CLIENT_USER;
  
EOSQL
