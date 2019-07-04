﻿using AutoMapper;

namespace TransactIt.Infrastructure.Profiles
{
    public class LedgerAccountProfile : Profile
    {
        public LedgerAccountProfile()
        {
            CreateMap<Domain.Entities.LedgerAccount, Domain.Models.LedgerAccount>()
                .ForMember(target => target.Id, source => source.MapFrom(prop => prop.Id))
                .ForMember(target => target.Number, source => source.MapFrom(prop => prop.Number))
                .ForMember(target => target.Name, source => source.MapFrom(prop => prop.Name))
                .ForMember(target => target.Description, source => source.MapFrom(prop => prop.Description))
                .ForMember(target => target.CreatedDate, source => source.MapFrom(prop => prop.CreatedDate))
                .ForMember(target => target.AccountingEntries, source => source.MapFrom(prop => prop.AccountingEntries))
                .ForSourceMember(source => source.LedgerSubAccountGroupId, option => option.DoNotValidate())
                .ForSourceMember(source => source.LedgerSubAccountGroup, option => option.DoNotValidate());

            CreateMap<Domain.Models.LedgerAccount, Domain.Entities.LedgerAccount>()
                .ForMember(target => target.Id, source => source.MapFrom(prop => prop.Id))
                .ForMember(target => target.Number, source => source.MapFrom(prop => prop.Number))
                .ForMember(target => target.Name, source => source.MapFrom(prop => prop.Name))
                .ForMember(target => target.Description, source => source.MapFrom(prop => prop.Description))
                .ForMember(target => target.CreatedDate, source => source.MapFrom(prop => prop.CreatedDate))
                .ForMember(target => target.AccountingEntries, option => option.Ignore())
                .ForMember(target => target.LedgerSubAccountGroupId, option => option.Ignore())
                .ForMember(target => target.LedgerSubAccountGroup, option => option.Ignore());
        }
    }
}
