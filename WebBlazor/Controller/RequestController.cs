using Application.Service.Command;
using Application.Service.Queries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebBlazor.ModelWebBlazor;

namespace WebBlazor.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public RequestController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        [Route("allRequests")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IList<RequestModel>>> GetAll()
        {
            try
            {
                List<RequestModel> result = new List<RequestModel>();
                var requests = await _mediator.Send(new GetRequestsQuery());


                if (requests == null) return NotFound();
                else
                {
                    foreach (var r in requests)
                    {
                        r.DayIndisposition.ToShortDateString();
                        var request = _mapper.Map<RequestModel>(r);
                        result.Add(request);
                    }
                    return Ok(result);
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Brak odpowiedzi z bazy, błąd 500");
            }
        }
     /*   [HttpGet("{id:int}")]
        [Route("getAllRequestsByEmployee/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IList<RequestModel>>> GetAllRequestsByEmployee(int id)
        {
            try
            {
                List<RequestModel> result = new List<RequestModel>();
                var requests = await _mediator.Send(new GetAllRequestsByEmployeeQuery(id));


                if (requests == null) return NotFound();
                else
                {
                    foreach (var r in requests)
                    {
                        var request = _mapper.Map<RequestModel>(r);
                        result.Add(request);
                    }
                    return Ok(result);
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Brak odpowiedzi z bazy, błąd 500");
            }
        }*/

        [HttpGet("{id:int}")]
        [Route("getRequestById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<RequestModel>> GetRequestById(int id)
        {
            try
            {
                var item = await _mediator.Send(new GetRequestByIdQuery(id));

                if (item == null) return NotFound();
                else
                {
                    var result = _mapper.Map<RequestModel>(item);
                    return Ok(result);
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Brak obiektu o podanym identyfikatorze");
            }
        }
        [HttpPost]
        [Route("addRequest")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RequestModel>> AddRequest(RequestModel request)
        {

            try
            {
                if (request == null)
                    return BadRequest();

                await _mediator.Send(_mapper.Map<CreateRequestCommand>(request));

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
        [Route("editRequest")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<RequestModel>> EditRequest(RequestModel employee)
        {
            try
            {
                if (employee == null)
                    return BadRequest();
                await _mediator.Send(_mapper.Map<EditRequestCommand>(employee));

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
        public async Task<ActionResult<EmployeeModel>> DeleteEmployee(EmployeeModel employee)
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
    }
}
