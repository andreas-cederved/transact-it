using AutoMapper;

namespace TransactIt.Infrastructure.Profiles
{
    public class AccountingEntryIncludeAccountProfile : Profile
    {
        public AccountingEntryIncludeAccountProfile()
        {
            CreateMap<Domain.Entities.AccountingEntry, Domain.Models.AccountingEntryIncludeAccount>()
                .ForMember(target => target.Side, source => source.MapFrom(prop => prop.Side))
                .ForMember(target => target.Amount, source => source.MapFrom(prop => prop.Amount))
                .ForMember(target => target.AccountId, source => source.MapFrom(prop => prop.AccountId))
                .ForMember(target => target.Account, source => source.MapFrom(prop => new Domain.Models.Account {
                    CreatedDate = prop.Account.CreatedDate,
                    Description = prop.Account.Description,
                    Id = prop.Account.Id,
                    Name = prop.Account.Name,
                    Number = prop.Account.Number
                } ))
                .ForSourceMember(source => source.Id, option => option.DoNotValidate())
                .ForSourceMember(source => source.Account, option => option.DoNotValidate())
                .ForSourceMember(source => source.TransactionId, option => option.DoNotValidate())
                .ForSourceMember(source => source.Transaction, option => option.DoNotValidate());
        }
    }
}
