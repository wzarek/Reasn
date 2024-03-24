CREATE OR REPLACE FUNCTION
general_schema.check_fk_exists(object_id INT, object_type_id INT)
RETURNS BOOLEAN AS $$
BEGIN
	IF object_type_id = 1 THEN
	RETURN EXISTS (SELECT 1 FROM users_schema.user WHERE "id" = object_id);
	ELSIF object_type_id = 2 THEN RETURN EXISTS (SELECT 1 FROM events_schema.event WHERE "id" = object_id);
	ELSE RETURN FALSE;
	END IF;
END;
$$ LANGUAGE plpgsql STABLE;

ALTER TABLE general_schema.image ADD CONSTRAINT chech_object_fk
CHECK (general_schema.check_fk_exists(object_id, object_type_id));	