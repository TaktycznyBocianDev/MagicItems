using MagicItems.Shared.Models;
using MagicItems.API.Data;
using Microsoft.AspNetCore.Mvc;
using Dapper;

namespace MagicItems.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShopController : ControllerBase
    {
        DataContextDapper _dapper;

        public ShopController(IConfiguration configuration)
        {
            _dapper = new DataContextDapper(configuration);
        }

        [HttpGet("GetShops/{ShopName}")]
        public IEnumerable<Shop> GetShops(string ShopName = "none") 
        {
            string sql = "EXEC GetShops ";

            if (ShopName.ToLower() != "none") sql += "@ShopName = @ShopName";

            return _dapper.LoadData<Shop>(sql, new { @ShopName = ShopName });
        }

        [HttpPost("CreateShop")]
        public IActionResult CreateShop(ShopDTO newShop)
        {
            string sql = "EXEC CreateShop @ShopName = @ShopName";
            if (newShop.ShopOwner is not null) sql += ", @ShopOwner = @ShopOwner";

            _dapper.ExecuteSql(sql, new { @ShopName = newShop.ShopName, @ShopOwner = newShop.ShopOwner});
            return Ok();
            
        }

        [HttpPut("UpdateShop")]
        public IActionResult UpdateShop(int shopId, ShopDTO ShopToUpdate)
        {
            string sql = @"EXEC UpdateShop
            @Id = @Id,
            @ShopName = @ShopName,
            @ShopOwner = @ShopOwner";

            _dapper.ExecuteSql(sql, new { @Id = shopId, @ShopName = ShopToUpdate.ShopName, @ShopOwner = ShopToUpdate.ShopOwner });
            return Ok();
        }

        [HttpDelete("DeleteShop")]
        public IActionResult DeleteShop(string ShopName)
        {
            string sql = "EXEC DeleteShop @ShopName = @ShopName";

            _dapper.ExecuteSql(sql, new { @ShopName = ShopName });
            return Ok();
        }
    }

}
