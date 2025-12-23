using Inventory.Api.Entity;
using Inventory.Api.Services;
using Inventory.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Inventory.Api.Interfaces;
using System.Text;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly JwtService _jwt;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IEmailService _emailService;
    private readonly RoleManager<IdentityRole> _roleManager;
    public AuthController(UserManager<ApplicationUser> userManager, JwtService jwt, SignInManager<ApplicationUser> signInManager, IEmailService emailService, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _jwt = jwt;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _emailService = emailService;

    }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto login)
    {
        if (login == null || string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Password))
        {
            return Ok(new
            {
                success = false,
                message = "Email and Password are required.",

            });
        }

        var user = await _userManager.FindByEmailAsync(login.Email);
        if (user == null)
        {
            return Ok(new
            {
                success = false,
                message = "Invalid email."

            });
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);

        if (result.Succeeded)
        {
            var token = _jwt.GenerateToken(user);
            var roles = await _userManager.GetRolesAsync(user);
            //SendCredentialsDetailsEmail("Ashish Kumar Singh", "kumarashu8287@gmail.com", login.Username, login.Password);

            return Ok(new
            {
                success = true,
                message = "email and password matched successfully!",
                token = token,
                username = user.UserName,
                roles = roles
            });

        }


        return Ok(new
        {
            success = false,
            message = "Invalid Password.",

        });
    }
    [NonAction]
    private async Task<bool> SendCredentialsDetailsEmail(string name, string emailId, string username, string password)
    {
        if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(emailId))
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<!DOCTYPE html>");
            sb.Append("<html>");
            sb.Append("<head>");
            sb.Append("<meta charset='UTF-8'>");
            sb.Append("<style>");
            sb.Append("body { font-family: Calibri, Arial, sans-serif; background-color: #f6f6f6; margin: 0; padding: 0; }");
            sb.Append(".container { width: 90%; max-width: 600px; margin: 20px auto; background: #ffffff; border-radius: 8px; box-shadow: 0 2px 8px rgba(0,0,0,0.1); overflow: hidden; }");
            sb.Append(".header { background-color: #F3731F; padding: 15px; text-align: center; color: white; font-size: 18pt; font-weight: bold; }");
            sb.Append(".content { padding: 25px; font-size: 12pt; line-height: 1.6; color: #333333; }");
            sb.Append(".credentials { background-color: #f9f9f9; border: 1px solid #ddd; border-radius: 5px; padding: 15px; margin-top: 15px; }");
            sb.Append(".credentials p { margin: 5px 0; font-size: 11pt; }");
            sb.Append(".footer { background-color: #f1f1f1; padding: 15px; text-align: center; font-size: 10pt; color: #555; }");
            sb.Append("</style>");
            sb.Append("</head>");
            sb.Append("<body>");
            sb.Append("<div class='container'>");
            sb.Append("<div class='header'>MyPortal Access Details</div>");
            sb.Append("<div class='content'>");
            sb.Append("<p>Dear " + name + ",</p>");
            sb.Append("<p>Welcome to <strong>MyPortal</strong>! Your account has been created successfully. Below are your login credentials:</p>");

            sb.Append("<div class='credentials'>");
            sb.Append("<p><strong>Username:</strong> " + username + "</p>");
            sb.Append("<p><strong>Password:</strong> " + password + "</p>");
            sb.Append("</div>");

            sb.Append("<p>For security reasons, we recommend changing your password after your first login.</p>");
            sb.Append("<p>If you did not request this account, please contact our support team immediately.</p>");
            sb.Append("<p>Warm regards,<br/>The Inventory Management Team</p>");
            sb.Append("</div>");
            sb.Append("<div class='footer'>© " + DateTime.Now.Year + " Inventory Management. All rights reserved.</div>");
            sb.Append("</div>");
            sb.Append("</body>");
            sb.Append("</html>");

            await _emailService.SendEmail(emailId, "Inventory Mgnt Login Credentials", sb.ToString());
            return true;
        }
        return false;
    } 
}
