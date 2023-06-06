using System.Security.Cryptography;
using MakeBitByte.IDP.DbContexts;
using MakeBitByte.IDP.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MakeBitByte.IDP.Services
{
    public class LocalUserService : ILocalUserService
    {
        private readonly UserDbContext _context;
        private readonly IPasswordHasher<Entities.User> _passwordHasher;

        public LocalUserService(
            UserDbContext context, IPasswordHasher<Entities.User> passwordHasher)
        {
            _context = context ??
                       throw new ArgumentNullException(nameof(context));
            _passwordHasher = passwordHasher ?? 
                              throw new ArgumentNullException(nameof(passwordHasher));
        }

        public async Task<bool> IsUserActive(string subject)
        {
            if (string.IsNullOrWhiteSpace(subject))
            {
                return false;
            }

            var user = await GetUserBySubjectAsync(subject);

            if (user == null)
            {
                return false;
            }

            return user.Active;
        }

        public async Task<bool> CheckActivationCodeAsync(string activationCode)
        {
            var user = await  _context.Users.SingleOrDefaultAsync(r => 
                r.SecurityCode == activationCode && 
                DateTime.UtcNow <= r.SecurityCodeExpiration);

            if (user == null)
            {
                return false;
            }

            user.Active = true;
            user.SecurityCode = null;
            return true;
        }

        public async Task<bool> ValidateCredentialsAsync(string userName,
          string password)
        {
            if (string.IsNullOrWhiteSpace(userName) ||
                string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            var user = await GetUserByUserNameAsync(userName);

            if (user == null)
            {
                return false;
            }

            if (!user.Active)
            {
                return false;
            }

            // Verify password  
            // return (user.Password == password);
            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, password);

            return (result == PasswordVerificationResult.Success);
        } 

        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentNullException(nameof(userName));
            }

            return await _context.Users
                 .FirstOrDefaultAsync(u => u.UserName == userName);
        }

        public async Task<IEnumerable<UserClaim>> GetUserClaimsBySubjectAsync(string subject)
        {
            if (string.IsNullOrWhiteSpace(subject))
            {
                throw new ArgumentNullException(nameof(subject));
            }

            return await _context.UserClaims.Where(u => u.User.Subject == subject).ToListAsync();
        }

        public async Task<User> GetUserBySubjectAsync(string subject)
        {
            if (string.IsNullOrWhiteSpace(subject))
            {
                throw new ArgumentNullException(nameof(subject));
            }

            return await _context.Users.FirstOrDefaultAsync(u => u.Subject == subject);
        }

        public void AddUser(User userToAdd, string password)
        {
            if (userToAdd == null)
            {
                throw new ArgumentNullException(nameof(userToAdd));
            }

            if (_context.Users.Any(u => u.UserName == userToAdd.UserName))
            {
                // in a real-life scenario you'll probably want to 
                // return this as a validation issue
                throw new Exception("Username must be unique");
            }

            if (_context.Users.Any(u => u.Email == userToAdd.Email))
            {
                throw new Exception("Email must be unique");
            }

            userToAdd.SecurityCode = Convert.ToBase64String(RandomNumberGenerator.GetBytes(128));
            userToAdd.SecurityCodeExpiration = DateTime.UtcNow.AddHours(1);
            userToAdd.Active = false;

            userToAdd.Password = _passwordHasher.HashPassword(userToAdd, password);

            _context.Users.Add(userToAdd);
        }

  
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }
    }
}
