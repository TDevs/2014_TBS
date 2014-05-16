using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Collections;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Net.Mime;
using System.IO;
using System.Net.Configuration;
using System.Configuration;
using System.Web.Configuration;
using System.Net;


namespace Oas.Service.Mail
{
    public class SendEmailService
    {
        static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(SendEmailService));
        /// <summary>
        /// HTML email
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <param name="addresses"></param>
        public void SendEmail(string subject, string message, string[] addresses)
        {
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            System.Net.Mail.MailMessage m = new System.Net.Mail.MailMessage();
            MailAddress mailAdds;
            foreach (string k in addresses)
            {
                mailAdds = new MailAddress(k);
                m.To.Add((mailAdds));
            }
            m.Subject = subject;
            m.Body = message;
            m.IsBodyHtml = true;
            client.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
            //client.Send(m);

            //hoantq change
            SendEmail(client, m);
        }

        public void EmailUs(string subject, string message, string[] addresses)
        {
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            System.Net.Mail.MailMessage m = new System.Net.Mail.MailMessage();
            MailAddress mailAdds;
            foreach (string k in addresses)
            {
                mailAdds = new MailAddress(k);
                m.To.Add((mailAdds));
            }
            m.Subject = subject;
            m.Body = message;
            m.IsBodyHtml = true;
            client.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;

            SendEmail(client, m);
        }

        /// <summary>
        /// not HTML
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <param name="address"></param>
        /// <param name="isHtml"></param>
        public void SendEmail(string subject, string message, string[] addresses, bool isHtml)
        {
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            System.Net.Mail.MailMessage m = new System.Net.Mail.MailMessage();
            foreach (string k in addresses)
            {
                m.To.Add(k);
            }
            m.Subject = subject;
            m.Body = message;
            m.IsBodyHtml = isHtml;
            client.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;

            SendEmail(client, m);
        }

        public void SendEmail(string m_subject, string m_message, string[] m_address, bool m_isHtml, System.Net.Mail.MailPriority m_priority)
        {
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            System.Net.Mail.MailMessage m = new System.Net.Mail.MailMessage();
            foreach (string k in m_address)
            {
                m.To.Add(k);
            }

            m.Subject = m_subject;
            m.Body = m_message;
            m.IsBodyHtml = m_isHtml;
            m.Priority = m_priority;

            client.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
            //client.Send(m);

            //hoantq change
            SendEmail(client, m);
        
        }
        
        public void SendEmail(string subject, string message, string address, string header, bool isHtml)
        {
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            System.Net.Mail.MailMessage m = new System.Net.Mail.MailMessage();
            string accountMail = ConfigurationManager.AppSettings["AccountMail"];
            string password = ConfigurationManager.AppSettings["Password"];
            string[] addressList = address.Split(',', ';');
            foreach (var item in addressList)
            {
                m.To.Add((item));
            }

            m.Subject = subject;
            m.Body = message;
            m.IsBodyHtml = isHtml;
            m.Headers.Add("Info", header);
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(accountMail, password);
            client.SendCompleted += client_SendCompleted;
            try
            {
                string token = "";
                client.SendAsync(m, token);
            }
            catch(Exception ex)
            {
                logger.Error(ex);
            }            
        }

        public void SendMailWithAttachment(string subject, string message, string address, string header, bool isHtml,string fileAttach)
        {
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            System.Net.Mail.MailMessage m = new System.Net.Mail.MailMessage();
            string accountMail = ConfigurationManager.AppSettings["AccountMail"];
            string password = ConfigurationManager.AppSettings["Password"];
            string[] addressList = address.Split(',', ';');
            foreach (var item in addressList)
            {
                m.To.Add((item));
            }
            System.Net.Mail.Attachment attachment;
            attachment = new System.Net.Mail.Attachment(fileAttach);
            m.Attachments.Add(attachment);

            m.Subject = subject;
            m.Body = message;
            m.IsBodyHtml = isHtml;           

            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(accountMail, password);
            client.SendCompleted += client_SendCompleted;
            try
            {
                string token = "";
                client.SendAsync(m, token);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }            
           
        }

        void client_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {

        }

        public void SendEmail(string subject, string message, string address, bool isHtml)
        {
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            System.Net.Mail.MailMessage m = new System.Net.Mail.MailMessage();

            string[] addressList = address.Split(',', ';');
            foreach (var item in addressList)
            {
                m.To.Add((item));
            }

            m.Subject = subject;
            m.Body = message;
            m.IsBodyHtml = isHtml;
            client.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;

            SendEmail(client, m);
        }

        public void SendEmail(string subject, string message, string address, bool isHtml, System.Net.Mail.MailPriority m_priority)
        {
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            System.Net.Mail.MailMessage m = new System.Net.Mail.MailMessage();
            m.To.Add((address));
            m.Subject = subject;
            m.Body = message;
            m.IsBodyHtml = isHtml;
            m.Priority = m_priority;
            client.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;

            SendEmail(client, m);
        }

        public void SendEmail(string subject, string message, System.Collections.ArrayList addresses, bool isHtml)
        {
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            System.Net.Mail.MailMessage m = new System.Net.Mail.MailMessage();
            foreach (string k in addresses)
            {
                m.To.Add(k);
            }
            string q = System.Text.RegularExpressions.Regex.Replace(subject, @"\r\n+", " ");
            m.Subject = q;
            m.Body = message;
            m.IsBodyHtml = isHtml;
            client.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;

            SendEmail(client, m);
        }

