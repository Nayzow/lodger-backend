namespace LodgerBackend.App.Auth.Models.Payloads;

public class RequestPasswordResetPayload
{
    public required string Email { get; init; }
}