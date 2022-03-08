using Boilerplate.Application.Common.Models;
using Boilerplate.Application.Dtos.Product;
using Boilerplate.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Boilerplate.Api.Controllers.v1;

public class ProductController:BaseApiController
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(BaseResponse<PagedResponse<ProductDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse<PagedResponse<ProductDto>>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get([FromQuery] ProductRequest request)
    {
        var response = await _productService.GetAllAsync(request);

        if (!response.HasError)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(BaseResponse<ProductDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse<ProductDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(BaseResponse<ProductDto>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var response = await _productService.GetByIdAsync(id);

        if (!response.HasError)
        {
            return Ok(response);
        }

        if (!response.HasError && response.Result == null)
        {
            return NotFound(response);
        }

        return BadRequest(response);
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(BaseResponse<Guid>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(BaseResponse<Guid>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody] CreateProductRequest request)
    {
        var response = await _productService.CreateProductAsync(request);
    
        if (!response.HasError)
        {
            return Created("", response);
        }
    
        return BadRequest(response);
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Put(int id, [FromBody] UpdateProductRequest request)
    {
        return Ok();
    }
    
    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    public IActionResult Patch()
    {
        return Accepted();
    }
    
    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Delete([FromRoute]int id)
    {
        return NoContent();
    }
}