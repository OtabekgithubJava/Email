namespace EmailSenderApp.Domain.Entities.Models;

public class EmailModel
{
    public string? Receiver { get; set; }
    public string? Subject { get; set; }
    public string? Body { get; set; }
}