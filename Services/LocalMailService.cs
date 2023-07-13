using System.Net;
using System.Net.Mail;

namespace Web_Api_Application.Services
{
  public class LocalMailService
  {
    string _mailFrom = "log@test.com";
    string _mailTo = "amirjob75@gmail.com";

    public void Send(string subject, string message)
    {
      try
      {
        MailMessage _mailMessage = new MailMessage();
        SmtpClient smtpClient = new SmtpClient();
        _mailMessage.From = new MailAddress(_mailFrom);
        _mailMessage.To.Add(new MailAddress(_mailTo));
        _mailMessage.Subject = subject;
        _mailMessage.Body = message;
        smtpClient.Port = 587;
        smtpClient.Host = "smtp.gmail.com";
        smtpClient.EnableSsl = true;
        smtpClient.UseDefaultCredentials = false;
        smtpClient.Credentials = new NetworkCredential("FromMailAdress", "Password");
        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtpClient.Send(_mailMessage);
      }
      catch (Exception ex)
      {

      }
    }
  }
}
