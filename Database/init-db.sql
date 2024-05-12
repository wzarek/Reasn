CREATE SCHEMA events;
CREATE SCHEMA users;
CREATE SCHEMA common;

CREATE TYPE common.status AS ENUM ('Interested', 'Participating', 'Completed', 'In progress', 'Waiting for approval');
CREATE TYPE users.role AS ENUM ('User', 'Organizer', 'Admin');
CREATE TYPE common.object_type AS ENUM ('Event', 'User');

CREATE TABLE IF NOT EXISTS events.event (
  "id" SERIAL PRIMARY KEY,
  "name" text NOT NULL CONSTRAINT events_event_name_maxlength CHECK (LENGTH("name") <= 64),
  "address_id" integer NOT NULL,
  "description" text NOT NULL CONSTRAINT events_event_description_maxlength CHECK (LENGTH("description") <= 4048),
  "organizer_id" integer NOT NULL,
  "start_at" timestamptz NOT NULL,
  "end_at" timestamptz NOT NULL,
  "created_at" timestamptz NOT NULL,
  "updated_at" timestamptz NOT NULL,
  "slug" text NOT NULL CONSTRAINT events_event_slug_maxlength CHECK (LENGTH("slug") <= 128),
  "status" common.status NOT NULL
);

CREATE TABLE IF NOT EXISTS events.participant (
  "id" SERIAL PRIMARY KEY,
  "event_id" integer NOT NULL,
  "user_id" integer NOT NULL,
  "status" common.status NOT NULL,
  UNIQUE ("event_id", "user_id")
);

CREATE TABLE IF NOT EXISTS events.tag (
  "id" SERIAL PRIMARY KEY,
  "name" text NOT NULL CONSTRAINT events_tag_name_maxlength CHECK (LENGTH("name") <= 64) UNIQUE
);

CREATE TABLE IF NOT EXISTS events.event_tag (
  "event_id" integer NOT NULL,
  "tag_id" integer NOT NULL,
  PRIMARY KEY (event_id, tag_id) 
);

CREATE TABLE IF NOT EXISTS users.user (
  "id" SERIAL PRIMARY KEY,
  "name" text NOT NULL CONSTRAINT users_user_name_maxlength CHECK (LENGTH(name) <= 64),
  "surname" text NOT NULL  CONSTRAINT users_user_surname_maxlength CHECK (LENGTH("surname") <= 64),
  "username" text NOT NULL  CONSTRAINT users_user_username_maxlength CHECK (LENGTH("username") <= 64) UNIQUE,
  "password" text NOT NULL,
  "created_at" timestamptz NOT NULL,
  "updated_at" timestamptz NOT NULL,
  "role" users.role NOT NULL,
  "email" text NOT NULL  CONSTRAINT users_user_email_maxlength CHECK (LENGTH("email") <= 255) UNIQUE,
  "is_active" boolean NOT NULL,
  "address_id" integer NOT NULL,
  "phone" text  CONSTRAINT users_user_phone_maxlength CHECK (LENGTH("phone") <= 16) UNIQUE
);

CREATE TABLE IF NOT EXISTS events.parameter (
  "id" SERIAL PRIMARY KEY,
  "key" text NOT NULL CONSTRAINT events_patameter_key_maxlength CHECK (LENGTH("key") <= 32),
  "value" text NOT NULL CONSTRAINT events_patameter_value_maxlength CHECK (LENGTH("value") <= 64),
  UNIQUE("key", "value")
);

CREATE TABLE IF NOT EXISTS events.event_parameter (
  "parameter_id" integer NOT NULL,
  "event_id" integer NOT NULL,
  PRIMARY KEY (parameter_id, event_id)
);

CREATE TABLE IF NOT EXISTS events.comment (
  "id" SERIAL PRIMARY KEY,
  "event_id" integer NOT NULL,
  "content" text NOT NULL CONSTRAINT events_comment_content_maxlength CHECK (LENGTH("content") <= 1024),
  "created_at" timestamptz NOT NULL,
  "user_id" integer NOT NULL
);

CREATE TABLE IF NOT EXISTS common.address (
  "id" SERIAL PRIMARY KEY,
  "city" text NOT NULL CONSTRAINT common_address_city_maxlength CHECK (LENGTH("city") <= 64),
  "country" text NOT NULL CONSTRAINT common_address_country_maxlength CHECK (LENGTH("country") <= 64),
  "street" text NOT NULL CONSTRAINT common_address_street_maxlength CHECK (LENGTH("street") <= 64),
  "state" text NOT NULL CONSTRAINT common_address_state_maxlength CHECK (LENGTH("state") <= 64),
  "zip_code" text CONSTRAINT common_address_zip_code_maxlength CHECK (LENGTH("zip_code") <= 8)
);

CREATE TABLE IF NOT EXISTS common.image (
  "id" SERIAL PRIMARY KEY,
  "image_data" bytea NOT NULL,
  "object_type" common.object_type NOT NULL,
  "object_id" integer  NOT NULL
);

CREATE TABLE IF NOT EXISTS users.user_interest (
  "user_id" integer NOT NULL,
  "interest_id" integer NOT NULL,
  "level" integer NOT NULL,
  PRIMARY KEY (user_id, interest_id)
);

CREATE TABLE IF NOT EXISTS users.interest (
  "id" SERIAL PRIMARY KEY,
  "name" text NOT NULL CONSTRAINT users_interest_name_maxlength CHECK (LENGTH("name") <= 32) UNIQUE
);

ALTER TABLE events.event ADD FOREIGN KEY ("address_id") REFERENCES common.address ("id");

ALTER TABLE events.comment ADD FOREIGN KEY ("event_id") REFERENCES events.event ("id");

ALTER TABLE events.event ADD FOREIGN KEY ("organizer_id") REFERENCES users.user ("id");

ALTER TABLE events.participant ADD FOREIGN KEY ("event_id") REFERENCES events.event ("id");

ALTER TABLE events.participant ADD FOREIGN KEY ("user_id") REFERENCES users.user ("id");

ALTER TABLE events.comment ADD FOREIGN KEY ("user_id") REFERENCES users.user ("id");

ALTER TABLE users.user_interest ADD FOREIGN KEY ("user_id") REFERENCES users.user ("id");

ALTER TABLE users.user ADD FOREIGN KEY ("address_id") REFERENCES common.address ("id");

ALTER TABLE events.event_parameter ADD FOREIGN KEY ("parameter_id") REFERENCES events.parameter ("id");

ALTER TABLE events.event_parameter ADD FOREIGN KEY ("event_id") REFERENCES events.event ("id");

ALTER TABLE events.event_tag ADD FOREIGN KEY ("tag_id") REFERENCES events.tag ("id");

ALTER TABLE events.event_tag ADD FOREIGN KEY ("event_id") REFERENCES events.event ("id");

ALTER TABLE users.user_interest ADD FOREIGN KEY ("interest_id") REFERENCES users.interest ("id");