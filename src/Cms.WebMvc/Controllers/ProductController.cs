using AutoMapper;
using Cms.Application.Admin.Models.Product;
using Cms.Infrastructure.Persistence.Entities;
using Cms.Infrastructure.Persistence.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cms.WebMvc.Controllers;

[Route("product")]
public class ProductController : Controller
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;
    
    public ProductController(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    // GET
    //[Route("abc")]
    public IActionResult Index()
    {
        return View();
    }
    
    [Route("add")]
    // GET
    public async Task<IActionResult> Add()
    {
        Random random = new Random(100);
        int index = random.Next();
        CreateProductDto productDto = new CreateProductDto()
        {
            Description = "Desc Test Product 01 - " + index,
            Name = "Name Test Product 01 - " + index,
            Price = 50,
            Summary = "Sum Test Product 01 - " + index,
            No = Guid.NewGuid().ToString()
            
        };
        
        var productEntity = await _repository.GetProductByNoAsync(productDto.No);
        if (productEntity != null) return BadRequest($"Product No: {productDto.No} is existed.");
        
        var product = _mapper.Map<Product>(productDto);
        await _repository.CreateProductAsync(product);
        var result = _mapper.Map<ProductDto>(product);
        
        return View();
    }
}