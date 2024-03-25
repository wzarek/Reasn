CREATE SCHEMA events;
CREATE SCHEMA users;
CREATE SCHEMA common;

CREATE TABLE IF NOT EXISTS events.event (
  "id" SERIAL PRIMARY KEY,
  "name" varchar NOT NULL,
  "address_id" integer NOT NULL,
  "description" text NOT NULL,
  "organizer_id" integer NOT NULL,
  "start_at" timestamptz NOT NULL,
  "end_at" timestamptz NOT NULL,
  "created_at" timestamptz NOT NULL,
  "updated_at" timestamptz NOT NULL,
  "slug" varchar NOT NULL,
  "status_id" integer NOT NULL
);
 
CREATE TABLE IF NOT EXISTS events.participant (
  "id" SERIAL PRIMARY KEY NOT NULL,
  "event_id" integer NOT NULL,
  "user_id" integer NOT NULL,
  "status_id" integer NOT NULL
);
 
CREATE TABLE IF NOT EXISTS common.status (
  "id" SERIAL PRIMARY KEY,
  "name" varchar(255) NOT NULL,
  "object_type_id" integer NOT NULL
);
 
CREATE TABLE IF NOT EXISTS events.tag (
  "id" SERIAL PRIMARY KEY,
  "name" varchar(255) NOT NULL
);
 
CREATE TABLE IF NOT EXISTS events.event_tag (
  "event_id" integer NOT NULL,
  "tag_id" integer NOT NULL
);
 
CREATE TABLE IF NOT EXISTS users.user (
  "id" SERIAL PRIMARY KEY,
  "name" varchar(255) NOT NULL,
  "surname" varchar(255) NOT NULL,
  "username" varchar(255) NOT NULL,
  "password" varchar(255) NOT NULL,
  "created_at" timestamptz NOT NULL,
  "updated_at" timestamptz NOT NULL,
  "role_id" integer NOT NULL,
  "email" varchar(255) NOT NULL,
  "is_active" boolean NOT NULL,
  "address_id" integer NOT NULL,
  "phone" varchar(255)
);
 
CREATE TABLE IF NOT EXISTS events.parameter (
  "id" SERIAL PRIMARY KEY,
  "key" varchar(255) NOT NULL,
  "value" varchar(255) NOT NULL
);
 
CREATE TABLE IF NOT EXISTS events.event_parameter (
  "parameter_id" integer NOT NULL,
  "event_id" integer NOT NULL
);
 
CREATE TABLE IF NOT EXISTS users.role (
  "id" SERIAL PRIMARY KEY NOT NULL,
  "role" varchar(255) NOT NULL
);
 
CREATE TABLE IF NOT EXISTS events.comment (
  "id" SERIAL PRIMARY KEY,
  "event_id" integer NOT NULL,
  "content" varchar(255) NOT NULL,
  "created_at" date NOT NULL,
  "user_id" integer NOT NULL
);
 
CREATE TABLE IF NOT EXISTS common.address (
  "id" SERIAL PRIMARY KEY,
  "city" varchar(255) NOT NULL,
  "country" varchar(255) NOT NULL,
  "street" varchar(255) NOT NULL,
  "state" varchar(255) NOT NULL,
  "zip_code" varchar(255)
);
 
CREATE TABLE IF NOT EXISTS common.image (
  "id" SERIAL PRIMARY KEY,
  "image_data" bytea NOT NULL,
  "object_type_id" integer NOT NULL,
  "object_id" integer  NOT NULL
);
 
CREATE TABLE IF NOT EXISTS common.object_type (
  "id" SERIAL PRIMARY KEY,
  "name" varchar(255) NOT NULL
);
 
CREATE TABLE IF NOT EXISTS users.user_interest (
  "user_id" integer NOT NULL,
  "interest_id" integer NOT NULL
);
 
CREATE TABLE IF NOT EXISTS users.interest (
  "id" SERIAL PRIMARY KEY,
  "name" varchar(255) NOT NULL,
  "level" integer NOT NULL
);
 
ALTER TABLE users.user ADD FOREIGN KEY ("role_id") REFERENCES users.role ("id");
 
ALTER TABLE events.event ADD FOREIGN KEY ("address_id") REFERENCES common.address ("id");
 
ALTER TABLE events.comment ADD FOREIGN KEY ("event_id") REFERENCES events.event ("id");
 
ALTER TABLE events.event ADD FOREIGN KEY ("organizer_id") REFERENCES users.user ("id");
 
ALTER TABLE events.participant ADD FOREIGN KEY ("event_id") REFERENCES events.event ("id");
 
ALTER TABLE events.participant ADD FOREIGN KEY ("user_id") REFERENCES users.user ("id");
 
ALTER TABLE events.comment ADD FOREIGN KEY ("user_id") REFERENCES users.user ("id");
 
ALTER TABLE common.image ADD FOREIGN KEY ("object_type_id") REFERENCES common.object_type ("id");
 
ALTER TABLE users.user_interest ADD FOREIGN KEY ("user_id") REFERENCES users.user ("id");
 
ALTER TABLE users.user ADD FOREIGN KEY ("address_id") REFERENCES common.address ("id");
 
ALTER TABLE events.participant ADD FOREIGN KEY ("status_id") REFERENCES common.status ("id");
 
ALTER TABLE events.event_parameter ADD FOREIGN KEY ("parameter_id") REFERENCES events.parameter ("id");
 
ALTER TABLE events.event_parameter ADD FOREIGN KEY ("event_id") REFERENCES events.event ("id");
 
ALTER TABLE events.event_tag ADD FOREIGN KEY ("tag_id") REFERENCES events.tag ("id");
 
ALTER TABLE events.event_tag ADD FOREIGN KEY ("event_id") REFERENCES events.event ("id");
 
ALTER TABLE common.status ADD FOREIGN KEY ("object_type_id") REFERENCES common.object_type ("id");
 
ALTER TABLE events.event ADD FOREIGN KEY ("status_id") REFERENCES common.status ("id");
 
ALTER TABLE users.user_interest ADD FOREIGN KEY ("interest_id") REFERENCES users.interest ("id");
 
INSERT INTO users.role ("id", "role") VALUES
(1, 'Użytkownik'),
(2, 'Organizator'),
(3, 'Admin');
 
INSERT INTO common.object_type ("id", "name") VALUES (1, 'Wydarzenie'),
(2, 'Użytkownik');
 
INSERT INTO common.status ("id", "name", "object_type_id") VALUES
(1, 'Zainteresoway', 2),
(2, 'Wezmę udział', 2),
(3, 'Zrealizowano', 1),
(4, 'W trakcie realizacji', 1);
 
INSERT INTO events.tag ("id", "name") VALUES
(1, 'Technologia'),
(2, 'Sport'),
(3, 'Piłka nożna'),
(4, 'Muzyka'),
(5, 'Rock'),
(6, 'Koszykówka'),
(7, 'Hip-Hop'),
(8, 'Esport'),
(9, 'League of Legends'),
(10, 'Zdrowie');