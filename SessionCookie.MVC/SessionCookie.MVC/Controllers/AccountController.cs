using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using SessionCookie.MVC.Models;
using System.Reflection.Metadata.Ecma335;

namespace SessionCookie.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _httpClient;
        public AccountController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginUser(UserLoginRequest logInRequest)
        {
            var response = await _httpClient.PostAsJsonAsync("http://localhost:5145/api/User/login", logInRequest);
            if (response.IsSuccessStatusCode)
            {
                ViewBag.Message = "Login Successful!";
                //return RedirectToAction("Login");
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Message = "Login Failed. Try a different Username";
            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(UserRegistrationRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("http://localhost:5145/api/User/register", request);
            if(response.IsSuccessStatusCode)
            {
                ViewBag.Message = "Registration Successful!";

                //return RedirectToAction("Register");
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Message = "Registration Failed. Try a different Username";
            /*return RedirectToAction("Register");*/
            return RedirectToAction("Index", "Home");
        }
    }
}
