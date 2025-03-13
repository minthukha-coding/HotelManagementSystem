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

    public async Task<string> SendEmail(string userSubject,string userBody,string toMail)
    {
        try
        {
            //var result = await _email
            //   .To(toMail)
            //   .Subject(userSubject)
            //   .Body(userBody,isHtml: true)
            //   .SendAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending email. {ex}");
            //return new Exception("Error sending email.");
        }

        return "Email sent successfully!";
    }
}