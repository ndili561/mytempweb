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
    public class PersonAlertCommentService : IPersonAlertCommentService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public PersonAlertCommentService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PersonAlertCommentDto>> GetAsync()
        {
            var list = await _repository.Get<PersonAlertComment>().ToListAsync();
            return _mapper.Map<IList<PersonAlertCommentDto>>(list);
        }

        public async Task<bool> PersonAlertCommentExistsAsync(int id)
        {
            return await _repository.Get<PersonAlertComment>().AnyAsync(p => p.Id == id);
        }



        public async Task<PersonAlertCommentDto> GetAsync(int id)
        {
            var personAlertComment = await _repository.CRMContext.PersonAlertComments
                .Include(x => x.PersonAlert)
                .ThenInclude(x => x.AlertStatus)
                .FirstOrDefaultAsync(x => x.Id == id);
            return _mapper.Map<PersonAlertCommentDto>(personAlertComment);
        }

        public async Task<int> SaveAsync(PersonAlertCommentDto personAlertComment)
        {
            var result = await SaveAndReturnEntityAsync(personAlertComment);
            return result.Id;
        }

        public async Task<BaseEntity> SaveAndReturnEntityAsync(PersonAlertCommentDto entityDto)
        {
            var personAlertComment = _mapper.Map<PersonAlertComment>(entityDto);
            if (entityDto.PersonAlert != null)
            {
                var personAlert = _repository.CRMContext.PersonAlerts.FirstOrDefaultAsync(x => x.Id == entityDto.PersonAlertId).Result;
                if (personAlert.AlertStatusId != entityDto.PersonAlert.AlertStatusId)
                {
                    personAlert.AlertStatusId = entityDto.PersonAlert.AlertStatusId;
                    await _repository.SaveAndReturnEntityAsync(personAlert);
                }
                personAlertComment.PersonAlert = null;
            }
            if (entityDto.Id == 0)
            {
                var loggedUsed = _repository.CRMContext.IdentityUserView.FirstOrDefault(
                        x => x.Id == _repository.CRMContext.CurrentLoggedUserId);
                personAlertComment.CreatedBy = loggedUsed?.FirstName + " " + loggedUsed?.LastName;
                personAlertComment.CreatedOn = DateTime.Now;
            }
            var result = await _repository.SaveAndReturnEntityAsync(personAlertComment);
            result.SuccessMessage = "The data is saved successfully";
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var personAlertComment = await _repository.GetAsync<PersonAlertComment>(id);

            if (personAlertComment == null)
            {
                return false;
            }

            var result = await _repository.HardDeleteAsync(personAlertComment);

            return result;
        }
    }
}
