using BarcodeEncoder;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace System.App.Repositories.Common
{
    public static class StaticHelper
    {
        public static string proc_L_TienSuDieuTri = "proc_L_TienSuDieuTri";
        public static string proc_L_DiUng = "proc_L_DiUng";
        public static string proc_CN_TienSuPhuKhoa = "proc_CN_TienSuPhuKhoa";
        public static string proc_CN_TienSuSanKhoa_Para = "proc_CN_TienSuSanKhoa_Para";
        public static string proc_CN_SoLanCoThai = "proc_CN_SoLanCoThai";
        public static string proc_CN_PhuongPhapHTSS = "proc_CN_PhuongPhapHTSS";
        public static string proc_CN_TienSuDieuTri = "proc_CN_TienSuDieuTri";
        public static string proc_CN_DiUng = "proc_CN_DiUng";
        public static string proc_CN_CacYeuToAnhHuongSinhSan = "proc_CN_CacYeuToAnhHuongSinhSan";
        public static string proc_LS_TienSuBenh = "proc_LS_TienSuBenh";

        #region Barcode

        private static Font FindBestFitFont(Graphics g, String text, Font font, Size proposedSize)
        {
            // Compute actual size, shrink if needed
            while (true)
            {
                SizeF size = g.MeasureString(text, font);

                // It fits, back out
                if (size.Height <= proposedSize.Height &&
                     size.Width <= proposedSize.Width) { return font; }

                // Try a smaller font (90% of old size)
                Font oldFont = font;
                font = new Font(font.Name, (float)(font.Size * .9), font.Style);
                oldFont.Dispose();
            }
        }

        public static string GenBarCode(string barCode)
        {
            Int32 barcodeHeight = 30, barcodeWidth = 120;
            Bitmap bmpBarcode = new Bitmap(barcodeWidth, barcodeHeight);
            bmpBarcode.SetResolution(100, 100);
            Font drawFont;
            string image = "";

            if (barCode == "")
            {
            }
            else
            {
                // draw the barcode and text to the bitmap
                clsBarCode barcodeGenerator = new clsBarCode();
                String barcodeReadyData = barcodeGenerator.Code128(barCode, false);

                using (drawFont = new Font("IDAutomationC128M", 16))
                {
                    using (SolidBrush drawBrush = new SolidBrush(Color.Black))
                    {
                        using (Graphics dc = Graphics.FromImage(bmpBarcode))
                        {
                            // paint the whole bitmap white
                            dc.FillRectangle(new SolidBrush(Color.White), new RectangleF(0, 0, bmpBarcode.Width, bmpBarcode.Height));
                            // draw the barcode
                            //dc.DrawString(barcodeReadyData, drawFont, drawBrush, new RectangleF(0, 0, barcodeWidth, barcodeHeight - 70));
                            dc.DrawString(barcodeReadyData, drawFont, drawBrush, new RectangleF(0, 0, 150, 30));
                            // draw the human readable
                            //dc.DrawString(barCode, readableFont, drawBrush, new RectangleF(0, 30, barcodeWidth, barcodeHeight));
                        }
                    }
                }
                using (MemoryStream ms = new MemoryStream())
                {
                    bmpBarcode.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] byteImage = ms.ToArray();
                    Convert.ToBase64String(byteImage);
                    image = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                }
            }

            return image;

            //using (Bitmap bitMap = new Bitmap(barCode.Length * 40, 80))
            //{
            //    using (Graphics graphics = Graphics.FromImage(bitMap))
            //    {
            //        Font oFont = new Font("IDAUTOMATIONC128S", 16);
            //        PointF point = new PointF(2f, 2f);
            //        SolidBrush blackBrush = new SolidBrush(Color.Black);
            //        SolidBrush whiteBrush = new SolidBrush(Color.White);
            //        graphics.FillRectangle(whiteBrush, 0, 0, bitMap.Width, bitMap.Height);
            //        graphics.DrawString("*" + barCode + "*", oFont, blackBrush, point);
            //    }
            //    using (MemoryStream ms = new MemoryStream())
            //    {
            //        bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            //        byte[] byteImage = ms.ToArray();

            //        Convert.ToBase64String(byteImage);
            //        image = "data:image/png;base64," + Convert.ToBase64String(byteImage);
            //    }
            //}

            //return image;
        }

        #endregion Barcode

        #region Bỏ dấu

        private static readonly string[] VietnameseSigns = new string[]
            {
            "aAeEoOuUiIdDyY",
            "áàạảãâấầậẩẫăắằặẳẵ",
            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
            "éèẹẻẽêếềệểễ",
            "ÉÈẸẺẼÊẾỀỆỂỄ",
            "óòọỏõôốồộổỗơớờợởỡ",
            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
            "úùụủũưứừựửữ",
            "ÚÙỤỦŨƯỨỪỰỬỮ",
            "íìịỉĩ",
            "ÍÌỊỈĨ",
            "đ",
            "Đ",
            "ýỳỵỷỹ",
            "ÝỲỴỶỸ"
            };
        public static string LocDau(string str)
        {
            for (int i = 1; i < VietnameseSigns.Length; i++)
            {
                for (int j = 0; j < VietnameseSigns[i].Length; j++)
                    str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
            }
            return str;
        }

        #endregion Bỏ dấu

        #region Chuỗi tùm lum: Const.RandomString(20)
        public static int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
        public static string RandomID()
        {
            Random random = new Random();

            return string.Format("{0}{1}{2}",
                DateTime.UtcNow.AddHours(7).ToString("yyMMdd"),
                DateTime.UtcNow.AddHours(7).ToString("HHmmss"),
                random.Next(0, 999).ToString("000"));
        }
        public static string RandomString(int length)
        {
            Random _random = new Random(Environment.TickCount);

            string chars = "0123456789abcdefghijklmnopqrstuvwxyz";
            StringBuilder builder = new StringBuilder(length);

            for (int i = 0; i < length; ++i)
                builder.Append(chars[_random.Next(chars.Length)]);

            return builder.ToString();
        }

        #endregion Chuỗi tùm lum: Const.RandomString(20)

        #region Lấy tên tất cả Area trong project: Const.GetAreas()

        public static IEnumerable<object> GetAreas()
        {
            return RouteTable.Routes.OfType<Route>().Where(d => d.DataTokens != null && d.DataTokens.ContainsKey("area")).Select(r => r.DataTokens["area"]).ToArray();
        }

        #endregion Lấy tên tất cả Area trong project: Const.GetAreas()

        #region Lấy tên tất cả Controller trong Area: Const.GetControllers(namespace)

        public static List<string> GetControllers(string _namespace)
        {
            List<string> controllerNames = new List<string>();

            GetSubClasses<Controller>(_namespace).ForEach(type => controllerNames.Add(type.Name));

            return controllerNames;
        }

        private static List<Type> GetSubClasses<T>(string _namespace)
        {
            return Assembly.GetCallingAssembly().GetTypes().Where(type => type.IsSubclassOf(typeof(T)) && type.Namespace == _namespace).ToList();
        }

        #endregion Lấy tên tất cả Controller trong Area: Const.GetControllers(namespace)

        #region Lấy tên tất cả Action in Controller: Const.GetActions(controllerName)

        public static IEnumerable<string> GetActions(string controllerName)
        {
            var types = from a in AppDomain.CurrentDomain.GetAssemblies()
                        from t in a.GetTypes()
                        where typeof(IController).IsAssignableFrom(t) &&
                        string.Equals(controllerName + "Controller", t.Name, StringComparison.OrdinalIgnoreCase)
                        select t;

            var controllerType = types.FirstOrDefault();

            if (controllerType == null)
            {
                return Enumerable.Empty<string>().ToList();
            }
            return new ReflectedControllerDescriptor(controllerType).GetCanonicalActions().Select(x => x.ActionName).ToList();
        }

        #endregion Lấy tên tất cả Action in Controller: Const.GetActions(controllerName)

        #region Mã hóa code ở đây: Const.GeneratePassword(20)

        public static string GeneratePassword(int length) //length of salt
        {
            const string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            var randNum = new Random();
            var chars = new char[length];
            var allowedCharCount = allowedChars.Length;
            for (var i = 0; i <= length - 1; i++)
            {
                chars[i] = allowedChars[Convert.ToInt32((allowedChars.Length) * randNum.NextDouble())];
            }
            return new string(chars);
        }

        private static string EncodePasswordMd5(string pass) //Encrypt using MD5
        {
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5;
            //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)
            md5 = new MD5CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(pass);
            encodedBytes = md5.ComputeHash(originalBytes);
            //Convert encoded bytes back to a 'readable' string
            return BitConverter.ToString(encodedBytes);
        }

        public static string EncodePassword(string pass, string salt) //encrypt password
        {
            byte[] bytes = Encoding.Unicode.GetBytes(pass);
            byte[] src = Encoding.Unicode.GetBytes(salt);
            byte[] dst = new byte[src.Length + bytes.Length];
            System.Buffer.BlockCopy(src, 0, dst, 0, src.Length);
            System.Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);
            HashAlgorithm algorithm = HashAlgorithm.Create("SHA1");
            byte[] inArray = algorithm.ComputeHash(dst);
            //return Convert.ToBase64String(inArray);
            return EncodePasswordMd5(Convert.ToBase64String(inArray));
        }
        public static string MD5HashPassword(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits
                //for each byte
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }

        //var keyNew = Helper.GeneratePassword(10);
        //var password = Helper.EncodePassword(objUser.Password, keyNew);
        //lưu cả keynew và password vào cơ sở dữ liệu
        public static string base64Encode(string sData) // Encode
        {
            try
            {
                byte[] encData_byte = new byte[sData.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(sData);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }

        //var encodingPasswordString = Helper.EncodePassword(password, hashCode);
        //lấy ra keynew và ghép với mã sẵn có
        //var hashCode = getUser.keynew;
        //var encodingPasswordString = Helper.EncodePassword(password, hashCode);
        public static string base64Decode(string sData) //Decode
        {
            try
            {
                var encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();
                byte[] todecodeByte = Convert.FromBase64String(sData);
                int charCount = utf8Decode.GetCharCount(todecodeByte, 0, todecodeByte.Length);
                char[] decodedChar = new char[charCount];
                utf8Decode.GetChars(todecodeByte, 0, todecodeByte.Length, decodedChar, 0);
                string result = new String(decodedChar);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Decode" + ex.Message);
            }
        }

        #endregion Mã hóa code ở đây: Const.GeneratePassword(20)

        #region Convert chuỗi sang dạng đường link

        public static string ConvertStringToUrl(string _string)
        {
            try
            {
                Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
                string strRuler = _string.ToLower().Normalize(System.Text.NormalizationForm.FormD);
                strRuler = regex.Replace(strRuler, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
                return Regex.Replace(strRuler, @"[^\w\.-]", "-");
            }
            catch
            {
                return "Đã có lỗi xảy ra trong quá trình chuyển đổi ký tự";
            }
        }

        #endregion Convert chuỗi sang dạng đường link

        #region Convert chuỗi sang dạng tên file không dấu

        public static string ConvertStringToFile(string _string)
        {
            try
            {
                Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
                string strRuler = _string.Normalize(System.Text.NormalizationForm.FormD);
                strRuler = regex.Replace(strRuler, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
                return Regex.Replace(strRuler, @"[^\w\.]", "_");
            }
            catch
            {
                return "Đã có lỗi xảy ra trong quá trình chuyển đổi ký tự";
            }
        }

        #endregion Convert chuỗi sang dạng tên file không dấu

        #region Các hàm về xử lý và upload ảnh

        /// <summary>
        /// Delete file by specified path.
        /// </summary>
        /// <param name="path">path of file.</param>
        public static void DeleteTargetFile(string path)
        {
            if (File.Exists(path))
            {
                File.SetAttributes(path, FileAttributes.Normal);
                File.Delete(path);
            }
        }
        public static void UploadImageWithThum(HttpPostedFileBase fullFileName, string fileName)
        {
            string _directoryPath = System.Web.Hosting.HostingEnvironment.MapPath("~") + "files\\uploads\\";
            string thumbPath = _directoryPath + "thumb\\";
            string mediumPath = _directoryPath + "large\\";
            string path = System.IO.Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/files/uploads/original"), fileName);
            fullFileName.SaveAs(path);
            ResizeAndSave(thumbPath, fileName, fullFileName.InputStream, 100, false);
            ResizeAndSave(mediumPath, fileName, fullFileName.InputStream, 300, false);
        }

        public static void ResizeAndSave(string savePath, string fileName, Stream imageBuffer, int maxSideSize, bool makeItSquare)
        {
            int newWidth;
            int newHeight;
            Image image = Image.FromStream(imageBuffer);
            int oldWidth = image.Width;
            int oldHeight = image.Height;
            Bitmap newImage;
            if (makeItSquare)
            {
                int smallerSide = oldWidth >= oldHeight ? oldHeight : oldWidth;
                double coeficient = maxSideSize / (double)smallerSide;
                newWidth = Convert.ToInt32(coeficient * oldWidth);
                newHeight = Convert.ToInt32(coeficient * oldHeight);
                Bitmap tempImage = new Bitmap(image, newWidth, newHeight);
                int cropX = (newWidth - maxSideSize) / 2;
                int cropY = (newHeight - maxSideSize) / 2;
                newImage = new Bitmap(maxSideSize, maxSideSize);
                Graphics tempGraphic = Graphics.FromImage(newImage);
                tempGraphic.SmoothingMode = SmoothingMode.AntiAlias;
                tempGraphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                tempGraphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
                tempGraphic.DrawImage(tempImage, new Rectangle(0, 0, maxSideSize, maxSideSize), cropX, cropY, maxSideSize, maxSideSize, GraphicsUnit.Pixel);
            }
            else
            {
                int maxSide = oldWidth >= oldHeight ? oldWidth : oldHeight;

                if (maxSide > maxSideSize)
                {
                    double coeficient = maxSideSize / (double)maxSide;
                    newWidth = Convert.ToInt32(coeficient * oldWidth);
                    newHeight = Convert.ToInt32(coeficient * oldHeight);
                }
                else
                {
                    newWidth = oldWidth;
                    newHeight = oldHeight;
                }
                newImage = new Bitmap(image, newWidth, newHeight);
            }
            newImage.Save(savePath + fileName);
            image.Dispose();
            newImage.Dispose();
        }

        public static bool CheckImageType(String fileName)
        {
            try
            {
                switch (Path.GetExtension(fileName).ToLower())
                {
                    case ".gif":
                    case ".png":
                    case ".jpg":
                    case ".jpeg":
                        return true;

                    default:
                        return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool CheckFileType(String fileName)
        {
            try
            {
                switch (Path.GetExtension(fileName).ToLower())
                {
                    case ".gif":
                    case ".png":
                    case ".jpg":
                    case ".jpeg":
                    case ".pdf":
                    case ".doc":
                    case ".docx":
                    case ".xls":
                    case ".rar":
                    case ".zip":
                        return true;

                    default:
                        return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Các hàm về xử lý và upload ảnh

        #region Email

        /// <summary>
        /// Verifies that a string is in valid e-mail format
        /// </summary>
        /// <param name="email">Email to verify</param>
        /// <returns>true if the string is a valid e-mail address and false if it's not</returns>
        public static bool IsValidEmail(string email)
        {
            if (String.IsNullOrEmpty(email))
                return false;

            email = email.Trim();
            var result = Regex.IsMatch(email, @"^(?:[\\w\\!\\#\\$\\%\\&\\
            '\\*\\+\\-\\/\\=\\?\\^\\`\\{\\|\\}\\~]+\\.)*[\\w\\!\\#\\$\\%\\&\\
            '\\*\\+\\-\\/\\=\\?\\^\\`\\{\\|\\}\\~]+@(?:(?:(?:[a-zA-Z0-9](?:[a-zA-Z0-9\\-] (?!\\.)){0,61}[a-zA-Z0-9]?\\.)+[a-zA-Z0-9](?:[a-zA-Z0-9\\-] (?!$)){0,61}[a-zA-Z0-9]?)|(?:\\[(?:(?:[01] ?\\d{1, 2}|2[0-4]\\d|25[0-5])\\.){3}(?:[01]?\\d{1,2}|2[0-4]\\d|25[0-5])\\]))$", RegexOptions.IgnoreCase);
            return result;
        }

        /// <summary>
        /// Sent email to target email address with attachment.
        /// </summary>
        /// <param name="toEmail">Email addresses of
        /// one or multiple receipients semi colon (;) separated values.</param>
        /// <param name="subject">Email subject</param>
        /// <param name="body">Email body</param>
        /// <returns>True | False</returns>
        public static bool SendEmailToTarget(string toEmail, string subject, string body)
        {
            bool success = false;
            try
            {
                SmtpClient SmtpServer = new SmtpClient();
                MailMessage mail = new MailMessage();

                SmtpServer.Credentials = new NetworkCredential(
                    Convert.ToString(ConfigurationManager.AppSettings["fromEmail"]),
                    Convert.ToString(ConfigurationManager.AppSettings["fromPassword"]));

                SmtpServer.Host = Convert.ToString
                (ConfigurationManager.AppSettings["hostName"]);
                SmtpServer.Port = Convert.ToInt32
                (ConfigurationManager.AppSettings["portNumber"]);

                if (Convert.ToBoolean
                (ConfigurationManager.AppSettings["isEnableSSL"]) == true)
                    SmtpServer.EnableSsl = true;

                mail.From = new MailAddress(Convert.ToString
                (ConfigurationManager.AppSettings["senderName"]));

                string[] multiEmails = toEmail.Split(';');
                foreach (string email in multiEmails)
                {
                    mail.To.Add(email);
                }

                mail.Subject = subject;
                mail.IsBodyHtml = true;
                mail.Body = body;
                SmtpServer.Send(mail);
                mail.Dispose();
                success = true;
            }
            catch (Exception)
            {
                success = false;
            }
            return success;
        }

        /// <summary>
        /// Sent email to target email address with attachment.
        /// </summary>
        /// <param name="toEmail">Email addresses of
        /// one or multiple receipients semi colon (;) separated values.</param>
        /// <param name="subject">Email subject</param>
        /// <param name="body">Email body</param>
        /// <param name="body">Email attachment file path</param>
        /// <returns>True | False</returns>
        public static bool SendEmailToTarget(string toEmail, string subject, string body, string attachmentPath)
        {
            bool success = false;
            try
            {
                SmtpClient SmtpServer = new SmtpClient();
                MailMessage mail = new MailMessage();

                SmtpServer.Credentials = new NetworkCredential(
                    Convert.ToString(ConfigurationManager.AppSettings["fromEmail"]),
                    Convert.ToString(ConfigurationManager.AppSettings["fromPassword"]));

                SmtpServer.Host = Convert.ToString
                (ConfigurationManager.AppSettings["hostName"]);
                SmtpServer.Port = Convert.ToInt32
                (ConfigurationManager.AppSettings["portNumber"]);

                if (Convert.ToBoolean(ConfigurationManager.AppSettings["isEnableSSL"]) == true)
                    SmtpServer.EnableSsl = true;

                mail.From = new MailAddress(Convert.ToString
                (ConfigurationManager.AppSettings["senderName"]));

                string[] multiEmails = toEmail.Split(';');
                foreach (string email in multiEmails)
                {
                    mail.To.Add(email);
                }

                Attachment attachment;
                attachment = new System.Net.Mail.Attachment(attachmentPath);
                mail.Attachments.Add(attachment);

                mail.Subject = subject;
                mail.IsBodyHtml = true;
                mail.Body = body;
                SmtpServer.Send(mail);
                mail.Dispose();
                success = true;
            }
            catch (Exception)
            {
                success = false;
            }
            return success;
        }

        #endregion Email

        #region Send Mail

        public static void SendEmail(string host, string emailSender, string passwordSender, String ToEmail, string cc, string bcc, String Subj, string Message, string fileLocation, bool attachedFile)
        {
            try
            {
                //Reading sender Email credential from web.config file
                string HostAdd = host;
                string FromEmailid = emailSender;
                string Pass = passwordSender;

                //creating the object of MailMessage
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(FromEmailid); //From Email Id
                mailMessage.Subject = Subj; //Subject of Email
                mailMessage.Body = Message; //body or message of Email

                if (attachedFile)
                    mailMessage.Attachments.Add(new Attachment(fileLocation));
                mailMessage.IsBodyHtml = true;

                string[] ToMuliId = ToEmail.Split(',');

                foreach (string ToEMailId in ToMuliId)
                {
                    mailMessage.To.Add(new MailAddress(ToEMailId));
                }

                string[] CCId = cc.Split(',');

                if (!String.IsNullOrEmpty(CCId[0].ToString()))
                {
                    foreach (string CCEmail in CCId)
                    {
                        mailMessage.CC.Add(new MailAddress(CCEmail));
                    }
                }

                string[] bccid = bcc.Split(',');

                if (!String.IsNullOrEmpty(bccid[0].ToString()))
                {
                    foreach (string bccEmailId in bccid)
                    {
                        mailMessage.Bcc.Add(new MailAddress(bccEmailId));
                    }
                }

                SmtpClient smtp = new SmtpClient
                {
                    Host = HostAdd,

                    Port = 587,
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(FromEmailid, Pass),
                    DeliveryMethod = SmtpDeliveryMethod.Network
                };
                smtp.Send(mailMessage);
            }
            catch (Exception ex)
            {
            }
            finally
            {
            }
        }

        #endregion Send Mail
    }
}