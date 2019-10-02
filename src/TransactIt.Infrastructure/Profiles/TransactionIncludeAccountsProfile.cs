using AutoMapper;

namespace TransactIt.Infrastructure.Profiles
{
    public class TransactionIncludeAccountsProfile : Profile
    {
        public TransactionIncludeAccountsProfile()
        {
            CreateMap<Domain.Entities.Transaction, Domain.Models.TransactionIncludeAccounts>()
                .ForMember(target => target.Id, source => source.MapFrom(prop => prop.Id))
                .ForMember(target => target.IdentifyingCode, source => source.MapFrom(prop => prop.IdentifyingCode))
                .ForMember(target => target.Description, source => source.MapFrom(prop => prop.Description))
                .ForMember(target => target.TransactionDate, source => source.MapFrom(prop => prop.TransactionDate))
                .ForMember(target => target.CreatedDate, source => source.MapFrom(prop => prop.CreatedDate))
                .ForMember(target => target.AccountingEntries, source => source.MapFrom(prop => prop.AccountingEntries))
                .ForSourceMember(source => source.LedgerId, option => option.DoNotValidate())
                .ForSourceMember(source => source.Ledger, option => option.DoNotValidate());
        }
    }
}
