using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Areas.AdminArea.Controllers;
using static Warehouse.Core.Constants.UserConstants;
using Warehouse.Core.Constants;
using Warehouse.Core.Contracts;
using Warehouse.Infrastructure.Data.Identity;
using Warehouse.Core.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Warehouse.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        private readonly RoleManager<IdentityRole> roleManager;

        private readonly UserManager<ApplicationUser> userManager;

        private readonly IUserService service;

        public UserController(
            RoleManager<IdentityRole> _roleManager,
            UserManager<ApplicationUser> _userManager,
            IUserService _service
            )
        {
            roleManager = _roleManager;
            userManager = _userManager;
            service = _service;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = UserConstants.Roles.Administrator)]
        public async Task<IActionResult> ManageUsers()
        {
            var users = await service.GetUsers();
            return View(users);
        }
        public async Task<IActionResult> Roles(string id)
        {
            //var model = await service.GetUserForEdit(id);
            //return View(model);
            return Ok(id);
        }

        
        public async Task<IActionResult> Edit(string id)
        {
            var model = await service.GetUserForEdit(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserEditViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            if(await service.UpdateUser(model))
            {
                ViewData[MessageConstant.SuccessMessage] = "Успешен запис";
            }
            else
            {
                ViewData[MessageConstant.SuccessMessage] = "Възникна грешка";
            }
            return View(model);
        }

        public async Task<IActionResult> CreateRole()
        {
            //comment for now if i want to create new role
            //await roleManager.CreateAsync(new IdentityRole()
            //{
            //    Name = "Administrator"
            //});
            return Ok();

        }
    }
}
