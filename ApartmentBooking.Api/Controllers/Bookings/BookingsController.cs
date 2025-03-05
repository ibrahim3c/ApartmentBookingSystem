using ApartmentBooking.Application.Bookings.ReserveBooking;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApartmentBooking.Api.Controllers.Bookings
{
    [ApiController]
    [Route("api/booking")]
    public class BookingsController:ControllerBase
    {
        private readonly ISender sender;

        public BookingsController(ISender sender)
        {
            this.sender = sender;
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetBookingById(int id)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> ReserveBooking(ReserveBookingRequest reserveBookingRequest,
            CancellationToken cancellationToken)
        {

            var command =new ReserveBookingCommand(
                reserveBookingRequest.ApartmentId,
                reserveBookingRequest.UserId,
                reserveBookingRequest.StartDate,
                reserveBookingRequest.EndDate
            );
            var result= await sender.Send(command, cancellationToken);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            /*
                🔹 First Parameter: nameof(GetBookingById) → The action method that retrieves the booking.
                🔹 Second Parameter: new { id = result.Value } → The route values for the action method.
                🔹 Third Parameter: result.Value → This is the response body (the created object).
                🔹 The response header:
                        HTTP/1.1 201 Created
                        Location: https://localhost:/api/bookings/{result.Value}
             */

            return CreatedAtAction(nameof(GetBookingById),new {id =result.Value},result.Value);

        }
    }
}
