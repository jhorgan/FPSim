-- application
INSERT INTO Application ("Name", "IsArchived", "DateCreated", "DateModified") VALUES ('FP Sim Application', false, current_timestamp, current_timestamp);
SELECT currval('"application_Id_seq"');

-- user
INSERT INTO "user" ("Name", "Email") VALUES ('Jeremy Horgan', 'jhorgan@lanner.com');
SELECT currval('"user_Id_seq"');

-- "project"
INSERT INTO project ("ApplicationId", "UserId", "Name", "IsArchived", "DateCreated", "DateModified") VALUES (1, 1, 'Project Test', false, current_timestamp, current_timestamp);
SELECT currval('"project_Id_seq"');

-- "scenario"
INSERT INTO scenario ("ProjectId", "Name", "Status", "ResultStatus", "IsArchived", "DateCreated", "DateModified") VALUES (1, 'Scenario Test', 0, 0, false, current_timestamp, current_timestamp);
SELECT currval('"scenario_Id_seq"');