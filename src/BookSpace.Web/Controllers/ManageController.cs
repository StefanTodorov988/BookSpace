using System;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BookSpace.Web.Models.ManageViewModels;
using BookSpace.Web.Services;
using BookSpace.Models;
using BookSpace.Web.Extensions;
using BookSpace.BlobStorage.Contracts;
using Microsoft.AspNetCore.Http;
using System.IO;
using BookSpace.CognitiveServices.Contract;
using BookSpace.Web.Services.SmtpService.Contract;

namespace BookSpace.Web.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class ManageController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IFaceService faceService;
        private readonly ISmtpSender smtpSender;
        private readonly IBlobStorageService blobStorageService;
        private const string blobContainer = "bookspace";

        private const string AuthenticatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";
        private const string RecoveryCodesKey = nameof(RecoveryCodesKey);

        public ManageController(
          UserManager<ApplicationUser> userManager,
          SignInManager<ApplicationUser> signInManager,
          IEmailSender emailSender,
          UrlEncoder urlEncoder,
          IBlobStorageService blobService,
          IFaceService faceService,
          ISmtpSender smtpSender)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this.blobStorageService = blobService;
            this.faceService = faceService;
            this.smtpSender = smtpSender;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var model = new IndexViewModel
            {
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                IsEmailConfirmed = user.EmailConfirmed,
                StatusMessage = StatusMessage
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(IndexViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var email = user.Email;
            if (model.Email != email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, model.Email);
                if (!setEmailResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting email for user with ID '{user.Id}'.");
                }
            }

            var phoneNumber = user.PhoneNumber;
            if (model.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, model.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting phone number for user with ID '{user.Id}'.");
                }
            }

            StatusMessage = "Your profile has been updated";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost("UploadPicture")]
        public async Task<IActionResult> UploadPicture(IFormFile file)
        {
            string currentUserEmail;
            string userId;
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                var user = await this._userManager.GetUserAsync(HttpContext.User);
                currentUserEmail = user.Email;
                userId = user.Id;
                await this.blobStorageService.UploadAsync(user.Id, blobContainer, stream.ToArray());

                //sets profile pictureurl
                var pictureUri = await this.blobStorageService.GetAsync(userId, blobContainer);
                user.ProfilePictureUrl = pictureUri.Url.ToString();

            }

            var faceAtributes = faceService.DetectFaceAtribytesAsync(blobStorageService.GetAsync(userId, blobContainer).Result.Url);

            try
            {
                smtpSender.SendMail(currentUserEmail, GenerateMessageByEmotion(
                        faceAtributes.Result[0].Emotion.Happiness,
                        faceAtributes.Result[0].Emotion.Sadness,
                        faceAtributes.Result[0].Emotion.Anger));
            }
            catch (IndexOutOfRangeException e)
            {
                smtpSender.SendMail(currentUserEmail, "No face on picture");
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var model = new ChangePasswordViewModel { StatusMessage = StatusMessage };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                AddErrors(changePasswordResult);
                return View(model);
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
            StatusMessage = "Your password has been changed.";

            return RedirectToAction(nameof(ChangePassword));
        }


        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private string GenerateMessageByEmotion(float happiness, float sadness, float anger)
        {
            if (happiness > 0.5f && sadness < 0.1f)
            {
                return "Happy email Message.";
            }
            else if (happiness < 0.2 && sadness > 0.4)
            {
                return "Sad email message.";
            }
            else if (anger > 0.34)
            {
                return "Angry email message.";
            }
            return "Default email message";
        }
        #endregion
    }
}
