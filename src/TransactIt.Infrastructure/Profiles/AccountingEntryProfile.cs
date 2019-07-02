using AutoMapper;

namespace TransactIt.Infrastructure.Profiles
{
    public class AccountingEntryProfile : Profile
    {
        public AccountingEntryProfile()
        {
            CreateMap<Domain.Entities.AccountingEntry, Domain.Models.AccountingEntry>()
                .ForMember(target => target.Id, source => source.MapFrom(prop => prop.Id))
                .ForMember(target => target.Side, source => source.MapFrom(prop => prop.Side))
                .ForMember(target => target.Amount, source => source.MapFrom(prop => prop.Amount))
                .ForSourceMember(source => source.LedgerAccountId, option => option.DoNotValidate())
                .ForSourceMember(source => source.LedgerAccount, option => option.DoNotValidate())
                .ForSourceMember(source => source.FinancialTransactionId, option => option.DoNotValidate())
                .ForSourceMember(source => source.FinancialTransaction, option => option.DoNotValidate());

            CreateMap<Domain.Models.AccountingEntry, Domain.Entities.AccountingEntry>()
                .ForMember(target => target.Id, source => source.MapFrom(prop => prop.Id))
                .ForMember(target => target.Side, source => source.MapFrom(prop => prop.Side))
                .ForMember(target => target.Amount, source => source.MapFrom(prop => prop.Amount))
                .ForMember(target => target.LedgerAccountId, option => option.Ignore())
                .ForMember(target => target.LedgerAccount, option => option.Ignore())
                .ForMember(target => target.FinancialTransactionId, option => option.Ignore())
                .ForMember(target => target.FinancialTransaction, option => option.Ignore());
        }
    }
}
