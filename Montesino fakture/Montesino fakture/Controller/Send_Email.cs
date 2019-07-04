using System.Net.Mail;

namespace Montesino_fakture.Controller
{
    public class Send_Email
    {
        private string logFilename;
        private string text;

        public Send_Email(string logFilename, string text)
        {
            this.logFilename = logFilename;
            this.text = text;
            Email_send(this.logFilename, this.text);
        }

        public void Email_send(string logFilename, string text)
        {
            using (MailMessage mail = new MailMessage())
            {
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress("logizvestaj@gmail.com");
                mail.To.Add("ivan.maric0707@gmail.com");
                mail.Subject = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
                mail.Body = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + System.Environment.NewLine + text;
                System.Net.Mail.Attachment attachment;
                attachment = new System.Net.Mail.Attachment(logFilename);
                mail.Attachments.Add(attachment);
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("logizvestaj@gmail.com", "usczkliqqtilenwx");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
            }
            //MailMessage mail = new MailMessage();
            //SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            //mail.From = new MailAddress("logizvestaj@gmail.com");
            //mail.To.Add("ivan.maric0707@gmail.com");
            //mail.Subject = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            //mail.Body = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + System.Environment.NewLine + text;
            //System.Net.Mail.Attachment attachment;
            //attachment = new System.Net.Mail.Attachment(logFilename);
            //mail.Attachments.Add(attachment);
            //SmtpServer.Port = 587;
            //SmtpServer.Credentials = new System.Net.NetworkCredential("logizvestaj@gmail.com", "usczkliqqtilenwx");
            //SmtpServer.EnableSsl = true;
            //SmtpServer.Send(mail);
        }
    }
}