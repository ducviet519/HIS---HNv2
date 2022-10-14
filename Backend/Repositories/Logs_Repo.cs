using Dapper;
using System.App.Entities;
using System.App.Repositories.Common;
using System.Data;
using System.Data.SqlClient;

namespace System.App.Repositories
{
    public class Logs_Repo
    {
        public bool Insert(string connectionString, Logs obj)
        {
            return SqlHelper.Insert(obj, connectionString);
        }

        public void Insert_Logs(string connectionString, Logs obj)
        {
            string query = @"SET XACT_ABORT ON; BEGIN TRAN
                    INSERT INTO Logs([ID], [Name], [Action], [Controller], [Data], [Message], [DateCreated], [IP], [CreatedBy])
                    VALUES (@ID, @Name, @Action, @Controller, @Data, @Message, @DateCreated, @IP, @CreatedBy)
                    COMMIT";

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                if (sqlConnection.State == Data.ConnectionState.Closed)
                    sqlConnection.Open();

                using (var cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.AddWithValue("@ID", obj.ID);
                    cmd.Parameters.AddWithValue("@Name", obj.Name);
                    cmd.Parameters.AddWithValue("@Action", obj.Action);
                    cmd.Parameters.AddWithValue("@Controller", obj.Controller);
                    cmd.Parameters.AddWithValue("@Data", String.IsNullOrEmpty(obj.Data) ? "" : obj.Data.ToString());
                    cmd.Parameters.AddWithValue("@Message", String.IsNullOrEmpty(obj.Message) ? "" : obj.Message.ToString());
                    cmd.Parameters.AddWithValue("@DateCreated", obj.DateCreated);
                    cmd.Parameters.AddWithValue("@IP", obj.IP);
                    cmd.Parameters.AddWithValue("@CreatedBy", obj.CreatedBy);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Add_ActionLog(string connectionString, Logs obj)
        {
            string query = @"INSERT INTO ActionLogs(ID, Action, Controller, Data, DateCreated, CreatedBy, IP) VALUES (@ID, @Action, @Controller, @Data, @DateCreated, @CreatedBy, @IP)";

            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("ID", obj.ID);
                parameters.Add("Action", obj.Action);
                parameters.Add("Controller", obj.Controller);
                parameters.Add("Data", obj.Data);
                parameters.Add("DateCreated", obj.DateCreated);
                parameters.Add("CreatedBy", obj.CreatedBy);
                parameters.Add("IP", obj.IP);

                db.Execute(query, parameters, commandType: CommandType.Text);
            }
        }
    }
}