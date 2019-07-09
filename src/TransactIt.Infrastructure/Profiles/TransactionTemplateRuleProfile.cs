using AutoMapper;

namespace TransactIt.Infrastructure.Profiles
{
    public class TransactionTemplateRuleProfile : Profile
    {
        public TransactionTemplateRuleProfile()
        {
            CreateMap<Domain.Entities.TransactionTemplateRule, Domain.Models.TransactionTemplateRule>()
                .ForMember(target => target.Id, source => source.MapFrom(prop => prop.Id))
                .ForMember(target => target.Multiplier, source => source.MapFrom(prop => prop.Multiplier))
                .ForMember(target => target.Side, source => source.MapFrom(prop => prop.Side))
                .ForMember(target => target.LedgerAccountId, source => source.MapFrom(prop => prop.AccountId))
                .ForSourceMember(source => source.Account, option => option.DoNotValidate())
                .ForSourceMember(source => source.TransactionTemplateId, option => option.DoNotValidate())
                .ForSourceMember(source => source.TransactionTemplate, option => option.DoNotValidate());

            CreateMap<Domain.Models.TransactionTemplateRule, Domain.Entities.TransactionTemplateRule>()
                .ForMember(target => target.Id, source => source.MapFrom(prop => prop.Id))
                .ForMember(target => target.Multiplier, source => source.MapFrom(prop => prop.Multiplier))
                .ForMember(target => target.Side, source => source.MapFrom(prop => prop.Side))
                .ForMember(target => target.AccountId, source => source.MapFrom(prop => prop.LedgerAccountId))
                .ForMember(target => target.Account, option => option.Ignore())
                .ForMember(target => target.TransactionTemplateId, option => option.Ignore())
                .ForMember(target => target.TransactionTemplate, option => option.Ignore());
        }
    }
}
