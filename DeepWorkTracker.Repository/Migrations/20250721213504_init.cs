using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeepWorkTracker.Repository.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeepWorkSessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndTíme = table.Column<TimeOnly>(type: "time", nullable: false),
                    ContextSwitches = table.Column<int>(type: "int", nullable: false),
                    FinishedTasks = table.Column<int>(type: "int", nullable: false),
                    Output = table.Column<int>(type: "int", nullable: false),
                    OutputUnit = table.Column<int>(type: "int", nullable: false),
                    FocusScore = table.Column<double>(type: "float", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeepWorkSessions", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeepWorkSessions");
        }
    }
}
