using LibraryManagementSystem.IRepository;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    public class MasterController : Controller
    {
        private readonly IMaster _master;
        private readonly ILogger<MasterController> _logger;

        public MasterController(ILogger<MasterController> logger,IMaster master)
        {
            _logger = logger;
            _master = master;
        }

        //[Route("Master/HourMaster")]
        [HttpGet]
        public IActionResult HourMaster()
        {
            if (HttpContext.Session == null)
            {
                RedirectToAction("login", "Account");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddHour([FromBody] Hour obj)
        {
            if (HttpContext.Session == null)
            {
                RedirectToAction("login", "Account");
            }
            var res = await _master.saveHour(obj);
            return Ok(new { message = res });
        }
        [HttpGet]
        public async Task<IActionResult> getHourDetails()
        {
            if (HttpContext.Session == null)
            {
                RedirectToAction("login", "Account");
            }
            var res = await _master.GetHourdetails();
            return Ok(new { message = res });
        }
        [HttpPost]
        public async Task<IActionResult> deleteHours(int id)
        {
            var res = await _master.DeleteHour(id);
            return Ok(new { message = res });
        }
        [HttpGet]
        public IActionResult CourseMaster()
        {
            if (HttpContext.Session == null)
            {
                RedirectToAction("login", "Account");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddCourse([FromBody] course obj)
        {
            if (HttpContext.Session == null)
            {
                RedirectToAction("login", "Account");
            }
            var res = await _master.saveCourse(obj);
            return Ok(new { message = res });
        }
        [HttpGet]
        public async Task<IActionResult> getCourseDetails()
        {
            if (HttpContext.Session == null)
            {
                RedirectToAction("login", "Account");
            }
            var res = await _master.getCourseDetails();
            return Ok(new { message = res });
        }
        [HttpPost]
        public async Task<IActionResult> deleteCourse(int id)
        {
            var res = await _master.DeleteCourse(id);
            return Ok(new { message = res });
        }
        [HttpGet]
        public IActionResult MembershipMaster()
        {
            if (HttpContext.Session == null)
            {
                RedirectToAction("login", "Account");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddMembership([FromBody] Membership obj)
        {
            if (HttpContext.Session == null)
            {
                RedirectToAction("login", "Account");
            }
            var res = await _master.saveMembership(obj);
            return Ok(new { message = res });
        }
        [HttpGet]
        public async Task<IActionResult> getMembershipTypes()
        {
            if (HttpContext.Session == null)
            {
                RedirectToAction("login", "Account");
            }
            var res = await _master.getMembershipDetails();
            return Ok(new { message = res });
        }
    }
}
