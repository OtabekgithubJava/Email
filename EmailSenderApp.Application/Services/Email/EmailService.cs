using System.Net;
using System.Net.Mail;
using EmailSenderApp.Domain.Entities.Models;
using Microsoft.Extensions.Configuration;

namespace EmailSenderApp.Application.Services.Email;

public class EmailService: IEmailService
{
    private readonly IConfiguration _config;

    public EmailService(IConfiguration config)
    {
        _config = config;
    }

    public async Task SendEmailAsync(EmailModel model)
    {
        var emailSettings = _config.GetSection("EmailSettings");
        
        // string path = @"/Users/otabek_coding/C# codes/RiderProjects/Email/EmailSenderApp.Application/Body.html";

        string path = @"/Users/otabek_coding/C# codes/RiderProjects/Email/EmailSenderApp.Application/Code.html";
        
        using(var stream = new StreamReader(path))
        {
            model.Body = await stream.ReadToEndAsync();
        }
        
        var mailMessage = new MailMessage
        {
            From = new MailAddress(emailSettings["Sender"], emailSettings["SenderName"]),
            Subject = model.Subject,
            Body = model.Body,
            IsBodyHtml = true
        };
        mailMessage.To.Add(model.Receiver);

        using var smtpClient = new SmtpClient(emailSettings["EmailServer"], int.Parse(emailSettings["MailPort"]))
        {
            Port = Convert.ToInt32(emailSettings["MailPort"]),
            DeliveryMethod = SmtpDeliveryMethod.Network,
            Credentials = new NetworkCredential(emailSettings["Sender"], emailSettings["Password"]),
            EnableSsl = true,
        };
        
        await smtpClient.SendMailAsync(mailMessage);
    }

    public async Task<bool> CheckEmailAsync(string code)
    {
        return code == "CR7";
    }

    public async Task SetPassword(User user)
    {
        string filePath = @"/Users/otabek_coding/C# codes/RiderProjects/Email/EmailSenderApp.Application/AvailableUsers.txt";

        string userEntry = $"{user.Email},{user.Password}\n";

        string[] userEntries = await File.ReadAllLinesAsync(filePath);

        foreach (var entry in userEntries)
        {
            var parts = entry.Split(',');

            if (parts.Length == 2 && parts[0] == user.Email)
            {
                return;
            }
        }
        await File.AppendAllTextAsync(filePath, userEntry);
    }

    public async Task<bool> IsUserRegistered(string email)
    {
        string filePath = @"/Users/otabek_coding/C# codes/RiderProjects/Email/EmailSenderApp.Application/AvailableUsers.txt";

        string[] userEntries = await File.ReadAllLinesAsync(filePath);

        foreach (var entry in userEntries)
        {
            var parts = entry.Split(',');

            if (parts.Length == 2 && parts[0] == email)
            {
                return true;
            }
        }

        return false;
    }



    public async Task<bool> VerifyCredentials(User user)
    {
        string filePath = @"/Users/otabek_coding/C# codes/RiderProjects/Email/EmailSenderApp.Application/AvailableUsers.txt";
    
        string[] userEntries = await File.ReadAllLinesAsync(filePath);
    
        foreach (var entry in userEntries)
        {
            var parts = entry.Split(',');
    
            if (parts.Length == 2 && parts[0] == user.Email && parts[1] == user.Password)
            {
                return true;
            }
        }
    
        return false;
    }
}