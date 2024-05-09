using MagicItems.Shared.Models;
using MagicItems.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Collections.Generic;
using MagicItems.Shared.DTOs;
using static MudBlazor.CategoryTypes;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

    [HttpGet("GetCategory/{Id}/{CategoryName}")]
    public IEnumerable<Categories> GetCategory(int Id = 0, string? CategoryName = "none")
    {

        string sql = "EXEC GetCategories";
        string parameters = "";
    

        if (Id != 0) parameters += ", @Id = @Id";
        if (CategoryName?.ToLower() != "none") parameters += ", @CategoryName = @CategoryName";

        if (parameters.StartsWith(",")) sql += parameters.Substring(1);

        return _dapper.LoadData<Categories>(sql, new { Id = Id, CategoryName = CategoryName });
    }

    [HttpPut("CreateCategory/{CategoriesDTO}")]
    public IActionResult CreateCategory(CategoriesDTO newCategory)
    {
        string sql = "EXEC CreateCustomCategory @newCategory";

        _dapper.ExecuteSql(sql, new {@newCategory = newCategory.CategoryName });

        return Ok();
    }

    [HttpDelete("DeleteCategory/{Id}/{CategoryName}")]
    public IActionResult DeleteCategory(int Id = 0, string? CategoryName = "none")
    {
        string sql = "EXEC RemoveCategory";
        string parameters = "";

        if (Id != 0) parameters += ", @Id = @Id";
        if (CategoryName?.ToLower() != "none") parameters += ", @CategoryName = @CategoryName";

        if (parameters.StartsWith(",")) sql += parameters.Substring(1);

        _dapper.ExecuteSql(sql, new { Id = Id, CategoryName = CategoryName });

        return Ok();
    }

}           