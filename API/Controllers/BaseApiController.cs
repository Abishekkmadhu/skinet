using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController] // this is api controller attribute used to define all the controllers in this is an api
    [Route("api/[controller]")]

    public class BaseApiController : ControllerBase // return https endpoints so use this without view
    {
    }
}
