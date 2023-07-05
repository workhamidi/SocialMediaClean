using MediatR;

namespace SocialMediaClean.Application.Common.Behaviors;

public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            // var requestName = typeof(TRequest).Name;

            // _logger.LogError(ex, "CleanTest Request: Unhandled Exception for Request {Name} {@Request}", requestName, request);
            

            throw;
        }
    }
}


