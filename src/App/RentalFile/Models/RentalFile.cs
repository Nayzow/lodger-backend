using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LodgerBackend.App.RentalFile.Enums;

namespace LodgerBackend.App.RentalFile.Models;

[Table("rental_files")]
public class RentalFile
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }

    [Column("status_pro")]
    public EStatusPro StatusPro { get; set; } = EStatusPro.CDI;

    [Column("net_monthly_income")]
    public decimal NetMonthlyIncome { get; set; } = 0;

    [Column("guarantor")]
    public EGarant Guarantor { get; set; } = EGarant.Non;

    [Column("net_monthly_income_garant")]
    public decimal NetMonthlyIncomeGarant { get; set; } = 0;

    [Column("family_status")]
    public  EFamilyStatus FamilyStatus { get; set; } = EFamilyStatus.VEUF;

    [Column("roomates_nb")]
    public  int RoommatesNb { get; set; } = 0;
    [Column("pets")] 
    public  bool Pets {  get; set; } = false;
    [Column("smoker")]
    public  bool Smoker { get; set; } = false;

    public virtual User.Models.Entities.User? User { get; set; }

}