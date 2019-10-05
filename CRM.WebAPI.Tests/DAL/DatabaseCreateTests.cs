using System.Collections.Generic;
using CRM.DAL.Database;
using CRM.DAL.Database.Entities.Customer;
using CRM.DAL.Database.Entities.Employee;
using CRM.DAL.Database.Entities.Lookup;
using FizzWare.NBuilder;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CRM.WebAPI.Tests.DAL
{
    public class DatabaseCreateTests : BaseTests
    {
        [Fact]
        public void ShouldInsertNewThreat()
        {
            var context = GetInMemoryContext();
            var person = new Person();
            context.Persons.Add(person);
            var entityId = context.SaveChangesAsync().Result;
            Assert.Equal(2, entityId);

        }

        private CRMContext GetInMemoryContext()
        {

            var options = new DbContextOptionsBuilder<CRMContext>()
                .UseSqlite("DataSource=:memory:")
                .EnableSensitiveDataLogging()
                .Options;


            var context = new CRMContext(options);
            context.Database.OpenConnection();
            context.Database.EnsureCreated();
            var user = Builder<User>.CreateNew().Build();
            user.Subject = "identity_id_of_the_user";
            user.UserGroupId = null;
            context.Users.Add(user);
            context.SaveChanges();
            context.RecordCustomAudit = true;
            var lookup = Builder<Lookup>.CreateNew()
                .With(x => x.Genders = Builder<List<Gender>>.CreateNew().Build())
                .With(x => x.Ethnicities = Builder<List<Ethnicity>>.CreateNew().Build())
                .With(x => x.Nationalities = Builder<List<NationalityType>>.CreateNew().Build())
                .With(x => x.Languages = Builder<List<Language>>.CreateNew().Build())
                .Build();
            context.Lookups.Add(lookup);

            //var person = Builder<Person>.CreateNew()
            //    .With(x => x.NationalityType = Builder<NationalityType>.CreateNew().Build())
            //    .With(x => x.Gender = Builder<Gender>.CreateNew().Build())
            //    .With(x => x.Ethnicity = Builder<Ethnicity>.CreateNew().Build())
            //    .With(x => x.PreferredLanguage = Builder<Language>.CreateNew().Build())
            //    .Build();
            //context.Persons.Add(person);
            context.CurrentLoggedUserId = user.Subject;
            context.SaveChanges();
            return context;
        }
    }
}
