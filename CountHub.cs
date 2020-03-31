using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace sgr
{
    public class CountHub:Hub
    {
        private readonly CountService _countService;
        public CountHub(CountService countService)
        {
            _countService = countService;
        }
        public async Task GetLatestCount(string random)
        {
            int count;
            do
            {
                count = _countService.GetLatestCount();
                Thread.Sleep(1000);
                await Clients.All.SendAsync("ReceiveUpdate", count);
            }
            while (count < 10);
            await Clients.All.SendAsync("Finished");
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
