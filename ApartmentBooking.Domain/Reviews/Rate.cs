namespace ApartmentBooking.Domain.Reviews
{
    public record Rating
    {
        public int Value { get; }

        private Rating(int value)
        {
            if (value < 1 || value > 5)
            {
                throw new ArgumentException("Rating must be between 1 and 5.");
            }
            Value = value;
        }

        public static Rating Create(int value) => new(value);
    }
}
