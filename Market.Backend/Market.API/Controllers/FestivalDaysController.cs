using Market.Application.Modules.Festival.FestivalDays.Commands.Create;
using Market.Shared.Constants;

namespace Market.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FestivalDaysController(ISender sender) : ControllerBase
    {
        [Authorize(Policy =PolicyNames.Admin)]
        [HttpPost]
        public async Task<ActionResult<int>> CreateFestivalDay(CreateFestivalDayCommand command, CancellationToken ct)
        {
            int id = await sender.Send(command, ct);
            return id;
        }
    }
}
