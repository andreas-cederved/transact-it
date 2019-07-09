using AutoMapper;

namespace TransactIt.Infrastructure.Profiles
{
    public class TransactionTemplateProfile : Profile
    {
        public TransactionTemplateProfile()
        {
            CreateMap<Domain.Entities.TransactionTemplate, Domain.Models.TransactionTemplate>()
                .ForMember(target => target.Id, source => source.MapFrom(prop => prop.Id))
                .ForMember(target => target.Name, source => source.MapFrom(prop => prop.Name))
                .ForMember(target => target.Description, source => source.MapFrom(prop => prop.Description))
                .ForMember(target => target.DefaultTransactionDescription, source => source.MapFrom(prop => prop.DefaultTransactionDescription))
                .ForMember(target => target.CreatedDate, source => source.MapFrom(prop => prop.CreatedDate))
                .ForMember(target => target.TransactionTemplateRules, source => source.MapFrom(prop => prop.TransactionTemplateRules))
                .ForSourceMember(source => source.LedgerId, option => option.DoNotValidate())
                .ForSourceMember(source => source.Ledger, option => option.DoNotValidate());

            CreateMap<Domain.Models.TransactionTemplate, Domain.Entities.TransactionTemplate>()
                .ForMember(target => target.Id, source => source.MapFrom(prop => prop.Id))
                .ForMember(target => target.Name, source => source.MapFrom(prop => prop.Name))
                .ForMember(target => target.Description, source => source.MapFrom(prop => prop.Description))
                .ForMember(target => target.DefaultTransactionDescription, source => source.MapFrom(prop => prop.DefaultTransactionDescription))
                .ForMember(target => target.CreatedDate, source => source.MapFrom(prop => prop.CreatedDate))
                .ForMember(target => target.TransactionTemplateRules, source => source.MapFrom(prop => prop.TransactionTemplateRules))
                .ForMember(target => target.LedgerId, option => option.Ignore())
                .ForMember(target => target.Ledger, option => option.Ignore());
        }
    }
}
