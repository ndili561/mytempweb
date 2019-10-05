using AutoMapper;
using CRM.DAL.Database;
using CRM.Dto.Lookup;
using CRM.WebAPI.BLL.Interface;
using System.Collections.Generic;
using System.Linq;

namespace CRM.WebAPI.BLL
{
    public class LookupBll : BaseBll, ILookupBll
    {
        private readonly IMapper _mapper;
        public LookupBll(IMapper mapper, CRMContext context) : base(context)
        {
            _mapper = mapper;
        }

        public LookupDto Get()
        {
            var entity = new LookupDto
            {
                Titles = _mapper.Map<List<TitleDto>>(Context.Titles.ToList()),
                Genders = _mapper.Map<List<GenderDto>>(Context.Genders.ToList()),
                Ethnicities = _mapper.Map<List<EthnicityDto>>(Context.Ethnicities.ToList()),
                Nationalities = _mapper.Map<List<NationalityTypeDto>>(Context.NationalityTypes.ToList()),
                Languages = _mapper.Map<List<LanguageDto>>(Context.Languages.ToList()),
                DocumentTypes = _mapper.Map<List<DocumentTypeDto>>(Context.DocumentTypes.ToList()),
                ContactByOptions = _mapper.Map<List<ContactByOptionDto>>(Context.ContactByOptions.ToList()),
                Relationships = _mapper.Map<List<RelationshipDto>>(Context.Relationships.ToList()),
                ContactTypes = _mapper.Map<List<ContactTypeDto>>(Context.ContactTypes.ToList())
            };
            return entity;
        }
    }
}
