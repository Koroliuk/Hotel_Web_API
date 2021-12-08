using Hotel.BLL.interfaces;
using Hotel.BLL.Services;
using Hotel.DAL.Interfaces;
using Hotel.DAL.Repositories;
using Ninject.Modules;

namespace Hotel.BLL.Utils
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<IUserRepository>().To<UserRepository>();
            Bind<IRoomCategoryRepository>().To<RoomCategoryRepository>();
            Bind<IRoomRepository>().To<RoomRepository>();
            Bind<IOrderRepository>().To<OrderRepository>();
            Bind<IUnitOfWork>().To<EFUnitOfWork>();

            Bind<IUserService>().To<UserService>();
            Bind<IRoomCategoryService>().To<RoomCategoryService>();
            Bind<IRoomService>().To<RoomService>();
            Bind<IOrderService>().To<OrderService>();
        }
    }
}
