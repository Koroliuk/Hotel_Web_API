using Hotel.DAL.Entities;

namespace Hotel.BLL.interfaces
{
    public interface IUserService
    {
        void SignUp(User user);
        User FindByLogin(string login);
        bool IsExistByLogin(string login);
    }
}
