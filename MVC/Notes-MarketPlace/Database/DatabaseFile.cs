using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Notes_MarketPlace.Models;
using System.Net;
using System.Net.Mail;
using PagedList;

namespace Notes_MarketPlace.Database
{
    public class DatabaseFile
    {
        DBEntities db = new DBEntities();
        public bool contactus(Contactu contact)
        {
            try
            {
                db.Contactus.Add(contact);
                db.SaveChanges();
                ManageSystem ms = GetManageData.GetData();
                MailMessage mm = new MailMessage(ms.SupportEmail, ms.EmailAddress);
                mm.Subject = contact.Name + " - Query";
                mm.Body = "Hello,<br/><br/>" + contact.Comment + "<br/><br/>Regards,<br/>" + contact.Name;
                mm.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;

                NetworkCredential nc = new NetworkCredential(ms.SupportEmail, ms.EmailPassword);
                smtp.EnableSsl = true;
                smtp.Credentials = nc;
                smtp.Send(mm);
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        } 
    }

    public class GetManageData
    {
        public static ManageSystem GetData()
        {
            DBEntities mdb = new DBEntities();
            ManageSystem mb = mdb.ManageSystems.FirstOrDefault(x => x.Id == 1);
            mb.EmailPassword = "Digi5vgec@2021";
            return mb;
        }
    }
}