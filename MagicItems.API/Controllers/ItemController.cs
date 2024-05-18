using MagicItems.Shared.Models;
using MagicItems.API.Data;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class ItemController : ControllerBase
{
    DataContextDapper _dapper;

    public ItemController(IConfiguration configuration)
    {
        _dapper = new DataContextDapper(configuration);
    }

    [HttpGet("GetItems/{Id}/{CategoryId}/{SearchItemName}/{MaxPrice}/{MinPrice}/{RarityId}")]
    public IEnumerable<Items> GetItems(
        int Id = 0, 
        int CategoryId = 0, 
        string SearchItemName = "none", 
        int MaxPrice = 0, 
        int MinPrice = 0, 
        int RarityId = 0)
    {

        string sql = "EXEC GetItems";
        string parameters = "";

        if (Id != 0) parameters += ", @Id = @Id";
        if (CategoryId != 0) parameters += ", @CategoryId = @CategoryId";
        if (SearchItemName.ToLower() != "none") parameters += ", @SearchItemName = @SearchItemName";
        if (MaxPrice != 0) parameters += ", @MaxPrice = @MaxPrice";
        if (MinPrice != 0) parameters += ", @MinPrice = @MinPrice";
        if (RarityId != 0) parameters += ", @RarityId = @RarityId";

        if(parameters.StartsWith(",")) sql += parameters.Substring(1);

        return _dapper.LoadData<Items>(sql, new { 
            @Id = Id, 
            @CategoryId = CategoryId,
            @SearchItemName = SearchItemName, 
            @MaxPrice = MaxPrice,
            @MinPrice = MinPrice,
            @RarityId = RarityId
        });
    }

    [HttpPost("AddItem/{ItemsDTO}")]
    public IActionResult AddItem(ItemsDTO itemToAdd)
    {
        string sql = @"EXEC AddItem 
                   @CategoryId = @CategoryId, 
                   @ItemName = @ItemName, 
                   @ShortDescription = @ShortDescription, 
                   @LongDescription = @LongDescription, 
                   @Price = @Price, 
                   @RarityId = @RarityId";

        _dapper.ExecuteSql(sql, new
        {
            CategoryId = itemToAdd.CategoryId,
            ItemName = itemToAdd.ItemName,
            ShortDescription = itemToAdd.ShortDescription,
            LongDescription = itemToAdd.LongDescription,
            Price = itemToAdd.Price,
            RarityId = itemToAdd.RarityId
        });

        return Ok();
    }

    [HttpDelete("DeleteItem/{itemName}")]
    public IActionResult DeleteItem(string itemName)
    {
        string sql = "EXEC DeleteItem @ItemName = @ItemName";

        _dapper.ExecuteSql(sql, new { ItemName = itemName });

        return Ok();
    }

    [HttpPut("UpdateItem/{itemId}")]
    public IActionResult UpdateItem(int itemId, ItemsDTO itemToUpdate)
    {
        string sql = @"EXEC UpdateItem 
        @Id = @itemId,
        @CategoryId = @CategoryId, 
        @ItemName = @ItemName, 
        @ShortDescription = @ShortDescription, 
        @LongDescription = @LongDescription, 
        @Price = @Price, 
        @RarityId = @RarityId";

        _dapper.ExecuteSql(sql, new
        {
            itemId,
            itemToUpdate.CategoryId,
            itemToUpdate.ItemName,
            itemToUpdate.ShortDescription,
            itemToUpdate.LongDescription,
            itemToUpdate.Price,
            itemToUpdate.RarityId
        });

        return Ok();
    }

}
