using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using CRM.DAL.Database.Utilities;

namespace CRM.DAL.Migrations
{
    public partial class EnableTemporalTable : Migration
    {
        private string schemaName = "Logging";
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"CREATE SCHEMA {schemaName}");
            var entityTypes = base.TargetModel.GetEntityTypes();
            foreach (var entityType in entityTypes)
            {
                migrationBuilder.AddAsTemporalTable(entityType, schemaName);
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var entityTypes = base.TargetModel.GetEntityTypes();
            foreach (var entityType in entityTypes)
            {
                migrationBuilder.RemoveAsTemporalTable(entityType, schemaName);
            }
            migrationBuilder.Sql($"DROP SCHEMA {schemaName}");
        }
    }

}
