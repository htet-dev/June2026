using June2026.Database.AppDbContextModels;
using June2026.WebApiHttpClientAssignment.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace June2026.WebApiHttpClientAssignment.Api.Controllers;

// api/product
[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly AppDbContext _db;
    public ProductController()
    {
        _db = new AppDbContext();
    }

    [HttpGet]
    public IActionResult GetProducts()
    {
        var lst = _db.TblProducts.ToList();
        return Ok(lst);
    }

    [HttpGet("{id}")]
    public IActionResult GetProduct(int id)
    {
        var item = _db.TblProducts.FirstOrDefault(x => x.ProductId == id);

        if (item is null)
        {
            return NotFound("Product does not exist.");
        }
        return Ok(item);
    }

    [HttpPost]
    public IActionResult CreateProduct([FromBody] ProductCreateRequestModel requestModel)
    {
        TblProduct product = new TblProduct
        {
            ProductName = requestModel.ProductName,
            Description = requestModel.Description,
            Price = requestModel.Price,
            ExpiryDate = requestModel.ExpiryDate,
            TotalAvailableQty = requestModel.TotalAvailableQty
        };

        _db.TblProducts.Add(product);
        int result = _db.SaveChanges();

        ProductCreateResponseModel model = new ProductCreateResponseModel
        {
            IsSuccess = result > 0,
            Message = result > 0 ? "Create Successful." : "Create Failed.",
            ProductId = product.ProductId
        };

        return Ok(model);
    }

    [HttpPatch("{id}")]
    public IActionResult PatchProduct(int id, ProductPatchRequestModel requestModel)
    {
        var item = _db.TblProducts.FirstOrDefault(x => x.ProductId == id);
        if (item is null)
        {
            return NotFound(new ProductPatchResponseModel
            {
                Message = "Product does not exist."
            });
        }

        if(!string.IsNullOrEmpty(requestModel.ProductName))
        {
            item.ProductName = requestModel.ProductName;
        }
        if (!string.IsNullOrEmpty(requestModel.Description))
        {
            item.Description = requestModel.Description;
        }
        if (requestModel.Price > 0)
        {
            item.Price = requestModel.Price;
        }
        if (requestModel.ExpiryDate is not null)
        {
            item.ExpiryDate = requestModel.ExpiryDate;
        }
        if (requestModel.TotalAvailableQty > 0)
        {
            item.TotalAvailableQty = requestModel.TotalAvailableQty;
        }

        int result =_db.SaveChanges();

        ProductPatchResponseModel model = new ProductPatchResponseModel
        {
            IsSuccess = result > 0,
            Message = result > 0 ? "Update Successful." : "Update Failed."
        };

        return Ok(model);
    }

    [HttpDelete("{productId}")]
    public IActionResult DeleteProduct([FromRoute] ProductDeleteRequestModel requestModel)
    {
        var item = _db.TblProducts.FirstOrDefault(x => x.ProductId == requestModel.ProductId);
        if (item is null)
        {
            return NotFound(new ProductDeleteResponseModel
            {
                Message = "Product does not exist."
            });
        }

        _db.Remove(item);
        int result = _db.SaveChanges();

        ProductDeleteResponseModel model = new ProductDeleteResponseModel
        {
            IsSuccess = result > 0,
            Message = result > 0 ? "Delete Successful." : "Delete Failed."
        };

        return Ok(model);
    }
}