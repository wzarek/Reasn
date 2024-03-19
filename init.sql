CREATE ROLE admin_user WITH LOGIN PASSWORD 'admin_password' SUPERUSER;

CREATE ROLE readonly_user WITH LOGIN PASSWORD 'readonly_password';

GRANT CONNECT ON DATABASE reasn TO readonly_user;
GRANT USAGE ON SCHEMA public TO readonly_user;
GRANT SELECT ON ALL TABLES IN SCHEMA public TO readonly_user;

CREATE TABLE IF NOT EXISTS "event" (
  "id" integer PRIMARY KEY,
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
 
CREATE TABLE IF NOT EXISTS "participant" (
  "id" integer PRIMARY KEY,
  "event_id" integer,
  "user_id" integer,
  "status_id" int
);
 
CREATE TABLE IF NOT EXISTS "status" (
  "id" integer PRIMARY KEY,
  "name" varchar(255),
  "object_type_id" integer
);
 
CREATE TABLE IF NOT EXISTS "tag" (
  "id" integer PRIMARY KEY,
  "name" varchar(255)
);
 
CREATE TABLE IF NOT EXISTS "event_tag" (
  "event_id" integer,
  "tag_id" integer
);
 
CREATE TABLE IF NOT EXISTS "user" (
  "id" integer PRIMARY KEY,
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
 
CREATE TABLE IF NOT EXISTS "parameter" (
  "id" integer PRIMARY KEY,
  "key" varchar(255),
  "value" varchar(255)
);
 
CREATE TABLE IF NOT EXISTS "event_parameter" (
  "parameter_id" integer,
  "event_id" integer
);
 
CREATE TABLE IF NOT EXISTS "role" (
  "id" integer PRIMARY KEY,
  "role" varchar(255)
);
 
CREATE TABLE IF NOT EXISTS "comment" (
  "id" integer PRIMARY KEY,
  "event_id" integer,
  "content" varchar(255),
  "created_at" date,
  "user_id" integer
);
 
CREATE TABLE IF NOT EXISTS "address" (
  "id" integer PRIMARY KEY,
  "city" varchar(255),
  "country" varchar(255),
  "street" varchar(255),
  "state" varchar(255),
  "zip_code" varchar(255)
);
 
CREATE TABLE IF NOT EXISTS "image" (
  "id" integer PRIMARY KEY,
  "image_data" bytea,
  "object_type_id" integer,
  "object_id" integer
);
 
CREATE TABLE IF NOT EXISTS "object_type" (
  "id" integer PRIMARY KEY,
  "name" varchar(255)
);
 
CREATE TABLE IF NOT EXISTS "user_interest" (
  "user_id" integer,
  "interest_id" integer
);
 
CREATE TABLE IF NOT EXISTS "interest" (
  "id" integer PRIMARY KEY,
  "name" varchar(255),
  "level" integer
);
 
CREATE TABLE IF NOT EXISTS "xddd" (
  "id" integer  
);
 
ALTER TABLE "user" ADD FOREIGN KEY ("role_id") REFERENCES "role" ("id");
 
ALTER TABLE "event" ADD FOREIGN KEY ("address_id") REFERENCES "address" ("id");
 
ALTER TABLE "comment" ADD FOREIGN KEY ("event_id") REFERENCES "event" ("id");
 
ALTER TABLE "event" ADD FOREIGN KEY ("organizer_id") REFERENCES "user" ("id");
 
ALTER TABLE "participant" ADD FOREIGN KEY ("event_id") REFERENCES "event" ("id");
 
ALTER TABLE "participant" ADD FOREIGN KEY ("user_id") REFERENCES "user" ("id");
 
ALTER TABLE "comment" ADD FOREIGN KEY ("user_id") REFERENCES "user" ("id");
 
ALTER TABLE "image" ADD FOREIGN KEY ("object_type_id") REFERENCES "object_type" ("id");
 
ALTER TABLE "user_interest" ADD FOREIGN KEY ("user_id") REFERENCES "user" ("id");
 
ALTER TABLE "user" ADD FOREIGN KEY ("address_id") REFERENCES "address" ("id");
 
ALTER TABLE "image" ADD FOREIGN KEY ("object_id") REFERENCES "event" ("id");
 
ALTER TABLE "image" ADD FOREIGN KEY ("object_id") REFERENCES "user" ("id");
 
ALTER TABLE "participant" ADD FOREIGN KEY ("status_id") REFERENCES "status" ("id");
 
ALTER TABLE "event_parameter" ADD FOREIGN KEY ("parameter_id") REFERENCES "parameter" ("id");
 
ALTER TABLE "event_parameter" ADD FOREIGN KEY ("event_id") REFERENCES "event" ("id");
 
ALTER TABLE "event_tag" ADD FOREIGN KEY ("tag_id") REFERENCES "tag" ("id");
 
ALTER TABLE "event_tag" ADD FOREIGN KEY ("event_id") REFERENCES "event" ("id");
 
ALTER TABLE "status" ADD FOREIGN KEY ("object_type_id") REFERENCES "object_type" ("id");
 
ALTER TABLE "event" ADD FOREIGN KEY ("status_id") REFERENCES "status" ("id");
 
ALTER TABLE "user_interest" ADD FOREIGN KEY ("interest_id") REFERENCES "interest" ("id");
 
INSERT INTO "role" ("id", "role") VALUES
(1, 'System Analyst'),
(2, 'Project Manager');
 
INSERT INTO "address" ("id", "city", "country", "street", "state", "zip_code") VALUES
(1, 'Springfield', 'USA', 'Main St', 'Illinois', '62701'),
(2, 'Shelbyville', 'USA', 'Oak St', 'Illinois', '62702');
 
INSERT INTO "object_type" ("id", "name") VALUES (1, 'Event'),
(2, 'User');
 
INSERT INTO "status" ("id", "name", "object_type_id") VALUES
(1, 'Active', 1),
(2, 'Inactive', 2);
 
INSERT INTO "user" ("id", "name", "surname", "username", "password", "created_at", "updated_at", "role_id", "email", "is_active", "address_id", "phone") VALUES
(1, 'John', 'Doe', 'johndoe', 'password', '2023-03-20 08:00:00', '2023-03-20 08:00:00', 1, 'john.doe@example.com', B'1', 1, '555-1234'),
(2, 'Jane', 'Smith', 'janesmith', '12345', '2023-03-21 09:00:00', '2023-03-21 09:00:00', 2, 'jane.smith@example.com', B'1', 2, '555-5678');
 
 
INSERT INTO "event" ("id", "name", "address_id", "description", "organizer_id", "start_at", "end_at", "created_at", "updated_at", "slug", "status_id") VALUES
(1, 'Tech Conference', 1, 'Annual tech conference', 1, '2023-10-01 09:00:00', '2023-10-02 17:00:00', '2023-09-01 08:00:00', '2023-09-01 08:00:00', 'tech-conference', 1),
(2, 'Health Symposium', 2, 'Health and wellness symposium', 2, '2023-11-05 09:00:00', '2023-11-06 17:00:00', '2023-10-05 08:00:00', '2023-10-05 08:00:00', 'health-symposium', 2);
 
 
INSERT INTO "participant" ("id", "event_id", "user_id", "status_id") VALUES
(1, 1, 1, 1),
(2, 2, 2, 2);
 
 
-- Insert data into "tag"
INSERT INTO "tag" ("id", "name") VALUES
(1, 'Technology'),
(2, 'Health');
 
INSERT INTO "event_tag" ("event_id", "tag_id") VALUES
(1, 1),
(2, 2);
 
INSERT INTO "parameter" ("id", "key", "value") VALUES
(1, 'Location', 'Virtual'),
(2, 'SpeakerCount', '5');
 
INSERT INTO "event_parameter" ("parameter_id", "event_id") VALUES
(1, 1),
(2, 2);
 
INSERT INTO "comment" ("id", "event_id", "content", "created_at", "user_id") VALUES
(1, 1, 'Looking forward to this!', '2023-09-10', 1),
(2, 2, 'Cant wait to attend!', '2023-10-15', 2);
 
INSERT INTO "interest" ("id", "name", "level") VALUES
(1, 'Programming', 5),
(2, 'Reading', 4);
 
INSERT INTO "user_interest" ("user_id", "interest_id") VALUES
(1, 1),
(2, 2);