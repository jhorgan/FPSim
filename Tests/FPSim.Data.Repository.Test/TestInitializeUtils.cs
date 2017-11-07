using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;

namespace FPSim.Data.Repository.Test
{
    class TestInitializeUtils
    {
        private static int ExecuteInsertCommand(string sql)
        {
            int id;
            using (var connection = new NpgsqlConnection(TestConnectionUtils.ConnectionString))
            {
                connection.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = sql;
                    var reader = cmd.ExecuteReader();

                    reader.Read();
                    id = reader.GetInt32(0);
                }
            }
            return id;
        }

        public static int CreateTestUser()
        {
            var guid = Guid.NewGuid();
            var sql = $@"INSERT INTO ""user"" (""Name"", ""Email"") VALUES ('User {guid}', '{guid}@lanner.com');
                        SELECT currval('""user_Id_seq""');";

            return ExecuteInsertCommand(sql);
        }

        internal static int CreateTestApplication()
        {
            var sql = $@"INSERT INTO Application (""Name"", ""IsArchived"", ""DateCreated"", ""DateModified"") VALUES ('FP Sim Application', false, current_timestamp, current_timestamp);
                        SELECT currval('""application_Id_seq""');";

            return ExecuteInsertCommand(sql);
        }

        public static int CreateTestProject(int applicationId, int userId)
        {
            var guid = Guid.NewGuid();
            var sql = $@"INSERT INTO project (""ApplicationId"", ""UserId"", ""Name"", ""IsArchived"", ""DateCreated"", ""DateModified"") 
                         VALUES ({applicationId}, {userId}, 'Project {guid}', false, current_timestamp, current_timestamp);
                         SELECT currval('""project_Id_seq""');";

            return ExecuteInsertCommand(sql);
        }

        public static int CreateTestProject(int applicationId, int userId, string imageOctets)
        {
            var guid = Guid.NewGuid();
            var sql = $@"INSERT INTO project (""ApplicationId"", ""UserId"", ""Name"", ""IsArchived"", ""DateCreated"", ""DateModified"", ""Image"") 
                         VALUES ({applicationId}, {userId}, 'Project {guid}', false, current_timestamp, current_timestamp, '\x{imageOctets}');
                         SELECT currval('""project_Id_seq""');";

            return ExecuteInsertCommand(sql);
        }

        public static int CreateTestScenario(int projectId)
        {
            var guid = Guid.NewGuid();
            var sql = $@"INSERT INTO scenario (""ProjectId"", ""Name"", ""Status"", ""ResultStatus"", ""IsArchived"", ""DateCreated"", ""DateModified"") 
                         VALUES ({projectId}, 'Scenario {guid}', 0, 0, false, current_timestamp, current_timestamp);
                         SELECT currval('""scenario_Id_seq""');";

            return ExecuteInsertCommand(sql);
        }
    }
}
