using Dapper;
using System;
using System.App.Entities;
using System.App.Entities.HCNS;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;

namespace System.App.Repositories.HCNS
{
    /// <summary>
    /// Sử dụng SQL Server - WiseEye
    /// </summary>
    public class Room_Repo
    {
        public IEnumerable<RoomType> DS_LoaiPhong(string connectionString)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                try
                {
                    string selectQuery = @"SELECT ID, Code, Name, Status FROM RoomTypes ORDER BY Name";
                    return db.Query<RoomType>(selectQuery).ToList();
                }
                finally
                {
                    if (db != null)
                    {
                        if (db.State == ConnectionState.Open)
                        {
                            db.Close();
                            db.Dispose();
                        }
                    }
                }
            }
        }
        public IEnumerable<RoomAccessory> DS_PhuKien(string connectionString)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                try
                {
                    string selectQuery = @"SELECT ID, Name, Note, Status FROM RoomAccessories ORDER BY Name";
                    return db.Query<RoomAccessory>(selectQuery).ToList();
                }
                finally
                {
                    if (db != null)
                    {
                        if (db.State == ConnectionState.Open)
                        {
                            db.Close();
                            db.Dispose();
                        }
                    }
                }
            }
        }
        public IEnumerable<Department> DS_KhoaPhongHC(string connectionString)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                try
                {
                    string selectQuery = @"SELECT STT, KhoaP FROM Depts ORDER BY KhoaP";
                    return db.Query<Department>(selectQuery).ToList();
                }
                finally
                {
                    if (db != null)
                    {
                        if (db.State == ConnectionState.Open)
                        {
                            db.Close();
                            db.Dispose();
                        }
                    }
                }
            }
        }
        public IEnumerable<Room> GetRooms(string connectionString, RoomSearch obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                try
                {
                    string selectQuery = @"SELECT CAST(a.ID AS VARCHAR(36)) as ID, RoomType_ID, c.Name AS RoomTypeName, CONVERT(VARCHAR(30),DateReg,103) AS DateReg, Department_ID, b.KhoaP AS DepartmentName, StartTime, EndTime, PurposeUsed, Accessories, Note, 
                                            CONVERT(VARCHAR(30), a.DateCreated,103) AS DateCreatedView, 
                                            CONVERT(VARCHAR(30), a.DateModified,103) AS DateUpdatedView, 
                                            a.CreatedBy, a.UpdatedBy, a.Status, a.Approve, a.Level, d.UserFullName, a.IT
                                        FROM Rooms a
	                                        LEFT JOIN Depts b ON a.Department_ID = b.STT
	                                        LEFT JOIN RoomTypes c ON a.RoomType_ID = c.ID
                                            LEFT JOIN UserInfExt d ON a.CreatedBy = d.UPN
                                        WHERE (a.DateReg >= @DateRegF AND @DateRegT >= a.DateReg) AND (@KhoaPhong = -1 OR a.Department_ID = @KhoaPhong) AND (@RoomType = -1 OR a.RoomType_ID = @RoomType) ORDER BY a.DateReg, a.StartTime";
                    DynamicParameters filter = new DynamicParameters();
                    filter.Add("@DateRegF", DateTime.ParseExact(obj.DateRegF, "dd/MM/yyyy", CultureInfo.InvariantCulture));
                    filter.Add("@DateRegT", DateTime.ParseExact(obj.DateRegT, "dd/MM/yyyy", CultureInfo.InvariantCulture));
                    filter.Add("@KhoaPhong", obj.KhoaPhong);
                    filter.Add("@RoomType", obj.RoomType);
                    return db.Query<Room>(selectQuery, filter).ToList();
                }
                finally
                {
                    if (db != null)
                    {
                        if (db.State == ConnectionState.Open)
                        {
                            db.Close();
                            db.Dispose();
                        }
                    }
                }
            }
        }
        public int Check_ExistsBetweenDate(string connectionString, Room obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                try
                {
                    string selectQuery;
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@DateReg", DateTime.ParseExact(obj.DateReg, "dd/MM/yyyy", CultureInfo.InvariantCulture));
                    parameters.Add("@RoomType_ID", obj.RoomType_ID);
                    parameters.Add("@StartDate", DateTime.ParseExact(obj.DateReg + " " + obj.StartTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss"));
                    parameters.Add("@EndDate", DateTime.ParseExact(obj.DateReg + " " + obj.EndTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss"));

                    if (string.IsNullOrEmpty(obj.ID))
                    {
                        selectQuery = @"SELECT COUNT(ID) AS COUNTER FROM Rooms
                            WHERE DateReg = @DateReg AND RoomType_ID = @RoomType_ID AND Status <> -1 AND ((FStartTime <= @StartDate AND @StartDate <= FEndTime) OR (FStartTime <= @EndDate AND @EndDate <= FEndTime) OR (@StartDate <= FStartTime AND @EndDate >= FEndTime))";
                    }
                    else
                    {
                        selectQuery = @"SELECT COUNT(ID) AS COUNTER FROM Rooms
                            WHERE DateReg = @DateReg AND ID <> @ID AND Status <> -1 AND RoomType_ID = @RoomType_ID AND ((FStartTime <= @StartDate AND @StartDate <= FEndTime) OR (FStartTime <= @EndDate AND @EndDate <= FEndTime) OR (@StartDate <= FStartTime AND @EndDate >= FEndTime))";
                        parameters.Add("@ID", obj.ID);
                    }
                    return db.ExecuteScalar<int>(selectQuery, parameters);
                }
                finally
                {
                    if (db != null)
                    {
                        if (db.State == ConnectionState.Open)
                        {
                            db.Close();
                            db.Dispose();
                        }
                    }
                }
            }
        }
        public Room GetRoom(string connectionString, string id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                try
                {
                    string selectQuery = @"
                            SELECT 
	                            CAST(a.ID AS VARCHAR(36)) as ID, 
	                            RoomType_ID, c.Name AS RoomTypeName, 
	                            CONVERT(VARCHAR(30),DateReg, 103) AS DateReg, 
	                            Department_ID, b.KhoaP AS DepartmentName, 
	                            StartTime, EndTime, 
	                            PurposeUsed, 
	                            Accessories, 
	                            Note, 
	                            CONVERT(VARCHAR(30), a.DateCreated, 103) AS DateCreatedView, 
	                            CONVERT(VARCHAR(30), a.DateModified, 103) AS DateUpdatedView, 
	                            a.CreatedBy, a.UpdatedBy, 
	                            a.Status, a.Approve, a.Level, d.UserFullName, a.IT
                            FROM Rooms a
	                            LEFT JOIN Depts b ON a.Department_ID = b.STT
	                            LEFT JOIN RoomTypes c ON a.RoomType_ID = c.ID
                                LEFT JOIN UserInfExt d ON a.CreatedBy = d.UPN
                            WHERE CAST(a.ID AS VARCHAR(36)) = @ID";
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@ID", id);
                    return db.Query<Room>(selectQuery, parameters).SingleOrDefault();
                }
                finally
                {
                    if (db != null)
                    {
                        if (db.State == ConnectionState.Open)
                        {
                            db.Close();
                            db.Dispose();
                        }
                    }
                }
            }
        }
        public int Add_Room(string connectionString, Room obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                string insertQuery = @"
                    DECLARE @returnTable table([ReturnID] [uniqueidentifier]);

                    INSERT INTO Rooms(ID, RoomType_ID, DateReg, Department_ID, StartTime, FStartTime, EndTime, FEndTime, PurposeUsed, Note, Accessories, Status, Approve, Level, DateCreated, DateModified, CreatedBy, UpdatedBy)
                    OUTPUT INSERTED.[ID] INTO @returnTable
                    VALUES (newid(), @RoomType_ID, @DateReg, @Department_ID, @StartTime, @FStartTime, @EndTime, @FEndTime, @PurposeUsed, @Note, @Accessories, @Status, @Approve, @Level, @DateCreated, @DateUpdated, @CreatedBy, @UpdatedBy)
                    SELECT [ID] FROM @returnTable;
                ";


                try
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@RoomType_ID", obj.RoomType_ID);
                    parameters.Add("@DateReg", DateTime.ParseExact(obj.DateReg, "dd/MM/yyyy", CultureInfo.InvariantCulture));
                    parameters.Add("@Department_ID", obj.Department_ID);
                    parameters.Add("@StartTime", obj.StartTime);
                    parameters.Add("@FStartTime", DateTime.ParseExact(obj.DateReg + " " + obj.StartTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture));
                    parameters.Add("@EndTime", obj.EndTime);
                    parameters.Add("@FEndTime", DateTime.ParseExact(obj.DateReg + " " + obj.EndTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture));
                    parameters.Add("@PurposeUsed", obj.PurposeUsed);
                    parameters.Add("@Note", obj.Note);
                    parameters.Add("@Status", obj.Status);
                    parameters.Add("@Approve", obj.Approve);
                    parameters.Add("@Level", obj.Level);
                    parameters.Add("@Accessories", obj.Accessories);
                    parameters.Add("@DateCreated", obj.DateCreated);
                    parameters.Add("@DateUpdated", obj.DateUpdated);
                    parameters.Add("@CreatedBy", obj.CreatedBy);
                    parameters.Add("@UpdatedBy", obj.UpdatedBy);

                    return db.Execute(insertQuery, parameters);
                }
                finally
                {
                    if (db != null)
                    {
                        if (db.State == ConnectionState.Open)
                        {
                            db.Close();
                            db.Dispose();
                        }
                    }
                }
            }
        }
        public string Add_RoomReturnID(string connectionString, Room obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                string insertQuery = @"
                    DECLARE @returnTable table([ReturnID] [uniqueidentifier]);
                    
                    INSERT INTO Rooms(ID, RoomType_ID, DateReg, Department_ID, StartTime, FStartTime, EndTime, FEndTime, PurposeUsed, Note, Accessories, Status, Approve, Level, IT, DateCreated, DateModified, CreatedBy, UpdatedBy)
                    OUTPUT INSERTED.[ID] INTO @returnTable
                    VALUES (newid(), @RoomType_ID, @DateReg, @Department_ID, @StartTime, @FStartTime, @EndTime, @FEndTime, @PurposeUsed, @Note, @Accessories, @Status, @Approve, @Level, @IT, @DateCreated, @DateUpdated, @CreatedBy, @UpdatedBy)

                    SELECT [ReturnID] FROM @returnTable;
                ";

                try
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@RoomType_ID", obj.RoomType_ID);
                    parameters.Add("@DateReg", DateTime.ParseExact(obj.DateReg, "dd/MM/yyyy", CultureInfo.InvariantCulture));
                    parameters.Add("@Department_ID", obj.Department_ID);
                    parameters.Add("@StartTime", obj.StartTime);
                    parameters.Add("@FStartTime", DateTime.ParseExact(obj.DateReg + " " + obj.StartTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture));
                    parameters.Add("@EndTime", obj.EndTime);
                    parameters.Add("@FEndTime", DateTime.ParseExact(obj.DateReg + " " + obj.EndTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture));
                    parameters.Add("@PurposeUsed", obj.PurposeUsed);
                    parameters.Add("@Note", obj.Note);
                    parameters.Add("@Status", obj.Status);
                    parameters.Add("@Approve", obj.Approve);
                    parameters.Add("@Level", obj.Level);
                    parameters.Add("@Accessories", obj.Accessories);
                    parameters.Add("@IT", obj.IT);
                    parameters.Add("@DateCreated", obj.DateCreated);
                    parameters.Add("@DateUpdated", obj.DateUpdated);
                    parameters.Add("@CreatedBy", obj.CreatedBy);
                    parameters.Add("@UpdatedBy", obj.UpdatedBy);

                    return ((Guid)db.ExecuteScalar(insertQuery, parameters)).ToString();
                }
                finally
                {
                    if (db != null)
                    {
                        if (db.State == ConnectionState.Open)
                        {
                            db.Close();
                            db.Dispose();
                        }
                    }
                }
            }
        }
        public int Upd_Room(string connectionString, Room obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                string insertQuery = @"UPDATE Rooms SET RoomType_ID = @RoomType_ID, DateReg = @DateReg, Department_ID = @Department_ID, StartTime = @StartTime, FStartTime = @FStartTime, EndTime = @EndTime, FEndTime = @FEndTime, PurposeUsed = @PurposeUsed, Note = @Note, Accessories = @Accessories, Status = @Status, Approve = @Approve, IT = @IT, DateModified = @DateUpdated, UpdatedBy = @UpdatedBy WHERE CONVERT(NVARCHAR(50), ID) = @ID";

                try
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@ID", obj.ID);
                    parameters.Add("@RoomType_ID", obj.RoomType_ID);
                    parameters.Add("@DateReg", DateTime.ParseExact(obj.DateReg, "dd/MM/yyyy", CultureInfo.InvariantCulture));
                    parameters.Add("@Department_ID", obj.Department_ID);
                    parameters.Add("@StartTime", obj.StartTime);
                    parameters.Add("@FStartTime", DateTime.ParseExact(obj.DateReg + " " + obj.StartTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture));
                    parameters.Add("@EndTime", obj.EndTime);
                    parameters.Add("@FEndTime", DateTime.ParseExact(obj.DateReg + " " + obj.EndTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture));
                    parameters.Add("@PurposeUsed", obj.PurposeUsed);
                    parameters.Add("@Note", obj.Note);
                    parameters.Add("@Approve", obj.Approve);
                    parameters.Add("@Status", obj.Status);
                    parameters.Add("@Accessories", obj.Accessories);
                    parameters.Add("@IT", obj.IT);
                    parameters.Add("@DateUpdated", obj.DateUpdated);
                    parameters.Add("@UpdatedBy", obj.UpdatedBy);

                    return db.Execute(insertQuery, parameters);
                }
                finally
                {
                    if (db != null)
                    {
                        if (db.State == ConnectionState.Open)
                        {
                            db.Close();
                            db.Dispose();
                        }
                    }
                }
            }
        }
        public int Del_Room(string connectionString, string id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                try
                {
                    string delQuery = "DELETE Rooms WHERE ID = @ID AND Status = @Status AND Approve < 2";

                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@ID", id);
                    parameters.Add("@Status", 0);

                    return db.Execute(delQuery, parameters);
                }
                finally
                {
                    if (db != null)
                    {
                        if (db.State == ConnectionState.Open)
                        {
                            db.Close();
                            db.Dispose();
                        }
                    }
                }
            }
        }

        public int UpdateApprove_Room(string connectionString, string id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                try
                {
                    string delQuery = "UPDATE Rooms SET Approve = 2 WHERE ID = @ID AND Approve < 2";

                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@ID", id);

                    return db.Execute(delQuery, parameters);
                }
                finally
                {
                    if (db != null)
                    {
                        if (db.State == ConnectionState.Open)
                        {
                            db.Close();
                            db.Dispose();
                        }
                    }
                }
            }
        }
        public int UpdateStatus_Room(string connectionString, string id, int status)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                try
                {
                    string updQuery = "UPDATE Rooms SET Status = @Status WHERE ID = @ID";

                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@ID", id);
                    parameters.Add("@Status", status);

                    return db.Execute(updQuery, parameters);
                }
                finally
                {
                    if (db != null)
                    {
                        if (db.State == ConnectionState.Open)
                        {
                            db.Close();
                            db.Dispose();
                        }
                    }
                }
            }
        }
    }
}
