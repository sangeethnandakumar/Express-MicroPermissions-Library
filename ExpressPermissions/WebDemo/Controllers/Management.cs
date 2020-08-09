using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpressPermissions;
using Microsoft.AspNetCore.Mvc;
using WebDemo.Models;

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

        [HttpGet]
        [Route("GetDisabledPermissions")]
        public IActionResult GetDisabledPermissions(string username)
        {
            var users = Permissions.getInstance.DisabledPermissions(username);
            return Json(users);
        }

        
        [HttpGet]
        [Route("GetPermissionGroups")]
        public IActionResult GetPermissionGroups(string username)
        {
            var users = Permissions.getInstance.GetDisabledPermisionGroups(username);
            return Json(users);
        }



        [HttpPost]
        [Route("BindNewPermissions")]
        public IActionResult BindNewPermissions([FromBody] PermissionModel data)
        {
            foreach(var permission in data.Permissions)
            {
                Permissions.getInstance.BindPermission(data.Username, permission);
            }            
            return Json(true);
        }

        [HttpPost]
        [Route("BindNewPermissionGroup")]
        public IActionResult BindNewPermissionGroup([FromBody] PermissionModel data)
        {
            foreach(var permission in data.Permissions)
            {
                Permissions.getInstance.BindPermissionGroup(data.Username, permission);
            }            
            return Json(true);
        }



        [HttpGet]
        [Route("UnbindPermission")]
        public IActionResult UnbindPermission(string username, string permission)
        {
            Permissions.getInstance.UnBindPermission(username, permission);
            return Json(true);
        }

    }
}
