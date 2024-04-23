using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloudHotel.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initialmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Guests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "varchar(120)", nullable: false),
                    Email = table.Column<string>(type: "varchar(120)", nullable: false),
                    Phone = table.Column<string>(type: "varchar(30)", nullable: false),
                    AddressCity = table.Column<string>(type: "varchar(60)", nullable: false),
                    AddressCountry = table.Column<string>(type: "varchar(60)", nullable: false),
                    AddresPostalCode = table.Column<string>(type: "varchar(30)", nullable: false),
                    AddressState = table.Column<string>(type: "varchar(60)", nullable: false),
                    AddressStreet = table.Column<string>(type: "varchar(120)", nullable: false),
                    DocumentNumber = table.Column<string>(type: "varchar(30)", nullable: false),
                    DocumentType = table.Column<string>(type: "varchar(30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "varchar(120)", nullable: false),
                    Description = table.Column<string>(type: "varchar(120)", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Photos = table.Column<string[]>(type: "text[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Arrival = table.Column<DateOnly>(type: "date", nullable: false),
                    Departure = table.Column<DateOnly>(type: "date", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    RoomId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoomCode = table.Column<string>(type: "varchar(60)", nullable: false),
                    GuestId = table.Column<Guid>(type: "uuid", nullable: false),
                    GuestName = table.Column<string>(type: "varchar(120)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Guests_GuestId",
                        column: x => x.GuestId,
                        principalTable: "Guests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_Arrival",
                table: "Reservations",
                column: "Arrival");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_Departure",
                table: "Reservations",
                column: "Departure");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_GuestId",
                table: "Reservations",
                column: "GuestId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_RoomId",
                table: "Reservations",
                column: "RoomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Guests");

            migrationBuilder.DropTable(
                name: "Rooms");
        }
    }
}
