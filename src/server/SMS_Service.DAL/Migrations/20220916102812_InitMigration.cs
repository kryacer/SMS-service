using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMS_Service.DAL.Migrations
{
    public partial class InitMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SMS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    From = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SmsToReceivers",
                columns: table => new
                {
                    SmsId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReceiverNumber = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmsToReceivers", x => new { x.SmsId, x.ReceiverNumber });
                    table.ForeignKey(
                        name: "FK_SmsToReceivers_SMS_SmsId",
                        column: x => x.SmsId,
                        principalTable: "SMS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SmsToReceivers");

            migrationBuilder.DropTable(
                name: "SMS");
        }
    }
}
