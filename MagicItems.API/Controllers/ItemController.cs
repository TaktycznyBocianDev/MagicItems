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

    [HttpGet("GetItemIdByName/{itemName}")]
    public int GetItemIdByName(string itemName)
    {
        string sql = "EXEC GetItemIdByName @ItemName = @itemName";
        return _dapper.LoadDataSingle<int>(sql, new { itemName });
    }

    [HttpGet("GetItems/{Id}/{SearchItemName}/{MaxPrice}/{MinPrice}/{CategoryName}/{RarityName}")]
    public IEnumerable<Items> GetItems(
        int Id = 0, 
        string SearchItemName = "none", 
        int MaxPrice = 0, 
        int MinPrice = 0, 
        string CategoryName = "none",
        string RarityName = "none")
    {

        string sql = "EXEC GetItems";
        string parameters = "";

        if (Id != 0) parameters += ", @Id = @Id";
        if (SearchItemName.ToLower() != "none") parameters += ", @SearchItemName = @SearchItemName";
        if (MaxPrice != 0) parameters += ", @MaxPrice = @MaxPrice";
        if (MinPrice != 0) parameters += ", @MinPrice = @MinPrice";
        if (CategoryName.ToLower() != "none") parameters += ", @CategoryName = @CategoryName";
        if (RarityName.ToLower() != "none") parameters += ", @RarityName = @RarityName";

        if (parameters.StartsWith(",")) sql += parameters.Substring(1);

        return _dapper.LoadData<Items>(sql, new { 
            @Id = Id, 
            @SearchItemName = SearchItemName, 
            @MaxPrice = MaxPrice,
            @MinPrice = MinPrice,
            @CategoryName = CategoryName,
            @RarityName = RarityName,
        });
    }

    [HttpPost("AddItem/{ItemsDTO}")]
    public IActionResult AddItem(ItemsDTO itemToAdd)
    {
        string sql = @"EXEC AddItem 
                   @ItemName = @ItemName, 
                   @ShortDescription = @ShortDescription, 
                   @LongDescription = @LongDescription, 
                   @Price = @Price, 
                   @CategoryName = @CategoryName,
                   @RarityName = @RarityName";

        _dapper.ExecuteSql(sql, new
        {
            ItemName = itemToAdd.ItemName,
            ShortDescription = itemToAdd.ShortDescription,
            LongDescription = itemToAdd.LongDescription,
            Price = itemToAdd.Price,
            @CategoryName = itemToAdd.CategoryName,
            @RarityName = itemToAdd.RarityName

        });

        return Ok();
    }

    [HttpPost("AddListOfItems")]
    public IActionResult AddListOfItems(ItemsDTO[] itemsToAdd)
    {
        foreach (ItemsDTO item in itemsToAdd)
        {
            string sql = @"EXEC AddItem 
                   @ItemName = @ItemName, 
                   @ShortDescription = @ShortDescription, 
                   @LongDescription = @LongDescription, 
                   @Price = @Price, 
                   @CategoryName = @CategoryName,
                   @RarityName = @RarityName";

            _dapper.ExecuteSql(sql, new
            {
                ItemName = item.ItemName,
                ShortDescription = item.ShortDescription,
                LongDescription = item.LongDescription,
                Price = item.Price,
                @CategoryName = item.CategoryName,
                @RarityName = item.RarityName

            });
        }
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
        @ItemName = @ItemName, 
        @ShortDescription = @ShortDescription, 
        @LongDescription = @LongDescription, 
        @Price = @Price, 
        @CategoryName = @CategoryName,
        @RarityName = @RarityName";

        _dapper.ExecuteSql(sql, new
        {
            itemId,
            itemToUpdate.ItemName,
            itemToUpdate.ShortDescription,
            itemToUpdate.LongDescription,
            itemToUpdate.Price,
            itemToUpdate.CategoryName,
            itemToUpdate.RarityName,
        });

        return Ok();
    }



}
