using System.Data.Entity;

namespace MaxBox.Db.Services
{
    public interface IMaintanceService
    {
        void DeleteAllTables<TContext>(bool areYouSure = false) where TContext : DbContext, new();
    }
}