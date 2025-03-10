﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace myshop.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddImgCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "img",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "img",
                table: "Categories");
        }
    }
}
