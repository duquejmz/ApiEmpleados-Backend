using Microsoft.AspNetCore.Mvc;
using ApiEmpleados_Backend.Application.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ApiEmpleados_Backend.Controllers
{
    public class CreateRoomRequest
    {
        public string? Name { get; set; }
        public int CreatorId { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly ChatService _chatService;
        private readonly ILogger<ChatController> _logger;

        public ChatController(ChatService chatService, ILogger<ChatController> logger)
        {
            _chatService = chatService;
            _logger = logger;
        }

        [HttpPost("room")]
        public async Task<IActionResult> CreateRoom([FromBody] CreateRoomRequest request)
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                return BadRequest("Room name is required.");
            }

            try
            {
                var roomCode = await _chatService.CreateRoomAsync(request.Name, request.CreatorId);
                return Ok(new { roomCode });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while creating a room.");
                return StatusCode(500, "An unexpected internal server error occurred while creating the room.");
            }
        }

        [HttpGet("room/{code}")]
        public async Task<IActionResult> JoinRoom(string code)
        {
            try
            {
                var success = await _chatService.JoinRoomAsync(code);
                if (success)
                {
                    return Ok();
                }
                return NotFound("A room with the specified code was not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while validating a room code.");
                return StatusCode(500, "An unexpected internal server error occurred while validating the room code.");
            }
        }
    }
}