using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CRM.DAL.Database.Entities.Common;
using CRM.DAL.Repository;
using CRM.Dto.Common;
using CRM.WebAPI.Services.Interfaces.Common;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebAPI.Services.Common
{
    public class PropertyDetailViewService : IPropertyDetailViewService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public PropertyDetailViewService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public PageResult<PropertyDetailViewDto> GetAllAsync(ODataQueryOptions<PropertyDetailView> options)
        {
            var items = _repository.CRMContext.PropertyDetailView.AsQueryable();
            int count;
            if (options.OrderBy != null)
                items = options.OrderBy.ApplyTo(items);  //perform sort 
            if (options.Filter != null)
                items = options.Filter.ApplyTo(items, new ODataQuerySettings()).Cast<PropertyDetailView>();  //perform filter 
            count = items.ToList().Count;
            if (options.Skip != null)
                items = options.Skip.ApplyTo(items, new ODataQuerySettings());  //perform skip 
            if (options.Top != null)
                items = options.Top.ApplyTo(items, new ODataQuerySettings());  //perform take 
            var result = _mapper.Map<List<PropertyDetailViewDto>>(items);
            return new PageResult<PropertyDetailViewDto>(result, null, count);
        }

        public async Task<PropertyDetailViewDto> GetAsyncByPropertyCode(string propertyCode)
        {
            var result = await _repository.CRMContext.PropertyDetailView.FirstOrDefaultAsync(x=>x.PropertyCode == propertyCode);
            var propertyDetailViewDto = _mapper.Map<PropertyDetailViewDto>(result);
            return propertyDetailViewDto;
        }
    }
}
