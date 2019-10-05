using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Common;

namespace CRM.WebAPI.Services.Interfaces.Common
{
    public interface IPdfService
    {
        Task<Document> CreateTemplatePdf();
    }
}