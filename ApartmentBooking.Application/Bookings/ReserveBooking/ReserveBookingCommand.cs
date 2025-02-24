using ApartmentBooking.Application.Abstractions.Messaging;

namespace ApartmentBooking.Application.Bookings.ReserveBooking;

public record ReserveBookingCommand(
    //Groups all parameters into one object.
    Guid ApartmentId,
    Guid UserId,
    DateOnly StartDate,
    DateOnly EndDate
    // why we define return:
        // to enforce a consistent contract for handlers.
        // This ensures that any handler processing this command knows what it must return,
        // improving maintainability, type safety, and predictability across the application.
    ) : ICommand<Guid>; // will return the id of reserved Command
