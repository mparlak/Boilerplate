namespace Boilerplate.Application.Common.Mailing;

public class MailRequest
{
    public MailRequest(List<string> to, string subject, string? body = null, string? from = null, List<string>? bcc = null, List<string>? cc = null, IDictionary<string, byte[]>? attachmentData = null)
    {
        To = to;
        Subject = subject;
        Body = body;
        From = from;
        Bcc = bcc ?? new List<string>();
        Cc = cc ?? new List<string>();
        AttachmentData = attachmentData ?? new Dictionary<string, byte[]>();
    }

    public List<string> To { get; }

    public string Subject { get; }

    public string? Body { get; }

    public string? From { get; }

    public List<string> Bcc { get; }

    public List<string> Cc { get; }

    public IDictionary<string, byte[]> AttachmentData { get; }
}