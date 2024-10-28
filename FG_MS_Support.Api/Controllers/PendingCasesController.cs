using FGM_MS_Support.BusinessLogic.Entities;
using FGM_MS_Support.BusinessLogic.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FG_MS_Support.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PendingCasesController : ControllerBase
    {
        private readonly IPendingCasesRepository _pendingCasesRepository;

        public PendingCasesController(IPendingCasesRepository pendingCasesRepository)
        {
            _pendingCasesRepository = pendingCasesRepository;
        }

        [HttpGet("FetchPendingCases")]
        public ResponseObject FetchPendingCases()
        {
            ResponseObject response = _pendingCasesRepository.GetNBSyncPendingCases();

            return response;
        }
    }
}
