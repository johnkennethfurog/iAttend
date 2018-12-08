using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IAttend.API.Models;
using Microsoft.EntityFrameworkCore;

namespace IAttend.API.Data
{
    public class AuthenticationMSql : IAuthentication
    {
        private readonly DataContext _dataContext;

        public AuthenticationMSql(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Instructor> Login(string instructorNumber, string password)
        {
            var instructor = await _dataContext.Instructors.FirstOrDefaultAsync(x => x.InstructorNumber == instructorNumber);

            if (instructor == null)
                return null;

            if (!VerifyPasswordHash(password, instructor.PasswordHash, instructor.PasswordSalt))
                return null;

            return instructor;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
               var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for(int i =0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }

            }
            return true;

        }

        public async Task<bool> ResetPassword(string instructorNumber)
        {
            return true;
        }

        public async Task<bool> SetPassword(string instructorNumber, string password)
        {
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            var instructor = await _dataContext.Instructors.FirstOrDefaultAsync(x => x.InstructorNumber == instructorNumber);

            instructor.PasswordHash = passwordHash;
            instructor.PasswordSalt = passwordSalt;

            return await _dataContext.SaveChangesAsync() > 0;
        }

        void CreatePasswordHash(string password,out byte[]passwordHash,out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
