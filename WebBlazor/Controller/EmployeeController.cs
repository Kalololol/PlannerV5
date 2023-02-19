using Application.Service.Command;
using Application.Service.Queries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebBlazor.Exceptions;
using WebBlazor.ModelWebBlazor;

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
        [Authorize]
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

        [HttpGet("{id:int}")]
        [Route("getEmployeeById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<EmployeeModel>> GetEmployeeById(int id)
        {
            try
            {
                var item = await _mediator.Send(new GetEmployeeByIdQuery(id));

                if (item == null) return NotFound();
                else
                {
                    var result = _mapper.Map<EmployeeModel>(item);
                    return Ok(result);
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Brak użtkownika o podanym identyfikatorze");
            }
        }
        [HttpPost]
        [Authorize(Roles = "Manager")]
        [Route("addEmployee")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EmployeeModel>> AddEmployee(EmployeeModel employee)
        {

            try
            {
                if (employee == null)
                    return BadRequest();

                await _mediator.Send(_mapper.Map<CreateEmployeeCommand>(employee));

                //   return CreatedAtAction(nameof(GetEmployeeById),  employee);
                return Ok("Dodano");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new employee record");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        [Route("editEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<EmployeeModel>> EditEmployee(EmployeeModel employee)
        {
            try
            {
                if (employee == null)
                    return BadRequest();
                await _mediator.Send(_mapper.Map<EditEmployeeCommand>(employee));

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new employee record");
            }
        }

        [HttpPost]
        [Route("deleteEmployee")]
        [Authorize(Roles = "Manager")]

        public async Task<ActionResult<EmployeeModel>> DeleteEmployee(EmployeeModel employee)
        {
            try
            {
                if (employee == null)
                    return BadRequest();
                await _mediator.Send(_mapper.Map<DeleteEmployeeCommand>(employee));

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new employee record");
            }
        }     
    }
}
