using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections;
using System.Text;
using Inventory.Api.Interfaces;
using Microsoft.AspNetCore.Identity;
using Inventory.Api.Data;
using Inventory.Api.Models;
using Inventory.Api.Entity;
using static System.Net.WebRequestMethods;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Mail;

namespace Inventory.Api.Services
{
    public class ProductServices : IProductServices
    {
        private readonly InventoryDbContext _ctx;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _config;

        public ProductServices(InventoryDbContext ctx ,IEmailService emailService, IConfiguration config)
        {
            _ctx = ctx;
            _emailService = emailService;
            _config = config;
        }

        public async Task<List<GetAllProductsModel>> GetAllProductsList()
        {
            try
            {
                var personList = await _ctx.Products
                    .Select(pn => new GetAllProductsModel
                    {
                        ProductId = pn.ProductId,
                        Name = pn.ProductName,
                        TenantId = pn.TenantId,
                       Quantity = pn.Quantity,
                       CategoryId = pn.CategoryId,
                       SKU = pn.SKU,
                    })
                    .ToListAsync();

                return personList;
            }
            catch (Exception ex)
            {
                SendExceptionEmail(ex.Message, ex.StackTrace, "Get All Employees Function");
                return new List<GetAllProductsModel>();
            }
        }
        #region ------------ All Emails Function ----------------
        [NonAction]
        private async Task SendExceptionEmail(string exceptionMessage, string stackTrace, string module)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<!DOCTYPE html>");
            sb.Append("<html>");
            sb.Append("<head>");
            sb.Append("<meta charset='UTF-8'>");
            sb.Append("<style>");
            sb.Append("body { font-family: Calibri, Arial, sans-serif; background-color: #f6f6f6; margin: 0; padding: 0; }");
            sb.Append(".container { width: 90%; max-width: 600px; margin: 20px auto; background: #ffffff; border-radius: 8px; box-shadow: 0 2px 8px rgba(0,0,0,0.1); overflow: hidden; }");
            sb.Append(".header { background-color: #D32F2F; padding: 15px; text-align: center; color: white; font-size: 16pt; font-weight: bold; }");
            sb.Append(".content { padding: 20px; font-size: 11pt; line-height: 1.5; color: #333333; }");
            sb.Append(".section { background-color: #f9f9f9; border: 1px solid #ddd; border-radius: 5px; padding: 10px; margin-top: 10px; }");
            sb.Append(".footer { background-color: #f1f1f1; padding: 15px; text-align: center; font-size: 9pt; color: #555; }");
            sb.Append("</style>");
            sb.Append("</head>");
            sb.Append("<body>");
            sb.Append("<div class='container'>");
            sb.Append("<div class='header'>Application Exception Alert</div>");
            sb.Append("<div class='content'>");
            sb.Append("<p>Dear Team,</p>");
            sb.Append("<p>An exception has occurred in the application. Please review the details below:</p>");

            sb.Append("<div class='section'><strong>Module:</strong> " + module + "</div>");
            sb.Append("<div class='section'><strong>Exception Message:</strong><br/>" + exceptionMessage + "</div>");
            sb.Append("<div class='section'><strong>Stack Trace:</strong><br/><pre>" + stackTrace + "</pre></div>");
            sb.Append("<div class='section'><strong>Timestamp:</strong> " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "</div>");

            sb.Append("<p>Please investigate this issue at the earliest. If needed, contact the development team.</p>");
            sb.Append("<p>Regards,<br/>Inventory Management System</p>");
            sb.Append("</div>");
            sb.Append("<div class='footer'>© " + DateTime.Now.Year + " Inventory Management. All rights reserved.</div>");
            sb.Append("</div>");
            sb.Append("</body>");
            sb.Append("</html>");
            string exceptionEmail = _config["EmailSetting:ExceptionEmail"];
            // Send email using your existing EmailService
            await _emailService.SendEmail(exceptionEmail, "Application Exception Alert", sb.ToString());
        }

        #endregion
    }
}
