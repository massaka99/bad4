using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bad4.Migrations
{
    /// <inheritdoc />
    public partial class Migration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Batch",
                keyColumn: "BatchID",
                keyValue: 1,
                columns: new[] { "FinishTime", "StartTime" },
                values: new object[] { new DateTime(2024, 5, 5, 23, 35, 19, 402, DateTimeKind.Local).AddTicks(6057), new DateTime(2024, 5, 5, 22, 55, 19, 402, DateTimeKind.Local).AddTicks(6003) });

            migrationBuilder.UpdateData(
                table: "Batch",
                keyColumn: "BatchID",
                keyValue: 2,
                columns: new[] { "FinishTime", "StartTime" },
                values: new object[] { new DateTime(2024, 5, 6, 0, 5, 19, 402, DateTimeKind.Local).AddTicks(6064), new DateTime(2024, 5, 5, 23, 45, 19, 402, DateTimeKind.Local).AddTicks(6063) });

            migrationBuilder.UpdateData(
                table: "Batch",
                keyColumn: "BatchID",
                keyValue: 3,
                columns: new[] { "FinishTime", "StartTime" },
                values: new object[] { new DateTime(2024, 5, 6, 0, 35, 19, 402, DateTimeKind.Local).AddTicks(6067), new DateTime(2024, 5, 6, 0, 5, 19, 402, DateTimeKind.Local).AddTicks(6066) });

            migrationBuilder.UpdateData(
                table: "Batch",
                keyColumn: "BatchID",
                keyValue: 4,
                columns: new[] { "FinishTime", "StartTime" },
                values: new object[] { new DateTime(2024, 5, 6, 1, 25, 19, 402, DateTimeKind.Local).AddTicks(6070), new DateTime(2024, 5, 6, 1, 5, 19, 402, DateTimeKind.Local).AddTicks(6069) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Batch",
                keyColumn: "BatchID",
                keyValue: 1,
                columns: new[] { "FinishTime", "StartTime" },
                values: new object[] { new DateTime(2024, 5, 5, 23, 35, 3, 652, DateTimeKind.Local).AddTicks(8190), new DateTime(2024, 5, 5, 22, 55, 3, 652, DateTimeKind.Local).AddTicks(8119) });

            migrationBuilder.UpdateData(
                table: "Batch",
                keyColumn: "BatchID",
                keyValue: 2,
                columns: new[] { "FinishTime", "StartTime" },
                values: new object[] { new DateTime(2024, 5, 6, 0, 5, 3, 652, DateTimeKind.Local).AddTicks(8199), new DateTime(2024, 5, 5, 23, 45, 3, 652, DateTimeKind.Local).AddTicks(8197) });

            migrationBuilder.UpdateData(
                table: "Batch",
                keyColumn: "BatchID",
                keyValue: 3,
                columns: new[] { "FinishTime", "StartTime" },
                values: new object[] { new DateTime(2024, 5, 6, 0, 35, 3, 652, DateTimeKind.Local).AddTicks(8203), new DateTime(2024, 5, 6, 0, 5, 3, 652, DateTimeKind.Local).AddTicks(8201) });

            migrationBuilder.UpdateData(
                table: "Batch",
                keyColumn: "BatchID",
                keyValue: 4,
                columns: new[] { "FinishTime", "StartTime" },
                values: new object[] { new DateTime(2024, 5, 6, 1, 25, 3, 652, DateTimeKind.Local).AddTicks(8207), new DateTime(2024, 5, 6, 1, 5, 3, 652, DateTimeKind.Local).AddTicks(8206) });
        }
    }
}
