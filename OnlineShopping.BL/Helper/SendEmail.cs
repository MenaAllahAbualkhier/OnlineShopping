using OnlineShopping.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;



namespace OnlineShopping.BL.Helper
{
	public static class SendEmail
	{
        public static string SendMaile(EmailVM model)
        {
            try
            {
                
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential("amenaabdo2002@gmail.com", "hkxhjtcqxeqkxmux");
                smtp.Send("amenaabdo2002@gmail.com", model.Mail, model.Title, model.Massage);
                var result = "Mail Send Succesfluy";
                return result;
            }
            catch (Exception ex)
            {
                var result = ex.Message;
                return result;
            }
        }
    }
}
