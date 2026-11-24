using Market.Application.Modules.Festival.FestivalDays.Commands.Create;
using Market.Application.Modules.Festival.FestivalDays.Commands.Update;
using Market.Application.Modules.Festival.FestivalDays.Queries.GetById;
using Market.Application.Modules.Festival.FestivalDays.Queries.List;
using Market.Shared.Constants;

namespace Market.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FestivalDaysController(ISender sender) : ControllerBase
    {
        [Authorize(Policy = PolicyNames.Admin)]
        [HttpPost]
        public async Task<ActionResult<int>> CreateFestivalDay(CreateFestivalDayCommand command, CancellationToken ct)
        {
            int id = await sender.Send(command, ct);
            return CreatedAtAction(nameof(GetById), new { id }, new { id });
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<PageResult<ListFestivalDaysQueryDto>> List([FromQuery] ListFestivalDaysQuery query, CancellationToken ct)
        {
            var result = await sender.Send(query, ct);
            return result;
        }

        [HttpGet("{id:int}")]
        public async Task<GetFestivalDayByIdQueryDto> GetById(int id, CancellationToken ct)
        {
            var festival = await sender.Send(new GetFestivalDayByIdQuery { Id = id }, ct);
            return festival;
        }

        [HttpPut("{id:int}")]
        [Authorize(Policy = PolicyNames.Admin)]
        public async Task Update(int id, UpdateFestivalDayCommand command, CancellationToken ct)
        {
            command.Id = id;
            await sender.Send(command, ct);
        }
    }


}
