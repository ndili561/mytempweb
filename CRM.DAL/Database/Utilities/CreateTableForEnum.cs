using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.DAL.Database.Utilities
{
    public static class EnumSeeder
    {
        /// <summary>
        /// Populate a table with values based on defined enum values.
        /// </summary>
        /// <typeparam name="TEnum">Type of the enum</typeparam>
        /// <param name="context">A DbContext to use to run queries against
        /// the database.</param>
        /// <param name="idField">Id field, that should be populated with
        /// the numeric value of the enum.</param>
        /// <param name="descriptionField">Description field, that should be
        /// populated with the contents of the Description attribute (if
        /// there is any defined).</param>
        /// <param name="tableName">Name of the table. Assumed to be the same
        /// as the enum name plus an "s" for pluralization if nothing
        /// else is defined</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design",
          "CA1062:Validate arguments of public methods", MessageId = "0"),
          System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design",
          "CA1004:GenericMethodsShouldProvideTypeParameter")]
        public static string GetSqlForEnumSeed<TEnum>(string idField = "Id",string descriptionField = "description", string tableName = null)
        {
            if (tableName == null)
            {
                tableName = typeof(TEnum).Name ;
            }

            var commandBuilder = new StringBuilder();

            commandBuilder.AppendFormat(@"IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'{0}') AND type in (N'U')){2} BEGIN {2}" +
                                        "DROP TABLE {0} {2} END {2} " +
                                        "CREATE TABLE {0} ({2}" +
                "Id {1} NOT NULL PRIMARY KEY,{2}" +
                "Description NVARCHAR(50)){2}{2}", tableName, GetIdType<TEnum>(), Environment.NewLine);

            AddValues<TEnum>(commandBuilder, tableName);

            string descriptionUpdate = descriptionField == null ? string.Empty :
                string.Format(CultureInfo.InvariantCulture,
                "WHEN MATCHED THEN UPDATE\n" +
                "SET dst.{0} = src.Description\n", descriptionField);

            string descriptionInsert = descriptionField == null ? string.Empty :
                ", src.Description";

            string descriptionInFieldList = descriptionField == null ? string.Empty :
                ", " + descriptionField;

            commandBuilder.AppendFormat(CultureInfo.InvariantCulture,
                "MERGE {0} dst\n" +
                "USING {0} src\n" +
                "ON (src.Id = dst.{1})\n" +
                "{2}" +
                "WHEN NOT MATCHED THEN\n" +
                "INSERT ({1}{3}) VALUES (src.Id{4})\n" +
                "WHEN NOT MATCHED BY SOURCE THEN DELETE;\n\n",
                tableName, idField, descriptionUpdate, descriptionInFieldList, descriptionInsert);

           // context.Sql(commandBuilder.ToString());
             return commandBuilder.ToString();
        }

        private static void AddValues<TEnum>(StringBuilder commandBuilder, string tableName)
        {
            var values = System.Enum.GetValues(typeof(TEnum));

            if (values.Length > 0)
            {
                commandBuilder.AppendFormat(CultureInfo.InvariantCulture,
                    "INSERT {0} VALUES\n\n", tableName);

                var descriptions = GetDescriptions<TEnum>();

                bool firstValue = true;
                foreach (var v in values)
                {
                    if (firstValue)
                    {
                        firstValue = false;
                    }
                    else
                    {
                        commandBuilder.AppendFormat(CultureInfo.InvariantCulture, ",\n");
                    }
                    string valueString = v.ToString();

                    commandBuilder.AppendFormat(CultureInfo.InvariantCulture, "({0}, '{1}')",
                        (int)v, descriptions[valueString]);
                }

                commandBuilder.AppendFormat(CultureInfo.InvariantCulture, "\n\n");
            }
        }

        private static IDictionary<string, string> GetDescriptions<TEnum>()
        {
            return typeof(TEnum).GetMembers(BindingFlags.Static | BindingFlags.Public)
                .Select(m => new
                {
                    Name = m.Name,
                    Description = m.GetCustomAttributes(typeof(DescriptionAttribute), true)
                    .Cast<DescriptionAttribute>().SingleOrDefault()
                })
                .ToDictionary(a => a.Name,
                a => a.Description == null ? a.Name : a.Description.Description);
        }

        private static string GetIdType<TEnum>()
        {
            var underlyingType = System.Enum.GetUnderlyingType(typeof(TEnum));

            if (underlyingType == typeof(int))
            {
                return "INT";
            }

            if (underlyingType == typeof(short))
            {
                return "SMALLINT";
            }

            if (underlyingType == typeof(byte))
            {
                return "TINYINT";
            }

            throw new NotImplementedException();
        }
    }
}
