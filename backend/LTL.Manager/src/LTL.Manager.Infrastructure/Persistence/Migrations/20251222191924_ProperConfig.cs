using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LTL.Manager.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ProperConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Document_Orders_OrderId1",
                table: "Document");

            migrationBuilder.DropForeignKey(
                name: "FK_Load_Orders_OrderId1",
                table: "Load");

            migrationBuilder.DropIndex(
                name: "IX_Load_OrderId1",
                table: "Load");

            migrationBuilder.DropIndex(
                name: "IX_Document_OrderId1",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "OrderId1",
                table: "Load");

            migrationBuilder.DropColumn(
                name: "OrderId1",
                table: "Document");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdated",
                table: "Load",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Load",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdated",
                table: "Document",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "Document",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Document",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateIndex(
                name: "IX_Load_OrderId",
                table: "Load",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Document_OrderId",
                table: "Document",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Document_Orders_OrderId",
                table: "Document",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Load_Orders_OrderId",
                table: "Load",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Document_Orders_OrderId",
                table: "Document");

            migrationBuilder.DropForeignKey(
                name: "FK_Load_Orders_OrderId",
                table: "Load");

            migrationBuilder.DropIndex(
                name: "IX_Load_OrderId",
                table: "Load");

            migrationBuilder.DropIndex(
                name: "IX_Document_OrderId",
                table: "Document");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdated",
                table: "Load",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Load",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AddColumn<int>(
                name: "OrderId1",
                table: "Load",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdated",
                table: "Document",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "Document",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Document",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AddColumn<int>(
                name: "OrderId1",
                table: "Document",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Load_OrderId1",
                table: "Load",
                column: "OrderId1");

            migrationBuilder.CreateIndex(
                name: "IX_Document_OrderId1",
                table: "Document",
                column: "OrderId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Document_Orders_OrderId1",
                table: "Document",
                column: "OrderId1",
                principalTable: "Orders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Load_Orders_OrderId1",
                table: "Load",
                column: "OrderId1",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
