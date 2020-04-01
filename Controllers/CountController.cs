using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace sgr.Controllers
{
    [Route("api/count")]
    public class CountController : Controller
    {
        private readonly IHubContext<CountHub> _countHub;
        /// <summary>
        /// 控制器的构造器，初始化一个countHub成员
        /// </summary>
        /// <param name="countHub"></param>
        public CountController(IHubContext<CountHub> countHub)
        {
            _countHub = countHub;
        }
        /// <summary>
        /// 一个post服务端方法，调用客户端someFunc方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post()
        {
            await _countHub.Clients.All.SendAsync("someFunc",new { random = "abcd"});
            return Accepted(1);
        }
    }
}
