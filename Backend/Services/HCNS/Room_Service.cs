using System;
using System.App.Entities.Common;
using System.App.Entities.HCNS;
using System.App.Repositories;
using System.App.Repositories.HCNS;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace System.App.Services.HCNS
{
    public interface IRoom_Service
    {
        string[] GetPermission();
        IEnumerable<Room> GetRooms(RoomSearch obj, ref string error);
        IEnumerable<RoomAccessory> GetRoomAccessories();
        Room Info(string id, ref string error);
        bool Check_ExistsBetweenDate(Room obj);
        bool Add_Room(Room obj, ref string error);
        string Add_RoomReturnID(Room obj, ref string error);
        bool Upd_Room(Room obj, ref string error);
        bool Delete_Room(string id, ref string error);
        bool UpdateApprove_Room(string id, ref string error);
        bool UpdateStatus_Room(string id, int status, ref string error);
        //
        List<SelectListItem> Department_Selects(int selected = -1);
        List<SelectListItem> RoomType_Selects(int selected = -1);

        HCNS_NhanVien GetUserInfo();
    }
    public class Room_Service : IRoom_Service
    {
        private readonly Room_Repo roomRepo;
        private readonly Logs_Repo logRepo;
        private readonly NhanVien_Repo nvRepo;
        private string user, ip;
        private string[] roles;

        public Room_Service()
        {
            roomRepo = new Room_Repo();
            logRepo = new Logs_Repo();
            nvRepo = new NhanVien_Repo();

            ip = HttpContext.Current.Request.UserHostAddress;
            user = HttpContext.Current.User.Identity.Name;
            roles = (string[])HttpContext.Current.Items["Roles"];
        }
        public string[] GetPermission()
        {
            List<string> permission = new List<string>();

            if (string.IsNullOrEmpty(user))
            {
                return null;
            }
            if (roles.Contains(StaticParams.HCNS_RoomAdmin)) // Mức 1
            {
                permission.Add(StaticParams.HCNS_RoomAdmin);
            }
            return permission.ToArray();
        }

        public HCNS_NhanVien GetUserInfo()
        {
            try
            {
                return nvRepo.TimThongTinNhanVien(StaticParams.connectionStringWiseEyeWebOn, new HCNS_NhanVien() { TaiKhoan = user });
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private bool Check_BeforeUpdateRoom(Room obj, ref string error)
        {
            bool rowResult = true;

            if (string.IsNullOrEmpty(obj.DateReg))
            {
                error = "Không xác định được ngày đặt lịch.";
                rowResult = false;
            }
            else if (string.IsNullOrEmpty(obj.StartTime))
            {
                error = "Không xác định được thời điểm bắt đầu.";
                rowResult = false;
            }
            else if (string.IsNullOrEmpty(obj.EndTime))
            {
                error = "Không xác định được thời điểm kết thúc.";
                rowResult = false;
            }
            else if (Check_ExistsBetweenDate(obj))
            {
                error = "Đã có lịch được hẹn được xác nhận trong khoảng thời gian bạn chọn.";
                rowResult = false;
            }
            else if (DateTime.ParseExact(obj.DateReg + " " + obj.StartTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture) <= DateTime.UtcNow.AddHours(7))
            {
                error = "Lịch đặt hẹn không được nhỏ hơn thời điểm hiện tại.";
                rowResult = false;
            }
            return rowResult;
        }
        public IEnumerable<Room> GetRooms(RoomSearch obj, ref string error)
        {
            try
            {
                return roomRepo.GetRooms(StaticParams.connectionStringWiseEyeWebOn, obj);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return null;
            }
        }

        public Room Info(string id, ref string error)
        {
            try
            {
                return roomRepo.GetRoom(StaticParams.connectionStringWiseEyeWebOn, id);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return null;
            }
        }

        public IEnumerable<RoomAccessory> GetRoomAccessories()
        {
            return roomRepo.DS_PhuKien(StaticParams.connectionStringWiseEyeWebOn);
        }

        /// <summary>
        /// Kiểm tra trùng lịch
        /// Return false => không trùng
        /// Return true => Đã có lịch
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public bool Check_ExistsBetweenDate(Room obj)
        {
            try
            {
                if (obj.Status == -1)
                {
                    return false;
                }
                return roomRepo.Check_ExistsBetweenDate(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                return true;
            }
        }

        public bool Add_Room(Room obj, ref string error)
        {
            string message = "";
            int approve = 1, level = 1;
            try
            {
                if (GetPermission().Contains(StaticParams.HCNS_RoomAdmin))
                {
                    approve = 2;
                    level = 2;
                }
                //else if (GetPermission().Contains(StaticParams.HCNS_RoomUser))
                //{
                //    approve = 1;
                //    level = 1;
                //}

                if (!Check_BeforeUpdateRoom(obj, ref message))
                {
                    error = message;
                    return false;
                }
                else
                {
                    obj.DateCreated = DateTime.UtcNow.AddHours(7);
                    obj.DateUpdated = DateTime.UtcNow.AddHours(7);
                    obj.CreatedBy = user;
                    obj.UpdatedBy = user;
                    obj.Approve = approve;
                    obj.Level = level;

                    return roomRepo.Add_Room(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
        public string Add_RoomReturnID(Room obj, ref string error)
        {
            string message = "";
            int approve = 1, level = 1;
            try
            {
                if (GetPermission().Contains(StaticParams.HCNS_RoomAdmin))
                {
                    approve = 2;
                    level = 2;
                }
                //else if (GetPermission().Contains(StaticParams.HCNS_RoomUser))
                //{
                //    approve = 1;
                //    level = 1;
                //}

                if (!Check_BeforeUpdateRoom(obj, ref message))
                {
                    error = message;
                    return "";
                }
                else
                {
                    obj.DateCreated = DateTime.UtcNow.AddHours(7);
                    obj.DateUpdated = DateTime.UtcNow.AddHours(7);
                    obj.CreatedBy = user;
                    obj.UpdatedBy = user;
                    obj.Approve = approve;
                    obj.Level = level;

                    return roomRepo.Add_RoomReturnID(StaticParams.connectionStringWiseEyeWebOn, obj);
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return "";
            }
        }

        public bool Upd_Room(Room obj, ref string error)
        {
            string message = "";

            try
            {
                var info = roomRepo.GetRoom(StaticParams.connectionStringWiseEyeWebOn, obj.ID);

                if (info == null)
                {
                    error = "Không xác định được đối tượng cần sửa";
                    return false;
                }
                else if (!string.IsNullOrEmpty(info.ID) && info.Status == 2)
                {
                    error = "Lịch hẹn đã được thực hiện.";
                    return false;
                }

                if (!Check_BeforeUpdateRoom(obj, ref message))
                {
                    error = message;
                    return false;
                }
                else
                {
                    info.RoomType_ID = obj.RoomType_ID;
                    info.Department_ID = obj.Department_ID;
                    info.DateReg = obj.DateReg;
                    info.StartTime = obj.StartTime;
                    info.EndTime = obj.EndTime;
                    info.PurposeUsed = obj.PurposeUsed;
                    info.Note = obj.Note;
                    info.Accessories = obj.Accessories;
                    info.Status = obj.Status;
                    info.DateUpdated = DateTime.UtcNow.AddHours(7);
                    info.UpdatedBy = user;

                    return roomRepo.Upd_Room(StaticParams.connectionStringWiseEyeWebOn, info) > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public bool Delete_Room(string id, ref string error)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    error = "Không xác định được đối tượng cần xóa";
                    return false;
                }
                return roomRepo.Del_Room(StaticParams.connectionStringWiseEyeWebOn, id) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
        public bool UpdateApprove_Room(string id, ref string error)
        {
            try
            {
                var info = roomRepo.GetRoom(StaticParams.connectionStringWiseEyeWebOn, id);

                if (info == null)
                {
                    error = "Không xác định được đối tượng cần sửa";
                    return false;
                }
                if (Check_ExistsBetweenDate(info))
                {
                    error = "Đã có lịch trong khoảng thời gian đăng ký.";
                    return false;
                }

                return roomRepo.UpdateApprove_Room(StaticParams.connectionStringWiseEyeWebOn, id) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public bool UpdateStatus_Room(string id, int status, ref string error)
        {
            try
            {
                var info = roomRepo.GetRoom(StaticParams.connectionStringWiseEyeWebOn, id);

                if (info == null)
                {
                    error = "Không xác định được đối tượng cần sửa";
                    return false;
                }

                return roomRepo.UpdateStatus_Room(StaticParams.connectionStringWiseEyeWebOn, id, status) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public List<SelectListItem> Department_Selects(int selected)
        {
            var userinfo = nvRepo.TimThongTinNhanVien(StaticParams.connectionStringWiseEyeWebOn, new HCNS_NhanVien() { UPN = user });

            List<SelectListItem> items = new List<SelectListItem>();

            var depts = roomRepo.DS_KhoaPhongHC(StaticParams.connectionStringWiseEyeWebOn);

            items.Add(new SelectListItem() { Value = "-1", Text = "-- Xem tất cả --" });

            foreach (var item in depts)
            {
                if (item.STT == selected)
                    items.Add(new SelectListItem() { Value = item.STT.ToString(), Text = item.KhoaP, Selected = true });
                else
                    items.Add(new SelectListItem() { Value = item.STT.ToString(), Text = item.KhoaP });
            }

            return items;
        }

        public List<SelectListItem> RoomType_Selects(int selected)
        {
            List<SelectListItem> items = new List<SelectListItem>();

            var depts = roomRepo.DS_LoaiPhong(StaticParams.connectionStringWiseEyeWebOn);

            items.Add(new SelectListItem() { Value = "-1", Text = "-- Xem tất cả --" });

            foreach (var item in depts)
            {
                if (item.ID == selected)
                    items.Add(new SelectListItem() { Value = item.ID.ToString(), Text = item.Name, Selected = true });
                else
                    items.Add(new SelectListItem() { Value = item.ID.ToString(), Text = item.Name });
            }
            return items;
        }


    }
}
