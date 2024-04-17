CREATE OR REPLACE FUNCTION
common.check_fk_exists(object_id INT, object_type_id INT)
RETURNS BOOLEAN AS $$
BEGIN
	IF object_type_id = 1 THEN
	RETURN EXISTS (SELECT 1 FROM users.user WHERE "id" = object_id);
	ELSIF object_type_id = 2 THEN RETURN EXISTS (SELECT 1 FROM events.event WHERE "id" = object_id);
	ELSE RETURN FALSE;
	END IF;
END;
$$ LANGUAGE plpgsql STABLE;

ALTER TABLE common.image ADD CONSTRAINT chk_object_fk
CHECK (common.check_fk_exists(object_id, object_type_id));

ALTER TABLE common.address ADD CONSTRAINT chk_address_street CHECK (street ~ '^[[:alnum:]]+(?:(\s)[[:alpha:]]+)*(\s(?:(\d+|\d+/\d+)))?$');

ALTER TABLE common.address ADD CONSTRAINT chk_address_zip_code CHECK (zip_code ~ '^[[:alnum:]\s-]{3,}$');

ALTER TABLE common.address ADD CONSTRAINT chk_address_city CHECK (city ~ '^[[:upper:]][[:lower:]]+(?:(\s|-)[[:upper:]][[:lower:]]+)*$');

ALTER TABLE common.address ADD CONSTRAINT chk_address_state CHECK (state ~ '^[[:upper:]][[:lower:]]+(?:(\s|-)[[:alpha:]]+)*$');

ALTER TABLE common.address ADD CONSTRAINT chk_address_country CHECK (country ~ '^[[:upper:]][[:lower:]]+(?:\s[[:upper:]][[:lower:]]+){0,1}$');

ALTER TABLE users.user  ADD CONSTRAINT chk_user_email CHECK (email ~ '^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$');

ALTER TABLE users.user ADD CONSTRAINT chk_user_phone CHECK (phone ~ '^\+\d{1,3}\s\d{1,15}$');

ALTER TABLE users.user ADD CONSTRAINT chk_user_name CHECK (name ~ '^[[:upper:]][[:lower:]]+$');

ALTER TABLE users.user ADD CONSTRAINT chk_user_surname CHECK (surname ~ '^[[:alpha:]]+((\s)?('')?(-)?)?[[:alpha:]]+$');

ALTER TABLE users.user ADD CONSTRAINT chk_user_username CHECK (username ~ '^[[:alnum:]]+$');

ALTER TABLE users.interest ADD CONSTRAINT chk_interest_name CHECK (name ~ '^[[:upper:]][[:lower:]]+(?:\s[[:alpha:]]+)*$');

ALTER TABLE events.event ADD CONSTRAINT chk_event_slug CHECK (slug ~ '^[[:alnum:]]+(?:-[[:alnum:]]+)*$');

ALTER TABLE events.event ADD CONSTRAINT check_start_end_time CHECK (start_at < end_at);

ALTER TABLE events.tag ADD CONSTRAINT chk_tag_name CHECK (name ~ '^[[:alpha:]]+(?:\s[[:alpha:]]+)*$');

ALTER TABLE events.parameter ADD CONSTRAINT chk_parameter_value CHECK (value ~ '^[[:alnum:]]+(?:\s[[:alnum:]]+)*$');

ALTER TABLE events.parameter ADD CONSTRAINT chk_parameter_key CHECK (key ~ '^[[:alpha:]]+(?:\s[[:alpha:]]+)*$');

ALTER TABLE users.role ADD CONSTRAINT chk_three_roles CHECK (id <= 3);

ALTER TABLE users.role ADD CONSTRAINT chk_role_name CHECK (name ~ '^(Organizer|Admin|User)$');

ALTER TABLE common.status ADD CONSTRAINT chk_five_statuses CHECK (id <= 5);

ALTER TABLE common.status ADD CONSTRAINT chk_status_name  CHECK (name ~ '^(Interested|Participating|Completed|In progress|Waiting for approval)$');

ALTER TABLE common.object_type ADD CONSTRAINT chk_two_object_types CHECK (id <= 2);

ALTER TABLE common.object_type ADD CONSTRAINT chk_object_type_name CHECK (name ~ '^(Event|User)$');

ALTER TABLE common.status
ADD CONSTRAINT chk_object_type_for_status
CHECK (
    (name IN ('Interested', 'Participating') AND object_type_id = 2) OR
    (name IN ('Completed', 'In progress', 'Waiting for approval') AND object_type_id = 1)
);