
using Warehouse.Core.Contracts;
using Warehouse.Core.Models;
using Warehouse.Infrastructure.Data.Identity;
using Warehouse.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Web.Mvc;
using Microsoft.AspNetCore.Identity;
using Warehouse.Infrastructure.Data;

namespace Warehouse.Core.Services
{
    public class UserService:IUserService
    {
        private readonly IApplicatioDbRepository repo;


        public UserService(IApplicatioDbRepository _repo)
        {
            this.repo = _repo;
        }

        public async Task<ApplicationUser> GetUserById(string id)
        {
           return await repo.GetByIdAsync<ApplicationUser>(id);

        }

        public async Task<UserEditViewModel> GetUserForEdit(string id)
        {
            var user = await repo.GetByIdAsync<ApplicationUser>(id);

            return new UserEditViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                ProfilePicture = user.ProfilePicture,
            };
                
        }


        public async Task<IEnumerable<UserListViewModel>> GetUsers()
        {
            return await repo.All<ApplicationUser>()
                .Select(u=> new UserListViewModel()
                {
                    Email = u.Email,
                    Id = u.Id,
                    Name = $"{u.FirstName} {u.LastName}"
                })
                .ToListAsync();

                

        }

        public async Task<bool> UpdateUser(UserEditViewModel model)
        {
            bool result = false;
            var user = await repo.GetByIdAsync<ApplicationUser>(model.Id);

            if(user != null)
            {
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.ProfilePicture = model.ProfilePicture;

                await repo.SaveChangesAsync();
                result = true;

            }

            return result;
        }

        

        //public async Task<bool> UpdateUserProfilePicture(UserEditViewModel model)
        //{
        //    bool result = false;
        //    var user = await repo.GetByIdAsync<ApplicationUser>(model.Id);

        //    if (user != null)
        //    {
        //        user.ProfilePicture = model.ProfilePicture;

        //        await repo.SaveChangesAsync();
        //        result = true;
        //    }

        //    return result;
        //}
    }
}
