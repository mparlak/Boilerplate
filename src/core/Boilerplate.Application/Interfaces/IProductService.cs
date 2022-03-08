using Boilerplate.Application.Common.Models;
using Boilerplate.Application.Dtos.Product;

namespace Boilerplate.Application.Interfaces;

public interface IProductService
{
    Task<BaseResponse<PagedResponse<ProductDto>>> GetAllAsync(ProductRequest request);
    Task<BaseResponse<ProductDto>> GetByIdAsync(int id);
    Task<BaseResponse<int>> CreateProductAsync(CreateProductRequest request);
    Task<BaseResponse<ProductDto>> UpdateProductAsync(int id, UpdateProductRequest request);
    Task<BaseResponse<bool>> DeleteProductAsync(DeleteProductRequest request);
}