using June2026.Database.AppDbContextModels;
using June2026.WebApiHttpClientAssignment.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace June2026.WebApiHttpClientAssignment.Api.Controllers;

// api/sale
[Route("api/[controller]")]
[ApiController]
public class SaleController : ControllerBase
{
    private readonly AppDbContext _db;
    public SaleController()
    {
        _db = new AppDbContext();
    }

    [HttpGet]
    public IActionResult GetSales()
    {
        var sales = from sale in _db.TblSales
                    join detail in _db.TblSaleDetails
                        on sale.SaleId equals detail.SaleId
                    join product in _db.TblProducts
                        on detail.ProductId equals product.ProductId
                    select new
                    {
                        sale.SaleId,
                        detail.SaleDetailId,
                        sale.TotalAmount,
                        product.ProductName,
                        detail.UnitPrice,
                        detail.Qty
                    };

        var groupSales = sales.GroupBy(x => new
        {
            x.SaleId,
            x.TotalAmount
        });

        return Ok(sales);
    }

    [HttpGet("{id}")]
    public IActionResult GetSale(int id)
    {
        var item = from sale in _db.TblSales
                   join detail in _db.TblSaleDetails
                        on sale.SaleId equals detail.SaleId
                   join product in _db.TblProducts
                        on detail.ProductId equals product.ProductId
                   where sale.SaleId == id
                   select new
                   {
                       sale.SaleId,
                       detail.SaleDetailId,
                       sale.TotalAmount,
                       product.ProductName,
                       detail.UnitPrice,
                       detail.Qty
                   };                   

        if(item is null)
        {
            return NotFound("Sale does not exist.");
        }    
        return Ok(item);
    }

    [HttpPost]
    public IActionResult CreateSale([FromBody] SaleCreateRequestModel requestModel)
    {
        // save Tbl_Sale data
        TblSale sale = new TblSale
        {
            SaleDate = requestModel.SaleDate,
            TotalAmount = requestModel.TotalAmount
        };

        _db.TblSales.Add(sale);
        _db.SaveChanges();


        //save Tbl_SaleDetail data
        foreach(var item in requestModel.SaleDetails)
        {
            TblSaleDetail detail = new TblSaleDetail
            {
                SaleId = sale.SaleId,
                ProductId = item.ProductId,
                UnitPrice = item.UnitPrice,
                Qty = item.Qty
            };

            _db.TblSaleDetails.Add(detail);
        }                              

        int result = _db.SaveChanges();

        return Ok(new SaleCreateResponseModel 
        {
            IsSuccess = result > 0,
            Message = result > 0 ? "Create Successful." : "Create Failed.",
            SaleId = sale.SaleId
        });        
    }
}
