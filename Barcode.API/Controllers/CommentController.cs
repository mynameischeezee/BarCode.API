using Barcode.Services.Abstracitons;
using DataAccess.Daos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BarCodeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentController
    {
        private readonly ILogger<CommentController> _logger;
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService, ILogger<CommentController> logger)
        {
            _commentService = commentService;
            _logger = logger;
        }
        [HttpGet("~/GetComment")]
        public string GetComment(int id)
        {
            var comment = _commentService.Get(id);
            return JsonConvert.SerializeObject(comment);
        }

        [HttpPost("~/AddComment")]
        public ActionResult AddComment(string data, int rating, int scanId)
        {
            _commentService.Create(data, rating, scanId);
            return new OkResult();
        }
        
        [HttpPost("~/EditComment")]
        public ActionResult EditComment(int id,string data, int rating, int scanId)
        {
            _commentService.Edit(id, data, rating, scanId);
            return new OkResult();
        }
        
        [HttpPost("~/RemoveComment")]
        public ActionResult RemoveComment(int id)
        {
            _commentService.Remove(id);
            return new OkResult();
        }
    }
}
