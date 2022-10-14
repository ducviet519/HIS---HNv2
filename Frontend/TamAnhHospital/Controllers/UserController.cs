using Newtonsoft.Json;
using System;
using System.App.Entities.Common;
using System.App.Security;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TamAnhHospital.Models;
using UserAuthentication;


namespace TamAnhHospital.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Login(string ReturnUrl)
        {
            string domainUser = ConfigurationManager.AppSettings["DefaultActiveDirectoryServer"];
            ViewBag.ReturnUrl = ReturnUrl;
            return View(new UserModel() { Domain = domainUser });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserModel obj, FormCollection collection)
        {
            bool chkResult = false;
            string[] authenticate = new string[] { };
            string errorMess = "";
            string test = "";

            try
            {
                if (ModelState.IsValid)
                {
                    var returnUrl = collection["ReturnUrl"].ToString();

                    //authenticate = AuthenticateUser(obj.Domain, obj.UserName, obj.Password, ref errorMess, ref test);

                    CustomSerializeModel serializeModel = AuthenticateUser(obj.Domain, obj.UserName, obj.Password, ref errorMess, ref test);

                    if (serializeModel != null)
                    {
                        //Lấy quyền mặc định
                        //string tstdb = ConfigurationManager.ConnectionStrings["TamAnhConnectionWiseEyeWebOn"].ConnectionString;
                        //string sgroup = string.Empty;
                        //using (SqlConnection conn1 = new SqlConnection(tstdb))
                        //{
                        //    if (conn1.State == System.Data.ConnectionState.Closed)
                        //        conn1.Open();

                        //    string strSql = "SELECT Roles FROM Roles where Object = 'HCNSUser'";

                        //    using (SqlCommand cmd1 = new SqlCommand(strSql, conn1))
                        //    {
                        //        var reader = cmd1.ExecuteReader();

                        //        if (reader.HasRows)
                        //        {
                        //            while (reader.Read())
                        //            {
                        //                sgroup = reader["Roles"].ToString();
                        //            }
                        //        }
                        //        else
                        //        {
                        //            sgroup = null;
                        //        }
                        //        reader.Close();
                        //    }
                        //}
                        //CustomPrincipalSerializeModel serializeModel = new CustomPrincipalSerializeModel
                        //{
                        //    IPAddress = authenticate[0].ToString(),
                        //    FullName = authenticate[1].ToString(),
                        //    //Roles = (authenticate[3].ToString() + "," +  sgroup.ToString()).Split(','),
                        //    Depts = authenticate[4]?.ToString().Split(','),
                        //    Account = authenticate[5].ToString(),
                        //};
                        //serializeModel.RoleGroups = authenticate[6].ToString().Split(',');
                        // string userData = JsonConvert.SerializeObject(serializeModel.Account);

                        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                             1, 
                             serializeModel.Account, 
                             DateTime.Now.AddHours(7), 
                             DateTime.Now.AddHours(7).AddMinutes(720), 
                             true,
                             JsonConvert.SerializeObject(serializeModel)
                        );

                        string encTicket = FormsAuthentication.Encrypt(authTicket);

                        HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket)
                        {
                            Name = "HCNS",
                            Expires = DateTime.Now.AddHours(7).AddMinutes(720)
                        };

                        Response.Cookies.Add(faCookie);

                        chkResult = true;

                        //var test = FormsAuthentication.Decrypt(encTicket);

                        //string[] role_default= {StaticParams.HCNS_DanhBaDienThoai, StaticParams.HCNS_XemChiTietCongTheoNV };
                        //System.Web.HttpContext.Current.Items.Add("Roles", GetPermissions(serializeModel.Account));

                        

                        //HttpContext.Session.Add("Roles", (serializeModel);
                        ////HttpContext.Session.Add("Roles", role_default);
                        //HttpContext.Session.Timeout = 43200;

                        //Session["Roles"] = serializeModel.Roles;
                        //HttpCookie roleCookies = new HttpCookie("RoleCookies");
                        //roleCookies.Value = authenticate[3].ToString();
                        //roleCookies.Expires = DateTime.Now.AddHours(24);

                        if (String.IsNullOrEmpty(returnUrl))
                        {
                            return Json(new { result = chkResult, url = "/home" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { result = chkResult, url = returnUrl }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //errorMess = ex.Message;
                chkResult = false;
            }
            return Json(new { result = chkResult, test = test, data = authenticate, err = errorMess }, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "User", null);
        }

        private CustomSerializeModel AuthenticateUser(string domainName, string userName, string password, ref string errorMessage, ref string test)
        {
            string tstdb = ConfigurationManager.ConnectionStrings["TamAnhConnectionWiseEyeWebOn"].ConnectionString;
            Configuration objConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            AppSettingsSection objAppsettings = (AppSettingsSection)objConfig.GetSection("appSettings");
            string UseAD = objAppsettings.Settings["UseAD"].Value.ToString();

            string[] sgroup = null;
            string[] sname = new string[7];
            string displayname = string.Empty;

            string adPath = "LDAP://" + ConfigurationManager.AppSettings["DefaultActiveDirectoryServer"];
            #region Không dùng AD
            //ActiveDirectoryValidator adAuth = new ActiveDirectoryValidator(adPath);
            if (UseAD == "NO")
            {
                if (CheckLogin(tstdb, userName, password, ref displayname))
                {
                    sname[0] = GetIPAddress(); // IP Address
                    sname[1] = displayname;
                    //sname[2] --> Phòng ban
                    //sname[3] --> Danh sách quyền
                    //sname[4] --> Phòng ban hành chính
                    sname[5] = userName;
                    //sname[6] = sgroupStr.Remove(sgroupStr.ToString().Length - 1); // Danh sách nhóm quyền

                    using (SqlConnection conn1 = new SqlConnection(tstdb))
                    {
                        if (conn1.State == System.Data.ConnectionState.Closed)
                            conn1.Open();

                        string strSql = "SELECT ID, username, Object FROM Roles_Users where username = @User ";

                        using (SqlCommand cmd1 = new SqlCommand(strSql, conn1))
                        {
                            cmd1.Parameters.AddWithValue("User", sname[5].ToString());

                            var reader = cmd1.ExecuteReader();

                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    sgroup = reader["Object"].ToString().Split(',');
                                }
                            }
                            else
                            {
                                sgroup = null;
                            }
                            reader.Close();
                        }
                    }

                    using (SqlConnection conn1 = new SqlConnection(tstdb))
                    {
                        if (conn1.State == System.Data.ConnectionState.Closed)
                            conn1.Open();

                        StringBuilder builder = new StringBuilder();
                        StringBuilder builder1 = new StringBuilder();
                        StringBuilder builder2 = new StringBuilder();

                        string getcol = "SELECT Depts, TADepts, Roles FROM [Roles] WHERE Object= @Group OR Object = @User";
                        if (sgroup != null)
                            for (int i = 0; i < sgroup.Length; i++)
                            {
                                using (SqlCommand cmd1 = new SqlCommand(getcol, conn1))
                                {
                                    cmd1.Parameters.AddWithValue("Group", sgroup[i]);
                                    cmd1.Parameters.AddWithValue("User", "usr" + userName);

                                    var reader = cmd1.ExecuteReader();

                                    if (reader.HasRows)
                                    {
                                        while (reader.Read())
                                        {
                                            builder.Append(reader["Depts"].ToString()).Append(",");
                                            builder1.Append(reader["Roles"].ToString()).Append(",");
                                            builder2.Append(reader["TADepts"].ToString()).Append(",");
                                        }
                                    }
                                    reader.Close();
                                }
                            }
                        if (builder.ToString().Length > 0)
                        {
                            sname[2] = string.Join(",", builder.ToString().Remove(builder.ToString().Length - 1).Split(',').Distinct().Distinct().Where(x => !string.IsNullOrEmpty(x)).ToArray());
                            sname[3] = string.Join(",", builder1.ToString().Remove(builder1.ToString().Length - 1).Split(',').Distinct().Where(x => !string.IsNullOrEmpty(x)).ToArray());
                            sname[4] = string.Join(",", builder2.ToString().Remove(builder2.ToString().Length - 1).Split(',').Distinct().Where(x => !string.IsNullOrEmpty(x)).ToArray());
                        }
                        else
                        {
                            sname[2] = "";
                            sname[3] = "";
                            sname[4] = "";
                        }
                    }

                }
            }
            #endregion

            #region Dùng AD
            if (UseAD == "YES")
            {
                try
                {
                    ActiveDirectoryValidator adAuth = new ActiveDirectoryValidator(adPath);

                    if (adAuth.IsAuthenticated(domainName, userName, password))
                    {
                        var userprincipal = (domainName + @"\" + userName);

                        using (PrincipalContext context = new PrincipalContext(ContextType.Domain, domainName, userprincipal, password))
                        {
                            UserPrincipal user = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, userName);

                            CustomSerializeModel userModel = new CustomSerializeModel()
                            {
                                Account = user.SamAccountName,
                                DisplayName = user.DisplayName,
                                Description = user.Description,
                                IP = GetIPAddress()
                                //Roles = GetPermissions(user.SamAccountName)
                            };

                            return userModel;




                            //try
                            //{
                            //    sgroup = user.GetGroups().Select(x => x.Name).ToArray();
                            //    var sgroupStr = StaticParams.ConvertStringArrayToString(sgroup);
                            //    sname[6] = sgroupStr.Remove(sgroupStr.ToString().Length - 1); // Danh sách nhóm quyền
                            //}
                            //catch (Exception)
                            //{
                            //    sgroup = new string[] { };
                            //    sname[6] = sgroup.ToString();
                            //}
                            ////sgroup = user.GetGroups().Select(x => x.Name).ToArray();

                            ////var sgroupStr = StaticParams.ConvertStringArrayToString(sgroup);

                            //sname[0] = GetIPAddress(); // IP Address
                            //sname[1] = user.DisplayName; // Tên hiển thị
                            //                             //sname[2] --> Phòng ban
                            //                             //sname[3] --> Danh sách quyền
                            //                             //sname[4] --> Phòng ban hành chính
                            //sname[5] = user.SamAccountName; // Tên tài khoản
                            ////sname[6] = sgroupStr.Remove(sgroupStr.ToString().Length - 1); // Danh sách nhóm quyền

                            //using (SqlConnection conn1 = new SqlConnection(tstdb))
                            //{
                            //    if (conn1.State == System.Data.ConnectionState.Closed)
                            //        conn1.Open();

                            //    string strSql = "SELECT ID, username, Object FROM Roles_Users where username = @User ";

                            //    using (SqlCommand cmd1 = new SqlCommand(strSql, conn1))
                            //    {
                            //        cmd1.Parameters.AddWithValue("User", sname[5].ToString());

                            //        var reader = cmd1.ExecuteReader();

                            //        if (reader.HasRows)
                            //        {
                            //            while (reader.Read())
                            //            {
                            //                sgroup = reader["Object"].ToString().Split(',');
                            //            }
                            //        }
                            //        else
                            //        {
                            //            sgroup = null;
                            //        }
                            //        reader.Close();
                            //    }
                            //}
                            //if (sgroup != null)
                            //{
                            //    using (SqlConnection conn1 = new SqlConnection(tstdb))
                            //    {
                            //        if (conn1.State == System.Data.ConnectionState.Closed)
                            //            conn1.Open();

                            //        StringBuilder builder = new StringBuilder();
                            //        StringBuilder builder1 = new StringBuilder();
                            //        StringBuilder builder2 = new StringBuilder();

                            //        string getcol = "SELECT Depts, TADepts, Roles FROM [Roles] WHERE Object= @Group OR Object = @User";

                            //        for (int i = 0; i < sgroup.Length; i++)
                            //        {
                            //            using (SqlCommand cmd1 = new SqlCommand(getcol, conn1))
                            //            {
                            //                cmd1.Parameters.AddWithValue("Group", sgroup[i]);
                            //                cmd1.Parameters.AddWithValue("User", "usr" + userName);

                            //                var reader = cmd1.ExecuteReader();

                            //                if (reader.HasRows)
                            //                {
                            //                    while (reader.Read())
                            //                    {
                            //                        builder.Append(reader["Depts"].ToString()).Append(",");
                            //                        builder1.Append(reader["Roles"].ToString()).Append(",");
                            //                        builder2.Append(reader["TADepts"].ToString()).Append(",");
                            //                    }
                            //                }
                            //                reader.Close();
                            //            }
                            //        }
                            //        if (builder.ToString().Length > 0)
                            //        {
                            //            sname[2] = string.Join(",", builder.ToString().Remove(builder.ToString().Length - 1).Split(',').Distinct().Distinct().Where(x => !string.IsNullOrEmpty(x)).ToArray());
                            //            sname[3] = string.Join(",", builder1.ToString().Remove(builder1.ToString().Length - 1).Split(',').Distinct().Where(x => !string.IsNullOrEmpty(x)).ToArray());
                            //            sname[4] = string.Join(",", builder2.ToString().Remove(builder2.ToString().Length - 1).Split(',').Distinct().Where(x => !string.IsNullOrEmpty(x)).ToArray());
                            //        }
                            //        else
                            //        {
                            //            sname[2] = "";
                            //            sname[3] = "";
                            //            sname[4] = "";
                            //        }
                            //    }
                            //}
                        }
                    }
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                }
            }
            #endregion
            return null;
            //return sname;
        }

        


        protected string GetIPAddress()
        {
            string ipAddress = System.Web.HttpContext.Current.Request.UserHostAddress;
            return ipAddress;
        }

        protected bool CheckLogin(string SQLConnectionString, string UserName, string PassWord, ref string DisplayName)
        {
            using (SqlConnection conn1 = new SqlConnection(SQLConnectionString))
            {
                if (conn1.State == System.Data.ConnectionState.Closed)
                    conn1.Open();

                StringBuilder builder = new StringBuilder();
                StringBuilder builder1 = new StringBuilder();
                StringBuilder builder2 = new StringBuilder();

                string SQL = "SELECT UserName, PassWord, DisplayName FROM Login WHERE UserName= @Username and PassWord = @Password";

                using (SqlCommand cmd1 = new SqlCommand(SQL, conn1))
                {
                    cmd1.Parameters.AddWithValue("Username", UserName);
                    cmd1.Parameters.AddWithValue("Password", PassWord);
                    var reader = cmd1.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            DisplayName = reader["DisplayName"].ToString();
                        }
                        return true;
                    }
                    reader.Close();
                }
            }
            return false;
        }
    }
}