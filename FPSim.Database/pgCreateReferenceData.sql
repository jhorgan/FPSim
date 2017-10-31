-- application
INSERT INTO Application ("Name", "IsArchived", "DateCreated", "DateModified") VALUES ('FP Sim Application', false, current_timestamp, current_timestamp);
SELECT currval('"application_Id_seq"');

-- user
INSERT INTO "user" ("Name", "Email") VALUES ('Jeremy Horgan', 'jhorgan@lanner.com');
SELECT currval('"user_Id_seq"');

-- "scenario-status"
INSERT INTO "scenario-status" ("Id", "Name") VALUES (1, 'Draft');
INSERT INTO "scenario-status" ("Id", "Name") VALUES (2, 'Running');
INSERT INTO "scenario-status" ("Id", "Name") VALUES (3, 'Error');
INSERT INTO "scenario-status" ("Id", "Name") VALUES (4, 'Completed');
INSERT INTO "scenario-status" ("Id", "Name") VALUES (5, 'Copying');

-- "scenario-result-status"
INSERT INTO "scenario-result-status" ("Id", "Name") VALUES (1, 'Unknown');
INSERT INTO "scenario-result-status" ("Id", "Name") VALUES (2, 'Red');
INSERT INTO "scenario-result-status" ("Id", "Name") VALUES (3, 'Amber');
INSERT INTO "scenario-result-status" ("Id", "Name") VALUES (4, 'Green');