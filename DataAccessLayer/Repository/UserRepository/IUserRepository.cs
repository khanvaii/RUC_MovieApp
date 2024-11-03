using DataAccessLayer.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository.UserRepository
{
    public interface IUserRepository
    {
        Task<user_info> getUserbyUsername(string userid);

        Task AddUser(user_info user);
        Task<user_info> LoginUser(string username, string password);
        Task UpdateUser(user_info user);
        Task DeleteUser(string Userid);
    }
}
