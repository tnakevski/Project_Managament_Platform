using Microsoft.AspNet.SignalR;
using ProjectManagementPlatform.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ProjectManagementPlatform.Hubs
{
    public class BaseHub : Hub
    {
        private readonly static UserConnectionMapping<int> _connections = UserConnectionMapping<int>.GetInstance();
        public BaseHub()
        {

        }


        public void TestSend(string message)
        {
            Clients.All.showTestMessage(message + " server tralalala");
        }

        #region Connection Events
        public override Task OnConnected()
        {
            string connectionId = Context.ConnectionId;
            int userId = 1;
            _connections.Add(userId, connectionId);
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            string connectionId = Context.ConnectionId;
            int userId = 1;
            _connections.Remove(userId, connectionId);

            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            string connectionId = Context.ConnectionId;
            int userId = 1;

            if (!_connections.GetConnections(userId).Contains(connectionId))
            {
                _connections.Add(userId, connectionId);
            }

            return base.OnReconnected();
        }
        #endregion

    }
}