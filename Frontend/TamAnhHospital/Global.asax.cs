using Newtonsoft.Json;
using System;
using System.App.Security;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using TamAnhHospital.Models;

namespace TamAnhHospital
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            DependencyResolver.SetResolver(new Security.Ninject());
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }


        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies["HCNS"];

            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                CustomSerializeModel serializeModel = JsonConvert.DeserializeObject<CustomSerializeModel>(authTicket.UserData);

                CustomPrincipal newUser = new CustomPrincipal(authTicket.Name)
                {
                    Account = serializeModel.Account,
                    FullName = serializeModel.DisplayName
                };
                HttpContext.Current.User = newUser;
                HttpContext.Current.Items["FullName"] = newUser.FullName;
                HttpContext.Current.Items["Roles"] = GetPermissions(serializeModel.Account).Split(',');
            }
        }

        private string GetPermissions(string account)
        {
            using (SqlConnection conn1 = new SqlConnection(ConfigurationManager.ConnectionStrings["TamAnhConnectionWiseEyeWebOn"].ConnectionString))
            {
                if (conn1.State == System.Data.ConnectionState.Closed)
                    conn1.Open();

                string strSql = @"
                            DECLARE @str VARCHAR(MAX) = (select [Object] from Roles_Users where username = @user)
                            SELECT STUFF((
	                            SELECT 
		                            ',' + Roles
	                            FROM [Roles] a
	                            WHERE a.[Object] IN (SELECT [Value] FROM StringSplitToColumn(@str,',')) OR a.Object = 'HCNSUser'
                            FOR XML PATH ('')
                            ), 1,1,'') as roles
                        ";

                var roles = new List<string>();
                var strRoles = string.Empty;

                using (SqlCommand cmd1 = new SqlCommand(strSql, conn1))
                {
                    cmd1.Parameters.AddWithValue("user", account);

                    var reader = cmd1.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            strRoles = reader["roles"].ToString();
                        }

                        var strSplit = strRoles.Split(',');

                        for (int i = 0; i < strSplit.Length; i++)
                        {
                            if (!roles.Contains(strSplit[i].ToString()))
                            {
                                roles.Add(strSplit[i].ToString());
                            }
                        }

                        return string.Join(",", roles).ToString();
                    }
                    reader.Close();

                    return null;
                }
            }
        }
    }
}