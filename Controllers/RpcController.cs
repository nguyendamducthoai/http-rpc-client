using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using http_rpc_client.Models;
using Microsoft.AspNetCore.Mvc;
using rpc_base;

namespace http_rpc_client.Controllers
{
    [Route("api")]
    [ApiController]
    public class RpcController: ControllerBase
    {
        readonly RpcClient _rpcClient;

        public RpcController()
        {
            var rpcClientConfig = new Dictionary<string, string>();
            rpcClientConfig.Add("ProducerEnpoint", "rpc_queue");
            rpcClientConfig.Add("HostName", "localhost");

            _rpcClient = new RpcClient(rpcClientConfig);
        }
        
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] RpcRequest request)
        {
            var callTask = _rpcClient.CallAsync(request._data, request._queue);
            int timeout = 3000;
            
            if (await Task.WhenAny(callTask, Task.Delay(timeout)) == callTask) {
                return Ok(callTask.Result);
            } else { 
                throw new Exception("timeout");
            }
        }
    }
}