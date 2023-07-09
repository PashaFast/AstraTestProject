using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace AstraTestProject.SignalR
{
    public class ChatHub : Hub
    {
		public async Task Send(string message, int selectedRowIndex)
		{
			await this.Clients.All.SendAsync("Receive", message, selectedRowIndex);
		}
	}
}
