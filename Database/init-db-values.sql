CREATE SCHEMA events_schema;
CREATE SCHEMA users_schema;
CREATE SCHEMA general_schema;


CREATE TABLE IF NOT EXISTS events_schema.event (
  "id" SERIAL PRIMARY KEY,
  "name" varchar,
  "address_id" integer,
  "description" text,
  "organizer_id" integer,
  "start_at" timestamp,
  "end_at" timestamp,
  "created_at" timestamp,
  "updated_at" timestamp,
  "slug" varchar,
  "status_id" integer
);
 
CREATE TABLE IF NOT EXISTS events_schema.participant (
  "id" SERIAL PRIMARY KEY,
  "event_id" integer,
  "user_id" integer,
  "status_id" int
);
 
CREATE TABLE IF NOT EXISTS general_schema.status (
  "id" SERIAL PRIMARY KEY,
  "name" varchar(255),
  "object_type_id" integer
);
 
CREATE TABLE IF NOT EXISTS events_schema.tag (
  "id" SERIAL PRIMARY KEY,
  "name" varchar(255)
);
 
CREATE TABLE IF NOT EXISTS events_schema.event_tag (
  "event_id" integer,
  "tag_id" integer
);
 
CREATE TABLE IF NOT EXISTS users_schema.user (
  "id" SERIAL PRIMARY KEY,
  "name" varchar(255),
  "surname" varchar(255),
  "username" varchar(255),
  "password" varchar(255),
  "created_at" timestamp,
  "updated_at" timestamp,
  "role_id" integer,
  "email" varchar(255),
  "is_active" bit,
  "address_id" integer,
  "phone" varchar(255)
);
 
CREATE TABLE IF NOT EXISTS events_schema.parameter (
  "id" SERIAL PRIMARY KEY,
  "key" varchar(255),
  "value" varchar(255)
);
 
CREATE TABLE IF NOT EXISTS events_schema.event_parameter (
  "parameter_id" integer,
  "event_id" integer
);
 
CREATE TABLE IF NOT EXISTS users_schema.role (
  "id" SERIAL PRIMARY KEY,
  "role" varchar(255)
);
 
CREATE TABLE IF NOT EXISTS users_schema.comment (
  "id" SERIAL PRIMARY KEY,
  "event_id" integer,
  "content" varchar(255),
  "created_at" date,
  "user_id" integer
);
 
CREATE TABLE IF NOT EXISTS users_schema.address (
  "id" SERIAL PRIMARY KEY,
  "city" varchar(255),
  "country" varchar(255),
  "street" varchar(255),
  "state" varchar(255),
  "zip_code" varchar(255)
);
 
CREATE TABLE IF NOT EXISTS general_schema.image (
  "id" SERIAL PRIMARY KEY,
  "image_data" bytea,
  "object_type_id" integer,
  "object_id" integer
);
 
CREATE TABLE IF NOT EXISTS general_schema.object_type (
  "id" SERIAL PRIMARY KEY,
  "name" varchar(255)
);
 
CREATE TABLE IF NOT EXISTS users_schema.user_interest (
  "user_id" integer,
  "interest_id" integer
);
 
CREATE TABLE IF NOT EXISTS users_schema.interest (
  "id" SERIAL PRIMARY KEY,
  "name" varchar(255),
  "level" integer
);
 

ALTER TABLE users_schema.user ADD FOREIGN KEY ("role_id") REFERENCES users_schema.role ("id");
 
ALTER TABLE events_schema.event ADD FOREIGN KEY ("address_id") REFERENCES users_schema.address ("id");
 
ALTER TABLE users_schema.comment ADD FOREIGN KEY ("event_id") REFERENCES events_schema.event ("id");
 
ALTER TABLE events_schema.event ADD FOREIGN KEY ("organizer_id") REFERENCES users_schema.user ("id");
 
ALTER TABLE events_schema.participant ADD FOREIGN KEY ("event_id") REFERENCES events_schema.event ("id");
 
ALTER TABLE events_schema.participant ADD FOREIGN KEY ("user_id") REFERENCES users_schema.user ("id");
 
ALTER TABLE users_schema.comment ADD FOREIGN KEY ("user_id") REFERENCES users_schema.user ("id");
 
ALTER TABLE general_schema.image ADD FOREIGN KEY ("object_type_id") REFERENCES general_schema.object_type ("id");
 
ALTER TABLE users_schema.user_interest ADD FOREIGN KEY ("user_id") REFERENCES users_schema.user ("id");
 
ALTER TABLE users_schema.user ADD FOREIGN KEY ("address_id") REFERENCES users_schema.address ("id");
 
ALTER TABLE general_schema.image ADD CONSTRAINT image_object_id_fkey_event FOREIGN KEY ("object_id") REFERENCES events_schema.event ("id"); 

ALTER TABLE general_schema.image ADD CONSTRAINT image_object_id_fkey_user FOREIGN KEY ("object_id") REFERENCES users_schema.user ("id"); 
 
ALTER TABLE events_schema.participant ADD FOREIGN KEY ("status_id") REFERENCES general_schema.status ("id");
 
ALTER TABLE events_schema.event_parameter ADD FOREIGN KEY ("parameter_id") REFERENCES events_schema.parameter ("id");
 
ALTER TABLE events_schema.event_parameter ADD FOREIGN KEY ("event_id") REFERENCES events_schema.event ("id");
 
ALTER TABLE events_schema.event_tag ADD FOREIGN KEY ("tag_id") REFERENCES events_schema.tag ("id");
 
ALTER TABLE events_schema.event_tag ADD FOREIGN KEY ("event_id") REFERENCES events_schema.event ("id");
 
ALTER TABLE general_schema.status ADD FOREIGN KEY ("object_type_id") REFERENCES general_schema.object_type ("id");
 
ALTER TABLE events_schema.event ADD FOREIGN KEY ("status_id") REFERENCES general_schema.status ("id");
 
ALTER TABLE users_schema.user_interest ADD FOREIGN KEY ("interest_id") REFERENCES users_schema.interest ("id");
 

INSERT INTO users_schema.role ("id", "role") VALUES
(1, 'User'),
(2, 'Organizer'),
(3, 'Admin');
 
INSERT INTO general_schema.object_type ("id", "name") VALUES (1, 'Event'),
(2, 'User');
 
INSERT INTO general_schema.status ("id", "name", "object_type_id") VALUES
(1, 'Interested', 2),
(2, 'Participants', 2),
(3, 'Archived', 1),
(4, 'In progress', 1);
 
INSERT INTO events_schema.tag ("id", "name") VALUES
(1, 'Technology'),
(2, 'Health');
 

