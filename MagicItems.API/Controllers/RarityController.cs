using MagicItems.Shared.Models;
using MagicItems.API.Data;
using Microsoft.AspNetCore.Mvc;

namespace MagicItems.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class RarityController : ControllerBase
{

    DataContextDapper _dapper;

    public RarityController(IConfiguration configuration)
    {
        _dapper = new DataContextDapper(configuration);
    }

    [HttpGet("GetRarity/{Id}")]
    public IEnumerable<Rarities> GetRarities(int Id = 0)
    {

        string sql = "EXEC GetRarities";
    

        if (Id != 0) sql += " @Id = @Id";


        return _dapper.LoadData<Rarities>(sql, new { Id = Id });
    }
}           
