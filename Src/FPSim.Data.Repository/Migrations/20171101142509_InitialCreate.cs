using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace FPSim.Data.Repository.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "application",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    DateCreated = table.Column<DateTime>(type: "timestamp", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Image = table.Column<byte[]>(type: "bytea", nullable: true),
                    IsArchived = table.Column<bool>(type: "bool", nullable: false),
                    ModelFile = table.Column<byte[]>(type: "bytea", nullable: true),
                    ModelFilename = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_application", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "project",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ApplicationId = table.Column<int>(type: "int4", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Image = table.Column<byte[]>(type: "bytea", nullable: true),
                    IsArchived = table.Column<bool>(type: "bool", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<int>(type: "int4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_project", x => x.Id);
                    table.ForeignKey(
                        name: "FK_project_application_ApplicationId",
                        column: x => x.ApplicationId,
                        principalSchema: "public",
                        principalTable: "application",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_project_user_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "scenario",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    DateCreated = table.Column<DateTime>(type: "timestamp", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp", nullable: true),
                    ExperimentReference = table.Column<string>(type: "text", nullable: true),
                    IsArchived = table.Column<bool>(type: "bool", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    ProjectId = table.Column<int>(type: "int4", nullable: false),
                    RandomSkip = table.Column<int>(type: "int4", nullable: true),
                    Replications = table.Column<int>(type: "int4", nullable: true),
                    ResultStatus = table.Column<int>(type: "int4", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp", nullable: true),
                    Status = table.Column<int>(type: "int4", nullable: false),
                    WarmUpPeriod = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_scenario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_scenario_project_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "public",
                        principalTable: "project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_project_ApplicationId",
                schema: "public",
                table: "project",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_project_UserId",
                schema: "public",
                table: "project",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_scenario_ProjectId",
                schema: "public",
                table: "scenario",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_user_Email",
                schema: "public",
                table: "user",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "scenario",
                schema: "public");

            migrationBuilder.DropTable(
                name: "project",
                schema: "public");

            migrationBuilder.DropTable(
                name: "application",
                schema: "public");

            migrationBuilder.DropTable(
                name: "user",
                schema: "public");
        }
    }
}
