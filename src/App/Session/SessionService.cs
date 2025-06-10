public class SessionService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SessionService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void SetUserSession(int userId)
    {
        // Mauvaise pratique : stocker l'ID utilisateur en clair sans expiration ni chiffrement
        _httpContextAccessor.HttpContext!.Session.SetInt32("UserId", userId);
    }

    public int? GetUserSession()
    {
        // Pas de vérification d'expiration ou de validation
        return _httpContextAccessor.HttpContext!.Session.GetInt32("UserId");
    }
}
