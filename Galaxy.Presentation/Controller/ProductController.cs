using Galaxy.Application.Features.Products.Commands.Create;
using Galaxy.Application.Features.Products.Commands.Delete;
using Galaxy.Application.Features.Products.Commands.Update;
using Galaxy.Application.Features.Products.Queries.GetAllProducts;
using Galaxy.Application.Features.Products.Queries.GetProductByBarCode;
using Galaxy.Application.Features.Products.Queries.GetProductById;
using Galaxy.Application.Features.Products.Queries.GetProductInDetails;
using Galaxy.Application.Features.Products.Queries.GetProductsNames;
using Galaxy.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SBS.Recruitment.Shared;

namespace Galaxy.Presentation.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.OWNER)]
    public class ProductController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mdiator)
        {
            _mediator = mdiator;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Add([FromForm] AddProductCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedResponse<GetAllProductsQueryDto>>> GetAll([FromQuery] GetAllProductsQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetProductByIdQuery>> GetById(int id)
        {
            return Ok(await _mediator.Send(new GetProductByIdQuery(id)));
        }

        [HttpGet("barcode/{barCode}")]
        public async Task<ActionResult<GetProductByBarCodeQueryDto>> GetProductByBarCode(string barCode)
        {
            return Ok(await _mediator.Send(new GetProductByBarCodeQuery(barCode)));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteProductCommand(id)));
        }

        [HttpGet("getProductsName")]
        public async Task<ActionResult<int>> GetProductionsNames()
        {
            return Ok(await _mediator.Send(new GetProductsNamesQuery()));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<int>> Update([FromForm] UpdateProductCommand command, int id)
        {
            if (id != command.id)
            {
                return BadRequest();
            }

            return Ok(await _mediator.Send(command));
        }
        [HttpGet("productInDetails/{productId}")]
        public async Task<ActionResult<int>> ProductInDetails(int productId)
        {
            return Ok(await _mediator.Send(new GetProductInDetailsQuery(productId)));
        }
    }
}
