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
    #region Get all products

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IEnumerable<ProductDTO> GetProductsAsync()
    {
      return ApplicationDbContext.ProductsList.ToList();
    }

    #endregion

    #region Get product by id

    [HttpGet("{id:int}", Name ="GetProduct")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetProductById(int id)
    {
      var product = ApplicationDbContext.ProductsList.FirstOrDefault(p => p.Id == id);

      if (product == null)
      {
        return NotFound();
      }

      return Ok(ApplicationDbContext.ProductsList.FirstOrDefault(p => p.Id == id));
    }

    #endregion

    #region Create product

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<CreateProductDTO> CreateProductAsync([FromBody]CreateProductDTO NewProduct)
    {
      if (NewProduct == null)
      {
        return BadRequest(NewProduct);
      }

      var newId = ApplicationDbContext.ProductsList.OrderByDescending(p => p.Id).FirstOrDefault().Id + 1;

      var newProduct = new ProductDTO()
      {
        Id = newId,
        Name = NewProduct.Name,
        Description = NewProduct.Description,
      };

      ApplicationDbContext.ProductsList.Add(newProduct);

      return CreatedAtRoute("GetProduct", new { id = newProduct.Id }, newProduct);
    }

    #endregion
  }
}
