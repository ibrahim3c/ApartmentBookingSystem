using ApartmentBooking.Application.Abstractions.Messaging;
using ApartmentBooking.Domain.Abstraction;
using ApartmentBooking.Domain.Apartments;
using ApartmentBooking.Domain.Bookings;
using ApartmentBooking.Domain.Users;

namespace ApartmentBooking.Application.Bookings.ReserveBooking
{
    // make it sealed to Improves Performance
         //The JIT (Just-In-Time) compiler can optimize method calls better when it knows a class won't be inherited.
    public sealed class ReserveBookingCommandHandler : ICommandHanlder<ReserveBookingCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IApartmentRepository _apartmentRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly PricingService _pricingService;

        public ReserveBookingCommandHandler(IUserRepository userRepository,
            IApartmentRepository apartmentRepository,
            IBookingRepository bookingRepository, 
            IUnitOfWork unitOfWork , 
            PricingService pricingService)
        {
            _userRepository = userRepository;
            _apartmentRepository = apartmentRepository;
            _bookingRepository = bookingRepository;
            _unitOfWork = unitOfWork;
            _pricingService = pricingService;
        }

        public async Task<Result<Guid>> Handle(ReserveBookingCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user is null)
                return Result.Failure<Guid>(UserErrors.NotFound);

            var apartment = await _apartmentRepository.GetByIdAsync(request.ApartmentId);
            if (apartment is null)
                return Result.Failure<Guid>(ApartmentErrors.NotFound);

            var duration = DateRange.Create(request.StartDate, request.EndDate);


            /*
             *  Imagine an apartment that is already booked from March 1 to March 10.
                    Now, someone tries to book the same apartment from March 5 to March 8 → Conflict! ❌
             *  This check ensures:
                    A user cannot book an already reserved apartment during overlapping dates.
                    Prevents double bookings and maintains booking consistency.
             */
            if (await _bookingRepository.IsOverlappingAsync(apartment,duration,cancellationToken))
                return Result.Failure<Guid>(BookingErrors.Overlap);

            var booking = Booking.Reserve(
                apartment,
                user.Id,
                duration,
                DateTime.UtcNow,
                _pricingService
                );

            // persist all changes to db
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return booking.Id;

        }
    }
}
