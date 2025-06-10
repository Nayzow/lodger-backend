using System.Globalization;
using AutoMapper;
using LodgerBackend.Device.Models;
using LodgerBackend.Document.Enum;
using LodgerBackend.Document.Models;
using LodgerBackend.Payment.Models;
using LodgerBackend.RentalFile.Enums;
using LodgerBackend.RentalFile.Models;
using LodgerBackend.Setting.Dtos;
using LodgerBackend.Setting.Enums;
using LodgerBackend.User.Models.Dtos;
using LodgerBackend.User.Models.Entities;
using LodgerBackend.User.Models.Enums;

namespace LodgerBackend.Configuration.MappingProfile

{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            // Configuration du mappage de l'entité User vers le DTO UserDetailsDto
            CreateMap<User.Models.Entities.User, UserDetailsDto>()
            .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Address != null ? src.Address.PostalCode : string.Empty))
            .ForMember(dest => dest.AddressName, opt => opt.MapFrom(src => src.Address != null ? src.Address.AddressName : string.Empty))
            .ForMember(dest => dest.GenderName, opt => opt.MapFrom(src => GenderExtensions.GetName(src.Gender)))
            .ForMember(dest => dest.NationalityName, opt => opt.MapFrom(src => NationalityExtensions.GetName(src.Nationality)))
            .ReverseMap()
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => GenderExtensions.FromName(src.GenderName)))
            .ForMember(dest => dest.Nationality, opt => opt.MapFrom(src => NationalityExtensions.FromName(src.NationalityName)))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address
            {
                PostalCode = src.PostalCode,
                AddressName = src.AddressName
            }));


            // Mappage de Settings vers UpdateNotificationSettingsDto
            CreateMap<Setting.Models.Settings, UpdateSettingsDto>()
                .ForMember(dest => dest.Push, opt => opt.MapFrom(src => src.Theme == ENotification.NotificationPush))  // Utiliser l'énumération pour remplir le booléen
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Theme == ENotification.Email))
                .ForMember(dest => dest.Sms, opt => opt.MapFrom(src => src.Theme == ENotification.SMS))
                .ForMember(dest => dest.FolderActivity, opt => opt.MapFrom(src => src.Theme == ENotification.RentalActivity))
                .ForMember(dest => dest.ListingUpdates, opt => opt.MapFrom(src => src.Theme == ENotification.FollowAdvert))
                .ForMember(dest => dest.AccountSecurity, opt => opt.MapFrom(src => src.Theme == ENotification.AccountSecurity));

            // Mappage inverse de UpdateNotificationSettingsDto vers Settings
            CreateMap<UpdateSettingsDto, Setting.Models.Settings>()
                .ForMember(dest => dest.Theme, opt => opt.MapFrom(src => GetThemeFromDto(src)));

            CreateMap<Payment.Models.Payment, PaymentDto>().ReverseMap();
            CreateMap<Document.Models.Document, DocumentDto>()
            .ForMember(dest => dest.DocumentCategorie, opt => opt.MapFrom(src => DocumentCategorieExtensions.GetName(src.DocumentCategorieId)))
            .ForMember(dest => dest.FileName, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.DocumentType, opt => opt.MapFrom(src => DocumentTypeExtensions.GetName(src.DocumentTypeId)))
            .ReverseMap()
            .ForMember(dest => dest.DocumentTypeId, opt => opt.MapFrom(src => DocumentTypeExtensions.FromName(src.DocumentType)))
            .ForMember(dest => dest.DocumentCategorieId, opt => opt.MapFrom(src => DocumentCategorieExtensions.FromName(src.DocumentCategorie)));


            CreateMap<DocumentType, DocumentTypeDto>().ReverseMap();

            // Conversion Model -> Dto
            CreateMap<RentalFile.Models.RentalFile, RentalFileDto>()
            .ForMember(dest => dest.MonthlyIncome, opt => opt.MapFrom(src => src.NetMonthlyIncome.ToString("0.##", CultureInfo.InvariantCulture)))
            .ForMember(dest => dest.GuarantorIncome, opt => opt.MapFrom(src => src.NetMonthlyIncomeGarant.ToString("0.##", CultureInfo.InvariantCulture)))
            .ForMember(dest => dest.ProStatus, opt => opt.MapFrom(src => StatusProExtensions.GetName(src.StatusPro)))
            .ForMember(dest => dest.HasGuarantor, opt => opt.MapFrom(src => GarantExtensions.GetName(src.Guarantor)))
            .ForMember(dest => dest.Smoker, opt => opt.MapFrom(src => src.Smoker ? "Oui" : "Non"))
            .ForMember(dest => dest.HasAnimals, opt => opt.MapFrom(src => src.Pets ? "Oui" : "Non"))
            .ForMember(dest => dest.StatusFamilial, opt => opt.MapFrom(src => FamilyStatusExtensions.GetName(src.FamilyStatus)))
            .ForMember(dest => dest.RoomatesNb, opt => opt.MapFrom(src => RoomatesNumberExtensions.GetName(src.RoommatesNb)))
            .ReverseMap();


            // Conversion Dto -> Model
            CreateMap<RentalFile.Models.RentalFile, UpdateRentalFileDto>()
            .ReverseMap()
            .ForMember(dest => dest.NetMonthlyIncome, opt => opt.MapFrom(src => MapperUtils.ParseDecimalOrDefault(src.MonthlyIncome, 0)))
            .ForMember(dest => dest.NetMonthlyIncomeGarant, opt => opt.MapFrom(src => MapperUtils.ParseDecimalOrDefault(src.GuarantorIncome, 0)))
            .ForMember(dest => dest.StatusPro, opt => opt.MapFrom(src => StatusProExtensions.FromName(src.ProStatus)))
            .ForMember(dest => dest.Guarantor, opt => opt.MapFrom(src => GarantExtensions.FromName(src.HasGuarantor)))
            .ForMember(dest => dest.FamilyStatus, opt => opt.MapFrom(src => FamilyStatusExtensions.FromName(src.StatusFamilial)))
            .ForMember(dest => dest.Guarantor, opt => opt.MapFrom(src => GarantExtensions.FromName(src.HasGuarantor)))
            .ForMember(dest => dest.Smoker, opt => opt.MapFrom(src => src.Smoker == "Oui" ? true : false))
            .ForMember(dest => dest.Pets, opt => opt.MapFrom(src => src.HasAnimals == "Oui" ? true : false))
            .ForMember(dest => dest.RoommatesNb, opt => opt.MapFrom(src => RoomatesNumberExtensions.FromName(src.RoomatesNb)));


            CreateMap<Device.Models.Device, DeviceDto>()
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString()))
            .ReverseMap();


        }
        public static class MapperUtils
        {
            public static decimal ParseDecimalOrDefault(string? input, decimal defaultValue = 0)
            {
                return decimal.TryParse(input, NumberStyles.Number, CultureInfo.InvariantCulture, out var result)
                    ? result
                    : defaultValue;
            }
        }

        // Méthode d'extension pour récupérer l'énumération à partir du DTO
        private ENotification GetThemeFromDto(UpdateSettingsDto dto)
        {
            if (dto.Push)
                return ENotification.NotificationPush;
            if (dto.Email)
                return ENotification.Email;
            if (dto.Sms)
                return ENotification.SMS;
            if (dto.FolderActivity)
                return ENotification.RentalActivity;
            if (dto.ListingUpdates)
                return ENotification.FollowAdvert;
            if (dto.AccountSecurity)
                return ENotification.AccountSecurity;

            // Retourner une valeur par défaut (Email par exemple)
            return ENotification.Email;
        }

    }

}
