using ApartmentBooking.Application.Apartments.GetApartments;
using ApartmentBooking.Domain.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApartmentBooking.Api.Controllers.Apartments
{
    [ApiController]
    // u can use template [Controller] but he prefered this 
    [Route("api/apartments")]
    public class ApartmentsController:ControllerBase
    {
        private readonly ISender sender;

        public ApartmentsController(ISender sender)
        {
            this.sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> SearchApartments(
            DateOnly startDate,
            DateOnly endDate,
            CancellationToken cancellationToken)
        {
            GetAppartmentsQuery query =new GetAppartmentsQuery(startDate, endDate);
            var result = await sender.Send(query, cancellationToken);
            return Ok(result.Value);

        }

    }
}
