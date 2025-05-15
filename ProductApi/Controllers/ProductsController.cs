using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProductApi.Models;
using ProductApi.Services;

namespace ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: Products
        [HttpGet("GetProducts")]
        public async Task<IActionResult> Index()
        {
            return Ok(_productService.GetAll());
        }

        // GET: Categories/Details/5
        [HttpGet("GetProduct/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _productService.GetByIdAsync(id.Value).Result;

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }
    }
}
