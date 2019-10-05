using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using CRM.DAL.Database.Utilities;
using CRM.DAL.Enum;

namespace CRM.DAL.Migrations
{
    public partial class MigrateDataToCrm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sqlScript = new StringBuilder();
            var folderPath = Path.Combine(Directory.GetParent(Assembly.GetExecutingAssembly().Location).FullName, "Migrations", "DefaultData");
            var files = Directory.GetFiles(folderPath);
            foreach (var file in files)
            {
                var sqlFolderPath = Path.Combine(folderPath, file);
                var scriptLines = SqlScriptsHelper.GetSqlScripts(sqlFolderPath).Split("\n");
                foreach (var scriptLine in scriptLines.Where(x => !string.IsNullOrWhiteSpace(x)))
                {
                    sqlScript.Append(scriptLine);
                }
            }
            sqlScript.AppendLine(Environment.NewLine);
            sqlScript.Append(EnumSeeder.GetSqlForEnumSeed<AccessLevel>());
            sqlScript.Append(EnumSeeder.GetSqlForEnumSeed<TaskCss>());
            sqlScript.Append(EnumSeeder.GetSqlForEnumSeed<PropertyInterestStatus>());
            var finalScriptFilePath = folderPath.Replace(@"\CRM.WebAPI\bin\Debug\netcoreapp2.0\Migrations\DefaultData", @"\CRM.DAL\Migrations\DefaultData\FinalScript.sql");
            File.WriteAllText(finalScriptFilePath, sqlScript.ToString());
            migrationBuilder.Sql(sqlScript.ToString());
            Console.WriteLine("In case data is not migrated please run FinalScript.SQL. The file Path is :" + finalScriptFilePath);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
