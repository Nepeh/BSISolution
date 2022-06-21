using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;
using BSISolution.Models;
using BSISolution.Data;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace BSISolutions.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _db;

        public AccountController(ApplicationDbContext context, SignInManager<ApplicationUser> signInManager,
            ILogger<AccountController> logger, IEmailSender emailSender,
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _db = context;
        }
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public ActionResult RoleList(string userId)
        {
            ViewBag.UserID = userId;
            var result = _db.SystemRole.ToList();
            return View(result);
        }
        public ActionResult AddUserRole(int[] chkCheckAll, string userID)
        {

            if (chkCheckAll != null)
            {                   
                foreach (var item in chkCheckAll)
                {
                    int roleId = Convert.ToInt32(item);

                    UserSystemRoles userSystemRoles = new UserSystemRoles
                    {
                        UserId = userID,
                        RoleID = roleId,
                        RecordedBy = User.Identity.Name,
                        DateRecorded = DateTime.Now,
                    };
                     _db.UserSystemRoles.Add(userSystemRoles);
                    _db.SaveChanges();
                }
            }
            //message = string.Format("Thanks! You have successfully added player(s) on {0}", club.Name);
            return RedirectToAction("UserList");

        }
        public ActionResult UserList()
        {
            var result = _db.AspNetUser.ToList();
            List<ApplicationUser> _users = result;
            return View(_users.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserList(string FirstName, string MiddleName, string LastName, string Password, string Email, string Phone, string returnUrl = null)
        {
            string userId = string.Empty;
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = Email, Email = Email, FirstName = FirstName, MiddleName = MiddleName, LastName = LastName };
                var result = await _userManager.CreateAsync(user, Password);
                if (result.Succeeded)
                {
                    userId = user.Id;
                    user.RecordedBy = User.Identity.Name;
                    user.DateRecorded = DateTime.Now;
                    user.IsActive = true;
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Email, returnUrl = returnUrl });
                    }
                    //else
                    //{
                    //    await _signInManager.SignInAsync(user, isPersistent: false);
                    //    return LocalRedirect(returnUrl);
                    //}
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return RedirectToAction("RoleList", new { userId = userId});
        }

        public async Task<IActionResult> Edit(string Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var aspNetUser = await _db.AspNetUser.FindAsync(Id);
            if (aspNetUser == null)
            {
                return NotFound();
            }
            return PartialView("_EditUserPartial", aspNetUser);
        }

        // POST: AspNetUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,FirstName,MiddleName,LastName,PhoneNumber,Email")] ApplicationUser applicationUser)
        {
            if (id != applicationUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var aspNetUser = _db.AspNetUser.Find(id);
                    aspNetUser.FirstName = applicationUser.FirstName;
                    aspNetUser.MiddleName = applicationUser.MiddleName;
                    aspNetUser.LastName = applicationUser.LastName;
                    aspNetUser.PhoneNumber = applicationUser.PhoneNumber;
                    aspNetUser.Email = applicationUser.Email;
                    _db.Entry(aspNetUser).State = EntityState.Modified;
                    //_db.AspNetUser.Update(applicationUser);
                    await _db.SaveChangesAsync();
                    return RedirectToAction("UserList");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!AspNetUserExists(applicationUser.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw ex;
                    }
                }
            }
            return PartialView("_EditUserPartial", applicationUser);
        }
        public ActionResult Login(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            if (returnUrl != null)
            {
                ViewBag.ReturnUrl = returnUrl;
                if (User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            ReturnUrl = returnUrl;
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> DisableUser(string Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            
            var aspNetUser = await _db.AspNetUser.FindAsync(Id);
            if (aspNetUser == null)
            {
                return NotFound();
            }
            aspNetUser.IsActive = false;
            _db.Entry(aspNetUser).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return RedirectToAction("UserList");
        }

        private bool AspNetUserExists(string id)
        {
            return _db.AspNetUser.Any(e => e.Id == id);
        }
    }
}
