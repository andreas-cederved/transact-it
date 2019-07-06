using AutoMapper;

namespace TransactIt.Infrastructure.Profiles
{
    public class AccountingTemplateRuleProfile : Profile
    {
        public AccountingTemplateRuleProfile()
        {
            CreateMap<Domain.Entities.AccountingTemplateRule, Domain.Models.AccountingTemplateRule>()
                .ForMember(target => target.Id, source => source.MapFrom(prop => prop.Id))
                .ForMember(target => target.Multiplier, source => source.MapFrom(prop => prop.Multiplier))
                .ForMember(target => target.Side, source => source.MapFrom(prop => prop.Side))
                .ForMember(target => target.LedgerAccountId, source => source.MapFrom(prop => prop.LedgerAccountId))
                .ForSourceMember(source => source.LedgerAccount, option => option.DoNotValidate())
                .ForSourceMember(source => source.AccountingTemplateId, option => option.DoNotValidate())
                .ForSourceMember(source => source.AccountingTemplate, option => option.DoNotValidate());

            CreateMap<Domain.Models.AccountingTemplateRule, Domain.Entities.AccountingTemplateRule>()
                .ForMember(target => target.Id, source => source.MapFrom(prop => prop.Id))
                .ForMember(target => target.Multiplier, source => source.MapFrom(prop => prop.Multiplier))
                .ForMember(target => target.Side, source => source.MapFrom(prop => prop.Side))
                .ForMember(target => target.LedgerAccountId, source => source.MapFrom(prop => prop.LedgerAccountId))
                .ForMember(target => target.LedgerAccount, option => option.Ignore())
                .ForMember(target => target.AccountingTemplateId, option => option.Ignore())
                .ForMember(target => target.AccountingTemplate, option => option.Ignore());
        }
    }
}
