using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Reversi.Controllers;

namespace Reversi.Pages
{
    public class LoginModel : PageModel
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public string loginmsg = "";
        public void OnGet()
        {
            ViewData["ReCaptchaKey"] = "6LdY4pMUAAAAABBM6NEpvhbBqW2gH6L_QUdoiIFa";
            if (HttpContext.Session.GetString("login") != null)
            {
                ViewData["login"] = "true";
            }
        }

        public void OnPost(string Username, string Password, string verify)
        {
            if (!ReCaptchaPassed(Request.Form["g-recaptcha-response"], "6LdY4pMUAAAAAHnVKHn4WS-qsDHqrrQABJxCQ6vk"))
            {
                ModelState.AddModelError(string.Empty, "You failed the CAPTCHA");
            }
            else
            {
                LoginController Logincontroller = new LoginController();
                bool login = Logincontroller.HandleLogin(Username, Password);
                if (login == true)
                {
                    if (Logincontroller.loginModel.Role.ToString() == "0")
                    {
                        ViewData["Error"] = "Uw account is gearchiveerd, neem contact op met een administrator";
                    }
                    else
                    {
                        if(verify == "Yes")
                        {
                            Random generator = new Random();
                            string randomnumber = generator.Next(0, 999999).ToString("D6");
                            HttpContext.Session.SetString("verify", randomnumber);
                            Logincontroller.SendVerifyMail(Logincontroller.loginModel.Email, randomnumber);
                            HttpContext.Session.SetString("loginnotverifyd", Logincontroller.loginModel.Username);
                            HttpContext.Session.SetString("rolenotverifyd", Logincontroller.loginModel.Role.ToString());
                            Response.Redirect("VerifyLogin");
                        }
                        else
                        {
                            HttpContext.Session.SetString("login", Logincontroller.loginModel.Username);
                            HttpContext.Session.SetString("role", Logincontroller.loginModel.Role.ToString());
                            Response.Redirect("Index");
                        }
                        
                    }
                }
                else
                {
                    loginmsg = "invalid username or password";
                    ViewData["Error"] = loginmsg;
                }

            }
        }

            public void OnPostLogout()
            {
                HttpContext.Session.Clear();
                Response.Redirect("Login");
            }

            public static bool ReCaptchaPassed(string gRecaptchaResponse, string secret)
            {
                HttpClient httpClient = new HttpClient();
                var res = httpClient.GetAsync($"https://www.google.com/recaptcha/api/siteverify?secret={secret}&response={gRecaptchaResponse}").Result;
                if (res.StatusCode != HttpStatusCode.OK)
                {
                    return false;
                }

                string JSONres = res.Content.ReadAsStringAsync().Result;
                dynamic JSONdata = JObject.Parse(JSONres);
                if (JSONdata.success != "true")
                {
                    return false;
                }

                return true;
            }
        }
    }