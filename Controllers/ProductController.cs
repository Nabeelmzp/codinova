using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CodinovaTask.Entities;
using CodinovaTask.Helper;
using CodinovaTask.Model;
using CodinovaTask.Repository.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CodinovaTask.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller 
    {
        private readonly CodinovaTaskcontext _context;
      //  private readonly UserManager<User> _userManager; 
         private readonly IHttpContextAccessor _httpContextAccessor;
        public ProductController(CodinovaTaskcontext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            // _userManager = userManager;

            _httpContextAccessor = httpContextAccessor;
        }
        // GET: /<controller>/
        [HttpGet] 
        [Route("GetProduct")]
        public async Task<ActionResult> GetProduct()   
        {  
            var product = await  _context.Products.ToListAsync();

            try
            {
                if (product != null)
                {

                    var resultReturnModel = new ReturnModel(enumReturnStatus.Success, "Product_List", "Product Details", product); 
                    return Ok(resultReturnModel);
                }

                else
                {
                    var resultReturnModel = new ReturnModel(enumReturnStatus.Failed, "No Product Not Fount", " No Product Not Fount", null);
                    return Ok(resultReturnModel);
                }
            }
            catch (Exception)
            {

                throw;
            }
          
        }
        
        //Pass the Id in QueryString Along the URL
        [HttpGet]
        [Route("GetProductById")]
        public async Task<ActionResult> GetProductById(int id) 
        {
            var result =  _context.Products.FirstOrDefault(x=> x.ProductId ==id); 

            try
            {
                if (result != null)
                {
                    var resultReturnModel = new ReturnModel(enumReturnStatus.Success, "Product Details", "Product Details", result);
                    return Ok(resultReturnModel);
                }
                else
                {
                    var resultReturnModel = new ReturnModel(enumReturnStatus.Failed, "Product Not Found", "Product Not Found", null);
                    return Ok(resultReturnModel);
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpPost]
        [Route("CreateProduct")]
        public async Task<ActionResult> CreateProduct(Product model)
        {
            try
            {
                if(model != null)
                {
                    //model.CreatedDate = DateTime.Now; 

                   await _context.Products.AddAsync(model);   
                    _context.SaveChanges();
                    return new OkObjectResult(new ReturnModel(enumReturnStatus.Success, "Product Created", "Product Created Cuccessfully.", model));
                }

                else
                {
                    return new OkObjectResult(new ReturnModel(enumReturnStatus.Failed, "Please Pass Proper Product", "Product Not Created.", null));
                }
            }
            catch (Exception)
            {

                throw;
            }

            
        }


        [HttpPut("{id}")]   
        [Route("EditProduct")]
        public async Task<IActionResult> EditProduct( int id, Product model)  
        {
            try
            {
               
                 if (id != model.ProductId)
                {
                    return Ok(new ReturnModel(enumReturnStatus.Failed, "model_invalid", "Mandatory field are required.", model));
                }
                else
                {
                    var Data =  _context.Products.SingleOrDefault(x=> x.ProductId == id);
                    if (Data != null)
                    {
                        Data.ProductName = model.ProductName;
                        Data.Price = model.Price;
                        Data.Description = model.Description;
                        Data.ProductImage = model.ProductImage;
                        Data.Quantity = model.Quantity;
                        _context.Products.Update(Data);
                        _context.SaveChanges();
                        return new OkObjectResult(new ReturnModel(enumReturnStatus.Success, "Product_updated", "Product updated successfully."));
                    }
                    else
                    {
                        var resultReturnModel = new ReturnModel(enumReturnStatus.Failed, "Product Not found", "product details not found.");
                        return Ok(resultReturnModel);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete]
        [Route("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(int id)  
        {

            try
            {
                var result = _context.Products.SingleOrDefault(x => x.ProductId == id);
                if (result == null)
                {
                    return Ok(new ReturnModel(enumReturnStatus.Failed, "No Data Found to be deleted", "No Product found."));

                }
                else
                {
                    _context.Remove(result);
                    _context.SaveChanges();
                    return Ok(new ReturnModel(enumReturnStatus.Success, "Product Deleted", "Product deleted successfully."));
                }
            }
            catch (Exception)
            {

                throw;
            }
          
        }

    }
}
