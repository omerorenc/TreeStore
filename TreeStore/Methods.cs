using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreeStore.Models;
using TreeStore.Models.Entities;

namespace TreeStore
{
    public static class Methods
    {
        public static string GetUniqueKey()
        {
            int length = 10;
            string guidResult = string.Empty;

            while (guidResult.Length < length)
            {

                guidResult += Guid.NewGuid().ToString().GetHashCode().ToString("x");
            }


            if (length <= 0 || length > guidResult.Length)
                throw new ArgumentException("Length must be between 1 and " + guidResult.Length);

            return guidResult.Substring(0, length);
        }
        public static void SendMail(MailSetting mailSetting, Contact contact)
        {
            var mimeMessage = new MimeMessage();
            string FromAddress = mailSetting.FromAddress;
            string FromAddressTitle = mailSetting.FromAddressTitle;
            string ToAddress = contact.Email;
            string ToAddressTitle = contact.FullName;
            string Subject = mailSetting.Subject;
            string BodyContent = mailSetting.BodyContent;
            string SmptServer = mailSetting.SmptServer;
            int SmptPortNumber = mailSetting.SmptPortNumber;
            mimeMessage.From.Add(new MailboxAddress(FromAddressTitle, FromAddress));
            mimeMessage.To.Add(new MailboxAddress(ToAddressTitle, ToAddress));
            mimeMessage.Subject = Subject;

            if (contact.Reply == null || contact.Reply == "")
            {
                mimeMessage.Body = new TextPart("plain")
                {
                    Text = BodyContent
                };

            }
            else
            {
                mimeMessage.Body = new TextPart("plain")
                {
                    Text = contact.Reply
                };

            }
            using (var client = new SmtpClient())
            {
                client.Connect(mailSetting.SmptServer, mailSetting.SmptPortNumber, false);
                client.Authenticate(mailSetting.FromAddress, mailSetting.FromAddressPassword);
                client.Send(mimeMessage);
                client.Disconnect(true);
            }

        }
        public static void SendMemberMail(MailSetting mailSetting, ApplicationUser user)
        {
            var mimeMessage = new MimeMessage();
            string BodyContent;
            string FromAddress = mailSetting.FromAddress;
            string FromAddressTitle = mailSetting.FromAddressTitle;
            string ToAddress = user.Email;
            string ToAddressTitle = user.CompanyName;
            string Subject = "Üyelik Başvurunuz Hakkında";
            string SmptServer = mailSetting.SmptServer;
            int SmptPortNumber = mailSetting.SmptPortNumber;
            if (user.EmailConfirmed == true)
            {
                BodyContent = "Firma üyeliğiniz onaylanmıştır. Firmanızın ürünlerini ekleyip paylaşabilirsiniz.\n Tree Store \n İyi Çalışmalar Dileriz..";
            }
            else
            {
                BodyContent = "Üyelik başvurunuz elimize ulaşmıştır. Firma üyeliğiniz onaylandığında size mail ile bildirilecektir.\n Tree Store \n İyi Çalışmalar Dileriz..";
            }
            mimeMessage.From.Add(new MailboxAddress(FromAddressTitle, FromAddress));
            mimeMessage.To.Add(new MailboxAddress(ToAddressTitle, ToAddress));
            mimeMessage.Subject = Subject;
            mimeMessage.Body = new TextPart("plain")
            {
                Text = BodyContent
            };
            using (var client = new SmtpClient())
            {
                client.Connect(mailSetting.SmptServer, mailSetting.SmptPortNumber, false);
                client.Authenticate(mailSetting.FromAddress, mailSetting.FromAddressPassword);
                client.Send(mimeMessage);
                client.Disconnect(true);
            }
        }


        public static void indirim(ICollection<Product> products, float rate)
        {
           foreach(var product in products)
            {
                product.DiscountPrice = product.Price * rate;
            } 
           


        }
 
            
    }
    
}