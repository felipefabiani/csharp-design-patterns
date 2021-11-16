using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpecificationDesignPattern.Migrations.Migrations
{
    public partial class AddingDirector : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DirectorId",
                table: "Movie",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Director",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Director", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movie_DirectorId",
                table: "Movie",
                column: "DirectorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movie_Director_DirectorId",
                table: "Movie",
                column: "DirectorId",
                principalTable: "Director",
                principalColumn: "Id");
            migrationBuilder.Sql($@"
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Director] ON 

GO
INSERT [dbo].[Director] ([ID], [Name]) VALUES (1, N'Marc Webb')
GO
INSERT [dbo].[Director] ([ID], [Name]) VALUES (2, N'Bill Condon')
GO
INSERT [dbo].[Director] ([ID], [Name]) VALUES (3, N'Chris Renaud')
GO
INSERT [dbo].[Director] ([ID], [Name]) VALUES (4, N'Jon Favreau')
GO
INSERT [dbo].[Director] ([ID], [Name]) VALUES (5, N'M. Night Shyamalan')
GO
INSERT [dbo].[Director] ([ID], [Name]) VALUES (6, N'Alex Kurtzman')
GO
SET IDENTITY_INSERT [dbo].[Director] OFF
GO
SET IDENTITY_INSERT [dbo].[Movie] ON 

GO
UPDATE [dbo].[Movie] SET DirectorId = Id
GO

");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movie_Director_DirectorId",
                table: "Movie");

            migrationBuilder.DropTable(
                name: "Director");

            migrationBuilder.DropIndex(
                name: "IX_Movie_DirectorId",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "DirectorId",
                table: "Movie");
        }
    }
}
