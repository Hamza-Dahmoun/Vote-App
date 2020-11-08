using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.BusinessService
{
    public class UserBusinessService
    {
        //this is used to get the currentUser
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;

        public UserBusinessService(UserManager<IdentityUser> userManager, IHttpContextAccessor contextAccessor)
        {
            _userManager = userManager;
            _contextAccessor = contextAccessor;
        }

        public async Task<IdentityResult> AddNewUser(string userName)
        {
            //This is user is going to have a 'PreVoter' Role
            var user = new IdentityUser { UserName = userName };

            //CreateAsync() is an asynchronous method, we have to mark this method with 'async task'
            //_logger.LogInformation("Storing the new IdentityUser instance in IdentityDB");
            var result = await _userManager.CreateAsync(user, "Pa$$w0rd");//this password will be automatically hashed
            if (result.Succeeded)
            {
                //so the user has been added successfully, lets assign him a PreVoter role
                var result1 = await _userManager.AddToRoleAsync(user, "PreVoter");
                return result1;
            }
            return result;
        }
    }
}
