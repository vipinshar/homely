using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace Homely.Utility
{
    public static class CommonUtility
    {

        public static void SendMail(string forQ,string m_strName,string m_strEmailId,string m_strPassword,string m_strToMailAddress,string m_strMailSubject)
        {
            try
            {
                StreamReader srmailerText;
                string m_strMailerText = string.Empty;
                switch (forQ)
                {
                    case "ForgotPassword":
                        srmailerText = File.OpenText(System.Web.HttpContext.Current.Server.MapPath("~/Mailers/forgot-password-email.html"));
                         m_strMailerText = srmailerText.ReadToEnd();
                        m_strMailerText = m_strMailerText.Replace("[NAME]", m_strName);
                        m_strMailerText = m_strMailerText.Replace("[USERNAME]", m_strEmailId);
                        m_strMailerText = m_strMailerText.Replace("[PASSWORD]", m_strPassword);
                        srmailerText.Close();
                        break;

                    case "Registration":
                        srmailerText = File.OpenText(System.Web.HttpContext.Current.Server.MapPath("~/Mailers/registration-email.html"));
                        m_strMailerText = srmailerText.ReadToEnd();
                        m_strMailerText = m_strMailerText.Replace("[NAME]", m_strName);
                        srmailerText.Close();
                        break;
                    case "Property":
                        srmailerText = File.OpenText(System.Web.HttpContext.Current.Server.MapPath("~/Mailers/Property.html"));
                        m_strMailerText = srmailerText.ReadToEnd();
                        m_strMailerText = m_strMailerText.Replace("[NAME]", m_strName);
                        m_strMailerText = m_strMailerText.Replace("[PASSWORD]", m_strPassword);
                        srmailerText.Close();
                        break;
                }

                //Send mail
                MailMessage mm = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                mm.From = new MailAddress("help@homely.co.in".ToString());
                mm.To.Add(new MailAddress(m_strToMailAddress));
                mm.Subject = m_strMailSubject;
                // string body = "";
                mm.Body = m_strMailerText;
                mm.IsBodyHtml = true;
                smtp.Host = "smtp.live.com";
                smtp.Port = 25;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = new System.Net.NetworkCredential("help@homely.co.in", "apple@123");
                smtp.EnableSsl = true;
                try
                {
                    smtp.Send(mm);
                }
                catch { }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}