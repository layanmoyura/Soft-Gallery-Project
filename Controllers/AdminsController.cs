using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.helper;
using PresentationLayer.Models;
using System.Threading.Tasks;
using System;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ContosoUniversity.Controllers
{
    public class AdminsController : Controller
    {

        private readonly IAdminServices _adminServices;

        public AdminsController(IAdminServices adminServices)
        {
            _adminServices = adminServices;
        }
        public ViewResult SignUp()
        {
            return View();
        }

        [HttpPost]

        public async Task<JsonResult> SignUp([Bind("UserID,FirstName,LastName,EmailAddress,Password")] AdminModel adminModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var admin = MappingFunctions.ToAdmin(adminModel);
                    await _adminServices.AddAdminAsync(admin);
                    return Json(new { success = true });


                }
                catch (Exception ex)
                {
                    return Json(new { success = false, error = ex.Message });
                }

            }


            return Json(new { success = false, error = "Model validation failed" });

        }



        public ViewResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Login([Bind("EmailAddress,Password")] AdminLoginModel adminLoginModel)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var admin = await _adminServices.GetAdminByEmailAsync(adminLoginModel.EmailAddress);
                    if (admin != null && BCrypt.Net.BCrypt.Verify(adminLoginModel.Password, admin.Password))
                    {
                        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                        var tokenOptions = new JwtSecurityToken(
                            issuer: "http://localhost:5000",
                            audience: "http://localhost:5000",
                            claims: new List<Claim>() {
                        new Claim("role","admin"),new Claim("name",admin.FirstName)},
                            expires: DateTime.Now.AddMinutes(5),
                            signingCredentials: signinCredentials
                        );
                        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                        return Json(new { success = true,token = tokenString });
                    }
                    else
                    {
                        return Json(new { success = false, error = "login failed" });
                    }
                }

                catch(Exception ex)
                {
                    return Json(new { success = false, error = ex.Message });
                }
                
            }

            return Json(new { success = false, error = "Model validation failed" });

        }
    }
}
