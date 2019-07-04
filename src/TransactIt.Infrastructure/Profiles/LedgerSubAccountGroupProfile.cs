using AutoMapper;

namespace TransactIt.Infrastructure.Profiles
{
    public class LedgerSubAccountGroupProfile : Profile
    {
        public LedgerSubAccountGroupProfile()
        {
            CreateMap<Domain.Entities.LedgerSubAccountGroup, Domain.Models.LedgerSubAccountGroup>()
                .ForMember(target => target.Id, source => source.MapFrom(prop => prop.Id))
                .ForMember(target => target.Number, source => source.MapFrom(prop => prop.Number))
                .ForMember(target => target.Name, source => source.MapFrom(prop => prop.Name))
                .ForMember(target => target.Description, source => source.MapFrom(prop => prop.Description))
                .ForMember(target => target.CreatedDate, source => source.MapFrom(prop => prop.CreatedDate))
                .ForMember(target => target.LedgerAccounts, source => source.MapFrom(prop => prop.LedgerAccounts))
                .ForSourceMember(source => source.LedgerMainAccountGroupId, option => option.DoNotValidate())
                .ForSourceMember(source => source.LedgerMainAccountGroup, option => option.DoNotValidate());

            CreateMap<Domain.Models.LedgerSubAccountGroup, Domain.Entities.LedgerSubAccountGroup>()
                .ForMember(target => target.Id, source => source.MapFrom(prop => prop.Id))
                .ForMember(target => target.Number, source => source.MapFrom(prop => prop.Number))
                .ForMember(target => target.Name, source => source.MapFrom(prop => prop.Name))
                .ForMember(target => target.Description, source => source.MapFrom(prop => prop.Description))
                .ForMember(target => target.CreatedDate, source => source.MapFrom(prop => prop.CreatedDate))
                .ForMember(target => target.LedgerAccounts, option => option.Ignore())
                .ForMember(target => target.LedgerMainAccountGroupId, option => option.Ignore())
                .ForMember(target => target.LedgerMainAccountGroup, option => option.Ignore());
        }
    }
}
