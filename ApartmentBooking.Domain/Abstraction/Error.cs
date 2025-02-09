namespace ApartmentBooking.Domain.Abstraction
{
    public record Error(string Code, string Message)
    {
        public static Error None=new (string.Empty, string.Empty);
        public static Error NullValue = new("Error.Null", "Null Value was provided");
    }
}
