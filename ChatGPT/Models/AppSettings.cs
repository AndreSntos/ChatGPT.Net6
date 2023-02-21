namespace ChatGPT.Models
{
    public class AppSettings
    {
        public ChatGPTSettings ChatGPT { get; set; }
    }

    public class ChatGPTSettings
    {
        public string OpenAIUrl { get; set; }
        public string OpenAIToken { get; set; }
    }
}
