using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projektna.Migrations
{
    public partial class ExtendBranch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerAccId",
                table: "Customer",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BranchAdminId",
                table: "Branch",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Branch",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PhoneNumber",
                table: "Branch",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Customer_CustomerAccId",
                table: "Customer",
                column: "CustomerAccId");

            migrationBuilder.CreateIndex(
                name: "IX_Branch_BranchAdminId",
                table: "Branch",
                column: "BranchAdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Branch_AspNetUsers_BranchAdminId",
                table: "Branch",
                column: "BranchAdminId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_AspNetUsers_CustomerAccId",
                table: "Customer",
                column: "CustomerAccId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branch_AspNetUsers_BranchAdminId",
                table: "Branch");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_AspNetUsers_CustomerAccId",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Customer_CustomerAccId",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Branch_BranchAdminId",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "CustomerAccId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "BranchAdminId",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Branch");
        }
    }
}
