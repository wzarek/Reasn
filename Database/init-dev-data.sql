INSERT INTO users_schema.address ("id", "city", "country", "street", "state", "zip_code") VALUES
(1, 'Kalisz', 'Polska', 'Stawiszyńska', 'Wielkopolskie', '62-800'),
(2, 'Wrocław', 'Polska', 'Legnicka', 'Dolnośląskie', '51-702');

INSERT INTO users_schema.user ("id", "name", "surname", "username", "password", "created_at", "updated_at", "role_id", "email", "is_active", "address_id", "phone") VALUES
(1, 'Kamil', 'Owczarski', 'bilimigus', 'password', '2023-03-20 08:00:00', '2023-03-20 08:00:00', 1, 'bilimigus@example.com', B'1', 1, '123456789'),
(2, 'Kamil', 'Owczarzyński', 'bilililimigosu', '12345', '2023-03-21 09:00:00', '2023-03-21 09:00:00', 2, 'bilililimigosu@example.com', B'1', 2, '696969691');
  

INSERT INTO events_schema.event ("id", "name", "address_id", "description", "organizer_id", "start_at", "end_at", "created_at", "updated_at", "slug", "status_id") VALUES
(1, 'Tech Conference', 1, 'Annual tech conference', 1, '2023-10-01 09:00:00', '2023-10-02 17:00:00', '2023-09-01 08:00:00', '2023-09-01 08:00:00', 'tech-conference', 1),
(2, 'Health Symposium', 2, 'Health and wellness symposium', 2, '2023-11-05 09:00:00', '2023-11-06 17:00:00', '2023-10-05 08:00:00', '2023-10-05 08:00:00', 'health-symposium', 2);

INSERT INTO events_schema.event_tag ("event_id", "tag_id") VALUES
(1, 1),
(2, 2);


INSERT INTO events_schema.participant ("id", "event_id", "user_id", "status_id") VALUES
(1, 1, 1, 1),
(2, 2, 2, 2);

INSERT INTO events_schema.parameter ("id", "key", "value") VALUES
(1, 'Location', 'Virtual'),
(2, 'SpeakerCount', '5');
 
INSERT INTO events_schema.event_parameter ("parameter_id", "event_id") VALUES
(1, 1),
(2, 2);
 
INSERT INTO users_schema.comment ("id", "event_id", "content", "created_at", "user_id") VALUES
(1, 1, 'Looking forward to this!', '2023-09-10', 1),
(2, 2, 'Boring...', '2023-10-15', 2);
 
INSERT INTO users_schema.interest ("id", "name", "level") VALUES
(1, 'Programming', 5),
(2, 'Reading', 4);
 
INSERT INTO users_schema.user_interest ("user_id", "interest_id") VALUES
(1, 1),
(2, 2);

