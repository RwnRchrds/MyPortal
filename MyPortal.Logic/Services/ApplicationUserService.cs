using System;
using System.IO;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Identity;
using MyPortal.Logic.Dictionaries;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Admin;
using MyPortal.Logic.Models.Business;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class ApplicationUserService : BaseService, IApplicationUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAcademicYearRepository _academicYearRepository;

        public ApplicationUserService(UserManager<ApplicationUser> userManager, IAcademicYearRepository academicYearRepository)
        {
            _userManager = userManager;
            _academicYearRepository = academicYearRepository;
        }

        public async Task CreateUser(CreateUser creator)
        {
            if (creator.UserType != UserTypeDictionary.Staff && creator.UserType != UserTypeDictionary.Student &&
                creator.UserType != UserTypeDictionary.Parent)
            {
                throw new ArgumentException("User type is not valid.");
            }
            
            var user = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                UserName = creator.Username,
                Email = creator.Email,
                PhoneNumber = creator.PhoneNumber,
                UserType = creator.UserType,
                Enabled = true
            };
            
            await _userManager.CreateAsync(user, creator.Password);
        }

        public async Task ResetPassword(PasswordReset model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId.ToString());

            await _userManager.RemovePasswordAsync(user);

            await _userManager.AddPasswordAsync(user, model.NewPassword);
        }

        public async Task<bool> EnableDisableUser(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            user.Enabled = !user.Enabled;

            await _userManager.UpdateAsync(user);

            return user.Enabled;
        }

        public async Task<Guid?> GetSelectedAcademicYearId(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            return user.SelectedAcademicYearId;
        }

        public async Task<AcademicYearModel> GetSelectedAcademicYear(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            var selected = user.SelectedAcademicYearId;

            if (selected != null)
            {
                var acadYear = await _academicYearRepository.GetById((Guid) selected);

                return _businessMapper.Map<AcademicYearModel>(acadYear);
            }

            return null;
        }

        public async Task<UserModel> GetUserByPrincipal(ClaimsPrincipal principal)
        {
            var user = await _userManager.GetUserAsync(principal);

            return _businessMapper.Map<UserModel>(user);
        }

        private byte[] EncryptToken(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }

        private string DecryptToken(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }
    }
}