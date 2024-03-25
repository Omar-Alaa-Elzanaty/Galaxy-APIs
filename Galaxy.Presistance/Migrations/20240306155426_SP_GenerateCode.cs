using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Galaxy.Presistance.Migrations
{
    /// <inheritdoc />
    public partial class SP_GenerateCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("create proc Galaxy.GenerateBarCode @StartSerial int,@productId int,@SupplierId int,@quantity int,@IntialCode varchar(max) " +
                                 " with encryption" +
                                 " as" +
                                 " declare @Serial varchar(max);" +
                                 " declare @Count int =0;" +
                                 " while @Count< @quantity" +
                                 " begin" +
                                 " declare @item varchar(max)=convert(varchar(max),@startSerial);" +
                                 " set @Serial=@IntialCode+(select REPLICATE('0', 5-Len(@item))+@item as newitem);" +
                                 " insert into Galaxy.ItemsInStock (IsInStock,ProductId,SupplierId,BarCode) values(1,@productId,@SupplierId,@Serial);" +
                                 " set @StartSerial+=1;" +
                                 " set @count+=1;" +
                                 " end");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("drop proc Galaxy.GenerateBarCode");
        }
    }
}
