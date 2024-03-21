INSERT INTO "participant" ("id", "event_id", "user_id", "status_id") VALUES
(1, 1, 1, 1),
(2, 2, 2, 2);

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