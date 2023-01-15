using Application.Service.Queries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebBlazor.ModelWebBlazor;
using WebBlazor.Pages.EmployeePage;

namespace WebBlazor.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public EmployeeController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        [Route("allEmployees")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IList<EmployeeModel>>> GetAll()
        {

            try
            {
                List<EmployeeModel> result = new List<EmployeeModel>();
                var employees = await _mediator.Send(new GetEmployeesQuery());

                if (employees == null) return NotFound();
                else
                {
                    foreach (var e in employees)
                    {
                        var employee = _mapper.Map<EmployeeModel>(e);
                        result.Add(employee);
                    }
                    return Ok(result);
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Brak odpowiedzi z bazy, błąd 500");
            }
        }
    }
}
