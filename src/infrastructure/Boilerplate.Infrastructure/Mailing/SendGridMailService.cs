using Boilerplate.Application.Common.Mailing;
using Microsoft.Extensions.Logging;

namespace Boilerplate.Infrastructure.Mailing;

public class SendGridMailService:IMailService
{
    private readonly ILogger<SendGridMailService> _logger;

    public SendGridMailService(ILogger<SendGridMailService> logger)
    {
        _logger = logger;
    }

    public Task SendAsync(MailRequest request, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}