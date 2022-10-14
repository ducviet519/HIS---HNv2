using HtmlAgilityPack;
using System;
using System.App.Entities.Common;
using System.App.Entities.HCNS;
using System.App.Services.HCNS;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HCNS.Controllers
{
    public class RoomController : Controller
    {
        private readonly IRoom_Service roomService;

        public RoomController(IRoom_Service room_service)
        {
            roomService = room_service;
        }

        public ActionResult Index()
        {
            ViewBag.DepartmentSelect = roomService.Department_Selects();
            ViewBag.TypeSelect = roomService.RoomType_Selects();
            ViewBag.UserAuth = roomService.GetPermission();

            return View();
        }

        public JsonResult Data_GetRooms(RoomSearch obj = null)
        {
            string _message = "";

            return Json(new { data = roomService.GetRooms(obj, ref _message), message = _message }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add()
        {
            var userInfo = roomService.GetUserInfo();
            ViewBag.UserAuth = roomService.GetPermission();
            ViewBag.DepartmentSelect = roomService.Department_Selects();
            ViewBag.TypeSelect = roomService.RoomType_Selects();
            ViewBag.Accessories = roomService.GetRoomAccessories();

            return PartialView("_Add", new Room() { ID = "", Department_ID = userInfo.PhongKhoaHC, DateReg = DateTime.UtcNow.AddHours(7).ToString("dd/MM/yyyy") });
        }

        public ActionResult Update(string id)
        {
            string _message = "";
            ViewBag.DepartmentSelect = roomService.Department_Selects();
            ViewBag.TypeSelect = roomService.RoomType_Selects();
            ViewBag.Accessories = roomService.GetRoomAccessories();
            ViewBag.UserAuth = roomService.GetPermission();
            return PartialView("_Update", roomService.Info(id, ref _message));
        }

        [HttpPost]
        public JsonResult PushUpdate(Room room)
        {
            string _mess = ""; bool _result = false;

            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(room.ID))
                {
                    room.ID = roomService.Add_RoomReturnID(room, ref _mess);

                    if (!string.IsNullOrEmpty(room.ID))
                        _result = true;
                }
                else
                {
                    _result = roomService.Upd_Room(room, ref _mess);
                }
                if (_result)
                {
                    var info = roomService.Info(room.ID, ref _mess);

                    string emailHost = ConfigurationManager.AppSettings["EmailHost"];
                    string passwordSender = ConfigurationManager.AppSettings["PasswordSender"];
                    string roomServiceSender = ConfigurationManager.AppSettings["RoomServiceSender"];
                    string roomReceiver = ConfigurationManager.AppSettings["RoomReceiver"];
                    string supportReceiver = ConfigurationManager.AppSettings["SupportReceiver"];
                    string cc = "";
                    string bcc = "";
                    string subject = info.UserFullName + " đã đặt lịch phòng họp ngày " + info.DateReg + " vào lúc: " + info.StartTime + " - " + info.EndTime;
                    string message = String.Format(@"<html><body><p>Đã có lịch hẹn mới trên hệ thống đăng ký phòng họp</p> 
                        <b>Nội dung</b><br />
                        - Địa điểm họp: {0}<br />
                        - Ngày đăng ký: {1} <br />
                        - Người đăng ký: {2} <br />
                        - Nội dung: {3} <br />
                        - Cần chuẩn bị: {4} <br />
                        <p><b style='color: #ff0000;'>Chú ý: Đây là mail tự động, vui lòng không phản hồi lại mail này.</b></p></html></body>", info.RoomTypeName, info.DateReg, info.UserFullName, info.PurposeUsed, string.IsNullOrEmpty(info.Accessories) ? "" : info.Accessories.Replace(",", ", "));

                    string fileLocation = "";
                    bool attachedFile = false;

                    System.App.Repositories.Common.StaticHelper.SendEmail(emailHost, roomServiceSender, passwordSender, roomReceiver, cc, bcc, subject, message, fileLocation, attachedFile);

                    if (room.IT == 1)
                    {
                        System.App.Repositories.Common.StaticHelper.SendEmail(emailHost, roomServiceSender, passwordSender, supportReceiver, cc, bcc, subject, message, fileLocation, attachedFile);
                    }

                }
            }
            return Json(new { result = _result, message = _mess }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult PushDelete(string id)
        {
            string _mess = "";

            return Json(new { result = roomService.Delete_Room(id, ref _mess) }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateStatus(string id, int status)
        {
            string _mess = "";

            return Json(new { result = roomService.UpdateStatus_Room(id, status, ref _mess) }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult PushUpdateApprove(string id)
        {
            string _mess = "";

            return Json(new { result = roomService.UpdateApprove_Room(id, ref _mess) }, JsonRequestBehavior.AllowGet);
        }
    }
}