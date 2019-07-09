using AutoMapper;

namespace TransactIt.Infrastructure.Profiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<Domain.Entities.Account, Domain.Models.Account>()
                .ForMember(target => target.Id, source => source.MapFrom(prop => prop.Id))
                .ForMember(target => target.Number, source => source.MapFrom(prop => prop.Number))
                .ForMember(target => target.Name, source => source.MapFrom(prop => prop.Name))
                .ForMember(target => target.Description, source => source.MapFrom(prop => prop.Description))
                .ForMember(target => target.CreatedDate, source => source.MapFrom(prop => prop.CreatedDate))
                .ForMember(target => target.AccountingEntries, source => source.MapFrom(prop => prop.AccountingEntries))
                .ForSourceMember(source => source.TransactionTemplateRules, option => option.DoNotValidate())
                .ForSourceMember(source => source.SubAccountGroupId, option => option.DoNotValidate())
                .ForSourceMember(source => source.SubAccountGroup, option => option.DoNotValidate());

            CreateMap<Domain.Models.Account, Domain.Entities.Account>()
                .ForMember(target => target.Id, source => source.MapFrom(prop => prop.Id))
                .ForMember(target => target.Number, source => source.MapFrom(prop => prop.Number))
                .ForMember(target => target.Name, source => source.MapFrom(prop => prop.Name))
                .ForMember(target => target.Description, source => source.MapFrom(prop => prop.Description))
                .ForMember(target => target.CreatedDate, source => source.MapFrom(prop => prop.CreatedDate))
                .ForMember(target => target.AccountingEntries, option => option.Ignore())
                .ForMember(target => target.TransactionTemplateRules, option => option.Ignore())
                .ForMember(target => target.SubAccountGroupId, option => option.Ignore())
                .ForMember(target => target.SubAccountGroup, option => option.Ignore());
        }
    }
}
