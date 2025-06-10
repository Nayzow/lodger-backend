using LodgerBackend.App.RentalFile.Enums;

namespace LodgerBackend.src.App.RentalFile.Enums
{
    public static class ERoomatesNumber
    {
        
    }
    public static class RoomatesNumberExtensions
    {
        public static string GetName(int number)
        {

            return number switch
            {
                0 => "0",
                1 => "1",
                2 => "2",
                3 => "3",
                > 3 => "3+",
                _ => "0"
            };
        }

        public static int FromName(string roomatesNb)
        {
            if (roomatesNb.Trim() == "3+")
                return 4; // ou une autre valeur pour "plus de 3"

            if (int.TryParse(roomatesNb, out int result))
                return result;

            return 0; // valeur par défaut si la chaîne est invalide
        }


    }
}
