using MediatR;
using SocialMediaClean.Application.Common.Enums;
using SocialMediaClean.Application.Common.Interfaces;
using SocialMediaClean.Application.Common.Models;

namespace SocialMediaClean.Application.Message.Commands.EditMessage;
public class EditMessageHandler : IRequestHandler<EditMessageRecord, Result>
{
    
    private readonly ISocialMediaCleanContext _context;

    public EditMessageHandler(
        ISocialMediaCleanContext context
    )
    {
        _context = context;
    }

    public async Task<Result> Handle(EditMessageRecord request, CancellationToken cancellationToken)
    {

        var message = _context.Message.FirstOrDefault(m => m.Id == request.MessageId);

        if (message is null)
            return new Result()
            {
                ResultCode = ResultCodeEnum.Failed,
                Description = $"the message with id {request.MessageId} not found"
            };


        if (DateTime.Now.Subtract(message.Created).Minutes <= 3)
        {
            message.MessageText = request.NewMessageText;
            message.LastModified = DateTime.Now;

            await _context.SaveChangesAsync(cancellationToken);

            return new Result()
            {
                ResultCode = ResultCodeEnum.Success,
                Description = "the message edited was successfully"
            };

        }

        return new Result()
        {
            Description = "the deadline date to editing message is finished"
        };

    }
}

