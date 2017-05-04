using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.Repository;

namespace FinalProject.Business
{
    public interface IUserManager
    {
        UserModel LogIn(string email, string password);
        UserModel Register(string email, string password);
    }

    public class UserModel
    {
        public int UserId { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public bool UserAdmin { get; set; }
    }

    public class UserManager : IUserManager
    {
        private readonly IUserRepository userRepository;

        public UserManager(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public UserModel LogIn(string email, string password)
        {
            var user = userRepository.LogIn(email, password);

            if (user == null)
            {
                return null;
            }

            return new UserModel { UserId = user.UserId, UserEmail = user.UserEmail, UserPassword = user.UserPassword };
        }

        public UserModel Register(string email, string password)
        {
            var user = userRepository.Register(email, password);

            if (user == null)
            {
                return null;
            }

            return new UserModel { UserId = user.UserId, UserEmail = user.UserEmail, UserPassword = user.UserPassword };
        }
    }
}
