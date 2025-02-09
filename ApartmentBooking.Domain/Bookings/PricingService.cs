using ApartmentBooking.Domain.Apartments;
using ApartmentBooking.Domain.Shared;

namespace ApartmentBooking.Domain.Bookings;

public class PricingService
{
    public PricingDetails CalculatePrice(Apartment apartment, DateRange duration)
    {

        /*
         Example Calculation
        ✔ Assume:
            Apartment price per day = $100
            Stay duration = 5 days
            Amenities: Garden View + Parking (+6%)
            Cleaning fee = $20
        Step-by-step calculation
            1️⃣ priceForPeriod = 100 × 5 = $500
            2️⃣ amenityUpCharge = $500 × 0.06 = $30
            3️⃣ TotalPrice = $20 (cleaning fee) + $30 (amenities) = $50
            4️⃣ Final total price → $50 (extra) + $500 (base) = $550
         */
        var currency = apartment.Price.currency;
        var priceForPeriod = new Money(apartment.Price.amount * duration.LengthInDays, currency);

        decimal percentageUpCharge = 0;
        foreach (var amenity in apartment.Amenties)
        {
            percentageUpCharge += amenity switch
            {
                Amenty.GarderView or Amenty.MountainView => 0.05m,
                Amenty.AirConditioning => 0.01m,
                Amenty.Parking => 0.01m,
                _ => 0
            };
        }

        var amenityUpCharge = Money.Zero(currency);
        if (percentageUpCharge > 0)
        {
            amenityUpCharge = new Money(priceForPeriod.amount * percentageUpCharge, currency);
        }

        var TotalPrice = Money.Zero();
        if (!apartment.CleaningFee.IsZero())
        {
            TotalPrice += apartment.CleaningFee;
        }

        TotalPrice += amenityUpCharge;

        return new PricingDetails(priceForPeriod, apartment.CleaningFee, amenityUpCharge, TotalPrice);
    }
}
