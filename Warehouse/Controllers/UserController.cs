using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Warehouse.Core.Constants;
using Warehouse.Core.Contracts;
using Warehouse.Infrastructure.Data.Identity;

namespace Warehouse.Controllers
{
    public class UserController : BaseController
    {   // we have to be logged because we inherit the baseController
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

        //[Authorize(Roles = UserConstants.Roles.Administrator)]
        //public async Task<IActionResult> ManageUsers()
        //{
        //    var users = await service.GetUsers();
        //    return Ok(users);
        //}

        //public async Task<IActionResult> CreateRole()
        //{
        //    //comment for now if i want to create new role
        //    //await roleManager.CreateAsync(new IdentityRole()
        //    //{
        //    //    Name = "Administrator"
        //    //});
        //    return Ok();
        
        //}
    }
}
