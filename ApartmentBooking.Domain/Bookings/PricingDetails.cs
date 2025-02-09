using ApartmentBooking.Domain.Shared;

namespace ApartmentBooking.Domain.Bookings
{
    public record PricingDetails(Money priceForPeriod, Money cleaningFee, Money amenityUpCharge, Money totalPrice);
   
}