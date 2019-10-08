using Altkom.DotnetCore.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Altkom.DotnetCore.SignalR.Hubs
{
    public class CustomersHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            this.Groups.AddToGroupAsync(this.Context.ConnectionId, "Ebicom");

            return base.OnConnectedAsync();
        }

        public async Task CustomerAdded(Customer customer)
        {
            await this.Clients.Others.SendAsync("Added", customer);

            await this.Clients.Groups("Ebicom").SendAsync("Added", customer);
        }

        public async Task Ping(string message = "Pong")
        {
            await this.Clients.Caller.SendAsync(message);
        }
    }
}
