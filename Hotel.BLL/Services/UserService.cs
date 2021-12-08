using Hotel.BLL.interfaces;
using Hotel.BLL.Validation;
using Hotel.DAL.Entities;
using Hotel.DAL.Interfaces;

namespace Hotel.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SignUp(User user)
        {
            if (user.Login == null || user.Login.Equals(string.Empty) ||
                user.PasswordHash == null || user.PasswordHash.Equals(string.Empty) ||
                user.Role.Equals(string.Empty))
            {
                throw new HotelException("Invalid input");
            }
            if (IsExistByLogin(user.Login))
            {
                throw new HotelException("User with a such login is already exists");
            }

            _unitOfWork.Users.Create(user);
            _unitOfWork.Save();
        }

        public User FindByLogin(string login)
        {
            return _unitOfWork.Users.FindByLogin(login);
        }

        public bool IsExistByLogin(string login)
        {
            return FindByLogin(login) != null;
        }
    }
}
