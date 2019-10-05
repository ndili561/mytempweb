using CRM.DAL.Database;

namespace CRM.WebAPI.BLL
{
    public abstract class BaseBll
    {
        protected CRMContext Context;
        protected BaseBll(CRMContext context)
        {
            Context = context;
        }
    }
}
