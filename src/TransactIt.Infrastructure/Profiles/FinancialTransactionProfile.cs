using AutoMapper;

namespace TransactIt.Infrastructure.Profiles
{
    public class FinancialTransactionProfile : Profile
    {
        public FinancialTransactionProfile()
        {
            CreateMap<Domain.Entities.FinancialTransaction, Domain.Models.FinancialTransaction>()
                .ForMember(target => target.Id, source => source.MapFrom(prop => prop.Id))
                .ForMember(target => target.IdentifyingCode, source => source.MapFrom(prop => prop.IdentifyingCode))
                .ForMember(target => target.Description, source => source.MapFrom(prop => prop.Description))
                .ForMember(target => target.TransactionDate, source => source.MapFrom(prop => prop.TransactionDate))
                .ForMember(target => target.CreatedDate, source => source.MapFrom(prop => prop.CreatedDate))
                .ForMember(target => target.AccountingEntries, source => source.MapFrom(prop => prop.AccountingEntries))
                .ForSourceMember(source => source.LedgerId, option => option.DoNotValidate())
                .ForSourceMember(source => source.Ledger, option => option.DoNotValidate());

            CreateMap<Domain.Models.FinancialTransaction, Domain.Entities.FinancialTransaction>()
                .ForMember(target => target.Description, source => source.MapFrom(prop => prop.Description))
                .ForMember(target => target.TransactionDate, source => source.MapFrom(prop => prop.TransactionDate))
                .ForMember(target => target.AccountingEntries, source => source.MapFrom(prop => prop.AccountingEntries))
                .ForMember(target => target.Id, option => option.Ignore())
                .ForMember(target => target.IdentifyingCode, option => option.Ignore())
                .ForMember(target => target.CreatedDate, option => option.Ignore())
                .ForMember(target => target.LedgerId, option => option.Ignore())
                .ForMember(target => target.Ledger, option => option.Ignore());
        }
    }
}
