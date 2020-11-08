using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Business;

namespace WebApplication1.BusinessService
{
    public class UserBusinessService
    {
        //this is used to get the currentUser
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IStringLocalizer<Messages> _messagesLocalizer;

        public UserBusinessService(UserManager<IdentityUser> userManager, IHttpContextAccessor contextAccessor,
            IStringLocalizer<Messages> messagesLocalizer)
        {
            _userManager = userManager;
            _contextAccessor = contextAccessor;
            _messagesLocalizer = messagesLocalizer;
        }

        public async Task<Guid> AddNewUser(string userName)
        {
            //This function adds a new user with a 'PreVoter' Role
            try
            {                
                var user = new IdentityUser { UserName = userName };

                //CreateAsync() is an asynchronous method, we have to mark this method with 'async task'
                var result = await _userManager.CreateAsync(user, "Pa$$w0rd");//this password will be automatically hashed
                if (result.Succeeded)
                {
                    //so the user has been added successfully, lets assign him a PreVoter role
                    var result1 = await _userManager.AddToRoleAsync(user, "PreVoter");
                    if (result1.Succeeded)
                    {
                        //the user has been stored successully lets return its ID
                        return Guid.Parse(user.Id);
                    }
                    else
                    {
                        //row not updated in the DB
                        throw new DataNotUpdatedException(_messagesLocalizer["Data not updated, operation failed."]);
                    }
                }
                else
                {
                    //row not updated in the DB
                    throw new DataNotUpdatedException(_messagesLocalizer["Data not updated, operation failed."]);
                }
            }
            catch (DataNotUpdatedException E)
            {
                throw E;
            }
            catch (Exception E)
            {
                throw E;
            }            
        }

        public async Task DeleteUser(string userId)
        {
            //this function takes a voter Id and delete his user accound from IdentityDB
            try
            {
                //lets get the User by his ID
                //_logger.LogInformation("Going to get the corresponding IdentityUser of the Voter instance");
                var voterUserAccount = await _userManager.FindByIdAsync(userId);

                //DeleteAsync() is an asynchronous method, we have to mark this method with 'async task'
                //_logger.LogInformation("Going to delete the corresponding IdentityUser of the Voter instance");
                var result = await _userManager.DeleteAsync(voterUserAccount);
                if (result.Succeeded)
                {
                    //_logger.LogInformation("done deleting the corresponding IdentityUser of the Voter instance");                                        
                }
                else
                {
                    //row not updated in the DB
                    throw new DataNotUpdatedException(_messagesLocalizer["Data not updated, operation failed."]);
                }
            }
            catch (DataNotUpdatedException E)
            {
                throw E;
            }
            catch (BusinessException E)
            {
                throw E;
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        public async Task<IdentityUser> GetCurrentUser()
        {
            return await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
        }
        public async Task<bool> IsUserInRole(IdentityUser user, string role)
        {
            return await _userManager.IsInRoleAsync(user, role);
        }
        public async Task<string> GeneratePasswordResetTokenForUser(IdentityUser user)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }
    }
}
