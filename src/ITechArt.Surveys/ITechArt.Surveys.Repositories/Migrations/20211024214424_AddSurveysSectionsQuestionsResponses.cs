using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ITechArt.Surveys.Repositories.Migrations
{
    public partial class AddSurveysSectionsQuestionsResponses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Surveys_SurveyId",
                table: "Questions");

            migrationBuilder.RenameColumn(
                name: "SurveyId",
                table: "Questions",
                newName: "SectionId");

            migrationBuilder.RenameIndex(
                name: "IX_Questions_SurveyId",
                table: "Questions",
                newName: "IX_Questions_SectionId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Surveys",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Questions",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Questions",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaxValue",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberInSection",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Required",
                table: "Questions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Responses",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RespondentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SurveyId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Responses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Responses_Surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Surveys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Responses_Users_RespondentId",
                        column: x => x.RespondentId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sections",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    NumberInSurvey = table.Column<int>(type: "int", nullable: false),
                    SurveyId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sections_Surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Surveys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FileResponses",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponseId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    QuestionId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileResponses_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FileResponses_Responses_ResponseId",
                        column: x => x.ResponseId,
                        principalTable: "Responses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MultipleChoiceResponses",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ResponseId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    QuestionId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultipleChoiceResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MultipleChoiceResponses_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MultipleChoiceResponses_Responses_ResponseId",
                        column: x => x.ResponseId,
                        principalTable: "Responses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NumericResponses",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    ResponseId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    QuestionId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumericResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NumericResponses_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NumericResponses_Responses_ResponseId",
                        column: x => x.ResponseId,
                        principalTable: "Responses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TextResponses",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    ResponseId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    QuestionId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TextResponses_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TextResponses_Responses_ResponseId",
                        column: x => x.ResponseId,
                        principalTable: "Responses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Choices",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    QuestionId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MultipleChoiceResponseId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Choices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Choices_MultipleChoiceResponses_MultipleChoiceResponseId",
                        column: x => x.MultipleChoiceResponseId,
                        principalTable: "MultipleChoiceResponses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Choices_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SingleChoiceResponses",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ChoiceId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ResponseId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    QuestionId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SingleChoiceResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SingleChoiceResponses_Choices_ChoiceId",
                        column: x => x.ChoiceId,
                        principalTable: "Choices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SingleChoiceResponses_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SingleChoiceResponses_Responses_ResponseId",
                        column: x => x.ResponseId,
                        principalTable: "Responses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Choices_MultipleChoiceResponseId",
                table: "Choices",
                column: "MultipleChoiceResponseId");

            migrationBuilder.CreateIndex(
                name: "IX_Choices_QuestionId",
                table: "Choices",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_FileResponses_QuestionId",
                table: "FileResponses",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_FileResponses_ResponseId",
                table: "FileResponses",
                column: "ResponseId");

            migrationBuilder.CreateIndex(
                name: "IX_MultipleChoiceResponses_QuestionId",
                table: "MultipleChoiceResponses",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_MultipleChoiceResponses_ResponseId",
                table: "MultipleChoiceResponses",
                column: "ResponseId");

            migrationBuilder.CreateIndex(
                name: "IX_NumericResponses_QuestionId",
                table: "NumericResponses",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_NumericResponses_ResponseId",
                table: "NumericResponses",
                column: "ResponseId");

            migrationBuilder.CreateIndex(
                name: "IX_Responses_RespondentId",
                table: "Responses",
                column: "RespondentId");

            migrationBuilder.CreateIndex(
                name: "IX_Responses_SurveyId",
                table: "Responses",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_SurveyId",
                table: "Sections",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_SingleChoiceResponses_ChoiceId",
                table: "SingleChoiceResponses",
                column: "ChoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_SingleChoiceResponses_QuestionId",
                table: "SingleChoiceResponses",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_SingleChoiceResponses_ResponseId",
                table: "SingleChoiceResponses",
                column: "ResponseId");

            migrationBuilder.CreateIndex(
                name: "IX_TextResponses_QuestionId",
                table: "TextResponses",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_TextResponses_ResponseId",
                table: "TextResponses",
                column: "ResponseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Sections_SectionId",
                table: "Questions",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Sections_SectionId",
                table: "Questions");

            migrationBuilder.DropTable(
                name: "FileResponses");

            migrationBuilder.DropTable(
                name: "NumericResponses");

            migrationBuilder.DropTable(
                name: "Sections");

            migrationBuilder.DropTable(
                name: "SingleChoiceResponses");

            migrationBuilder.DropTable(
                name: "TextResponses");

            migrationBuilder.DropTable(
                name: "Choices");

            migrationBuilder.DropTable(
                name: "MultipleChoiceResponses");

            migrationBuilder.DropTable(
                name: "Responses");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "MaxValue",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "NumberInSection",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "Required",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Questions");

            migrationBuilder.RenameColumn(
                name: "SectionId",
                table: "Questions",
                newName: "SurveyId");

            migrationBuilder.RenameIndex(
                name: "IX_Questions_SectionId",
                table: "Questions",
                newName: "IX_Questions_SurveyId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Surveys",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Questions",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Surveys_SurveyId",
                table: "Questions",
                column: "SurveyId",
                principalTable: "Surveys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
