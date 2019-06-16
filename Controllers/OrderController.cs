using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CodinovaTask.Entities;
using CodinovaTask.Helper;
using CodinovaTask.Repository.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CodinovaTask.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly CodinovaTaskcontext _context;

        public OrderController(CodinovaTaskcontext context)
        {
            _context = context;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {

            return View();
        }

        [HttpGet]
        [Route("PlaceOrder")]
        public ActionResult PlaceOrder(SaveOrder model)
        {
            try
            {
                if(model != null)
                {
                    var UserId  = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
                    model.OrderBy =int.Parse(UserId);
                    var product = _context.Products.SingleOrDefault(x => x.ProductId == model.ProductId);
                    var availableQuantity = product.Quantity;
                    var appliedQuantity = model.Quantity;

                    if(appliedQuantity > availableQuantity)
                    {
                        var resultReturnModel = new ReturnModel(enumReturnStatus.Failed, "There is no such quanties vailable", "Select Within a given quantity", null);
                        return Ok(resultReturnModel);
                    }

                    else if (appliedQuantity == availableQuantity)
                    {
                        _context.SaveOrders.Add(model);
                        _context.SaveChanges();
                        var data = _context.Products.SingleOrDefault(x => x.ProductId == model.ProductId);
                        _context.Remove(data);
                        _context.SaveChanges();
                        var resultReturnModel = new ReturnModel(enumReturnStatus.Success, "Order Purchaes", "Purchased", null);
                        return Ok(resultReturnModel);
                    }

                    else if(appliedQuantity < availableQuantity)
                    {
                        _context.SaveOrders.Add(model);
                        _context.SaveChanges();
                        var data = _context.Products.SingleOrDefault(x => x.ProductId == model.ProductId);
                        var CurrentQuantity = availableQuantity - appliedQuantity;
                        data.Quantity = CurrentQuantity;
                        _context.Update(data);
                        _context.SaveChanges();
                        var resultReturnModel = new ReturnModel(enumReturnStatus.Success, "Order Purchaes", "Purchased", null);
                        return Ok(resultReturnModel);
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            return null;
        }
    }
}
