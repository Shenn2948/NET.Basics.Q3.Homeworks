using System.Threading.Tasks;

using BrainstormSessions.Core.Interfaces;
using BrainstormSessions.ViewModels;

using Microsoft.AspNetCore.Mvc;

using Serilog;

namespace BrainstormSessions.Controllers
{
    public class SessionController : Controller
    {
        private readonly IBrainstormSessionRepository _sessionRepository;
        private readonly ILogger _logger = Log.Logger;

        public SessionController(IBrainstormSessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public async Task<IActionResult> Index(int? id)
        {
            _logger.Debug("Index() called with id:{0}", id);
            if (!id.HasValue)
            {
                _logger.Warning("Redirect to action: {0}, controller:{1}", nameof(Index), "Home");
                return RedirectToAction(actionName: nameof(Index), controllerName: "Home");
            }

            var session = await _sessionRepository.GetByIdAsync(id.Value);
            if (session == null)
            {
                _logger.Warning("Session not found.");
                return Content("Session not found.");
            }

            _logger.Debug("Found session with id:{0}", id);
            var viewModel = new StormSessionViewModel()
            {
                DateCreated = session.DateCreated,
                Name = session.Name,
                Id = session.Id
            };
            _logger.Debug("Created viewModel with id:{0}, name:{1}", session.Id, session.Name);

            return View(viewModel);
        }
    }
}
