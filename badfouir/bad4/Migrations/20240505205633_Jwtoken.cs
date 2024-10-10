using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bad4.Migrations
{
    /// <inheritdoc />
    public partial class Jwtoken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Batch",
                keyColumn: "BatchID",
                keyValue: 1,
                columns: new[] { "FinishTime", "StartTime" },
                values: new object[] { new DateTime(2024, 5, 5, 23, 36, 33, 159, DateTimeKind.Local).AddTicks(3089), new DateTime(2024, 5, 5, 22, 56, 33, 159, DateTimeKind.Local).AddTicks(3030) });

            migrationBuilder.UpdateData(
                table: "Batch",
                keyColumn: "BatchID",
                keyValue: 2,
                columns: new[] { "FinishTime", "StartTime" },
                values: new object[] { new DateTime(2024, 5, 6, 0, 6, 33, 159, DateTimeKind.Local).AddTicks(3096), new DateTime(2024, 5, 5, 23, 46, 33, 159, DateTimeKind.Local).AddTicks(3095) });

            migrationBuilder.UpdateData(
                table: "Batch",
                keyColumn: "BatchID",
                keyValue: 3,
                columns: new[] { "FinishTime", "StartTime" },
                values: new object[] { new DateTime(2024, 5, 6, 0, 36, 33, 159, DateTimeKind.Local).AddTicks(3099), new DateTime(2024, 5, 6, 0, 6, 33, 159, DateTimeKind.Local).AddTicks(3098) });

            migrationBuilder.UpdateData(
                table: "Batch",
                keyColumn: "BatchID",
                keyValue: 4,
                columns: new[] { "FinishTime", "StartTime" },
                values: new object[] { new DateTime(2024, 5, 6, 1, 26, 33, 159, DateTimeKind.Local).AddTicks(3102), new DateTime(2024, 5, 6, 1, 6, 33, 159, DateTimeKind.Local).AddTicks(3101) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Batch",
                keyColumn: "BatchID",
                keyValue: 1,
                columns: new[] { "FinishTime", "StartTime" },
                values: new object[] { new DateTime(2024, 5, 5, 23, 35, 46, 830, DateTimeKind.Local).AddTicks(442), new DateTime(2024, 5, 5, 22, 55, 46, 830, DateTimeKind.Local).AddTicks(350) });

            migrationBuilder.UpdateData(
                table: "Batch",
                keyColumn: "BatchID",
                keyValue: 2,
                columns: new[] { "FinishTime", "StartTime" },
                values: new object[] { new DateTime(2024, 5, 6, 0, 5, 46, 830, DateTimeKind.Local).AddTicks(455), new DateTime(2024, 5, 5, 23, 45, 46, 830, DateTimeKind.Local).AddTicks(452) });

            migrationBuilder.UpdateData(
                table: "Batch",
                keyColumn: "BatchID",
                keyValue: 3,
                columns: new[] { "FinishTime", "StartTime" },
                values: new object[] { new DateTime(2024, 5, 6, 0, 35, 46, 830, DateTimeKind.Local).AddTicks(460), new DateTime(2024, 5, 6, 0, 5, 46, 830, DateTimeKind.Local).AddTicks(458) });

            migrationBuilder.UpdateData(
                table: "Batch",
                keyColumn: "BatchID",
                keyValue: 4,
                columns: new[] { "FinishTime", "StartTime" },
                values: new object[] { new DateTime(2024, 5, 6, 1, 25, 46, 830, DateTimeKind.Local).AddTicks(468), new DateTime(2024, 5, 6, 1, 5, 46, 830, DateTimeKind.Local).AddTicks(465) });
        }
    }
}
