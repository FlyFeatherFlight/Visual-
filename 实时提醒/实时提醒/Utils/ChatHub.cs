using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace 实时提醒.Utils
{
    public class ChatHub : Hub
    {
        public void SendMessage(string name, string message)
        {
            // Clients.All.hello();
            Clients.All.receiveMessage(name, message);
            //用户调用客户端的函数
        }

    }
}