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
                .ForMember(target => target.AccountId, source => source.MapFrom(prop => prop.AccountId))
                .ForSourceMember(source => source.Account, option => option.DoNotValidate())
                .ForSourceMember(source => source.TransactionId, option => option.DoNotValidate())
                .ForSourceMember(source => source.Transaction, option => option.DoNotValidate());

            CreateMap<Domain.Models.AccountingEntry, Domain.Entities.AccountingEntry>()
                .ForMember(target => target.Id, source => source.MapFrom(prop => prop.Id))
                .ForMember(target => target.Side, source => source.MapFrom(prop => prop.Side))
                .ForMember(target => target.Amount, source => source.MapFrom(prop => prop.Amount))
                .ForMember(target => target.AccountId, source => source.MapFrom(prop => prop.AccountId))
                .ForMember(target => target.Account, option => option.Ignore())
                .ForMember(target => target.TransactionId, option => option.Ignore())
                .ForMember(target => target.Transaction, option => option.Ignore());
        }
    }
}
