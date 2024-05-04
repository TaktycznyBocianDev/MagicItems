using MagicItems.Shared.Models;
using MagicItems.API.Data;
using Microsoft.AspNetCore.Mvc;

namespace MagicItems.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{

    DataContextDapper _dapper;

    public CategoryController(IConfiguration configuration)
    {
        _dapper = new DataContextDapper(configuration);
    }

    [HttpGet("GetCategory/{Id}")]
    public IEnumerable<Categories> GetCategory(int Id = 0)
    {

        string sql = "EXEC GetCategories";
    

        if (Id != 0) sql += " @Id = @Id";


        return _dapper.LoadData<Categories>(sql, new { Id = Id });
    } 
}           