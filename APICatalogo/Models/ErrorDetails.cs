using System.Text.Json;

namespace APICatalogo.Models
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string? message { get; set; }
        public string? trace { get; set; }
        public override string ToString() {
            return JsonSerializer.Serialize(this);
        }
    }
}
