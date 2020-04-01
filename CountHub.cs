using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace sgr
{
    /// <summary>
    /// 通信hub
    /// </summary>
    public class CountHub:Hub
    {
        private readonly CountService _countService;
        public CountHub(CountService countService)
        {
            _countService = countService;   //初始化一个服务，服务实现变量自增
        }
        public async Task GetLatestCount(string random)
        {
            int count;
            do
            {
                count = _countService.GetLatestCount();     //循环count++
                Thread.Sleep(1000);     //sleep 1秒
                await Clients.All.SendAsync("ReceiveUpdate", count);    //将count作为参数，调用客户端js中的ReceiveUpdate方法
            }
            while (count < 10); //循环10次
            await Clients.All.SendAsync("Finished");    //调用客户端js的Finished方法
        }
        public override async Task OnConnectedAsync()
        {
            //var connectedId = Context.ConnectionId;
            //var client = Clients.Client(connectedId);
            //await client.SendAsync("SomeFunc",new { });
            //await Clients.AllExcept(connectedId).SendAsync("SomeFunc", new { });
            //await Groups.AddToGroupAsync(connectedId, "MyGroup");
            //await Groups.RemoveFromGroupAsync(connectedId, "MyGroup");
            //await Clients.Group("MyGroup").SendAsync("SomeFunc");
        }
    }
}
