using Boilerplate.Application.Common.Models;
using Boilerplate.Application.Common.Persistence;
using Boilerplate.Application.Dtos.Product;
using Boilerplate.Application.Interfaces;
using Boilerplate.Domain.Entities;

namespace Boilerplate.Application.Services;

public class ProductService:IProductService
{
    private readonly IRepository<Product> _repository;

    public ProductService(IRepository<Product> repository)
    {
        _repository = repository;
    }

    public async Task<BaseResponse<PagedResponse<ProductDto>>> GetAllAsync(ProductRequest request)
    {
        var response = new BaseResponse<PagedResponse<ProductDto>>();

        var abc = _repository.GetAll().ToList();

        response.Result = new PagedResponse<ProductDto>
        {
            Index = (request.Offset / request.Limit) + 1,
            PageSize = request.Limit
            //Total = list.Count(),
            //Items = list.OrderByDescending(w => w.Created).Skip(request.Offset).Take(request.Limit).Select(x => x.ToTemplateModel()).ToList()
        };

        return await Task.FromResult(response);
    }

    public async Task<BaseResponse<ProductDto>> GetByIdAsync(int id)
    {
        var response = new BaseResponse<ProductDto>();

        //Get and Map

        return await Task.FromResult(response);
    }

    public async Task<BaseResponse<int>> CreateProductAsync(CreateProductRequest request)
    {
        var response = new BaseResponse<int>();

        return await Task.FromResult(response);
    }

    public async Task<BaseResponse<ProductDto>> UpdateProductAsync(int id, UpdateProductRequest request)
    {
        var response = new BaseResponse<ProductDto>();


        return await Task.FromResult(response);
    }

    public async Task<BaseResponse<bool>> DeleteProductAsync(DeleteProductRequest request)
    {
        var response = new BaseResponse<bool>();


        return await Task.FromResult(response);
    }
}