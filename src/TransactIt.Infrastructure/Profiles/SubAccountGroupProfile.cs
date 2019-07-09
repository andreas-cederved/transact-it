using AutoMapper;

namespace TransactIt.Infrastructure.Profiles
{
    public class SubAccountGroupProfile : Profile
    {
        public SubAccountGroupProfile()
        {
            CreateMap<Domain.Entities.SubAccountGroup, Domain.Models.SubAccountGroup>()
                .ForMember(target => target.Id, source => source.MapFrom(prop => prop.Id))
                .ForMember(target => target.Number, source => source.MapFrom(prop => prop.Number))
                .ForMember(target => target.Name, source => source.MapFrom(prop => prop.Name))
                .ForMember(target => target.Description, source => source.MapFrom(prop => prop.Description))
                .ForMember(target => target.CreatedDate, source => source.MapFrom(prop => prop.CreatedDate))
                .ForMember(target => target.Accounts, source => source.MapFrom(prop => prop.Accounts))
                .ForSourceMember(source => source.MainAccountGroupId, option => option.DoNotValidate())
                .ForSourceMember(source => source.MainAccountGroup, option => option.DoNotValidate());

            CreateMap<Domain.Models.SubAccountGroup, Domain.Entities.SubAccountGroup>()
                .ForMember(target => target.Id, source => source.MapFrom(prop => prop.Id))
                .ForMember(target => target.Number, source => source.MapFrom(prop => prop.Number))
                .ForMember(target => target.Name, source => source.MapFrom(prop => prop.Name))
                .ForMember(target => target.Description, source => source.MapFrom(prop => prop.Description))
                .ForMember(target => target.CreatedDate, source => source.MapFrom(prop => prop.CreatedDate))
                .ForMember(target => target.Accounts, option => option.Ignore())
                .ForMember(target => target.MainAccountGroupId, option => option.Ignore())
                .ForMember(target => target.MainAccountGroup, option => option.Ignore());
        }
    }
}
