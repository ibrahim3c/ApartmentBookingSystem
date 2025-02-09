using ApartmentBooking.Domain.Abstraction;

namespace ApartmentBooking.Domain.Bookings
{   
    public static  class BookingErrors
    {
        public static Error NotFound = new Error("Booking.Found",
                               "The booking with the specific identifier is not found");

        public static Error Overlap = new Error("Booking.Overlap",
                             "The current booking is overlapping  with an existing one");

        public static Error NotReserved = new Error("Booking.NotReserved",
                     "The booking is not pending");

        public static Error NotConfirmed = new Error("Booking.NotConfirmed",
             "The booking is not NotConfirmed");

        public static Error AlreadyStarted = new Error("Booking.AlreadyStarted",
             "The booking has already started");
    }
}
