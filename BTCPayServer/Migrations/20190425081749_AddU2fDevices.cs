﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BTCPayServer.Migrations
{
    public partial class AddU2fDevices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            if (this.SupportDropColumn(migrationBuilder.ActiveProvider))
            {
                migrationBuilder.DropColumn(
                    name: "Facade",
                    table: "PairedSINData");
            }

            migrationBuilder.CreateTable(
                name: "U2FDevices",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    KeyHandle = table.Column<byte[]>(nullable: false),
                    PublicKey = table.Column<byte[]>(nullable: false),
                    AttestationCert = table.Column<byte[]>(nullable: false),
                    Counter = table.Column<int>(nullable: false),
                    ApplicationUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_U2FDevices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_U2FDevices_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_U2FDevices_ApplicationUserId",
                table: "U2FDevices",
                column: "ApplicationUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "U2FDevices");
            //if it did not support dropping it, then it is still here and re-adding it would throw
            if (this.SupportDropColumn(migrationBuilder.ActiveProvider))
            {
                migrationBuilder.AddColumn<string>(
                    name: "Facade",
                    table: "PairedSINData",
                    nullable: true);
            }
        }
    }
}
