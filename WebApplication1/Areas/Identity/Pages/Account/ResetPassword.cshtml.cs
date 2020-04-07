using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace WebApplication1.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ResetPasswordModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        public ResetPasswordModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            //WE DON'T NEED THE USER TO PROVIDE HIS EMAIL ADDRESS TO BE ABLE TO RESET HIS PASSWORD
            //[Required]
            //[EmailAddress]
            //public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            public string Code { get; set; }
        }

        public IActionResult OnGet(string code = null)
        {
            if (code == null)
            {
                return BadRequest("A code must be supplied for password reset.");
            }
            else
            {
                Input = new InputModel
                {
                    Code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code))
                };
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            //WE AVOIDED USING EMAIL OF USER TO RESET PASSWORD
            //var user = await _userManager.FindByEmailAsync(Input.Email);
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToPage("./ResetPasswordConfirmation");
            }

            var result = await _userManager.ResetPasswordAsync(user, Input.Code, Input.Password);
            if (result.Succeeded)
            {
                //we know that new users are forced to change their password, so this user may be a new user, lets handle this
                //lets check if he has a 'PreVoter' role, if so, we should grant him 'Voter' role, else, continue the standard process
                //var userRoles = await _userManager.GetRolesAsync(user);
                //int i = userRoles.Count;
                if(User.IsInRole("PreVoter"))
                {//so this is a new user who's just reset his password, lets grant him the role 'Voter' and remove the role 'PreVoter'
                    var result1 = await _userManager.AddToRoleAsync(user, "Voter");
                    if(result1.Succeeded)
                    {
                        var result2 = await _userManager.RemoveFromRoleAsync(user, "PreVoter");
                        if (result2.Succeeded)
                        {
                            return RedirectToPage("./ResetPasswordConfirmation");
                        }
                    }                    
                }
                else
                {//so this is a normal user who just reset his password, lets continue the standard process
                    return RedirectToPage("./ResetPasswordConfirmation");
                }                
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return Page();
        }
    }
}
