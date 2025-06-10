using System.ComponentModel;

namespace LodgerBackend.Auth.Models.Payloads;

public class SignupRequest
{
    public required string Name { get; init; }
#if DEBUG
    [DefaultValue("jetaimethibo@gmail.com")]
#endif
    public required string Email { get; init; }
#if DEBUG
    [DefaultValue("tagrandmerelemotdepasse")]
#endif
    public required string Password { get; init; }

}