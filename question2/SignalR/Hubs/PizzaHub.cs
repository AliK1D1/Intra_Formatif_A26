using Microsoft.AspNetCore.SignalR;
using SignalR.Services;

namespace SignalR.Hubs
{
    public class PizzaHub : Hub
    {
        private readonly PizzaManager _pizzaManager;

        public PizzaHub(PizzaManager pizzaManager) {
            _pizzaManager = pizzaManager;
        }

        public override async Task OnConnectedAsync()
        {   
            _pizzaManager.AddUser();
            await base.OnConnectedAsync();
          await Clients.All.SendAsync("Connected", _pizzaManager.NbConnectedUsers);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            _pizzaManager.RemoveUser();
            await base.OnConnectedAsync();
            await Clients.All.SendAsync("Connected", _pizzaManager.NbConnectedUsers);
        }

        public async Task SelectChoice(PizzaChoice choice)
        {
            var groupName = _pizzaManager.GetGroupName(choice);
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);  
           await Clients.Caller.SendAsync("SelectChoice", _pizzaManager.GetNbPizzas(choice), _pizzaManager.GetPizzaPrice(choice), _pizzaManager.GetMoney(choice));
        }

        public async Task UnselectChoice(PizzaChoice choice)
        {
            var groupName = _pizzaManager.GetGroupName(choice);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
           await Clients.Caller.SendAsync("UnselectChoice", choice);
        }

        public async Task AddMoney(PizzaChoice choice)
        {
          _pizzaManager.IncreaseMoney(choice);
          await Clients.Group(_pizzaManager.GetGroupName(choice)).SendAsync("AddMoney", _pizzaManager.GetMoney(choice));

        }

        public async Task BuyPizza(PizzaChoice choice)
        {
            _pizzaManager.BuyPizza(choice);
            await Clients.Group(_pizzaManager.GetGroupName(choice)).SendAsync("BuyPizza", _pizzaManager.GetNbPizzas(choice), _pizzaManager.GetMoney(choice));

        }
    }
}
