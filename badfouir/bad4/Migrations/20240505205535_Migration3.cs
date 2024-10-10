using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bad4.Migrations
{
    /// <inheritdoc />
    public partial class Migration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Batch",
                keyColumn: "BatchID",
                keyValue: 1,
                columns: new[] { "FinishTime", "StartTime" },
                values: new object[] { new DateTime(2024, 5, 5, 23, 35, 34, 603, DateTimeKind.Local).AddTicks(2135), new DateTime(2024, 5, 5, 22, 55, 34, 603, DateTimeKind.Local).AddTicks(2074) });

            migrationBuilder.UpdateData(
                table: "Batch",
                keyColumn: "BatchID",
                keyValue: 2,
                columns: new[] { "FinishTime", "StartTime" },
                values: new object[] { new DateTime(2024, 5, 6, 0, 5, 34, 603, DateTimeKind.Local).AddTicks(2143), new DateTime(2024, 5, 5, 23, 45, 34, 603, DateTimeKind.Local).AddTicks(2141) });

            migrationBuilder.UpdateData(
                table: "Batch",
                keyColumn: "BatchID",
                keyValue: 3,
                columns: new[] { "FinishTime", "StartTime" },
                values: new object[] { new DateTime(2024, 5, 6, 0, 35, 34, 603, DateTimeKind.Local).AddTicks(2146), new DateTime(2024, 5, 6, 0, 5, 34, 603, DateTimeKind.Local).AddTicks(2145) });

            migrationBuilder.UpdateData(
                table: "Batch",
                keyColumn: "BatchID",
                keyValue: 4,
                columns: new[] { "FinishTime", "StartTime" },
                values: new object[] { new DateTime(2024, 5, 6, 1, 25, 34, 603, DateTimeKind.Local).AddTicks(2150), new DateTime(2024, 5, 6, 1, 5, 34, 603, DateTimeKind.Local).AddTicks(2148) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Batch",
                keyColumn: "BatchID",
                keyValue: 1,
                columns: new[] { "FinishTime", "StartTime" },
                values: new object[] { new DateTime(2024, 5, 5, 23, 35, 27, 779, DateTimeKind.Local).AddTicks(1728), new DateTime(2024, 5, 5, 22, 55, 27, 779, DateTimeKind.Local).AddTicks(1669) });

            migrationBuilder.UpdateData(
                table: "Batch",
                keyColumn: "BatchID",
                keyValue: 2,
                columns: new[] { "FinishTime", "StartTime" },
                values: new object[] { new DateTime(2024, 5, 6, 0, 5, 27, 779, DateTimeKind.Local).AddTicks(1735), new DateTime(2024, 5, 5, 23, 45, 27, 779, DateTimeKind.Local).AddTicks(1733) });

            migrationBuilder.UpdateData(
                table: "Batch",
                keyColumn: "BatchID",
                keyValue: 3,
                columns: new[] { "FinishTime", "StartTime" },
                values: new object[] { new DateTime(2024, 5, 6, 0, 35, 27, 779, DateTimeKind.Local).AddTicks(1738), new DateTime(2024, 5, 6, 0, 5, 27, 779, DateTimeKind.Local).AddTicks(1737) });

            migrationBuilder.UpdateData(
                table: "Batch",
                keyColumn: "BatchID",
                keyValue: 4,
                columns: new[] { "FinishTime", "StartTime" },
                values: new object[] { new DateTime(2024, 5, 6, 1, 25, 27, 779, DateTimeKind.Local).AddTicks(1741), new DateTime(2024, 5, 6, 1, 5, 27, 779, DateTimeKind.Local).AddTicks(1740) });
        }
    }
}
