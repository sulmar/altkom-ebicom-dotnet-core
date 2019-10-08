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
            return base.OnConnectedAsync();
        }

        public async Task CustomerAdded(Customer customer)
        {
            await this.Clients.All.SendAsync("Added", customer);
        }
    }
}
