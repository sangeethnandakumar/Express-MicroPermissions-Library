using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpressPermissions;
using Microsoft.AspNetCore.Mvc;

namespace WebDemo.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
    public class Management : Controller
    {

        [HttpGet]
        [Route("GetUsers")]
        public IActionResult GetUsers()
        {
            var users = Permissions.getInstance.GetAllUsers();
            return Json(users);
        }

        [HttpGet]
        [Route("GetUserPermissions")]
        public IActionResult GetUserPermissions(string username)
        {
            var users = Permissions.getInstance.EnabledPermissions(username);
            return Json(users);
        }


    }
}
