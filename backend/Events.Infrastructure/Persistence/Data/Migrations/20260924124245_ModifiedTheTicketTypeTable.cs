using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Events.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedTheTicketTypeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TicketBackgroundImageUrl",
                table: "TicketTypes");

            migrationBuilder.AddColumn<string>(
                name: "ImagePublicId",
                table: "TicketTypes",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "TicketTypes",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePublicId",
                table: "TicketTypes");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "TicketTypes");

            migrationBuilder.AddColumn<string>(
                name: "TicketBackgroundImageUrl",
                table: "TicketTypes",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true);
        }
    }
}
