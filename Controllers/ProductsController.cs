using API_Project.Data;
using API_Project.Models;
using API_Project.Models.Dto;
using API_Project.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Project.Controllers
{
  [Route("api/products")]
  [ApiController]
  public class ProductsController : ControllerBase
  {
    private readonly ApplicationDbContext _context;
    private readonly ILogger _logger;
    public ProductsController(ApplicationDbContext context, ILogger<ProductsController> logger)
    {
      _context = context;
      _logger = logger;
    }

    #region Get all products

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductsAsync()
    {
      _logger.LogInformation("Get all products");

      try
      {
        var products = await _context.Products.ToListAsync();

        return Ok(products);
      }
      catch (Exception ex)
      {
        _logger.LogError($"Get all products failed. Error: {ex.Message}");

        return StatusCode(500, "An error occurred while processing your request.");
      }
    }

    #endregion

    #region Get product by id

    [HttpGet("{id:int}", Name = "GetProduct")]
    public async Task<ActionResult<ProductDTO>> GetProductById(int id)
    {
      try
      {
        var product = await _context.Products.SingleOrDefaultAsync(p => p.Id == id);

        if (product == null)
        {
          _logger.LogError($"Get product error: Product id not found. (id: {id})");

          return NotFound();
        }

        _logger.LogInformation($"Get product by id. (id: {id})");

        return Ok(product);
      }
      catch (Exception ex)
      {
        _logger.LogError($"Get product failed. Error: {ex.Message}");

        return StatusCode(500, "An error occurred while processing your request.");
      }
    }

    #endregion

    #region Create product

    [HttpPost, Authorize(Roles = SD.Admin + "," + SD.Moderator)]
    public async Task<ActionResult<ProductDTO>> CreateProductAsync([FromBody] CreateProductDTO NewProduct)
    {
      try
      {
        if (NewProduct == null)
        {
          return BadRequest(NewProduct);
        }

        var newProduct = new Product()
        {
          Name = NewProduct.Name,
          Description = NewProduct.Description,
          Category = NewProduct.Category,
          ImageUrl = NewProduct.ImageUrl,
          Price = NewProduct.Price,
          CreatedDate = DateTime.Now.ToUniversalTime(),
        };

        await _context.Products.AddAsync(newProduct);
        await _context.SaveChangesAsync();

        return CreatedAtRoute("GetProduct", new { id = newProduct.Id }, newProduct);
      }
      catch (Exception ex)
      {
        _logger.LogError($"Create product failed. Error: {ex.Message}");

        return StatusCode(500, "An error occurred while processing your request.");
      }
    }

    #endregion

    #region EditProduct

    [HttpPut("{id:int}"), Authorize(Roles = SD.Admin + "," + SD.Moderator)]
    public async Task<ActionResult> EditProduct(int id, [FromBody] CreateProductDTO editedProduct)
    {
      try
      {
        var GetProduct = await _context.Products.SingleOrDefaultAsync(p => p.Id == id);

        if (editedProduct == null || GetProduct == null)
        {
          return BadRequest(editedProduct);
        }

        GetProduct.Name = editedProduct.Name;
        GetProduct.Description = editedProduct.Description;
        GetProduct.Category = editedProduct.Category;
        GetProduct.ImageUrl = editedProduct.ImageUrl;
        GetProduct.Price = editedProduct.Price;
        GetProduct.UpdatedDate = DateTime.Now.ToUniversalTime();

        await _context.SaveChangesAsync();

        return NoContent();
      }
      catch (Exception ex)
      {
        _logger.LogError($"Edit product failed. Error: {ex.Message}");

        return StatusCode(500, "An error occurred while processing your request.");
      }
    }

    #endregion

    #region Edit partial product

    [HttpPatch("{id:int}"), Authorize(Roles = SD.Admin + "," + SD.Moderator)]
    public async Task<ActionResult> EditPartialProductAsync(int id, JsonPatchDocument<ProductDTO> patchProduct)
    {
      try
      {
        var product = await _context.Products.SingleOrDefaultAsync(p => p.Id == id);

        if (product == null || patchProduct == null)
        {
          return NotFound();
        }

        ProductDTO productDto = new()
        {
          Id = product.Id,
          Name = product.Name,
          Description = product.Description,
          Category = product.Category,
          ImageUrl = product.ImageUrl,
          Price = product.Price,
          CreatedDate = product.CreatedDate,
          UpdatedDate = product.UpdatedDate,
        };

        patchProduct.ApplyTo(productDto, ModelState);

        if (!ModelState.IsValid)
        {
          return BadRequest();
        }

        product.Id = productDto.Id;
        product.Name = productDto.Name;
        product.Description = productDto.Description;
        product.Category = productDto.Category;
        product.ImageUrl = productDto.ImageUrl;
        product.Price = productDto.Price;
        product.CreatedDate = productDto.CreatedDate;
        product.UpdatedDate = productDto.UpdatedDate;

        await _context.SaveChangesAsync();

        return NoContent();
      }
      catch (Exception ex)
      {
        _logger.LogError($"Edit partial product failed. Error: {ex.Message}");

        return StatusCode(500, "An error occurred while processing your request.");
      }
    }

    #endregion

    #region Delete product

    [HttpDelete("{id:int}"), Authorize(Roles = SD.Admin + "," + SD.Moderator)]
    public async Task<ActionResult> DeleteProductAsync(int id)
    {
      try
      {
        var product = await _context.Products.SingleOrDefaultAsync(p => p.Id == id);

        if (product == null)
        {
          return NotFound();
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return NoContent();
      }
      catch (Exception ex)
      {
        _logger.LogError($"Delete product failed. Error: {ex.Message}");

        return StatusCode(500, "An error occurred while processing your request.");
      }
    }

    #endregion
  }
}
