using AutoMapper;

namespace TransactIt.Infrastructure.Profiles
{
    public class LedgerMainAccountGroupProfile : Profile
    {
        public LedgerMainAccountGroupProfile()
        {
            CreateMap<Domain.Entities.LedgerMainAccountGroup, Domain.Models.LedgerMainAccountGroup>()
                .ForMember(target => target.Id, source => source.MapFrom(prop => prop.Id))
                .ForMember(target => target.Number, source => source.MapFrom(prop => prop.Number))
                .ForMember(target => target.Name, source => source.MapFrom(prop => prop.Name))
                .ForMember(target => target.Description, source => source.MapFrom(prop => prop.Description))
                .ForMember(target => target.CreatedDate, source => source.MapFrom(prop => prop.CreatedDate))
                .ForMember(target => target.LedgerSubAccountGroups, source => source.MapFrom(prop => prop.LedgerSubAccountGroups))
                .ForSourceMember(source => source.LedgerId, option => option.DoNotValidate())
                .ForSourceMember(source => source.Ledger, option => option.DoNotValidate());

            CreateMap<Domain.Models.LedgerMainAccountGroup, Domain.Entities.LedgerMainAccountGroup>()
                .ForMember(target => target.Id, source => source.MapFrom(prop => prop.Id))
                .ForMember(target => target.Number, source => source.MapFrom(prop => prop.Number))
                .ForMember(target => target.Name, source => source.MapFrom(prop => prop.Name))
                .ForMember(target => target.Description, source => source.MapFrom(prop => prop.Description))
                .ForMember(target => target.CreatedDate, source => source.MapFrom(prop => prop.CreatedDate))
                .ForMember(target => target.LedgerSubAccountGroups, option => option.Ignore())
                .ForMember(target => target.LedgerId, option => option.Ignore())
                .ForMember(target => target.Ledger, option => option.Ignore());
        }
    }
}
