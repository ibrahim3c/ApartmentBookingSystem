using ApartmentBooking.Application.Abstractions.Messaging;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ApartmentBooking.Application.Behaviors
{
    /*
     * adds logging for any command that implements IBaseCommand
     * we don't do it for query cuz we want make query as fast as possible 
        and i don't care about logging query 
     * Execution Flow
        Before executing the command handler → Logs "Executing command {command}"
        Executes the actual command handler → Calls next()
        After successful execution → Logs "command {command} processed successfully"
        If an exception occurs → Logs "command {command} processed failed" and rethrows the exception.
     */

    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IBaseCommand
    {
        //download this from nugget => Microsoft.Extensions.Logging.Abstractions
        private readonly ILogger<TRequest> logger;

        public LoggingBehavior(ILogger<TRequest> logger)
        {
            this.logger = logger;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var name=request.GetType().Name;

            try
            {
                // u can add extra information about command like command id and correlation id (i don't know :())
                logger.LogInformation("Executing command {command}", name);
                //It calls the actual command handler to process the command
                var result = await next();
                logger.LogInformation("command {command} processed successfully", name);
                return result;

            }
            catch (Exception)
            {

               logger.LogError("command {command} processed failed", name);
                throw;
            }

        }
    }
}
