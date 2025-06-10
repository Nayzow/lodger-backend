using System.Text.Json.Serialization;
using LodgerBackend.App.Document.Models;

namespace LodgerBackend.App.RentalFile.Models
{

    public class RentalFileDto
    {
        [JsonPropertyName("statutProfessionnel")]
        public required string ProStatus { get; set; }

        [JsonPropertyName("revenuMensuel")]
        public required string MonthlyIncome { get; set; }

        [JsonPropertyName("garant")]
        public required string HasGuarantor { get; set; }

        [JsonPropertyName("revenuGarant")]

        public required string GuarantorIncome { get; set; }
        [JsonPropertyName("statusFamiliale")]

        public required string StatusFamilial { get; set; }

        [JsonPropertyName("nbColocataires")]

        public required string RoomatesNb { get; set; }

        [JsonPropertyName("animaux")]

        public required string HasAnimals { get; set; }

        [JsonPropertyName("fumeur")]

        public required string Smoker { get; set; }


        [JsonPropertyName("files")]
        public required List<DocumentDto> Files { get; set; }
    }
}
