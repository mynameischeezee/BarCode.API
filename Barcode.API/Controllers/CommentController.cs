using System;
using Barcode.Services.Abstracitons;
using DataAccess.Daos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
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
        private readonly IRatingService _ratingService;

        public CommentController(ICommentService commentService, ILogger<CommentController> logger, IRatingService ratingService)
        {
            _commentService = commentService;
            _logger = logger;
            _ratingService = ratingService;
        }
        [HttpGet("~/GetComment")]
        public ActionResult GetComment(int id)
        {
            try
            {
                var comment = _commentService.Get(id);
                return new OkObjectResult(JsonConvert.SerializeObject(comment));
            }
            catch (Exception e)
            {
                var problemDetails = new ProblemDetails()
                    {Title = "Invalid arguments", Detail = "Id is wrong", Status = 400};
                return new BadRequestObjectResult(problemDetails);
            }
        }

        [HttpPost("~/AddComment")]
        public ActionResult AddComment(string data, int rating, int scanId)
        {
            try
            {
                _commentService.Create(data, rating, scanId);
            }
            catch (ArgumentException e)
            {
                var problemDetails = new ProblemDetails()
                    {Title = "Invalid arguments", Detail = "One of arguments is wrong", Status = 400};
                return new BadRequestObjectResult(problemDetails);
            }
            
            return new OkResult();
        }
        
        [HttpPost("~/EditComment")]
        public ActionResult EditComment(int id,string data, int rating, int scanId)
        {
            try
            {
                _commentService.Edit(id, data, rating, scanId);
                return new OkResult();
            }
            catch (Exception e)
            {
                var problemDetails = new ProblemDetails()
                    {Title = "Invalid arguments", Detail = "One of arguments is wrong", Status = 400};
                return new BadRequestObjectResult(problemDetails);
            }
        }
        
        [HttpPost("~/RemoveComment")]
        public ActionResult RemoveComment(int id)
        {
            try
            {
                _commentService.Remove(id);
                return new OkResult();
            }
            catch (Exception e)
            {
                var problemDetails = new ProblemDetails()
                    {Title = "Invalid arguments", Detail = "Id is wrong", Status = 400};
                return new BadRequestObjectResult(problemDetails);
            }
        }

        [HttpGet("/GetRating")]
        public ActionResult GetRating(string code)
        {
            var rating = _ratingService.GetAverageRating(code);
            return new OkObjectResult(rating);
        }

    }
}
