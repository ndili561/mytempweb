using CRM.DAL.Migrations.SQLScript;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CRM.DAL.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddressLine1 = table.Column<string>(nullable: true),
                    AddressLine2 = table.Column<string>(nullable: true),
                    AddressLine3 = table.Column<string>(nullable: true),
                    AddressLine4 = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    PostCode = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.Id);
                });


            migrationBuilder.CreateTable(
                name: "Lookups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lookups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PropertyCode = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.Id);
                });


            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsActive = table.Column<bool>(nullable: false),
                    IsAdministrator = table.Column<bool>(nullable: false),
                    RoleDescription = table.Column<string>(maxLength: 500, nullable: false),
                    RoleName = table.Column<string>(maxLength: 150, nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Comment = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    TaskCss = table.Column<string>(nullable: true),
                    TaskDuration = table.Column<int>(nullable: false),
                    TaskStyle = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmailAddress = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MenuItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApplicationId = table.Column<int>(nullable: false),
                    ControllerName = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    DisplayName = table.Column<string>(maxLength: 50, nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuItems_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "AddressTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 20, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    LookupId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    SortOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AddressTypes_Lookups_LookupId",
                        column: x => x.LookupId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AlertGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 20, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    LookupId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    SortOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlertGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlertGroups_Lookups_LookupId",
                        column: x => x.LookupId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AlertStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 20, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    LookupId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    SortOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlertStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlertStatuses_Lookups_LookupId",
                        column: x => x.LookupId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AlertTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 20, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    LookupId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    SortOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlertTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlertTypes_Lookups_LookupId",
                        column: x => x.LookupId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AntiSocialBehaviourCaseClosureReasons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 20, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsResolvedReason = table.Column<bool>(nullable: false),
                    LookupId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    SortOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AntiSocialBehaviourCaseClosureReasons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AntiSocialBehaviourCaseClosureReasons_Lookups_LookupId",
                        column: x => x.LookupId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AntiSocialBehaviourCaseStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 20, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    LookupId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    SortOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AntiSocialBehaviourCaseStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AntiSocialBehaviourCaseStatuses_Lookups_LookupId",
                        column: x => x.LookupId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AntiSocialBehaviourTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 20, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    LookupId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    SortOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AntiSocialBehaviourTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AntiSocialBehaviourTypes_Lookups_LookupId",
                        column: x => x.LookupId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApplicantTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 20, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    LookupId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    SortOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicantTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicantTypes_Lookups_LookupId",
                        column: x => x.LookupId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContactByOptions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 20, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    LookupId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    SortOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactByOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactByOptions_Lookups_LookupId",
                        column: x => x.LookupId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContactTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 20, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    LookupId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    SortOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactTypes_Lookups_LookupId",
                        column: x => x.LookupId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DocumentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 20, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    LookupId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    SortOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentTypes_Lookups_LookupId",
                        column: x => x.LookupId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmailCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 20, nullable: true),
                    CssClass = table.Column<string>(maxLength: 20, nullable: true),
                    CssStyle = table.Column<string>(maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    LookupId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    SortOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailCategories_Lookups_LookupId",
                        column: x => x.LookupId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmailLabelTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 20, nullable: true),
                    CssClass = table.Column<string>(maxLength: 20, nullable: true),
                    CssStyle = table.Column<string>(maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    LookupId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    SortOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailLabelTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailLabelTypes_Lookups_LookupId",
                        column: x => x.LookupId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmailStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 20, nullable: true),
                    CssClass = table.Column<string>(maxLength: 20, nullable: true),
                    CssStyle = table.Column<string>(maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    LookupId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    SortOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailStatuses_Lookups_LookupId",
                        column: x => x.LookupId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ethnicities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 20, nullable: true),
                    IbsCode = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    LookupId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    SortOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ethnicities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ethnicities_Lookups_LookupId",
                        column: x => x.LookupId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FlagGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 20, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    LookupId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    SortOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlagGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlagGroups_Lookups_LookupId",
                        column: x => x.LookupId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FlagTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 20, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    LookupId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    SortOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlagTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlagTypes_Lookups_LookupId",
                        column: x => x.LookupId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Genders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 20, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    LookupId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    SortOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Genders_Lookups_LookupId",
                        column: x => x.LookupId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ImageGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 20, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    LookupId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    SortOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImageGroups_Lookups_LookupId",
                        column: x => x.LookupId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 20, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    LookupId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    SortOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Languages_Lookups_LookupId",
                        column: x => x.LookupId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NationalityTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 20, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    LookupId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    SortOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NationalityTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NationalityTypes_Lookups_LookupId",
                        column: x => x.LookupId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonCaseStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 20, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    LookupId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    SortOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonCaseStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonCaseStatuses_Lookups_LookupId",
                        column: x => x.LookupId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonCaseTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 20, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    LookupId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    SortOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonCaseTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonCaseTypes_Lookups_LookupId",
                        column: x => x.LookupId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Priorities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 20, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    LookupId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    SortOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Priorities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Priorities_Lookups_LookupId",
                        column: x => x.LookupId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Relationships",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 20, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    LookupId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    SortOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relationships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Relationships_Lookups_LookupId",
                        column: x => x.LookupId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TaskNotifyTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 20, nullable: true),
                    CssClass = table.Column<string>(maxLength: 20, nullable: true),
                    CssStyle = table.Column<string>(maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    LookupId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    SortOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskNotifyTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskNotifyTypes_Lookups_LookupId",
                        column: x => x.LookupId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TaskReminderTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 20, nullable: true),
                    CssClass = table.Column<string>(maxLength: 20, nullable: true),
                    CssStyle = table.Column<string>(maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    LookupId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    SortOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskReminderTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskReminderTypes_Lookups_LookupId",
                        column: x => x.LookupId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TaskStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 20, nullable: true),
                    CssClass = table.Column<string>(maxLength: 20, nullable: true),
                    CssStyle = table.Column<string>(maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    LookupId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    SortOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskStatuses_Lookups_LookupId",
                        column: x => x.LookupId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TaskTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 20, nullable: true),
                    CssClass = table.Column<string>(maxLength: 20, nullable: true),
                    CssStyle = table.Column<string>(maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    LookupId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    SortOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskTypes_Lookups_LookupId",
                        column: x => x.LookupId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PropertyVoids",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PropertyId = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    VoidId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyVoids", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyVoids_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApplicationId = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    RoleId = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationRoles_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ApplicationRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationTasks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApplicationId = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    TaskId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationTasks_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ApplicationTasks_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: true),
                    EmployeeRef = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsSysAdmin = table.Column<bool>(nullable: false),
                    ManagerId = table.Column<int>(nullable: true),
                    Mobile = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    PersonId = table.Column<int>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Subject = table.Column<string>(nullable: true),
                    Telephone = table.Column<string>(nullable: true),
                    UserGroupId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Users_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_UserGroups_UserGroupId",
                        column: x => x.UserGroupId,
                        principalTable: "UserGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Titles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 20, nullable: true),
                    DefaultGenderId = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    LookupId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    SortOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Titles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Titles_Genders_DefaultGenderId",
                        column: x => x.DefaultGenderId,
                        principalTable: "Genders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Titles_Lookups_LookupId",
                        column: x => x.LookupId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccessLevel = table.Column<int>(nullable: false),
                    ApplicationRoleId = table.Column<int>(nullable: false),
                    MenuItemId = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permissions_ApplicationRoles_ApplicationRoleId",
                        column: x => x.ApplicationRoleId,
                        principalTable: "ApplicationRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Permissions_MenuItems_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApplicationAccessLevel = table.Column<int>(nullable: true),
                    ApplicationId = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationUsers_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ApplicationUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Audits",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateTime = table.Column<DateTime>(nullable: false),
                    KeyValues = table.Column<string>(nullable: true),
                    NewValues = table.Column<string>(nullable: true),
                    OldValues = table.Column<string>(nullable: true),
                    TableName = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Audits_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ChatMessages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GuestId = table.Column<int>(nullable: false),
                    HostId = table.Column<int>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatMessages_Users_GuestId",
                        column: x => x.GuestId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ChatMessages_Users_HostId",
                        column: x => x.HostId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Emails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Body = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    EmailCategoryId = table.Column<int>(nullable: true),
                    EmailStatusId = table.Column<int>(nullable: true),
                    RootMessageId = table.Column<int>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    SendExternal = table.Column<bool>(nullable: false),
                    SendOn = table.Column<DateTime>(nullable: true),
                    SenderId = table.Column<int>(nullable: true),
                    Subject = table.Column<string>(nullable: true),
                    ToEmailAddress = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Emails_EmailCategories_EmailCategoryId",
                        column: x => x.EmailCategoryId,
                        principalTable: "EmailCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Emails_EmailStatuses_EmailStatusId",
                        column: x => x.EmailStatusId,
                        principalTable: "EmailStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Emails_Users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Content = table.Column<string>(nullable: true),
                    NextReminderDate = table.Column<DateTime>(nullable: true),
                    ParentMessageId = table.Column<int>(nullable: true),
                    RaisedById = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    SendEmail = table.Column<bool>(nullable: false),
                    SendSms = table.Column<bool>(nullable: false),
                    Subject = table.Column<string>(nullable: true),
                    UserGroupId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Messages_ParentMessageId",
                        column: x => x.ParentMessageId,
                        principalTable: "Messages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Messages_Users_RaisedById",
                        column: x => x.RaisedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Messages_UserGroups_UserGroupId",
                        column: x => x.UserGroupId,
                        principalTable: "UserGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PropertyUserTasks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ActualCost = table.Column<decimal>(nullable: true),
                    ActualEndTime = table.Column<DateTime>(nullable: true),
                    ActualStartTime = table.Column<DateTime>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    EstimatedCost = table.Column<decimal>(nullable: true),
                    LegacyTaskId = table.Column<string>(nullable: true),
                    PropertyId = table.Column<int>(nullable: false),
                    PropertyVoidId = table.Column<int>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    ScheduleEndTime = table.Column<DateTime>(nullable: false),
                    ScheduleStartTime = table.Column<DateTime>(nullable: false),
                    TaskId = table.Column<int>(nullable: true),
                    TaskStatusId = table.Column<int>(nullable: false),
                    TaskTypeId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyUserTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyUserTasks_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PropertyUserTasks_PropertyVoids_PropertyVoidId",
                        column: x => x.PropertyVoidId,
                        principalTable: "PropertyVoids",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PropertyUserTasks_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PropertyUserTasks_TaskStatuses_TaskStatusId",
                        column: x => x.TaskStatusId,
                        principalTable: "TaskStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PropertyUserTasks_TaskTypes_TaskTypeId",
                        column: x => x.TaskTypeId,
                        principalTable: "TaskTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PropertyUserTasks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Smses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Message = table.Column<string>(nullable: true),
                    ReceiverNumber = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    SenderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Smses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Smses_Users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "UseMessages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsRead = table.Column<bool>(nullable: false),
                    NextReminderDate = table.Column<DateTime>(nullable: true),
                    RecipientId = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    SendSms = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UseMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UseMessages_Users_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "UserApplicationRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApplicationRoleId = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserApplicationRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserApplicationRoles_ApplicationRoles_ApplicationRoleId",
                        column: x => x.ApplicationRoleId,
                        principalTable: "ApplicationRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_UserApplicationRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DocumentTypeId = table.Column<int>(nullable: true),
                    EmailId = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    RelativePath = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    UploadById = table.Column<int>(nullable: false),
                    UploadOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documents_DocumentTypes_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalTable: "DocumentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Documents_Emails_EmailId",
                        column: x => x.EmailId,
                        principalTable: "Emails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Documents_Users_UploadById",
                        column: x => x.UploadById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "EmailLabels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmailId = table.Column<int>(nullable: false),
                    EmailLabelTypeId = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailLabels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailLabels_Emails_EmailId",
                        column: x => x.EmailId,
                        principalTable: "Emails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_EmailLabels_EmailLabelTypes_EmailLabelTypeId",
                        column: x => x.EmailLabelTypeId,
                        principalTable: "EmailLabelTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "UserEmails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Comment = table.Column<string>(nullable: true),
                    EmailId = table.Column<int>(nullable: false),
                    EmailStatusId = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsImportant = table.Column<bool>(nullable: false),
                    ReadOn = table.Column<DateTime>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEmails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserEmails_Emails_EmailId",
                        column: x => x.EmailId,
                        principalTable: "Emails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_UserEmails_EmailStatuses_EmailStatusId",
                        column: x => x.EmailStatusId,
                        principalTable: "EmailStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserEmails_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "UserSmses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Comment = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    SendOn = table.Column<DateTime>(nullable: true),
                    SmsId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSmses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSmses_Smses_SmsId",
                        column: x => x.SmsId,
                        principalTable: "Smses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_UserSmses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "PropertyDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Comment = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    DocumentId = table.Column<int>(nullable: false),
                    ImageGroupId = table.Column<int>(nullable: true),
                    IsDefaultImage = table.Column<bool>(nullable: false),
                    IsImageForPropertyShop = table.Column<bool>(nullable: false),
                    PropertyId = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    ViewOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyDocuments_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PropertyDocuments_ImageGroups_ImageGroupId",
                        column: x => x.ImageGroupId,
                        principalTable: "ImageGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PropertyDocuments_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "UserDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Comment = table.Column<string>(nullable: true),
                    DocumentId = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserDocuments_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_UserDocuments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "PersonAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddressId = table.Column<int>(nullable: false),
                    AddressTypeId = table.Column<int>(nullable: false),
                    LivingSince = table.Column<DateTime>(nullable: true),
                    LivingTill = table.Column<DateTime>(nullable: true),
                    PersonId = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonAddresses_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PersonAddresses_AddressTypes_AddressTypeId",
                        column: x => x.AddressTypeId,
                        principalTable: "AddressTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "PersonAlerts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AlertGroupId = table.Column<int>(nullable: true),
                    AlertStatusId = table.Column<int>(nullable: true),
                    AlertTypeId = table.Column<int>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    Details = table.Column<string>(nullable: true),
                    PersonId = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonAlerts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonAlerts_AlertGroups_AlertGroupId",
                        column: x => x.AlertGroupId,
                        principalTable: "AlertGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonAlerts_AlertStatuses_AlertStatusId",
                        column: x => x.AlertStatusId,
                        principalTable: "AlertStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonAlerts_AlertTypes_AlertTypeId",
                        column: x => x.AlertTypeId,
                        principalTable: "AlertTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonAlertComments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    PersonAlertId = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonAlertComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonAlertComments_PersonAlerts_PersonAlertId",
                        column: x => x.PersonAlertId,
                        principalTable: "PersonAlerts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "PersonAntiSocialBehaviours",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CaseClosureReasonId = table.Column<int>(nullable: true),
                    CaseStatusId = table.Column<int>(nullable: true),
                    CaseTypeId = table.Column<int>(nullable: true),
                    CompletionDate = table.Column<DateTime>(nullable: true),
                    IBSCaseReference = table.Column<string>(nullable: true),
                    LoggedDate = table.Column<DateTime>(nullable: false),
                    PersonId = table.Column<int>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonAntiSocialBehaviours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonAntiSocialBehaviours_AntiSocialBehaviourCaseClosureReasons_CaseClosureReasonId",
                        column: x => x.CaseClosureReasonId,
                        principalTable: "AntiSocialBehaviourCaseClosureReasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonAntiSocialBehaviours_AntiSocialBehaviourCaseStatuses_CaseStatusId",
                        column: x => x.CaseStatusId,
                        principalTable: "AntiSocialBehaviourCaseStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonAntiSocialBehaviours_AntiSocialBehaviourTypes_CaseTypeId",
                        column: x => x.CaseTypeId,
                        principalTable: "AntiSocialBehaviourTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonAntiSocialBehaviours_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "PersonAntiSocialBehaviourCaseNotes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ActionDateTime = table.Column<DateTime>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    PersonAntiSocialBehaviourId = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonAntiSocialBehaviourCaseNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonAntiSocialBehaviourCaseNotes_PersonAntiSocialBehaviours_PersonAntiSocialBehaviourId",
                        column: x => x.PersonAntiSocialBehaviourId,
                        principalTable: "PersonAntiSocialBehaviours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApplicantTypeId = table.Column<int>(nullable: true),
                    ApplicationId = table.Column<int>(nullable: true),
                    ContactTypeId = table.Column<int>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: true),
                    EthnicityId = table.Column<int>(nullable: true),
                    Forename = table.Column<string>(maxLength: 250, nullable: true),
                    GenderId = table.Column<int>(nullable: true),
                    GlobalIdentityKey = table.Column<Guid>(nullable: true),
                    HasDuplicate = table.Column<bool>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    IsDuplicate = table.Column<bool>(nullable: true),
                    MainContactPersonId = table.Column<int>(nullable: true),
                    NationalInsuranceNumber = table.Column<string>(nullable: true),
                    NationalityTypeId = table.Column<int>(nullable: true),
                    PreferredContactTime = table.Column<string>(nullable: true),
                    PreferredLanguageId = table.Column<int>(nullable: true),
                    RelationshipId = table.Column<int>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Surname = table.Column<string>(maxLength: 100, nullable: true),
                    TenantCode = table.Column<string>(maxLength: 50, nullable: true),
                    TenantId = table.Column<int>(nullable: true),
                    TitleId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Persons_ApplicantTypes_ApplicantTypeId",
                        column: x => x.ApplicantTypeId,
                        principalTable: "ApplicantTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Persons_ContactTypes_ContactTypeId",
                        column: x => x.ContactTypeId,
                        principalTable: "ContactTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Persons_Ethnicities_EthnicityId",
                        column: x => x.EthnicityId,
                        principalTable: "Ethnicities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Persons_Genders_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Genders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Persons_Persons_MainContactPersonId",
                        column: x => x.MainContactPersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Persons_NationalityTypes_NationalityTypeId",
                        column: x => x.NationalityTypeId,
                        principalTable: "NationalityTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Persons_Languages_PreferredLanguageId",
                        column: x => x.PreferredLanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Persons_Relationships_RelationshipId",
                        column: x => x.RelationshipId,
                        principalTable: "Relationships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MergePersons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CorrectPersonId = table.Column<int>(nullable: false),
                    DuplicatePersonId = table.Column<int>(nullable: false),
                    IsMerged = table.Column<bool>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MergePersons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MergePersons_Persons_CorrectPersonId",
                        column: x => x.CorrectPersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "PersonApplicationLinks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApplicationId = table.Column<int>(nullable: false),
                    ExternalLinkId = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    PersonId = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonApplicationLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonApplicationLinks_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PersonApplicationLinks_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "PersonCases",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CaseStatusId = table.Column<int>(nullable: true),
                    CaseTypeId = table.Column<int>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    Details = table.Column<string>(nullable: true),
                    PersonId = table.Column<int>(nullable: false),
                    PriorityId = table.Column<int>(nullable: true),
                    RaisedOn = table.Column<DateTime>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonCases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonCases_PersonCaseStatuses_CaseStatusId",
                        column: x => x.CaseStatusId,
                        principalTable: "PersonCaseStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonCases_PersonCaseTypes_CaseTypeId",
                        column: x => x.CaseTypeId,
                        principalTable: "PersonCaseTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonCases_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PersonCases_Priorities_PriorityId",
                        column: x => x.PriorityId,
                        principalTable: "Priorities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonCases_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "PersonContactDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Comment = table.Column<string>(nullable: true),
                    ContactByOptionId = table.Column<int>(nullable: false),
                    ContactValue = table.Column<string>(nullable: true),
                    IsDefault = table.Column<bool>(nullable: true),
                    PersonId = table.Column<int>(nullable: false),
                    PriorityOrder = table.Column<int>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonContactDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonContactDetails_ContactByOptions_ContactByOptionId",
                        column: x => x.ContactByOptionId,
                        principalTable: "ContactByOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PersonContactDetails_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "PersonDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Comment = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    DocumentId = table.Column<int>(nullable: false),
                    IsAntiSocialBehaviour = table.Column<bool>(nullable: false),
                    PersonId = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonDocuments_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PersonDocuments_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "PersonEmails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Comment = table.Column<string>(nullable: true),
                    EmailId = table.Column<int>(nullable: false),
                    EmailStatusId = table.Column<int>(nullable: true),
                    IsBcc = table.Column<bool>(nullable: false),
                    IsCc = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsImportant = table.Column<bool>(nullable: false),
                    PersonId = table.Column<int>(nullable: true),
                    ReadOn = table.Column<DateTime>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonEmails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonEmails_Emails_EmailId",
                        column: x => x.EmailId,
                        principalTable: "Emails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PersonEmails_EmailStatuses_EmailStatusId",
                        column: x => x.EmailStatusId,
                        principalTable: "EmailStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonEmails_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonFlags",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    Details = table.Column<string>(nullable: true),
                    FlagGroupId = table.Column<int>(nullable: true),
                    FlagTypeId = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    PersonId = table.Column<int>(nullable: false),
                    RaisedOn = table.Column<DateTime>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonFlags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonFlags_FlagGroups_FlagGroupId",
                        column: x => x.FlagGroupId,
                        principalTable: "FlagGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonFlags_FlagTypes_FlagTypeId",
                        column: x => x.FlagTypeId,
                        principalTable: "FlagTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonFlags_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "PersonSmses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Comment = table.Column<string>(nullable: true),
                    PersonId = table.Column<int>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    SendOn = table.Column<DateTime>(nullable: true),
                    SmsId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonSmses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonSmses_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonSmses_Smses_SmsId",
                        column: x => x.SmsId,
                        principalTable: "Smses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PersonId = table.Column<int>(nullable: false),
                    PropertyId = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    TenancyReference = table.Column<string>(maxLength: 50, nullable: true),
                    TenancyType = table.Column<string>(maxLength: 50, nullable: true),
                    TenantCode = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tenants_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Tenants_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "UserActivities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    PersonId = table.Column<int>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserActivities_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserActivities_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "UserDiaries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateTime = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    PersonId = table.Column<int>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDiaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserDiaries_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserDiaries_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "UserPersonTasks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ActualCost = table.Column<decimal>(nullable: true),
                    ActualEndTime = table.Column<DateTime>(nullable: true),
                    ActualStartTime = table.Column<DateTime>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    EstimatedCost = table.Column<decimal>(nullable: true),
                    LegacyTaskId = table.Column<string>(nullable: true),
                    PersonId = table.Column<int>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    ScheduleEndTime = table.Column<DateTime>(nullable: false),
                    ScheduleStartTime = table.Column<DateTime>(nullable: false),
                    TaskId = table.Column<int>(nullable: true),
                    TaskStatusId = table.Column<int>(nullable: false),
                    TaskTypeId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPersonTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPersonTasks_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserPersonTasks_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserPersonTasks_TaskStatuses_TaskStatusId",
                        column: x => x.TaskStatusId,
                        principalTable: "TaskStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_UserPersonTasks_TaskTypes_TaskTypeId",
                        column: x => x.TaskTypeId,
                        principalTable: "TaskTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_UserPersonTasks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "PersonFlagComments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    PersonFlagId = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonFlagComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonFlagComments_PersonFlags_PersonFlagId",
                        column: x => x.PersonFlagId,
                        principalTable: "PersonFlags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "UserTaskNotifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NotificationSent = table.Column<bool>(nullable: false),
                    NotificationSentOn = table.Column<DateTime>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    TaskNotifyTypeId = table.Column<int>(nullable: false),
                    UserPersonTaskId = table.Column<int>(nullable: true),
                    UserTaskId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTaskNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTaskNotifications_TaskNotifyTypes_TaskNotifyTypeId",
                        column: x => x.TaskNotifyTypeId,
                        principalTable: "TaskNotifyTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_UserTaskNotifications_UserPersonTasks_UserPersonTaskId",
                        column: x => x.UserPersonTaskId,
                        principalTable: "UserPersonTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserTaskReminders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NextReminderDue = table.Column<DateTime>(nullable: true),
                    ReminderSent = table.Column<bool>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    TaskReminderTypeId = table.Column<int>(nullable: false),
                    UserPersonTaskId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTaskReminders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTaskReminders_TaskReminderTypes_TaskReminderTypeId",
                        column: x => x.TaskReminderTypeId,
                        principalTable: "TaskReminderTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_UserTaskReminders_UserPersonTasks_UserPersonTaskId",
                        column: x => x.UserPersonTaskId,
                        principalTable: "UserPersonTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AddressTypes_LookupId",
                table: "AddressTypes",
                column: "LookupId");

            migrationBuilder.CreateIndex(
                name: "IX_AlertGroups_LookupId",
                table: "AlertGroups",
                column: "LookupId");

            migrationBuilder.CreateIndex(
                name: "IX_AlertStatuses_LookupId",
                table: "AlertStatuses",
                column: "LookupId");

            migrationBuilder.CreateIndex(
                name: "IX_AlertTypes_LookupId",
                table: "AlertTypes",
                column: "LookupId");

            migrationBuilder.CreateIndex(
                name: "IX_AntiSocialBehaviourCaseClosureReasons_LookupId",
                table: "AntiSocialBehaviourCaseClosureReasons",
                column: "LookupId");

            migrationBuilder.CreateIndex(
                name: "IX_AntiSocialBehaviourCaseStatuses_LookupId",
                table: "AntiSocialBehaviourCaseStatuses",
                column: "LookupId");

            migrationBuilder.CreateIndex(
                name: "IX_AntiSocialBehaviourTypes_LookupId",
                table: "AntiSocialBehaviourTypes",
                column: "LookupId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantTypes_LookupId",
                table: "ApplicantTypes",
                column: "LookupId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationRoles_ApplicationId",
                table: "ApplicationRoles",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationRoles_RoleId",
                table: "ApplicationRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationTasks_ApplicationId",
                table: "ApplicationTasks",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationTasks_TaskId",
                table: "ApplicationTasks",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsers_ApplicationId",
                table: "ApplicationUsers",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsers_UserId",
                table: "ApplicationUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Audits_UserId",
                table: "Audits",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_GuestId",
                table: "ChatMessages",
                column: "GuestId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_HostId",
                table: "ChatMessages",
                column: "HostId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactByOptions_LookupId",
                table: "ContactByOptions",
                column: "LookupId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactTypes_LookupId",
                table: "ContactTypes",
                column: "LookupId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_DocumentTypeId",
                table: "Documents",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_EmailId",
                table: "Documents",
                column: "EmailId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_UploadById",
                table: "Documents",
                column: "UploadById");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentTypes_LookupId",
                table: "DocumentTypes",
                column: "LookupId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailCategories_LookupId",
                table: "EmailCategories",
                column: "LookupId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailLabels_EmailId",
                table: "EmailLabels",
                column: "EmailId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailLabels_EmailLabelTypeId",
                table: "EmailLabels",
                column: "EmailLabelTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailLabelTypes_LookupId",
                table: "EmailLabelTypes",
                column: "LookupId");

            migrationBuilder.CreateIndex(
                name: "IX_Emails_EmailCategoryId",
                table: "Emails",
                column: "EmailCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Emails_EmailStatusId",
                table: "Emails",
                column: "EmailStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Emails_SenderId",
                table: "Emails",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailStatuses_LookupId",
                table: "EmailStatuses",
                column: "LookupId");

            migrationBuilder.CreateIndex(
                name: "IX_Ethnicities_LookupId",
                table: "Ethnicities",
                column: "LookupId");

            migrationBuilder.CreateIndex(
                name: "IX_FlagGroups_LookupId",
                table: "FlagGroups",
                column: "LookupId");

            migrationBuilder.CreateIndex(
                name: "IX_FlagTypes_LookupId",
                table: "FlagTypes",
                column: "LookupId");

            migrationBuilder.CreateIndex(
                name: "IX_Genders_LookupId",
                table: "Genders",
                column: "LookupId");

            migrationBuilder.CreateIndex(
                name: "IX_ImageGroups_LookupId",
                table: "ImageGroups",
                column: "LookupId");

            migrationBuilder.CreateIndex(
                name: "IX_Languages_LookupId",
                table: "Languages",
                column: "LookupId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_ApplicationId",
                table: "MenuItems",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_MergePersons_CorrectPersonId",
                table: "MergePersons",
                column: "CorrectPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ParentMessageId",
                table: "Messages",
                column: "ParentMessageId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_RaisedById",
                table: "Messages",
                column: "RaisedById");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_UserGroupId",
                table: "Messages",
                column: "UserGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_NationalityTypes_LookupId",
                table: "NationalityTypes",
                column: "LookupId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_ApplicationRoleId",
                table: "Permissions",
                column: "ApplicationRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_MenuItemId",
                table: "Permissions",
                column: "MenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonAddresses_AddressId",
                table: "PersonAddresses",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonAddresses_AddressTypeId",
                table: "PersonAddresses",
                column: "AddressTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonAddresses_PersonId",
                table: "PersonAddresses",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonAlertComments_PersonAlertId",
                table: "PersonAlertComments",
                column: "PersonAlertId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonAlerts_AlertGroupId",
                table: "PersonAlerts",
                column: "AlertGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonAlerts_AlertStatusId",
                table: "PersonAlerts",
                column: "AlertStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonAlerts_AlertTypeId",
                table: "PersonAlerts",
                column: "AlertTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonAlerts_PersonId",
                table: "PersonAlerts",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonAntiSocialBehaviourCaseNotes_PersonAntiSocialBehaviourId",
                table: "PersonAntiSocialBehaviourCaseNotes",
                column: "PersonAntiSocialBehaviourId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonAntiSocialBehaviours_CaseClosureReasonId",
                table: "PersonAntiSocialBehaviours",
                column: "CaseClosureReasonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonAntiSocialBehaviours_CaseStatusId",
                table: "PersonAntiSocialBehaviours",
                column: "CaseStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonAntiSocialBehaviours_CaseTypeId",
                table: "PersonAntiSocialBehaviours",
                column: "CaseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonAntiSocialBehaviours_PersonId",
                table: "PersonAntiSocialBehaviours",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonAntiSocialBehaviours_UserId",
                table: "PersonAntiSocialBehaviours",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonApplicationLinks_ApplicationId",
                table: "PersonApplicationLinks",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonApplicationLinks_PersonId",
                table: "PersonApplicationLinks",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonCases_CaseStatusId",
                table: "PersonCases",
                column: "CaseStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonCases_CaseTypeId",
                table: "PersonCases",
                column: "CaseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonCases_PersonId",
                table: "PersonCases",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonCases_PriorityId",
                table: "PersonCases",
                column: "PriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonCases_UserId",
                table: "PersonCases",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonCaseStatuses_LookupId",
                table: "PersonCaseStatuses",
                column: "LookupId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonCaseTypes_LookupId",
                table: "PersonCaseTypes",
                column: "LookupId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonContactDetails_ContactByOptionId",
                table: "PersonContactDetails",
                column: "ContactByOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonContactDetails_PersonId",
                table: "PersonContactDetails",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonDocuments_DocumentId",
                table: "PersonDocuments",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonDocuments_PersonId",
                table: "PersonDocuments",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonEmails_EmailId",
                table: "PersonEmails",
                column: "EmailId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonEmails_EmailStatusId",
                table: "PersonEmails",
                column: "EmailStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonEmails_PersonId",
                table: "PersonEmails",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonFlagComments_PersonFlagId",
                table: "PersonFlagComments",
                column: "PersonFlagId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonFlags_FlagGroupId",
                table: "PersonFlags",
                column: "FlagGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonFlags_FlagTypeId",
                table: "PersonFlags",
                column: "FlagTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonFlags_PersonId",
                table: "PersonFlags",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_ApplicantTypeId",
                table: "Persons",
                column: "ApplicantTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_ContactTypeId",
                table: "Persons",
                column: "ContactTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_EthnicityId",
                table: "Persons",
                column: "EthnicityId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_GenderId",
                table: "Persons",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_MainContactPersonId",
                table: "Persons",
                column: "MainContactPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_NationalityTypeId",
                table: "Persons",
                column: "NationalityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_PreferredLanguageId",
                table: "Persons",
                column: "PreferredLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_RelationshipId",
                table: "Persons",
                column: "RelationshipId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_TenantId",
                table: "Persons",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonSmses_PersonId",
                table: "PersonSmses",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonSmses_SmsId",
                table: "PersonSmses",
                column: "SmsId");

            migrationBuilder.CreateIndex(
                name: "IX_Priorities_LookupId",
                table: "Priorities",
                column: "LookupId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyDocuments_DocumentId",
                table: "PropertyDocuments",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyDocuments_ImageGroupId",
                table: "PropertyDocuments",
                column: "ImageGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyDocuments_PropertyId",
                table: "PropertyDocuments",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyUserTasks_PropertyId",
                table: "PropertyUserTasks",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyUserTasks_PropertyVoidId",
                table: "PropertyUserTasks",
                column: "PropertyVoidId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyUserTasks_TaskId",
                table: "PropertyUserTasks",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyUserTasks_TaskStatusId",
                table: "PropertyUserTasks",
                column: "TaskStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyUserTasks_TaskTypeId",
                table: "PropertyUserTasks",
                column: "TaskTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyUserTasks_UserId",
                table: "PropertyUserTasks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyVoids_PropertyId",
                table: "PropertyVoids",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Relationships_LookupId",
                table: "Relationships",
                column: "LookupId");

            migrationBuilder.CreateIndex(
                name: "IX_Smses_SenderId",
                table: "Smses",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskNotifyTypes_LookupId",
                table: "TaskNotifyTypes",
                column: "LookupId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskReminderTypes_LookupId",
                table: "TaskReminderTypes",
                column: "LookupId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskStatuses_LookupId",
                table: "TaskStatuses",
                column: "LookupId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskTypes_LookupId",
                table: "TaskTypes",
                column: "LookupId");

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_PersonId",
                table: "Tenants",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_PropertyId",
                table: "Tenants",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Titles_DefaultGenderId",
                table: "Titles",
                column: "DefaultGenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Titles_LookupId",
                table: "Titles",
                column: "LookupId");

            migrationBuilder.CreateIndex(
                name: "IX_UseMessages_RecipientId",
                table: "UseMessages",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_UserActivities_PersonId",
                table: "UserActivities",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_UserActivities_UserId",
                table: "UserActivities",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserApplicationRoles_ApplicationRoleId",
                table: "UserApplicationRoles",
                column: "ApplicationRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserApplicationRoles_UserId",
                table: "UserApplicationRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDiaries_PersonId",
                table: "UserDiaries",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDiaries_UserId",
                table: "UserDiaries",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDocuments_DocumentId",
                table: "UserDocuments",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDocuments_UserId",
                table: "UserDocuments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserEmails_EmailId",
                table: "UserEmails",
                column: "EmailId");

            migrationBuilder.CreateIndex(
                name: "IX_UserEmails_EmailStatusId",
                table: "UserEmails",
                column: "EmailStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_UserEmails_UserId",
                table: "UserEmails",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPersonTasks_PersonId",
                table: "UserPersonTasks",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPersonTasks_TaskId",
                table: "UserPersonTasks",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPersonTasks_TaskStatusId",
                table: "UserPersonTasks",
                column: "TaskStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPersonTasks_TaskTypeId",
                table: "UserPersonTasks",
                column: "TaskTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPersonTasks_UserId",
                table: "UserPersonTasks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ManagerId",
                table: "Users",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserGroupId",
                table: "Users",
                column: "UserGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSmses_SmsId",
                table: "UserSmses",
                column: "SmsId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSmses_UserId",
                table: "UserSmses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTaskNotifications_TaskNotifyTypeId",
                table: "UserTaskNotifications",
                column: "TaskNotifyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTaskNotifications_UserPersonTaskId",
                table: "UserTaskNotifications",
                column: "UserPersonTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTaskReminders_TaskReminderTypeId",
                table: "UserTaskReminders",
                column: "TaskReminderTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTaskReminders_UserPersonTaskId",
                table: "UserTaskReminders",
                column: "UserPersonTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonAddresses_Persons_PersonId",
                table: "PersonAddresses",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonAlerts_Persons_PersonId",
                table: "PersonAlerts",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonAntiSocialBehaviours_Persons_PersonId",
                table: "PersonAntiSocialBehaviours",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Tenants_TenantId",
                table: "Persons",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.Sql(SqlProgrammability.Create_IdentityUserView);
            migrationBuilder.Sql(SqlProgrammability.Create_PropertyDetailView);
            migrationBuilder.Sql(SqlProgrammability.Create_PropertyVoidView);
            migrationBuilder.Sql(SqlProgrammability.Create_TenantHistoryView);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(SqlProgrammability.Drop_IdentityUserView);
            migrationBuilder.Sql(SqlProgrammability.Drop_PropertyDetailView);
            migrationBuilder.Sql(SqlProgrammability.Drop_PropertyVoidView);
            migrationBuilder.Sql(SqlProgrammability.Drop_TenantHistoryView);
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicantTypes_Lookups_LookupId",
                table: "ApplicantTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactTypes_Lookups_LookupId",
                table: "ContactTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Ethnicities_Lookups_LookupId",
                table: "Ethnicities");

            migrationBuilder.DropForeignKey(
                name: "FK_Genders_Lookups_LookupId",
                table: "Genders");

            migrationBuilder.DropForeignKey(
                name: "FK_Languages_Lookups_LookupId",
                table: "Languages");

            migrationBuilder.DropForeignKey(
                name: "FK_NationalityTypes_Lookups_LookupId",
                table: "NationalityTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Relationships_Lookups_LookupId",
                table: "Relationships");

            migrationBuilder.DropForeignKey(
                name: "FK_Tenants_Persons_PersonId",
                table: "Tenants");

            migrationBuilder.DropTable(
                name: "ApplicationTasks");

            migrationBuilder.DropTable(
                name: "ApplicationUsers");

            migrationBuilder.DropTable(
                name: "Audits");

            migrationBuilder.DropTable(
                name: "ChatMessages");

            migrationBuilder.DropTable(
                name: "EmailLabels");

            migrationBuilder.DropTable(
                name: "MergePersons");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "PersonAddresses");

            migrationBuilder.DropTable(
                name: "PersonAlertComments");

            migrationBuilder.DropTable(
                name: "PersonAntiSocialBehaviourCaseNotes");

            migrationBuilder.DropTable(
                name: "PersonApplicationLinks");

            migrationBuilder.DropTable(
                name: "PersonCases");

            migrationBuilder.DropTable(
                name: "PersonContactDetails");

            migrationBuilder.DropTable(
                name: "PersonDocuments");

            migrationBuilder.DropTable(
                name: "PersonEmails");

            migrationBuilder.DropTable(
                name: "PersonFlagComments");

            migrationBuilder.DropTable(
                name: "PersonSmses");


            migrationBuilder.DropTable(
                name: "PropertyDocuments");

            migrationBuilder.DropTable(
                name: "PropertyUserTasks");


            migrationBuilder.DropTable(
                name: "Titles");

            migrationBuilder.DropTable(
                name: "UseMessages");

            migrationBuilder.DropTable(
                name: "UserActivities");

            migrationBuilder.DropTable(
                name: "UserApplicationRoles");

            migrationBuilder.DropTable(
                name: "UserDiaries");

            migrationBuilder.DropTable(
                name: "UserDocuments");

            migrationBuilder.DropTable(
                name: "UserEmails");

            migrationBuilder.DropTable(
                name: "UserSmses");

            migrationBuilder.DropTable(
                name: "UserTaskNotifications");

            migrationBuilder.DropTable(
                name: "UserTaskReminders");

            migrationBuilder.DropTable(
                name: "EmailLabelTypes");

            migrationBuilder.DropTable(
                name: "MenuItems");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "AddressTypes");

            migrationBuilder.DropTable(
                name: "PersonAlerts");

            migrationBuilder.DropTable(
                name: "PersonAntiSocialBehaviours");

            migrationBuilder.DropTable(
                name: "PersonCaseStatuses");

            migrationBuilder.DropTable(
                name: "PersonCaseTypes");

            migrationBuilder.DropTable(
                name: "Priorities");

            migrationBuilder.DropTable(
                name: "ContactByOptions");

            migrationBuilder.DropTable(
                name: "PersonFlags");

            migrationBuilder.DropTable(
                name: "ImageGroups");

            migrationBuilder.DropTable(
                name: "PropertyVoids");

            migrationBuilder.DropTable(
                name: "ApplicationRoles");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "Smses");

            migrationBuilder.DropTable(
                name: "TaskNotifyTypes");

            migrationBuilder.DropTable(
                name: "TaskReminderTypes");

            migrationBuilder.DropTable(
                name: "UserPersonTasks");

            migrationBuilder.DropTable(
                name: "AlertGroups");

            migrationBuilder.DropTable(
                name: "AlertStatuses");

            migrationBuilder.DropTable(
                name: "AlertTypes");

            migrationBuilder.DropTable(
                name: "AntiSocialBehaviourCaseClosureReasons");

            migrationBuilder.DropTable(
                name: "AntiSocialBehaviourCaseStatuses");

            migrationBuilder.DropTable(
                name: "AntiSocialBehaviourTypes");

            migrationBuilder.DropTable(
                name: "FlagGroups");

            migrationBuilder.DropTable(
                name: "FlagTypes");

            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "DocumentTypes");

            migrationBuilder.DropTable(
                name: "Emails");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "TaskStatuses");

            migrationBuilder.DropTable(
                name: "TaskTypes");

            migrationBuilder.DropTable(
                name: "EmailCategories");

            migrationBuilder.DropTable(
                name: "EmailStatuses");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "UserGroups");

            migrationBuilder.DropTable(
                name: "Lookups");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "ApplicantTypes");

            migrationBuilder.DropTable(
                name: "ContactTypes");

            migrationBuilder.DropTable(
                name: "Ethnicities");

            migrationBuilder.DropTable(
                name: "Genders");

            migrationBuilder.DropTable(
                name: "NationalityTypes");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "Relationships");

            migrationBuilder.DropTable(
                name: "Tenants");

            migrationBuilder.DropTable(
                name: "Properties");
        }
    }
}
