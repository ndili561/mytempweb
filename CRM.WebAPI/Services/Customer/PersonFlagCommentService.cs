using System;
using AutoMapper;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Customer;
using CRM.DAL.Repository;
using CRM.Dto.Customer;
using CRM.WebAPI.Services.Interfaces.Customer;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.WebAPI.Services.Customer
{
    public class PersonFlagCommentService : IPersonFlagCommentService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public PersonFlagCommentService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PersonFlagCommentDto>> GetAsync()
        {
            var list = await _repository.Get<PersonFlagComment>().ToListAsync();
            return _mapper.Map<IList<PersonFlagCommentDto>>(list);
        }

        public async Task<bool> PersonFlagCommentExistsAsync(int id)
        {
            return await _repository.Get<PersonFlagComment>().AnyAsync(p => p.Id == id);
        }

       
        public async Task<PersonFlagCommentDto> GetAsync(int id)
        {
            var personFlagComment = await _repository.GetAsync<PersonFlagComment>(id);

            return _mapper.Map<PersonFlagCommentDto>(personFlagComment);
        }

        public async Task<int> SaveAsync(PersonFlagCommentDto personFlagComment)
        {
            var result = await SaveAndReturnEntityAsync(personFlagComment);
            return result.Id;
        }

        public async Task<BaseEntity> SaveAndReturnEntityAsync(PersonFlagCommentDto entityDto)
        {
            var personFlagComment = _mapper.Map<PersonFlagComment>(entityDto);
            if (entityDto.Id == 0)
            {
                var loggedUsed = _repository.CRMContext.IdentityUserView.FirstOrDefault(
                    x => x.Id == _repository.CRMContext.CurrentLoggedUserId);
                personFlagComment.CreatedBy = loggedUsed?.FirstName + " " + loggedUsed?.LastName;
                personFlagComment.CreatedOn = DateTime.Now;
            }
            var result = await _repository.SaveAndReturnEntityAsync(personFlagComment);
            result.SuccessMessage = "The data is saved successfully";
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var personFlagComment = await _repository.GetAsync<PersonFlagComment>(id);

            if (personFlagComment == null)
            {
                return false;
            }

            var result = await _repository.HardDeleteAsync(personFlagComment);

            return result;
        }
    }
}
