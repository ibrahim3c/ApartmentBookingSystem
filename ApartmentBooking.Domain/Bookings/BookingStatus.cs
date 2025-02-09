namespace ApartmentBooking.Domain.Bookings;


/*
 *Pending	The booking is created but not yet confirmed (e.g., awaiting payment).
 *Confirmed	The booking is confirmed, and the user can check in at the scheduled date.
 *Cancelled	The booking was cancelled before it started (by user or admin).
 *Completed	The stay has ended, and the booking is finished.
 *Expired	The booking was never confirmed and expired automatically.
 */
public enum BookingStatus
{
    Reserved=1,
    Confirmed,
    Rejected,
    Cancelled,
    Completed
}
