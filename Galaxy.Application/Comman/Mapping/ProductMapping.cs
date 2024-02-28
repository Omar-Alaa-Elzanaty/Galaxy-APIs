using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galaxy.Application.Features.Products.Commands.Create;
using Galaxy.Application.Features.Products.Commands.Update;
using Galaxy.Application.Features.Products.Queries.GetAllProducts;
using Galaxy.Application.Features.Products.Queries.GetProductById;
using Galaxy.Domain.Models;
using Mapster;

namespace Galaxy.Application.Comman.Mapping
{
    public class ProductMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AddProductCommand, Product>();
            config.NewConfig<Product, GetProductByIdQueryDto>();
            config.NewConfig<Product,GetAllProductsQueryDto>();
            config.NewConfig<UpdateProductCommand,Product>();
        }
    }
}
