using FluentEmail.Core;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.Shared.Services;

public class EmailService
{
    private readonly IFluentEmail _email;

    public EmailService(IFluentEmail email)
    {
        _email = email;
    }

    public async Task<string> SendEmail(string userSubject,string userBody)
    {
        try
        {
            var result = await _email
               .To("minthukha1239788@gmail.com")
               .Subject(userSubject)
               .Body(userBody,isHtml: true)
               .SendAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending email. {ex}");
        }

        return "Email sent successfully!";
    }
}