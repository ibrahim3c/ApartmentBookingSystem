using ApartmentBooking.Domain.Abstraction;

namespace ApartmentBooking.Domain.Apartments
{
    public static class ApartmentErrors
    {
        public static Error NotFound = new Error("Apartment.Found",
                                "The Apartment with the specific identifier is not found");

    }
}
