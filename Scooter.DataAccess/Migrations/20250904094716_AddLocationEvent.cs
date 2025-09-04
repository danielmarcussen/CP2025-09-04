using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ScooterApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddLocationEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "locationevent",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    xcoordinate = table.Column<double>(type: "double precision", nullable: false),
                    ycoordinate = table.Column<double>(type: "double precision", nullable: false),
                    scooterid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_locationevent", x => x.id);
                    table.ForeignKey(
                        name: "fk_locationevent_scooter_scooterid",
                        column: x => x.scooterid,
                        principalTable: "scooter",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_locationevent_scooterid",
                table: "locationevent",
                column: "scooterid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "locationevent");
        }
    }
}
