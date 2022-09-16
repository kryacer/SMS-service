using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMS_Service.DAL.Migrations
{
    public partial class MovedStatusIntoReceiver : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SmsToReceivers_SMS_SmsId",
                table: "SmsToReceivers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SMS",
                table: "SMS");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "SMS");

            migrationBuilder.RenameTable(
                name: "SMS",
                newName: "SMSs");

            migrationBuilder.AddColumn<int>(
                name: "DeliveryStatus",
                table: "SmsToReceivers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SMSs",
                table: "SMSs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SmsToReceivers_SMSs_SmsId",
                table: "SmsToReceivers",
                column: "SmsId",
                principalTable: "SMSs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SmsToReceivers_SMSs_SmsId",
                table: "SmsToReceivers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SMSs",
                table: "SMSs");

            migrationBuilder.DropColumn(
                name: "DeliveryStatus",
                table: "SmsToReceivers");

            migrationBuilder.RenameTable(
                name: "SMSs",
                newName: "SMS");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "SMS",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SMS",
                table: "SMS",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SmsToReceivers_SMS_SmsId",
                table: "SmsToReceivers",
                column: "SmsId",
                principalTable: "SMS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
