using LodgerBackend.Auth.Models.Entities;

namespace LodgerBackend.Auth.Models.Payloads;

public class PerformPasswordResetPayload
{
    public required string NewPassword { get; init; }
    public required string ConfirmPassword { get; init; }
    
    public required ResetPasswordRequest ResetPasswordRequest { get; set; }
}