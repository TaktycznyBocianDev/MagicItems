using MagicItems.API.Data;
using MagicItems.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using static MudBlazor.CategoryTypes;

namespace MagicItems.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShopItemRelationController : ControllerBase
    {

        DataContextDapper _dapper;
        ShopController _shopController;
        ItemController _itemController;

        public ShopItemRelationController(IConfiguration configuration)
        {
            _dapper = new DataContextDapper(configuration);
            _shopController = new ShopController(configuration);
            _itemController = new ItemController(configuration);
        }

        [HttpGet("GetItemsInShop")]
        public IEnumerable<Items> GetItemsInShop(string ShopName)
        {
            string sql = "EXEC GetItemsInShop @ShopName = @ShopName";
            return _dapper.LoadData<Items>(sql, new { @ShopName = ShopName });
        }

        [HttpPut("AddOneItemToShop")]
        public IActionResult AddOneItemToShop(string ShopName, string ItemName)
        {

            int ShopId = _shopController.GetShopIdByName(ShopName);
            int ItemId = _itemController.GetItemIdByName(ItemName);

            string sql = "EXEC AddItemToShop @ShopId = @ShopId, @ItemId = @ItemId";

            _dapper.ExecuteSql(sql, new { @ShopId = ShopId, @ItemId = ItemId });
            return Ok();
        }

        [HttpPut("AddItemListToShop")]
        public IActionResult AddItemListToShop(string ShopName, Items[] items)
        {
            int ShopId = _shopController.GetShopIdByName(ShopName);
            string sql = "EXEC AddItemToShop @ShopId = @ShopId, @ItemId = @ItemId";

            foreach (Items item in items)
            {
                _dapper.ExecuteSql(sql, new { @ShopId = ShopId, @ItemId = item.Id });
            }
            return Ok();
        }

        [HttpPut("AddItemDTOsToShop")]
        public IActionResult AddItemDTOsToShop(string ShopName, ItemsDTO[] items)
        {

            int ShopId = _shopController.GetShopIdByName(ShopName);
            string sql = "EXEC AddItemToShop @ShopId = @ShopId, @ItemId = @ItemId";

            foreach (ItemsDTO item in items)
            {
                int ItemId = _itemController.GetItemIdByName(item.ItemName);
                _dapper.ExecuteSql(sql, new { @ShopId = ShopId, @ItemId = ItemId });
            }
            return Ok();

        }

        [HttpDelete("RemoveItemFromShop")]
        public IActionResult RemoveItemFromShop(string ShopName, string ItemName)
        {

            int ShopId = _shopController.GetShopIdByName(ShopName);
            int ItemId = _itemController.GetItemIdByName(ItemName);

            string sql = "EXEC RemoveItemFromShop @ShopId = @ShopId, @ItemId = @ItemId";

            _dapper.ExecuteSql(sql, new { @ShopId = ShopId, @ItemId = ItemId });
            return Ok();
        }

    }
}
