using AutoMapper;

namespace TransactIt.Infrastructure.Profiles
{
    public class LedgerAccountProfile : Profile
    {
        public LedgerAccountProfile()
        {
            CreateMap<Domain.Entities.LedgerAccount, Domain.Models.LedgerAccount>()
                .ForMember(target => target.Id, source => source.MapFrom(prop => prop.Id))
                .ForMember(target => target.Number, source => source.MapFrom(prop => prop.Number))
                .ForMember(target => target.Name, source => source.MapFrom(prop => prop.Name))
                .ForMember(target => target.Description, source => source.MapFrom(prop => prop.Description))
                .ForMember(target => target.CreatedDate, source => source.MapFrom(prop => prop.CreatedDate))
                .ForMember(target => target.AccountingEntries, source => source.MapFrom(prop => prop.AccountingEntries))
                .ForSourceMember(source => source.LedgerAccountGroupId, option => option.DoNotValidate())
                .ForSourceMember(source => source.LedgerAccountGroup, option => option.DoNotValidate());

            CreateMap<Domain.Models.LedgerAccount, Domain.Entities.LedgerAccount>()
                .ForMember(target => target.Id, source => source.MapFrom(prop => prop.Id))
                .ForMember(target => target.Number, source => source.MapFrom(prop => prop.Number))
                .ForMember(target => target.Name, source => source.MapFrom(prop => prop.Name))
                .ForMember(target => target.Description, source => source.MapFrom(prop => prop.Description))
                .ForMember(target => target.CreatedDate, source => source.MapFrom(prop => prop.CreatedDate))
                .ForMember(target => target.AccountingEntries, source => source.MapFrom(prop => prop.AccountingEntries))
                .ForMember(target => target.LedgerAccountGroupId, option => option.Ignore())
                .ForMember(target => target.LedgerAccountGroup, option => option.Ignore());
        }
    }
}
