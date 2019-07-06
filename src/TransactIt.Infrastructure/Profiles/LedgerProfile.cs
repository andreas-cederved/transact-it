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
                .ForMember(target => target.FinancialTransactions, source => source.MapFrom(prop => prop.FinancialTransactions))
                .ForMember(target => target.LedgerMainAccountGroups, source => source.MapFrom(prop => prop.LedgerMainAccountGroups))
                .ForMember(target => target.AccountingTemplates, source => source.MapFrom(prop => prop.AccountingTemplates));


            CreateMap<Domain.Models.Ledger, Domain.Entities.Ledger>()
                .ForMember(target => target.Id, source => source.MapFrom(prop => prop.Id))
                .ForMember(target => target.Name, source => source.MapFrom(prop => prop.Name))
                .ForMember(target => target.Description, source => source.MapFrom(prop => prop.Description))
                .ForMember(target => target.CreatedDate, source => source.MapFrom(prop => prop.CreatedDate))
                .ForMember(target => target.FinancialTransactions, option => option.Ignore())
                .ForMember(target => target.LedgerMainAccountGroups, option => option.Ignore())
                .ForMember(target => target.AccountingTemplates, option => option.Ignore());
        }
    }
}
