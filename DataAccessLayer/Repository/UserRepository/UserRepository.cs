
using DataAccessLayer.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly MovieDBContext _dbContext;
        public UserRepository(MovieDBContext dBContext) 
        {
            _dbContext = dBContext;        
        }

        public async Task<user_info> getUserbyUsername(string userid)
        {
            return await _dbContext.user_info.FirstOrDefaultAsync(u => u.user_id == userid);
        }
        public async Task AddUser(user_info user)
        {
            await _dbContext.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<user_info> LoginUser(string username, string password)
        {
            var user = await getUserbyUsername(username);
            if (user == null || !VerifyPassword(password,user.user_psw))
            {
                return null;
            }
            return user;
            
        }
        private bool VerifyPassword(string password, string storedPasswordHash)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedInputPassword = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(password)));
                return hashedInputPassword == storedPasswordHash;
            }
        }

        public async Task UpdateUser(user_info user)
        {
             _dbContext.user_info.Update(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteUser(string userid)
        {
            var user = await _dbContext.user_info.FindAsync(userid);
            if(user != null)
            {
                _dbContext.user_info.Remove(user);
                await _dbContext.SaveChangesAsync();
            }
           
        }
    }
}
