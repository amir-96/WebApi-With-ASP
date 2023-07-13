using Microsoft.AspNetCore.Mvc;
using Web_Api_Application.Dtos.Products;

namespace Web_Api_Application.Controllers
{
  [Route("api/products")]
  [ApiController]
  public class ProductsController : ControllerBase
  {
    private readonly ILogger<ProductsController> _logger;
    public ProductsController(ILogger<ProductsController> logger)
    {
      _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public IEnumerable<GetProductDto> productList;

    [HttpGet]
    public async Task<IActionResult> GetProductsAsync()
    {
      try
      {
        productList = new List<GetProductDto>()
        {
          new GetProductDto() { Id = 1, Name = "Laptop", Description = "test", Price = 2000 },
          new GetProductDto() { Id = 2, Name = "Mobile", Description = "test", Price = 800 },
        };

        return Ok(productList);
      }
      catch (Exception ex)
      {
        _logger.LogCritical($"Failed to get all products. Error: {ex.Message}");
        return StatusCode(500, "Server error");
      }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductByIdAsync(int id)
    {
      productList = new List<GetProductDto>()
      {
        new GetProductDto() { Id = 1, Name = "Laptop", Description = "test", Price = 2000 },
        new GetProductDto() { Id = 2, Name = "Mobile", Description = "test", Price = 800 },
      };

      var singleProduct = productList.SingleOrDefault(p => p.Id == id);

      if (singleProduct == null)
      {
        _logger.LogInformation($"Product with id ({id}) not found");

        return NotFound();
      }

      return Ok(singleProduct);
    }

    #region Edit region

    [HttpPut("{id}")]
    public async Task<IActionResult> EditProductAsync(int id, CreateProductDto editedProduct)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest();
      }

      productList = new List<GetProductDto>()
      {
        new GetProductDto() { Id = 1, Name = "Laptop", Description = "test", Price = 2000 },
        new GetProductDto() { Id = 2, Name = "Mobile", Description = "test", Price = 800 },
      };

      var singleProduct = productList.SingleOrDefault(p => p.Id == id);

      if (singleProduct == null)
      {
        return NotFound();
      }

      singleProduct.Name = editedProduct.Name;
      singleProduct.Description = editedProduct.Description;
      singleProduct.Price = editedProduct.Price;

      return Ok(singleProduct);
    }

    #endregion

    #region Delete region

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProductAsync(int id)
    {
      productList = new List<GetProductDto>()
      {
        new GetProductDto() { Id = 1, Name = "Laptop", Description = "test", Price = 2000 },
        new GetProductDto() { Id = 2, Name = "Mobile", Description = "test", Price = 800 },
      };

      var singleProduct = productList.SingleOrDefault(p => p.Id == id);

      if (singleProduct == null)
      {
        return NotFound();
      }

      return NoContent();
    }

    #endregion
  }
}
