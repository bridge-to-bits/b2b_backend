using Microsoft.Extensions.Options;
using Core.Interfaces.Auth;
using Core.Interfaces.Services;
using System.Net.Mail;
using System.Net;
using static System.Net.Mime.MediaTypeNames;

namespace Core.Services;

public class MailService(IOptions<MailerOptions> options) : IMailService
{
    private class Mail()
    {
        public string Subject { get; set; }
        public string Body { get; set; }
    }

    private readonly MailerOptions _options = options.Value;

    public async Task SendAgreementEmailAsync(string producerId, string producerUsername, string performerId, string performerEmail)
    {
        var mail = BuildAgreementEmail(producerId, producerUsername, performerEmail);

        var smtpClient = new SmtpClient(_options.Host)
        {
            Port = _options.Port,
            Credentials = new NetworkCredential(_options.Username, _options.Password),
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(_options.Username),
            Subject = mail.Subject,
            Body = mail.Body,
            IsBodyHtml = true
        };

        mailMessage.To.Add(performerEmail);

        await smtpClient.SendMailAsync(mailMessage);
    }

    private static Mail BuildAgreementEmail(string producerId, string producerUsername, string performerId)
    {
        var approvalLink = $"{AppConfig.GetSetting("FRONT_URL")}/mail/approove/{producerId}/{performerId}";
        var producerProfileUrl = $"{AppConfig.GetSetting("FRONT_URL")}/profile/{producerId}";

        var subject = "Approval Request for Collaboration Agreement";

        var body = $@"
    <!DOCTYPE html>
    <html>
    <head>
        <style>
            body {{
                font-family: Arial, sans-serif;
                background-color: #030303;
                color: #E5E5DE;
                margin: 0;
                padding: 20px;
            }}
            .container {{
                background-color: #2C2C2C;
                max-width: 600px;
                margin: 0 auto;
                padding: 20px;
                border-radius: 8px;
                box-shadow: 0 4px 12px rgba(0, 0, 0, 0.4);
            }}
            .header {{
                background-color: #1E18C2;
                color: #E5E5DE;
                padding: 10px;
                text-align: center;
                font-size: 24px;
                font-weight: bold;
                border-radius: 8px 8px 0 0;
            }}
            .content {{
                margin: 20px 0;
                font-size: 16px;
                line-height: 1.6;
            }}
            p {{
                color: #E5E5DE;
            }}
            a.button {{
                display: inline-block;
                padding: 12px 24px;
                background-color: #EC5D0B;
                color: #E5E5DE;
                text-decoration: none;
                font-weight: bold;
                border-radius: 4px;
                margin-top: 20px;
            }}
            a.link {{
                color: #EC5D0B;
                text-decoration: none;
            }}
            a.hover {{
                text-decoration: underline;
            }}
            .footer {{
                font-size: 12px;
                color: #999999;
                text-align: center;
                margin-top: 20px;
            }}
        </style>
    </head>
    <body>
        <div class=""container"">
            <div class=""header"">
                Approval Request for Collaboration
            </div>
            <div class=""content"">
                <p>
                    You have a new collaboration request from a
                    <a href=""{producerProfileUrl}"" class=""link"">
                        {producerUsername}
                    </a>.
                </p>
                <p>
                    Please click the button below to approve the agreement:
                </p>
                <p>
                    <a href=""{approvalLink}"" class=""button"">
                        Approve Agreement
                    </a>
                </p>
            </div>
            <div class=""footer"">
                If you did not expect this email, please ignore it.
            </div>
        </div>
    </body>
    </html>";

        return new Mail() { Body = body, Subject = subject };
    }
}