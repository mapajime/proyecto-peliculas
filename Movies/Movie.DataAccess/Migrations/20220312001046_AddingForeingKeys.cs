using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Movies.DataAccess.Migrations
{
    public partial class AddingForeingKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actors_Countries_NacionalityId",
                schema: "movie",
                table: "Actors");

            migrationBuilder.DropForeignKey(
                name: "FK_Actors_Genders_GenderId",
                schema: "movie",
                table: "Actors");

            migrationBuilder.DropForeignKey(
                name: "FK_Actors_Movies_MovieId",
                schema: "movie",
                table: "Actors");

            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Countries_CountryOfOriginId",
                schema: "movie",
                table: "Movies");

            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Languages_LanguageId",
                schema: "movie",
                table: "Movies");

            migrationBuilder.DropForeignKey(
                name: "FK_Movies_MovieGenders_GenderId",
                schema: "movie",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_CountryOfOriginId",
                schema: "movie",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_GenderId",
                schema: "movie",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Actors_MovieId",
                schema: "movie",
                table: "Actors");

            migrationBuilder.DropIndex(
                name: "IX_Actors_NacionalityId",
                schema: "movie",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "CountryOfOriginId",
                schema: "movie",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "GenderId",
                schema: "movie",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "MovieId",
                schema: "movie",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "NacionalityId",
                schema: "movie",
                table: "Actors");

            migrationBuilder.AlterColumn<Guid>(
                name: "LanguageId",
                schema: "movie",
                table: "Movies",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CountryId",
                schema: "movie",
                table: "Movies",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "MovieGenderId",
                schema: "movie",
                table: "Movies",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "GenderId",
                schema: "movie",
                table: "Actors",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "NationalityId",
                schema: "movie",
                table: "Actors",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "ActorMovie",
                schema: "movie",
                columns: table => new
                {
                    CastId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MoviesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActorMovie", x => new { x.CastId, x.MoviesId });
                    table.ForeignKey(
                        name: "FK_ActorMovie_Actors_CastId",
                        column: x => x.CastId,
                        principalSchema: "movie",
                        principalTable: "Actors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActorMovie_Movies_MoviesId",
                        column: x => x.MoviesId,
                        principalSchema: "movie",
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movies_CountryId",
                schema: "movie",
                table: "Movies",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_MovieGenderId",
                schema: "movie",
                table: "Movies",
                column: "MovieGenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Actors_NationalityId",
                schema: "movie",
                table: "Actors",
                column: "NationalityId");

            migrationBuilder.CreateIndex(
                name: "IX_ActorMovie_MoviesId",
                schema: "movie",
                table: "ActorMovie",
                column: "MoviesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Actors_Countries_NationalityId",
                schema: "movie",
                table: "Actors",
                column: "NationalityId",
                principalSchema: "movie",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Actors_Genders_GenderId",
                schema: "movie",
                table: "Actors",
                column: "GenderId",
                principalSchema: "movie",
                principalTable: "Genders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Countries_CountryId",
                schema: "movie",
                table: "Movies",
                column: "CountryId",
                principalSchema: "movie",
                principalTable: "Countries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Languages_LanguageId",
                schema: "movie",
                table: "Movies",
                column: "LanguageId",
                principalSchema: "movie",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_MovieGenders_MovieGenderId",
                schema: "movie",
                table: "Movies",
                column: "MovieGenderId",
                principalSchema: "movie",
                principalTable: "MovieGenders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actors_Countries_NationalityId",
                schema: "movie",
                table: "Actors");

            migrationBuilder.DropForeignKey(
                name: "FK_Actors_Genders_GenderId",
                schema: "movie",
                table: "Actors");

            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Countries_CountryId",
                schema: "movie",
                table: "Movies");

            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Languages_LanguageId",
                schema: "movie",
                table: "Movies");

            migrationBuilder.DropForeignKey(
                name: "FK_Movies_MovieGenders_MovieGenderId",
                schema: "movie",
                table: "Movies");

            migrationBuilder.DropTable(
                name: "ActorMovie",
                schema: "movie");

            migrationBuilder.DropIndex(
                name: "IX_Movies_CountryId",
                schema: "movie",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_MovieGenderId",
                schema: "movie",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Actors_NationalityId",
                schema: "movie",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "CountryId",
                schema: "movie",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "MovieGenderId",
                schema: "movie",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "NationalityId",
                schema: "movie",
                table: "Actors");

            migrationBuilder.AlterColumn<Guid>(
                name: "LanguageId",
                schema: "movie",
                table: "Movies",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "CountryOfOriginId",
                schema: "movie",
                table: "Movies",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GenderId",
                schema: "movie",
                table: "Movies",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "GenderId",
                schema: "movie",
                table: "Actors",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "MovieId",
                schema: "movie",
                table: "Actors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "NacionalityId",
                schema: "movie",
                table: "Actors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movies_CountryOfOriginId",
                schema: "movie",
                table: "Movies",
                column: "CountryOfOriginId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_GenderId",
                schema: "movie",
                table: "Movies",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Actors_MovieId",
                schema: "movie",
                table: "Actors",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Actors_NacionalityId",
                schema: "movie",
                table: "Actors",
                column: "NacionalityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Actors_Countries_NacionalityId",
                schema: "movie",
                table: "Actors",
                column: "NacionalityId",
                principalSchema: "movie",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Actors_Genders_GenderId",
                schema: "movie",
                table: "Actors",
                column: "GenderId",
                principalSchema: "movie",
                principalTable: "Genders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Actors_Movies_MovieId",
                schema: "movie",
                table: "Actors",
                column: "MovieId",
                principalSchema: "movie",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Countries_CountryOfOriginId",
                schema: "movie",
                table: "Movies",
                column: "CountryOfOriginId",
                principalSchema: "movie",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Languages_LanguageId",
                schema: "movie",
                table: "Movies",
                column: "LanguageId",
                principalSchema: "movie",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_MovieGenders_GenderId",
                schema: "movie",
                table: "Movies",
                column: "GenderId",
                principalSchema: "movie",
                principalTable: "MovieGenders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
