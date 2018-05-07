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
using System.Threading;
using BookSpace.CognitiveServices.Contract;
using BookSpace.Web.Services.SmtpService.Contract;
using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;

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

            var user = await this._userManager.GetUserAsync(HttpContext.User);
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);


                await this.blobStorageService.UploadAsync(user.Id, blobContainer, stream.ToArray());

                //sets profile pictureurl
                var pictureUri = await this.blobStorageService.GetAsync(userId, blobContainer);
                user.ProfilePictureUrl = pictureUri.ToString();

            }

            var faceAtributes = faceService.DetectFaceAtribytesAsync(blobStorageService.GetAsync(user.Id, blobContainer).Result.Url);


            try
            {
                smtpSender.SendMail(user.Email, GenerateMessageByEmotion(faceAtributes.Result[0], user.UserName));
            }
            catch (IndexOutOfRangeException e)
            {
               // In case of image without face
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

        private string GenerateMessageByEmotion(FaceAttributes faceAttributes, string userName)
        {
            string emotion;

            if (faceAttributes.Emotion.Happiness > 0.5f && faceAttributes.Emotion.Sadness < 0.2f)
            {
                emotion = "happy";
            }
            else if (faceAttributes.Emotion.Happiness < 0.2f && faceAttributes.Emotion.Sadness > 0.4f)
            {
                emotion = "sad";
            }
            else if (faceAttributes.Emotion.Anger > 0.34f)
            {
                emotion = "angry";
            }
            else if (faceAttributes.Emotion.Surprise > 0.40f)
            {
                emotion = "suprised";
            }
            else
            {
                emotion = "not detected";
            }

            if (emotion != "not detected")
            {
                return $"Greetings {userName}, \n" + Environment.NewLine + Environment.NewLine +
                        "We see that you have uploaded a profile picture!\n" +
                       $"Our face recognition service recognized you as being {emotion}. \n" + Environment.NewLine + Environment.NewLine +
                        "Best regards, \n" +
                        "Bookster Team";
            }
            else
            {
                return $"Greetings {userName}, \n" + Environment.NewLine + Environment.NewLine +
                       "We see that you have uploaded a profile picture!\n" +
                       $"Unfortunately our face recognition service didn't recognize your emotions \n" + Environment.NewLine + Environment.NewLine +
                       "Best regards, \n" +
                       "Bookster Team";
            }
        }
        #endregion
    }
}