        public void SendEmail(string m_subject, string m_message, System.Collections.ArrayList m_address, bool m_isHtml, System.Net.Mail.MailPriority m_priority)
        {
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            System.Net.Mail.MailMessage m = new System.Net.Mail.MailMessage();
            foreach (string k in m_address)
            {
                m.To.Add(k);
            }

            string q = System.Text.RegularExpressions.Regex.Replace(m_subject, @"\r\n+", " ");
            m.Subject = q;
            m.Body = m_message;
            m.IsBodyHtml = m_isHtml;
            m.Priority = m_priority;

            client.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;

            SendEmail(client, m);
        }

        public void SendEmailResetPassword(string subject, string message, System.Collections.ArrayList addresses)
        {
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            System.Net.Mail.MailMessage m = new System.Net.Mail.MailMessage();

            foreach (string k in addresses)
            {
                m.To.Add(k);
            }
            m.Subject = subject;
            m.Body = message;
            m.IsBodyHtml = false;
            client.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;

            SendEmail(client, m);
        }

        public void SendEmail(string subject, string message, System.Collections.ArrayList addresses)
        {
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            System.Net.Mail.MailMessage m = new System.Net.Mail.MailMessage();

            foreach (string k in addresses)
            {
                m.To.Add(k);
            }
            m.Subject = subject;
            m.Body = message;
            m.IsBodyHtml = true;
            client.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
            SendEmail(client, m);
        }

        #region SendMail Async
        public void SendEmail(System.Net.Mail.SmtpClient client, System.Net.Mail.MailMessage m)
        {
            try
            {
                SendEmailDelegate sd = new SendEmailDelegate(client.Send);

                AsyncCallback cb = new AsyncCallback(SendEmailResponse);
                sd.BeginInvoke(m, cb, sd);

                //client.Send(m);
            }
            catch (Exception ex)
            {
                logger.Error("----------- Error when send email to : " + m.To);
                logger.Error(ex.Message);
            }
        }

        private delegate void SendEmailDelegate(System.Net.Mail.MailMessage m);

        private static void SendEmailResponse(IAsyncResult ar)
        {
            try
            {
                SendEmailDelegate sd = (SendEmailDelegate)(ar.AsyncState);

                sd.EndInvoke(ar);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }
        #endregion

        public void SendMailWithAttachment(XmlReader emailContent, ArrayList attachments)
        {
            string subject = string.Empty;
            string message = string.Empty;
            System.Collections.ArrayList addresses = new ArrayList();
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            System.Net.Mail.MailMessage m = new System.Net.Mail.MailMessage();

            //content mail get from emailContent

            foreach (string k in addresses)
            {
                m.To.Add(k);
            }
            m.Subject = subject;
            m.Body = message;

            // The attachments array should point to a file location     
            // where 
            // the attachment resides - add the attachments to the
            // message
            foreach (string attach in attachments)
            {
                Attachment attached = new Attachment(attach, MediaTypeNames.Application.Octet);
                m.Attachments.Add(attached);
            }

            client.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
            //client.Send(m);

            //hoantq change
            SendEmail(client, m);
        }

        /// <summary>
        /// Send Email that has attach file
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="attachFile"></param>
        /// <returns></returns>
        /// <create>NghiaLT 20/11/2012</create>
        public bool SendEmail(string emailAddress, string subject, string body, string attachFile)
        {
            bool result = false;
            try
            {
                var emailSettings = GetEmailConfiguration();
                //List<string> listAttchFile = new List<string>();
                //listAttchFile.Add(attachFile);
                if (emailSettings != null)
                {
                    // WebMail.SmtpServer = emailSettings.Smtp.Network.Host;
                    // WebMail.SmtpPort = emailSettings.Smtp.Network.Port;
                    // WebMail.Send(emailAddress, subject, body, null, null, listAttchFile);
                    SmtpClient smtpClient = new SmtpClient();
                    var mailMessage = new MailMessage();

                    // mailMessage.From = new MailAddress("user01@sss.com");
                    // Subject and Body
                    mailMessage.Body = body;
                    mailMessage.Subject = subject;
                    mailMessage.To.Add(emailAddress);

                    Attachment attachment = new Attachment(attachFile, MediaTypeNames.Application.Octet);

                    mailMessage.Attachments.Add(attachment);

                    mailMessage.IsBodyHtml = true;

                    smtpClient.Send(mailMessage);

                    result = true;
                }
            }
            catch (Exception ce)
            {
                logger.Error("----------- Error when send email to : " + emailAddress);
                logger.Error(ce.Message);
            }

            return result;
        }

        /// <summary>
        /// Get Email configuration
        /// </summary>
        /// <returns></returns>
        private MailSettingsSectionGroup GetEmailConfiguration()
        {
            Configuration configuration = null;
            MailSettingsSectionGroup mailSettingsSectionGroup;

            try
            {
                configuration = WebConfigurationManager.OpenWebConfiguration("~");
            }
            catch
            {
                try
                {
                    configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                }
                catch { }
            }

            try
            {
                mailSettingsSectionGroup = configuration.GetSectionGroup("system.net/mailSettings") as MailSettingsSectionGroup;
                return mailSettingsSectionGroup;
            }
            catch { }

            return null;
        }

        /// <summary>
        /// Check an email address is valid
        /// </summary>
        /// <param name="emailAddress">Full email address to validate</param>
        /// <returns>True if email address is valid</returns>
        public static bool ValidateEmailAddress(string emailAddress)
        {
            try
            {
                string mailAddToValid = emailAddress;
                //declare validate email
                Regex expression = new Regex(@"\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}");
                // check with expression
                if (expression.IsMatch(mailAddToValid))
                {
                    // is valid email address
                    return true;
                }
                else
                {
                    // is not valid email address
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
