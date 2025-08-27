using LibraryManagementSystem.IRepository;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountManagement _account;
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger,IAccountManagement account)
        {
            _logger = logger;
            _account = account;
        }

        [HttpGet]
        public async Task<IActionResult> login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>login(userDetails um)
        {
            //CompanyDetails cdt = new();
             var cdt = await _account.AuthenticateUserAsync(um.Username, um.Password);
            if (cdt.Compid != 0)
            {
                if (cdt.IsActive = false)
                {
                    TempData["msg"] = "You are Disable for login. Contact to Administrator";
                    return View();
                }
                else
                {
                    if (cdt.trialst)
                    {
                        if (cdt.EndDate <= DateTime.Now)
                        {
                            TempData["msg"] = "Your Trial Expire.Payment Now";
                            return View();
                        }
                        else
                        {
                            HttpContext.Session.SetString("uid", cdt.UId.ToString());
                            HttpContext.Session.SetString("uname", cdt.UserName.ToString());
                            HttpContext.Session.SetString("isactive", cdt.IsActive.ToString());
                            HttpContext.Session.SetString("role", cdt.Userrole.ToString());
                            HttpContext.Session.SetString("dbname", cdt.dbname.ToString());
                            HttpContext.Session.SetString("password", cdt.password.ToString());
                            HttpContext.Session.SetString("compid", cdt.Compid.ToString());
                            HttpContext.Session.SetString("startdate", cdt.StartDate.ToString());
                            HttpContext.Session.SetString("enddate", cdt.EndDate.ToString());
                            HttpContext.Session.SetString("trialst", cdt.trialst.ToString());
                            HttpContext.Session.SetString("compname", cdt.Companame.ToString());
                            TempData["compname"] = cdt.Companame;
                            return RedirectToAction("Dashboard", "Master");
                        }
                    }
                    else if(cdt.StartDate<DateTime.Now && cdt.EndDate<=DateTime.Now)
                    {
                        TempData["msg"] = "Your Plan will expire today please do payment for proceed";
                        return View();
                    }
                    else
                    {
                        HttpContext.Session.SetString("uid", cdt.UId.ToString());
                        HttpContext.Session.SetString("uname", cdt.UserName.ToString());
                        HttpContext.Session.SetString("isactive", cdt.IsActive.ToString());
                        HttpContext.Session.SetString("role", cdt.Userrole.ToString());
                        HttpContext.Session.SetString("dbname", cdt.dbname.ToString());
                        HttpContext.Session.SetString("password", cdt.password.ToString());
                        HttpContext.Session.SetString("compid", cdt.Compid.ToString());
                        HttpContext.Session.SetString("startdate", cdt.StartDate.ToString());
                        HttpContext.Session.SetString("enddate", cdt.EndDate.ToString());
                        HttpContext.Session.SetString("trialst", cdt.trialst.ToString());
                        HttpContext.Session.SetString("compname", cdt.Companame.ToString());
                        TempData["compname"] = cdt.Companame;
                        return View("Dashboard");
                    }
                }
            }
            else
            {
                TempData["msg"] = "Invalid User";
                return View();
            }
        }
        [HttpGet]
        public async Task<IActionResult> logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("login","Account");
        }
        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            if (HttpContext.Session == null)
            {
                RedirectToAction("login", "Account");
            }
            return View();
        }
    }
}
