CREATE TABLE IF NOT EXISTS "events" (
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
 
CREATE TABLE IF NOT EXISTS "participants" (
  "id" SERIAL PRIMARY KEY,
  "event_id" integer,
  "user_id" integer,
  "status_id" int
);
 
CREATE TABLE IF NOT EXISTS "statuses" (
  "id" SERIAL PRIMARY KEY,
  "name" varchar(255),
  "object_type_id" integer
);
 
CREATE TABLE IF NOT EXISTS "tags" (
  "id" SERIAL PRIMARY KEY,
  "name" varchar(255)
);
 
CREATE TABLE IF NOT EXISTS "event_tags" (
  "event_id" integer,
  "tag_id" integer
);
 
CREATE TABLE IF NOT EXISTS "users" (
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
 
CREATE TABLE IF NOT EXISTS "parameters" (
  "id" SERIAL PRIMARY KEY,
  "key" varchar(255),
  "value" varchar(255)
);
 
CREATE TABLE IF NOT EXISTS "event_parameters" (
  "parameter_id" integer,
  "event_id" integer
);
 
CREATE TABLE IF NOT EXISTS "roles" (
  "id" SERIAL PRIMARY KEY,
  "role" varchar(255)
);
 
CREATE TABLE IF NOT EXISTS "comments" (
  "id" SERIAL PRIMARY KEY,
  "event_id" integer,
  "content" varchar(255),
  "created_at" date,
  "user_id" integer
);
 
CREATE TABLE IF NOT EXISTS "addresses" (
  "id" SERIAL PRIMARY KEY,
  "city" varchar(255),
  "country" varchar(255),
  "street" varchar(255),
  "state" varchar(255),
  "zip_code" varchar(255)
);
 
CREATE TABLE IF NOT EXISTS "images" (
  "id" SERIAL PRIMARY KEY,
  "image_data" bytea,
  "object_type_id" integer,
  "object_id" integer
);
 
CREATE TABLE IF NOT EXISTS "object_types" (
  "id" SERIAL PRIMARY KEY,
  "name" varchar(255)
);
 
CREATE TABLE IF NOT EXISTS "user_interests" (
  "user_id" integer,
  "interest_id" integer
);
 
CREATE TABLE IF NOT EXISTS "interests" (
  "id" SERIAL PRIMARY KEY,
  "name" varchar(255),
  "level" integer
);
 

ALTER TABLE "users" ADD FOREIGN KEY ("role_id") REFERENCES "roles" ("id");
 
ALTER TABLE "events" ADD FOREIGN KEY ("address_id") REFERENCES "addresses" ("id");
 
ALTER TABLE "comments" ADD FOREIGN KEY ("event_id") REFERENCES "events" ("id");
 
ALTER TABLE "events" ADD FOREIGN KEY ("organizer_id") REFERENCES "users" ("id");
 
ALTER TABLE "participants" ADD FOREIGN KEY ("event_id") REFERENCES "events" ("id");
 
ALTER TABLE "participants" ADD FOREIGN KEY ("user_id") REFERENCES "users" ("id");
 
ALTER TABLE "comments" ADD FOREIGN KEY ("user_id") REFERENCES "users" ("id");
 
ALTER TABLE "images" ADD FOREIGN KEY ("object_type_id") REFERENCES "object_types" ("id");
 
ALTER TABLE "user_interests" ADD FOREIGN KEY ("user_id") REFERENCES "users" ("id");
 
ALTER TABLE "users" ADD FOREIGN KEY ("address_id") REFERENCES "addresses" ("id");
 
ALTER TABLE "images" ADD FOREIGN KEY ("object_id") REFERENCES "events" ("id");
 
ALTER TABLE "images" ADD FOREIGN KEY ("object_id") REFERENCES "users" ("id");
 
ALTER TABLE "participants" ADD FOREIGN KEY ("status_id") REFERENCES "statuses" ("id");
 
ALTER TABLE "event_parameters" ADD FOREIGN KEY ("parameter_id") REFERENCES "parameters" ("id");
 
ALTER TABLE "event_parameters" ADD FOREIGN KEY ("event_id") REFERENCES "events" ("id");
 
ALTER TABLE "event_tags" ADD FOREIGN KEY ("tag_id") REFERENCES "tags" ("id");
 
ALTER TABLE "event_tags" ADD FOREIGN KEY ("event_id") REFERENCES "events" ("id");
 
ALTER TABLE "statuses" ADD FOREIGN KEY ("object_type_id") REFERENCES "object_types" ("id");
 
ALTER TABLE "events" ADD FOREIGN KEY ("status_id") REFERENCES "statuses" ("id");
 
ALTER TABLE "user_interests" ADD FOREIGN KEY ("interest_id") REFERENCES "interests" ("id");
 

INSERT INTO "roles" ("id", "role") VALUES
(1, 'System Analyst'),
(2, 'Project Manager');
 
INSERT INTO "object_types" ("id", "name") VALUES (1, 'Event'),
(2, 'User');
 
INSERT INTO "statuses" ("id", "name", "object_type_id") VALUES
(1, 'Interested', 1),
(2, 'Participants', 2),
(3, 'Archived', 3),
(4, 'In progress', 4);
 
INSERT INTO "tags" ("id", "name") VALUES
(1, 'Technology'),
(2, 'Health');
 

