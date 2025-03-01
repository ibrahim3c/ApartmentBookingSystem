namespace ApartmentBooking.Application.Exceptions
{
    public record ValidationError(string propertyName, string errorMessage);
}
