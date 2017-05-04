using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.Database;

namespace FinalProject.Repository
{
    public interface IUserRepository
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

    public class UserRepository : IUserRepository
    {
        public UserModel LogIn(string email, string password)
        {
            /*判断输入的email和password是否跟DatabaseAccessor里面的有符合的，如果有把第一个付给user*/
            var user = DatabaseAccessor.Instance.Users
                .FirstOrDefault(t => t.UserEmail.ToLower() == email.ToLower()
                                      && t.UserPassword == password);

            if (user == null)
            {
                return null;
            }
            /*新建一个UserModel,把User类里面的东西一一赋给UserModel*/
            return new UserModel { UserId = user.UserId, UserEmail = user.UserEmail, UserPassword=user.UserPassword };
        }

        public UserModel Register(string email, string password)
        {
            /*往DatabaseAccessor里面写入一行User类型的数据，把值赋给user*/
            var user = DatabaseAccessor.Instance.Users
                    .Add(new FinalProject.Database.User { UserEmail = email, UserPassword = password });
            /*保存*/
            DatabaseAccessor.Instance.SaveChanges();

            return new UserModel { UserId = user.UserId, UserEmail = user.UserEmail, UserPassword = user.UserPassword };
        }
    }
}
