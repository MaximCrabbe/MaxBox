using System.Data.Entity;

namespace MaxBox.Db.Services
{
    public interface IMaintanceService
    {
        void DeleteAllTables(DbContext context, bool areYouSure = false);
    }
}