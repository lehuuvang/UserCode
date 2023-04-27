using Newtonsoft.Json;

namespace UserCode.Models
{
    public class RecaptchaResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }
        [JsonProperty("error-codes")]
        public List<string> ErrorMessages { get; set; }
    }
}
