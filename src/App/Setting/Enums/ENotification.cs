namespace LodgerBackend.src.App.Settings.Enums
{
    public enum ENotification
    {
        Email = 0,
        NotificationPush = 1,
        SMS = 2,
        RentalActivity = 3,
        FollowAdvert = 4,
        AccountSecurity = 5
    }
    public static class NotificationExtensions
    {
        public static string GetName(this ENotification notification)
        {
            return notification switch
            {
                ENotification.Email => "Email",
                ENotification.NotificationPush => "Push Notification",
                ENotification.SMS => "SMS",
                ENotification.RentalActivity => "Rental Activity",
                ENotification.FollowAdvert => "Follow Advert",
                ENotification.AccountSecurity => "Account Security",
                _ => "Unknown"
            };
        }
        public static ENotification FromName(string? name)
        {
            return name?.Trim().ToLower() switch
            {
                "email" => ENotification.Email,
                "push notification" => ENotification.NotificationPush,
                "sms" => ENotification.SMS,
                "rental activity" => ENotification.RentalActivity,
                "follow advert" => ENotification.FollowAdvert,
                "account security" => ENotification.AccountSecurity,
                _ => throw new ArgumentException($"Invalid notification name: {name}")
            };
        }
    }
}
