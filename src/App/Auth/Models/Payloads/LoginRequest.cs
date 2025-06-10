using System.ComponentModel;

namespace LodgerBackend.App.Auth.Models.Payloads;

public class LoginRequest
{
#if DEBUG
    [DefaultValue("swagger@test.fr")]
#endif
    public required string Email { get; init;  }
#if DEBUG
    [DefaultValue("Swagger123!")]
#endif
    public required string Password { get; init;  }
}
