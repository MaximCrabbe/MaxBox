using System;
using System.Data.Entity;
using System.Linq;

namespace MaxBox.Db.Services
{
    public class MaintanceService : IMaintanceService
    {
        public void DeleteAllTables(DbContext context, bool areYouSure = false)
        {
            if (!areYouSure)
            {
                throw new Exception("You must be sure to delete all your data!");
            }
            var list = context.Database.SqlQuery<string>("SELECT 'DROP TABLE [' + SCHEMA_NAME(schema_id) + '].[' + name + ']' FROM sys.tables;SELECT 'DROP PROCEDURE [' + SCHEMA_NAME(schema_id) + '].[' + name + ']' FROM sys.procedures;").ToList(); ;
            var maxloops = list.Count;
            int currentloops = 0;
            bool wasThereAnError = false;
            do
            {
                if (currentloops == maxloops)
                {
                    throw new Exception("there was  problem deleting the database");
                }
                wasThereAnError = false;
                foreach (var tabledeletesql in list.ToList())
                {
                    try
                    {
                        context.Database.ExecuteSqlCommand(tabledeletesql);
                        list.Remove(tabledeletesql);
                    }
                    catch (Exception)
                    {
                        wasThereAnError = true;
                    }
                }
            } while (wasThereAnError == true);
        }
    }
}
