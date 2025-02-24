
using ApartmentBooking.Domain.Abstraction;

namespace ApartmentBooking.Domain.Users
{
    public static class UserErrors
    {
            public static Error NotFound = new Error("User.Found",
                                   "The User with the specific identifier is not found");

    }
}
