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
    public IEnumerable<Category> GetCategory(int Id = 0, string? CategoryName = "none")
    {

        string sql = "EXEC GetCategories";
        string parameters = "";
    

        if (Id != 0) parameters += ", @Id = @Id";
        if (CategoryName?.ToLower() != "none") parameters += ", @CategoryName = @CategoryName";

        if (parameters.StartsWith(",")) sql += parameters.Substring(1);

        return _dapper.LoadData<Category>(sql, new { Id = Id, CategoryName = CategoryName });
    }

    [HttpPost("CreateCategory/{CategoriesDTO}")]
    public IActionResult CreateCategory(CategoryDTO newCategory)
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

    [HttpPut("UpdateCategory/{Id}/{CategoryNewName}")]
    public IActionResult UpdateCategory(int Id = 0, string? CategoryNewName = "none")
    {
        string sql = "EXEC UpdateCategory @Id, @CategoryName";

        _dapper.ExecuteSql(sql, new { Id = Id, CategoryName = CategoryNewName });

        return Ok();
    }



}           