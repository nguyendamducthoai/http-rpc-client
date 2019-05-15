using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace http_rpc_client.Models
{
    public class RpcRequest
    {
        [Required]
        [JsonProperty("queue")]
        public string _queue { get; set; }

        [Required]
        [JsonProperty("data")]
        public string _data { get; set; }
        
    }
}