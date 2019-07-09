using AutoMapper;

namespace TransactIt.Infrastructure.Profiles
{
    public class MainAccountGroupProfile : Profile
    {
        public MainAccountGroupProfile()
        {
            CreateMap<Domain.Entities.MainAccountGroup, Domain.Models.MainAccountGroup>()
                .ForMember(target => target.Id, source => source.MapFrom(prop => prop.Id))
                .ForMember(target => target.Number, source => source.MapFrom(prop => prop.Number))
                .ForMember(target => target.Name, source => source.MapFrom(prop => prop.Name))
                .ForMember(target => target.Description, source => source.MapFrom(prop => prop.Description))
                .ForMember(target => target.CreatedDate, source => source.MapFrom(prop => prop.CreatedDate))
                .ForMember(target => target.SubAccountGroups, source => source.MapFrom(prop => prop.SubAccountGroups))
                .ForSourceMember(source => source.LedgerId, option => option.DoNotValidate())
                .ForSourceMember(source => source.Ledger, option => option.DoNotValidate());

            CreateMap<Domain.Models.MainAccountGroup, Domain.Entities.MainAccountGroup>()
                .ForMember(target => target.Id, source => source.MapFrom(prop => prop.Id))
                .ForMember(target => target.Number, source => source.MapFrom(prop => prop.Number))
                .ForMember(target => target.Name, source => source.MapFrom(prop => prop.Name))
                .ForMember(target => target.Description, source => source.MapFrom(prop => prop.Description))
                .ForMember(target => target.CreatedDate, source => source.MapFrom(prop => prop.CreatedDate))
                .ForMember(target => target.SubAccountGroups, option => option.Ignore())
                .ForMember(target => target.LedgerId, option => option.Ignore())
                .ForMember(target => target.Ledger, option => option.Ignore());
        }
    }
}
