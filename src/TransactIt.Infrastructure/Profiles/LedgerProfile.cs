using AutoMapper;

namespace TransactIt.Infrastructure.Profiles
{
    public class LedgerProfile : Profile
    {
        public LedgerProfile()
        {
            CreateMap<Domain.Entities.Ledger, Domain.Models.Ledger>()
                .ForMember(target => target.Id, source => source.MapFrom(prop => prop.Id))
                .ForMember(target => target.Name, source => source.MapFrom(prop => prop.Name))
                .ForMember(target => target.Description, source => source.MapFrom(prop => prop.Description))
                .ForMember(target => target.CreatedDate, source => source.MapFrom(prop => prop.CreatedDate))
                .ForMember(target => target.Transactions, source => source.MapFrom(prop => prop.Transactions))
                .ForMember(target => target.MainAccountGroups, source => source.MapFrom(prop => prop.MainAccountGroups))
                .ForMember(target => target.TransactionTemplates, source => source.MapFrom(prop => prop.TransactionTemplates));


            CreateMap<Domain.Models.Ledger, Domain.Entities.Ledger>()
                .ForMember(target => target.Id, source => source.MapFrom(prop => prop.Id))
                .ForMember(target => target.Name, source => source.MapFrom(prop => prop.Name))
                .ForMember(target => target.Description, source => source.MapFrom(prop => prop.Description))
                .ForMember(target => target.CreatedDate, source => source.MapFrom(prop => prop.CreatedDate))
                .ForMember(target => target.Transactions, option => option.Ignore())
                .ForMember(target => target.MainAccountGroups, option => option.Ignore())
                .ForMember(target => target.TransactionTemplates, option => option.Ignore());
        }
    }
}
