using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Database.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ResultLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Input1 = table.Column<int>(type: "int", nullable: false),
                    Input2 = table.Column<int>(type: "int", nullable: false),
                    Output = table.Column<int>(type: "int", nullable: false),
                    Result = table.Column<bool>(type: "bit", nullable: false),
                    ExecutionTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultLogs", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResultLogs");
        }
    }
}
