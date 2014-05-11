﻿using System;
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
                throw new Exception("You must be sure to delete all your data!");
            }
            var context = new TContext();
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
                        Debug.WriteLine(tabledeletesql);

                    }
                    catch (Exception)
                    {
                        wasThereAnError = true;
                    }
                }
            } while (wasThereAnError == true);
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
