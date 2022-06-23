using MediatR;
using MediatR.Pipeline;

namespace Portal.Application.Common;
public class CommitCommandPostProcessor<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse> where TRequest : IRequest<TResponse>
{

    private readonly UnitOfWork _UOW;
    public CommitCommandPostProcessor(UnitOfWork unitOfWork)
    {
        _UOW = unitOfWork;
    }
    public async Task Process(TRequest request, TResponse response, CancellationToken cancellationToken)
    {
        try
        {
            if (request is CommittableRequest)
            {
                await _UOW.CommitAsync();
            }

        }
        catch (Exception ex)
        {

            
        }
    }
}