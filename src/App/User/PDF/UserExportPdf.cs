using LodgerBackend.User.Models.Dtos;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace LodgerBackend.User.PDF
{
    public class UserExportPdf : IDocument
    {
        private readonly UserDetailsDto user;

        public UserExportPdf(UserDetailsDto user)
        {
            this.user = user;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Margin(30);
                page.Size(PageSizes.A4);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(12));

                page.Header()
                    .Text("Export des données personnelles (RGPD)")
                    .SemiBold().FontSize(20).FontColor(Colors.Blue.Medium);

                page.Content()
                    .Column(col =>
                    {
                        col.Spacing(10);

                        col.Item().Text($"Nom : {user.Name}");
                        col.Item().Text($"Prénom : {user.FirstName}");
                        col.Item().Text($"Email : {user.Email}");
                        col.Item().Text($"Téléphone : {user.Phone}");
                        col.Item().Text($"Genre : {user.GenderName}");
                        col.Item().Text($"Nationalité : {user.NationalityName}");

                        if (user.Birthday.HasValue)
                            col.Item().Text($"Date de naissance : {user.Birthday:dd/MM/yyyy}");

                        col.Item().Text($"Adresse : {user.AddressName}");
                        col.Item().Text($"Code Postal : {user.PostalCode}");

                        col.Item().PaddingTop(10).Text("🔐 Données d'identité :")
                            .SemiBold().FontSize(14).FontColor(Colors.Grey.Darken2);

                        col.Item().Text(user.IdentityFile != null
                            ? "Un fichier d'identité est stocké."
                            : "Aucun document d'identité enregistré.");
                    });

                page.Footer()
                    .AlignCenter()
                    .Text(txt =>
                    {
                        txt.Span("Document généré le ");
                        txt.Span($"{DateTime.Now:dd/MM/yyyy}").SemiBold();
                    });
            });

        }
    }

}
