using Hotel.DAL.Entities;
using System;
using System.Data.Entity;
using System.Globalization;

namespace Hotel.DAL.EF
{
    class HotelContextInitializer : DropCreateDatabaseAlways<HotelContext>
    {
        protected override void Seed(HotelContext context)
        {
            User user1 = new User { Login = "user1", PasswordHash = "PaSSw0rd", Role = UserRole.User };
            User user2 = new User { Login = "user2", PasswordHash = "qwerty", Role = UserRole.User };
            User user3 = new User { Login = "user3", PasswordHash = "PaSSw0rd123r23r23r", Role = UserRole.User };
            User user4 = new User { Login = "user4", PasswordHash = "s2398ryyte", Role = UserRole.User };
            User user5 = new User { Login = "admin", PasswordHash = "wefohifoo", Role = UserRole.Admin };

            context.Users.Add(user1);
            context.Users.Add(user2);
            context.Users.Add(user3);
            context.Users.Add(user4);
            context.Users.Add(user5);

            RoomCategory category1 = new RoomCategory
            {
                Name = "Category1",
                PricePerDay = 1200.50m,
                Capacity = 3,
                Description = "Super description1"
            };
            RoomCategory category2 = new RoomCategory
            {
                Name = "Category2",
                PricePerDay = 1000m,
                Capacity = 2,
                Description = "Super description2"
            };

            context.RoomCategories.Add(category1);
            context.RoomCategories.Add(category2);

            Room room1 = new Room { RoomCategory = category1 };
            Room room2 = new Room { RoomCategory = category1 };
            Room room3 = new Room { RoomCategory = category1 };
            Room room4 = new Room { RoomCategory = category1 };
            Room room5 = new Room { RoomCategory = category2 };
            Room room6 = new Room { RoomCategory = category2 };
            Room room7 = new Room { RoomCategory = category2 };
            Room room8 = new Room { RoomCategory = category2 };

            context.Rooms.Add(room1);
            context.Rooms.Add(room2);
            context.Rooms.Add(room3);
            context.Rooms.Add(room4);
            context.Rooms.Add(room5);
            context.Rooms.Add(room6);
            context.Rooms.Add(room7);
            context.Rooms.Add(room8);

            Order order1 = new Order
            {
                Start = DateTime.ParseExact("2021-12-23", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                End = DateTime.ParseExact("2021-12-25", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                Type = OrderType.Booked,
                User = user1,
                Room = room1
            };

            Order order2 = new Order
            {
                Start = DateTime.ParseExact("2021-12-30", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                End = DateTime.ParseExact("2021-12-31", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                Type = OrderType.Paid,
                User = user2,
                Room = room2
            };

            Order order3 = new Order
            {
                Start = DateTime.ParseExact("2021-12-29", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                End = DateTime.ParseExact("2022-01-01", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                Type = OrderType.Booked,
                User = user3,
                Room = room3
            };
        
            Order order4 = new Order
            {
                Start = DateTime.ParseExact("2021-12-24", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                End = DateTime.ParseExact("2022-12-28", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                Type = OrderType.Paid,
                User = user4,
                Room = room4
            };

            context.Orders.Add(order1);
            context.Orders.Add(order2);
            context.Orders.Add(order3);
            context.Orders.Add(order4);

            context.SaveChanges();
        }
    }
}
