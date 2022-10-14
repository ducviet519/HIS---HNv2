using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace System.App.Repositories.Common
{
    public class SendMail
    {
        public void SendEmail(String ToEmail, string cc, string bcc, String Subj, string Message, string fileLocation, bool attachedFile)
        {
            try
            {
                //Reading sender Email credential from web.config file
                string HostAdd = ConfigurationManager.AppSettings["Host"].ToString();
                string FromEmailid = ConfigurationManager.AppSettings["FromMail"].ToString();
                string Pass = ConfigurationManager.AppSettings["Password"].ToString();

                //creating the object of MailMessage
                MailMessage mailMessage = new MailMessage()
                {
                    From = new MailAddress(FromEmailid), //From Email Id
                    Subject = Subj, //Subject of Email
                    Body = Message //body or message of Email
                };

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

                SmtpClient smtp = new SmtpClient()
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
    }
}