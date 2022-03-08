namespace Boilerplate.Application.Common.Mailing;

public interface IMailService
{
    Task SendAsync(MailRequest request, CancellationToken ct);
}