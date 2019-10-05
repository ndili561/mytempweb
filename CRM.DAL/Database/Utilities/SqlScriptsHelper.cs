using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using CRM.DAL.Enum;
using CRM.DAL.Extension;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.DAL.Database.Utilities
{
    public static class SqlScriptsHelper
    {
        public static void ExecuteSqlScripts(CRMContext context)
        {
            var path = Assembly.GetExecutingAssembly().Location;
            var sqlFolderPath = Path.Combine(Directory.GetParent(path).FullName, "Migrations", "DefaultData");
            var files = GetSqlFiles(sqlFolderPath);
            //throw new Exception(files[0]);
            if (context != null)
            {
                try
                {
                    files.ForEach(f => context.Database.ExecuteSqlCommand(File.ReadAllText(f)));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }  
            }
               
        }
        public static string GetSqlScripts(string filePath)
        {
            string sqlContent;
            using (var reader = new StreamReader(filePath))
            {
                sqlContent = reader.ReadToEnd();
            }
            return sqlContent;
            //var files = GetSqlFiles(folderPath);
            ////throw new Exception(files[0]);
            //if (builder != null)
            //{
            //    if (files.Count == 0)
            //    {
            //        Debug.WriteLine("No file found : ");
            //    }
            //    foreach (var file in files)
            //    {
            //        Debug.WriteLine("FileName : "+ file.GetFullName());
            //    }
            //    files.ForEach(f => );
            //}

        }
        private static List<string> GetSqlFiles(string path)
        {
            var files1 = Directory.GetFiles(path);
            var files = files1
                //.Where(f => f.StartsWith("defaultdata", StringComparison.InvariantCultureIgnoreCase) == false)
                .OrderBy(f => f)
               // .Select(f => baseDir + "." + f)
                .ToList();
            return files;
        }

        public static string GetStoredProcedureSql(string storedProcedureName)
        {
            // var files = GetSqlFiles("StoredProcedure");
            var name = storedProcedureName + ".sql";
            var list = Array.Find(Assembly.GetExecutingAssembly().GetManifestResourceNames(), f => f.EndsWith(name));
            // var file = files.FirstOrDefault(f => f.Contains(storedProcedureName));
            return File.ReadAllText(list);
        }
        public static void CreateEnumAsTables(MigrationBuilder builder)
        {
            //builder.GetSqlForEnumSeed<AccessLevel>();
            //builder.GetSqlForEnumSeed<TaskCss>();
            //builder.GetSqlForEnumSeed<TaskDuration>();
            //builder.GetSqlForEnumSeed<PropertyInterestStatus>();
        }
    }
}