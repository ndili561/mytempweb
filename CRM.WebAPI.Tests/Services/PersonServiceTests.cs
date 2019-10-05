using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities.Customer;
using CRM.DAL.Repository;
using CRM.WebAPI.Infrastructure.AutoMapper;
using CRM.WebAPI.Services.Customer;
using Moq;
using Xunit;

namespace CRM.WebAPI.Tests.Services
{
    public class PersonServiceTests : BaseTests
    {
        private readonly Mock<IRepository> _reposiotryMock;
        private readonly PersonService _personService;

        public PersonServiceTests()
        {
            _reposiotryMock = new Mock<IRepository>();
            _personService = new PersonService(_reposiotryMock.Object, new MapperFactory().Build());
        }

        //[Fact]
        //public void GetAsync_ReturnsListOfActivePeople()
        //{
        //    // arrange
        //    _reposiotryMock.Setup(m => m.Get<Person>())
        //        .Returns(CreateDbSetForAsync(new List<Person> { new Person { } }));

        //    // act
        //    var result = _personService.GetAsync().Result;

        //    // assert
        //    Assert.Single(result);
        //}

       
        [Fact]
        public void DeleteAsync_DoesNotCallDelete_PersonDoesNotExist()
        {
            // arrange
            int id = 1;
            _reposiotryMock.Setup(m => m.GetAsync<Person>(id))
                .Returns(Task.FromResult((Person)null));

            // act
            var result = _personService.DeleteAsync(id).Result;

            // assert
            _reposiotryMock.Verify(m => m.HardDeleteAsync(It.IsAny<Person>()), Times.Never);
        }

        [Fact]
        public void DeleteAsync_DoesNotCallDelete_PersonIsDeleted()
        {
            // arrange
            int id = 1;
            _reposiotryMock.Setup(m => m.GetAsync<Person>(id))
                .Returns(Task.FromResult(new Person {Id = id}));

            // act
            var result = _personService.DeleteAsync(id).Result;

            // assert
            _reposiotryMock.Verify(m => m.HardDeleteAsync(It.IsAny<Person>()), Times.Never);
        }

        [Fact]
        public void DeleteAsync_CallDelete_PersonNotDeleted()
        {
            // arrange
            int id = 1;
            _reposiotryMock.Setup(m => m.GetAsync<Person>(id))
                .Returns(Task.FromResult(new Person { Id = id }));

            // act
            var result = _personService.DeleteAsync(id).Result;

            // assert
            _reposiotryMock.Verify(m => m.HardDeleteAsync(It.IsAny<Person>()), Times.Once);
        }

        [Fact]
        public void DeleteAsync_ReturnsTrue_PersonNotDeleted()
        {
            // arrange
            int id = 1;
            _reposiotryMock.Setup(m => m.GetAsync<Person>(id))
                .Returns(Task.FromResult(new Person { Id = id}));
            _reposiotryMock.Setup(m => m.HardDeleteAsync(It.IsAny<Person>()))
                .Returns(Task.FromResult(true));

            // act
            var result = _personService.DeleteAsync(id).Result;

            // assert
            Assert.True(result);
        }
    }
}
