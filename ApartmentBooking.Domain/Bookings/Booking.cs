using ApartmentBooking.Domain.Abstraction;
using ApartmentBooking.Domain.Shared;
using ApartmentBooking.Domain.Bookings.DomainEvents;
using ApartmentBooking.Domain.Apartments;

namespace ApartmentBooking.Domain.Bookings;

public sealed class Booking : Entity
{
    private Booking(Guid id, Guid apartmentId, Guid userId, DateRange duration, Money priceForPeriod,
        Money cleaningFee, Money amenitiesUpCharge, Money totalPrice,BookingStatus bookingStatus, DateTime createdOnUTC) : base(id)
    {
        ApartmentId = apartmentId;
        UserId = userId;
        Duration = duration;
        AmenitiesUpCharge = amenitiesUpCharge;
        PriceForPeriod = priceForPeriod;
        CleaningFee = cleaningFee;
        TotalPrice = totalPrice;
        Status = bookingStatus;
        CreatedOnUtc = createdOnUTC;
    }
    private Booking() { }

    public Guid ApartmentId { get;private set; }
    public Guid UserId { get;private set; }
    public DateRange Duration { get; private set; }
    public Money PriceForPeriod { get; private set; }
    public Money CleaningFee { get; private set; }
    public Money AmenitiesUpCharge { get;private set; }
    public Money TotalPrice { get;private set; }
    public BookingStatus Status { get; private set; }

    public DateTime CreatedOnUtc { get; private set; }
    public DateTime? ConfirmedOnUTC { get; private set; }
    public DateTime? RejectedOnUTC { get; private set; }
    public DateTime? CompletedOnUTC { get; private set; }
    public DateTime? CancelledOnUCT { get; private set; }

    public static Booking Reserve(Apartment apartment, Guid userId, DateRange duration,
     // to get pricingDetails i want some business logic but not  naturally belong to a single Entity 
        // apartment , booking
     // so i make domain service to get these information
        DateTime UtcNow,PricingService pricingService)
    {
        var pricingDetails = pricingService.CalculatePrice(apartment, duration);
        var booking = new Booking(
             Guid.NewGuid(),
             apartment.Id,
             userId,
             duration,
             pricingDetails.priceForPeriod,
             pricingDetails.cleaningFee,
             pricingDetails.amenityUpCharge,
             pricingDetails.totalPrice,
             BookingStatus.Reserved,
             UtcNow
            );

        booking.RaiseDomainEvent(new BookingReservedDomainEvent(booking.Id));


        // make LastBookedOnUTC is internal set not private set to enable access it 
        apartment.LastBookedOnUTC = UtcNow;
        
        return booking;
    }


    // add few methods 
    public Result Confirm(DateTime utcNow)
    {
        if (Status != BookingStatus.Reserved)
        {
            return Result.Failure(BookingErrors.NotReserved);
        }
            Status = BookingStatus.Confirmed;
        ConfirmedOnUTC = utcNow;

        RaiseDomainEvent(new BookingConfirmedDomainEvent(Id));
        return Result.Success();
    }

    public Result Reject(DateTime utcNow)
    {
        if (Status != BookingStatus.Reserved)
        {
            return Result.Failure(BookingErrors.NotReserved);
        }
        Status = BookingStatus.Rejected;
        RejectedOnUTC = utcNow;

        RaiseDomainEvent(new BookingRejectedDomainEvent(Id));
        return Result.Success();
    }

    public Result Cancel(DateTime utcNow)
    {
        if (Status != BookingStatus.Confirmed)
        {
            return Result.Failure(BookingErrors.NotConfirmed);
        }

        var currentDote=DateOnly.FromDateTime(utcNow);
        if (currentDote > Duration.Start)
        {
            return Result.Failure(BookingErrors.AlreadyStarted);
        }

        Status = BookingStatus.Cancelled;
        CancelledOnUCT = utcNow;

        RaiseDomainEvent(new BookingCancelledDomainEvent(Id));
        return Result.Success();
    }

}
