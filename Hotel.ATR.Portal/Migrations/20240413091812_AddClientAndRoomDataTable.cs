using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.ATR.Portal.Migrations
{
    public partial class AddClientAndRoomDataTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoomDataid",
                table: "Rooms",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RoomData",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomData", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PathToImg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PathToImgHover = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoomDataid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clients_RoomData_RoomDataid",
                        column: x => x.RoomDataid,
                        principalTable: "RoomData",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_RoomDataid",
                table: "Rooms",
                column: "RoomDataid");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_RoomDataid",
                table: "Clients",
                column: "RoomDataid");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_RoomData_RoomDataid",
                table: "Rooms",
                column: "RoomDataid",
                principalTable: "RoomData",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_RoomData_RoomDataid",
                table: "Rooms");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "RoomData");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_RoomDataid",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "RoomDataid",
                table: "Rooms");
        }
    }
}
