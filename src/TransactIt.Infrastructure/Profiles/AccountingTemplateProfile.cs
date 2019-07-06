using AutoMapper;

namespace TransactIt.Infrastructure.Profiles
{
    public class AccountingTemplateProfile : Profile
    {
        public AccountingTemplateProfile()
        {
            CreateMap<Domain.Entities.AccountingTemplate, Domain.Models.AccountingTemplate>()
                .ForMember(target => target.Id, source => source.MapFrom(prop => prop.Id))
                .ForMember(target => target.Name, source => source.MapFrom(prop => prop.Name))
                .ForMember(target => target.Description, source => source.MapFrom(prop => prop.Description))
                .ForMember(target => target.DefaultFinancialTransactionDescription, source => source.MapFrom(prop => prop.DefaultFinancialTransactionDescription))
                .ForMember(target => target.CreatedDate, source => source.MapFrom(prop => prop.CreatedDate))
                .ForMember(target => target.AccountingTemplateRules, source => source.MapFrom(prop => prop.AccountingTemplateRules))
                .ForSourceMember(source => source.LedgerId, option => option.DoNotValidate())
                .ForSourceMember(source => source.Ledger, option => option.DoNotValidate());

            CreateMap<Domain.Models.AccountingTemplate, Domain.Entities.AccountingTemplate>()
                .ForMember(target => target.Id, source => source.MapFrom(prop => prop.Id))
                .ForMember(target => target.Name, source => source.MapFrom(prop => prop.Name))
                .ForMember(target => target.Description, source => source.MapFrom(prop => prop.Description))
                .ForMember(target => target.DefaultFinancialTransactionDescription, source => source.MapFrom(prop => prop.DefaultFinancialTransactionDescription))
                .ForMember(target => target.CreatedDate, source => source.MapFrom(prop => prop.CreatedDate))
                .ForMember(target => target.AccountingTemplateRules, source => source.MapFrom(prop => prop.AccountingTemplateRules))
                .ForMember(target => target.LedgerId, option => option.Ignore())
                .ForMember(target => target.Ledger, option => option.Ignore());
        }
    }
}
