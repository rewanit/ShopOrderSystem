using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopOrderSystem.Models.DatabaseModels;
using ShopOrderSystem.Models.DTO.Product;
using ShopOrderSystem.Services.Interfaces;

namespace ShopOrderSystem.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    [Authorize]

    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly IMapper mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            this.productService = productService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получение всех товаров
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllProducts()
        {
            var products = await productService.GetAllAsync();
            var productDtos = mapper.Map<IEnumerable<ProductDTO>>(products);
            return Ok(productDtos);
        }

        /// <summary>
        /// Получение товара по ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProductById(int id)
        {
            var product = await productService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            var productDto = mapper.Map<ProductDTO>(product);
            return Ok(productDto);
        }

        /// <summary>
        /// Создание нового товара
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> CreateProduct(ProductDTO productDto)
        {
            var product = mapper.Map<Product>(productDto);
            await productService.AddAsync(product);
            var productResultDto = mapper.Map<ProductDTO>(product);
            return CreatedAtAction(nameof(GetProductById), new { id = product.ProductId }, productResultDto);
        }

        /// <summary>
        /// Обновление товара
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, ProductDTO productDto)
        {
            if (id != productDto.ProductId)
            {
                return BadRequest();
            }

            var existingProduct = await productService.GetByIdAsync(id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            var product = mapper.Map<Product>(productDto);
            await productService.UpdateAsync(product);
            return NoContent();
        }

        /// <summary>
        /// Удаление товара
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            await productService.DeleteAsync(id);
            return NoContent();
        }


        /// <summary>
        /// Получение списка товаров с фильтрацией и сортировкой
        /// </summary>
        /// <param name="filterDto"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts([FromQuery] ProductFilterDTO filterDto)
        {
            var products = await productService.GetFilteredProductsAsync(filterDto.ProductTypeId, filterDto.InStock, filterDto.SortOrder);
            var productDtos = mapper.Map<IEnumerable<ProductDTO>>(products);
            return Ok(productDtos);
        }


    }
}
