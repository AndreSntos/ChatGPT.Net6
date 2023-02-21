using ChatGPT.Services;
using ChatGPT.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ChatGPT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatGptController : ControllerBase
    {
        private readonly IOpenAIService _openAIService;

        public ChatGptController(IOpenAIService openAIService)
        {
            _openAIService = openAIService;
        }

        [HttpPost]
        public async Task<string> Post([FromBody] ChatGPTRequestModel requesModel)
        {
            var response = await _openAIService.AskQuestion(requesModel.Message);

            return response;
        }
    }
}
