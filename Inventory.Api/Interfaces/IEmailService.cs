using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory.Api.Services;
namespace Inventory.Api.Interfaces
{
    public interface IEmailService
    {
        Task SendEmail(string toEmail, string subject, string body);
		Task SendEmail(string[] toEmail, string subject, string body);
    }
}
