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

ALTER TABLE common.address ADD CONSTRAINT chk_address_street CHECK (street ~ '^[A-ZĄĆĘŁŃÓŚŹŻ][a-ząćęłńóśźż]+((-| )?[A-ZĄĆĘŁŃÓŚŹŻ][a-ząćęłńóśźż]+((-| )?[A-ZĄĆĘŁŃÓŚŹŻ][a-ząćęłńóśźż]+((-| )?[A-ZĄĆĘŁŃÓŚŹŻ][a-ząćęłńóśźż]+)?)?)? \d+([A-Z])?$');

ALTER TABLE common.address ADD CONSTRAINT chk_address_zip_code CHECK (zip_code ~ '^\d{2}-\d{3}$');

ALTER TABLE common.address ADD CONSTRAINT chk_address_city CHECK (city ~ '^[A-ZĄĆĘŁŃÓŚŹŻ][a-ząćęłńóśźż]+((-| )?[A-ZĄĆĘŁŃÓŚŹŻ][a-ząćęłńóśźż]+((-| )?[A-ZĄĆĘŁŃÓŚŹŻ][a-ząćęłńóśźż]+((-| )?[A-ZĄĆĘŁŃÓŚŹŻ][a-ząćęłńóśźż]+)?)?)?$');

ALTER TABLE common.address ADD CONSTRAINT chk_address_state CHECK (state ~ '^[A-ZĄĆĘŁŃÓŚŹŻ][a-ząćęłńóśźż]+(-[A-ZĄĆĘŁŃÓŚŹŻ][a-ząćęłńóśźż]+)?$');

ALTER TABLE common.address ADD CONSTRAINT chk_address_country CHECK (country ~ '^[A-Z][a-z]+$');

ALTER TABLE users.user  ADD CONSTRAINT chk_user_email CHECK (email ~ '^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$');

ALTER TABLE users.user ADD CONSTRAINT chk_user_phone CHECK (phone ~ '^\d{9}$');

ALTER TABLE users.user ADD CONSTRAINT chk_user_name CHECK (name ~ '^[a-zA-ZąćęłńóśźżĄĆĘŁŃÓŚŹŻàáâäãåčćèéêëėįìíîïńòôöõøùúûüųūÿýñçčšžÀÁÂÄÃÅČĖÈÉÊËÌÍÎÏĮÒÔÖÕØÙÚÛÜŲŪŸÝÑßÇŒÆČŠŽ∂ð][a-zA-ZąćęłńóśźżĄĆĘŁŃÓŚŹŻàáâäãåčćèéêëėįìíîïńòôöõøùúûüųūÿýñçčšžÀÁÂÄÃÅČĖÈÉÊËÌÍÎÏĮÒÔÖÕØÙÚÛÜŲŪŸÝÑßÇŒÆČŠŽ∂ð ''\\-]+$');

ALTER TABLE users.user ADD CONSTRAINT chk_user_surname CHECK (surname ~ '^[a-zA-ZąćęłńóśźżĄĆĘŁŃÓŚŹŻàáâäãåčćèéêëėįìíîïńòôöõøùúûüųūÿýñçčšžÀÁÂÄÃÅČĖÈÉÊËÌÍÎÏĮÒÔÖÕØÙÚÛÜŲŪŸÝÑßÇŒÆČŠŽ∂ð][a-zA-ZąćęłńóśźżĄĆĘŁŃÓŚŹŻàáâäãåčćèéêëėįìíîïńòôöõøùúûüųūÿýñçčšžÀÁÂÄÃÅČĖÈÉÊËÌÍÎÏĮÒÔÖÕØÙÚÛÜŲŪŸÝÑßÇŒÆČŠŽ∂ð ''\\-]+$');

ALTER TABLE users.user ADD CONSTRAINT chk_user_username CHECK (username ~ '^[a-zA-ZąćęłńóśźżĄĆĘŁŃÓŚŹŻàáâäãåčćèéêëėįìíîïńòôöõøùúûüųūÿýñçčšžÀÁÂÄÃÅČĖÈÉÊËÌÍÎÏĮÒÔÖÕØÙÚÛÜŲŪŸÝÑßÇŒÆČŠŽ∂ð 0-9 ]+$');

ALTER TABLE users.intrest ADD CONSTRAINT chk_intrest_name CHECK (name ~ '^[A-ZĄĆĘŁŃÓŚŹŻa-ząćęłńóśźż ]+$');

ALTER TABLE events.event ADD CONSTRAINT chk_event_slug CHECK (slug ~ '^[a-z0-9]+(?:-[a-z0-9]+)*$');

ALTER TABLE events.event ADD CONSTRAINT check_start_end_time CHECK (start_at < end_at);

ALTER TABLE events.tag ADD CONSTRAINT chk_tag_name CHECK (name ~ '^[A-ZĄĆĘŁŃÓŚŹŻa-ząćęłńóśźż ]+$');

ALTER TABLE events.parameter ADD CONSTRAINT chk_parameter_value CHECK (value ~ '^([A-ZĄĆĘŁŃÓŚŹŻa-ząćęłńóśźż0-9]+)(?: [A-ZĄĆĘŁŃÓŚŹŻa-ząćęłńóśźż 0-9]+)*$');

ALTER TABLE events.parameter ADD CONSTRAINT chk_parameter_key CHECK (key ~ '^([A-ZĄĆĘŁŃÓŚŹŻa-ząćęłńóśźż0-9]+)(?: [A-ZĄĆĘŁŃÓŚŹŻa-ząćęłńóśźż 0-9]+)*$');

ALTER TABLE users.role ADD CONSTRAINT chk_three_roles CHECK (id <= 3);

ALTER TABLE users.role ADD CONSTRAINT chk_role_name CHECK (name ~ '^(Organiser|Admin|User)$');

ALTER TABLE common.status ADD CONSTRAINT chk_four_statuses CHECK (id <= 4);

ALTER TABLE common.status ADD CONSTRAINT chk_status_name  CHECK (name ~ '^(Interested|Participating|Completed|In progress)$');

ALTER TABLE common.object_type ADD CONSTRAINT chk_two_object_types CHECK (id <= 2);

ALTER TABLE common.object_type ADD CONSTRAINT chk_object_type_name CHECK (name ~ '^(Event|User)$');