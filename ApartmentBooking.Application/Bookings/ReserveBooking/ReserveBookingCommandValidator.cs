using FluentValidation;

namespace ApartmentBooking.Application.Bookings.ReserveBooking
{
    public class ReserveBookingCommandValidator:AbstractValidator<ReserveBookingCommand>
    {
        public ReserveBookingCommandValidator() {
        
            RuleFor(r=>r.ApartmentId).NotEmpty();
            RuleFor(r=>r.UserId).NotEmpty();

            RuleFor(r=>r.StartDate).LessThan(r=>r.EndDate);
        }
    }
}
