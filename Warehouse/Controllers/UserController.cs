using Humanizer.Bytes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Logging;
using System.Net;
using Warehouse.Core.Constants;
using Warehouse.Core.Contracts;
using Warehouse.Core.Models;
using Warehouse.Core.Services;
using Warehouse.Infrastructure.Data;
using Warehouse.Infrastructure.Data.Identity;

namespace Warehouse.Controllers
{
    public class UserController : BaseController
    {   // we have to be logged because we inherit the baseController
        private readonly RoleManager<IdentityRole> roleManager;

        private readonly UserManager<ApplicationUser> userManager;

        private readonly IUserService service;
        private readonly IFileService fileService;


        public UserController(
            RoleManager<IdentityRole> _roleManager,
            UserManager<ApplicationUser> _userManager,
            IUserService _service,
            IFileService _fileService
            )
        {
            roleManager = _roleManager;
            userManager = _userManager;
            service = _service;
            fileService = _fileService;
        }

        public IActionResult Index()
        {

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UserProfile(string id)
        {
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UserProfile(IFormFile file)
        {
            try
            {
                var user = await userManager.GetUserAsync(HttpContext.User);


                if (file != null && file.Length > 0 && ModelState.IsValid)
                {
                    using (var stream = new MemoryStream())
                    {
                        await file.CopyToAsync(stream);

                        //var fileToSave = new ApplicationFile()
                        //{
                        //    FileName = file.FileName,
                        //    Content = stream.ToArray()
                        //};


                        user.ProfilePicture = stream.ToArray();
                        var model = new UserEditViewModel()
                        {
                            Id = user.Id,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            ProfilePicture = user.ProfilePicture
                        };

                        await service.UpdateUser(model);

                    }


                }
            }
            catch (Exception)
            {
                
                TempData[MessageConstant.ErrorMessage] = "There is a problem with uploading your profile picture.";
            }
            // TO DO: Write to catch exeptions !!! if the photo is under 1024 bytes it will throw exeption
            
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UserProfileInfoEdit(string id)
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UserProfileInfoEdit()
        {

            return View();
        }


        //if (ModelState.IsValid)
        //{
        //    var newUser = new ApplicationUser()
        //    {
        //        FirstName = registerVM.FirstName,
        //        LastName = registerVM.LastName,
        //        //ProfilePicture = (IFormFile)registerVM.ProfilePicture,
        //    };

        //    if (registerVM.ProfilePicture.Length > 0)
        //    {
        //        using (var memoryStream = new MemoryStream())
        //        {
        //            await registerVM.ProfilePicture.CopyToAsync(memoryStream);

        //            // Upload the file if less than 2 MB  
        //            if (memoryStream.Length < 2097152)
        //            {
        //                newUser.ProfilePicture = memoryStream.ToArray();
        //            }
        //            else
        //            {
        //                ModelState.AddModelError("File", "The file is too large.");
        //            }
        //        }
        //    }

        //    var result = await userManager.CreateAsync(newUser);
        //    if (result.Succeeded)
        //    {
        //        //...  

        //        return RedirectToAction("Index", "Home");
        //    }
        //}


    }
}

//they are moved into admin area

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
