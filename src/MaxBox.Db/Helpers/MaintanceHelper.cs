using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;

namespace MaxBox.Db.Helpers
{
    public class MaintanceHelper
    {
        public void DeleteAllTables<TContext>(bool areYouSure = false) where TContext : DbContext, new()
        {
            if (!areYouSure)
            {
                throw new Exception("You must be certain to delete all your data!",
                    new Exception("Add the true paramater for the areYouSure bool"));
            }
            var context = new TContext();
            List<string> list =
                context.Database.SqlQuery<string>(
                    "SELECT 'DROP TABLE [' + SCHEMA_NAME(schema_id) + '].[' + name + ']' FROM sys.tables;SELECT 'DROP PROCEDURE [' + SCHEMA_NAME(schema_id) + '].[' + name + ']' FROM sys.procedures;")
                    .ToList();
            ;
            int maxloops = list.Count;
            int currentloops = 0;
            bool wasThereAnError = false;
            do
            {
                if (currentloops == maxloops)
                {
                    throw new Exception("there was  problem deleting the database");
                }
                wasThereAnError = false;
                foreach (string tabledeletesql in list.ToList())
                {
                    try
                    {
                        context.Database.ExecuteSqlCommand(tabledeletesql);
                        list.Remove(tabledeletesql);
                        Debug.WriteLine(tabledeletesql);
                    }
                    catch (Exception)
                    {
                        wasThereAnError = true;
                    }
                }
            } while (wasThereAnError);
        }

        public void CreateAllTables<TContext>() where TContext : DbContext, new()
        {
            using (var context = new TContext())
            {
                context.Database.Initialize(true);
            }
        }

        public void ReCreateTables<TContext>(bool areYouSure = false) where TContext : DbContext, new()
        {
            DeleteAllTables<TContext>(areYouSure);
            CreateAllTables<TContext>();
        }
    }
}