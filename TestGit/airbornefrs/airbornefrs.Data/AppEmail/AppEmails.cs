using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using airbornefrs.Data.DBML;
using airbornefrs.Framework;

namespace airbornefrs.Data.AppEmail
{
    public class AppEmails: IDisposable 
    {
        public enum EmailSettingIDs
        {
            contactus = 6,
            purchasedproduct = 8,
            Reseller = 9,
            CorporateAdvertising = 10,
            PreSales = 11,
            rma = 12,
            mobileapp = 13
        }
        private EmailSettingIDs emailSettingID; // Enum

        public MailMessage MAIL { get; set; } //Dot new Mail Object


        /// <summary>
        /// Entity...class.. record details fetched from database
        /// </summary>
        private EmailSettings emailSetting;

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public AppEmails(EmailSettingIDs emailSettingID)
        {
            // TODO: Complete member initialization
            this.emailSettingID = emailSettingID;

            MAIL = new MailMessage();
            emailSetting = new EmailSettings();
            using (airbornefrsDataContext objDB = new airbornefrsDataContext())
            {
                var emailData = (from o in objDB.emailSettings_GET_sp_V02(Convert.ToInt32(emailSettingID)) select o).ToList();
                var email = emailData[0];

                if (!string.IsNullOrEmpty(email.defaultbcc))
                {
                    emailSetting.DefaultBCC = email.defaultbcc;
                    string[] BCC = emailSetting.DefaultBCC.Split(',');
                    foreach (var item in BCC)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            MAIL.Bcc.Add(item);
                        }
                    }
                }
                if (!string.IsNullOrEmpty(email.defaultcc))
                {
                    emailSetting.DefaultCC = email.defaultcc;
                    string[] CC = emailSetting.DefaultCC.Split(',');
                    foreach (var item in CC)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            MAIL.CC.Add(item);
                        }
                    }
                }

                MAIL.Body = email.defaultmailbody;


                if (!string.IsNullOrEmpty(email.defaultto))
                {
                    emailSetting.DefaultTo = email.defaultto;
                    string[] TO = emailSetting.DefaultTo.Split(',');
                    foreach (var item in TO)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            MAIL.To.Add(item);
                        }
                    }
                }
                emailSetting.EmailSettingID = Convert.ToInt32(emailSettingID);

                //email.enablessl
                if (!string.IsNullOrEmpty(email.fromemailaddress))
                {
                    emailSetting.FromEmailAddress = email.fromemailaddress;
                    if (!string.IsNullOrEmpty(email.fromemailname))
                    {
                        emailSetting.FromName = email.fromemailname;
                    }
                    else
                    {
                        emailSetting.FromName = "";
                    }
                    MAIL.From = new MailAddress(emailSetting.FromEmailAddress, emailSetting.FromName);
                }

                MAIL.IsBodyHtml = Convert.ToBoolean(email.isbodyhtml);
                emailSetting.IsBodyHtml = Convert.ToBoolean(email.isbodyhtml);

                MAIL.Subject = email.subject;
                emailSetting.Subject = email.subject;

                emailSetting.SMTP = email.outgoingsmtp;
                emailSetting.Port = email.port;
                emailSetting.FromEmailPassword = email.emailpwd;

            }
        }

        public BoolResponse FireEmail()
        {
            BoolResponse response = new BoolResponse();
            try
            {
                //Do not fire email when server is LocalHost

                if (1 == 2)
                {
                    response.status = 1;
                    response.message = "MAIL DOES NOT WORK ON LOCAL";
                    return response;
                }
                else
                {
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = emailSetting.SMTP;
                    smtp.Port = Convert.ToInt32(emailSetting.Port);
                    smtp.EnableSsl = emailSetting.EnableSSL;

                    smtp.Credentials = new System.Net.NetworkCredential(emailSetting.FromEmailAddress, emailSetting.FromEmailPassword);
                    MAIL.IsBodyHtml = true;
                    smtp.Send(MAIL);
                    smtp = null;

                    //this.Dispose();
                    response.status = 1;
                    response.message = "SUCCESS";
                }
            }
            catch (Exception ex)
            {
                //LogErrors.RecordErrors("FireEmail", ex.Message)
                response.status = -1;
                response.message = ex.Message;
            }

            return response;
        }
    }
}