using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "app_operator");

            migrationBuilder.EnsureSchema(
                name: "app_operator_roles");

            migrationBuilder.CreateTable(
                name: "operator",
                schema: "app_operator",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodeResetPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodeResetPasswordExpiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AcceptNewPassword = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "current_timestamp")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_operator", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "operatorRoles",
                schema: "app_operator_roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    OperatorId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "current_timestamp")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_operatorRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_operatorRoles_operator_OperatorId",
                        column: x => x.OperatorId,
                        principalSchema: "app_operator",
                        principalTable: "operator",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_operatorRoles_OperatorId",
                schema: "app_operator_roles",
                table: "operatorRoles",
                column: "OperatorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "operatorRoles",
                schema: "app_operator_roles");

            migrationBuilder.DropTable(
                name: "operator",
                schema: "app_operator");
        }
    }
}
