using Galaxy.Application.Features.Products.Commands.Create;
using Galaxy.Application.Features.Products.Commands.Update;
using Galaxy.Application.Features.Products.Queries.GetAllProducts;
using Galaxy.Application.Features.Products.Queries.GetAllProductsCards;
using Galaxy.Application.Features.Products.Queries.GetProductByBarCode;
using Galaxy.Application.Features.Products.Queries.GetProductById;
using Galaxy.Application.Features.Products.Queries.GetProductInDetails;
using Galaxy.Application.Features.Products.Queries.GetProductsNames;
using Galaxy.Domain.Constants;
using Galaxy.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Galaxy.Presentation.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mdiator)
        {
            _mediator = mdiator;
        }

        [Authorize(Roles = $"{Roles.OWNER},{Roles.MANAGER}")]
        [HttpPost]
        public async Task<ActionResult<int>> Add([FromForm] AddProductCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [Authorize]
        [HttpGet("getAllInCards")]
        public async Task<ActionResult<PaginatedResponse<GetAllProductsCardsQueryDto>>> GetAllInCards([FromQuery] GetAllProductsCardsQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<PaginatedResponse<int>>> GetAll([FromQuery] GetAllProductsQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<GetProductByIdQuery>> GetById(int id)
        {
            return Ok(await _mediator.Send(new GetProductByIdQuery(id)));
        }

        [Authorize]
        [HttpGet("barcode/{barCode}")]
        public async Task<ActionResult<GetProductByBarCodeQueryDto>> GetProductByBarCode(string barCode)
        {
            return Ok(await _mediator.Send(new GetProductByBarCodeQuery(barCode)));
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> Delete(int id)
        {
            return Ok(/*await _mediator.Send(new DeleteProductCommand(id))*/);
        }

        [Authorize]
        [HttpGet("getProductsName")]
        public async Task<ActionResult<int>> GetProductionsNames()
        {
            return Ok(await _mediator.Send(new GetProductsNamesQuery()));
        }

        [Authorize(Roles = $"{Roles.OWNER},{Roles.MANAGER}")]
        [HttpPut("{id}")]
        public async Task<ActionResult<int>> Update([FromForm] UpdateProductCommand command, int id)
        {
            if (id != command.id)
            {
                return BadRequest();
            }

            return Ok(await _mediator.Send(command));
        }

        [Authorize]
        [HttpGet("productInDetails/{productId}")]
        public async Task<ActionResult<int>> ProductInDetails(int productId)
        {
            return Ok(await _mediator.Send(new GetProductInDetailsQuery(productId)));
        }
    }
}
