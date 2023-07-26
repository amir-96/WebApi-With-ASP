using API_Project.Data;
using API_Project.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Project.Controllers
{
  [Route("api/Products")]
  [ApiController]
  public class ProductsController : ControllerBase
  {
    [HttpGet]
    public IEnumerable<ProductDTO> GetProductsAsync()
    {
      return ApplicationDbContext.ProductsList.ToList();
    }

    [HttpGet("{id:int}")]
    public IActionResult GetProductById(int id)
    {
      var product = ApplicationDbContext.ProductsList.FirstOrDefault(p => p.Id == id);

      if (product == null)
      {
        return NotFound();
      }

      return Ok(ApplicationDbContext.ProductsList.FirstOrDefault(p => p.Id == id));
    }
  }
}
