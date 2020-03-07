using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace increaseDifficulty.client
{
	public class Client
	{
		public string UserName { get; set; }
		public string Id { get; private protected set; }
		protected internal NetworkStream Stream { get; private protected set; }
		public TcpClient TcpClient { get; set; }

		public Client(TcpClient client)
		{
			Id = Guid.NewGuid().ToString();
			TcpClient = client;
		}

		protected internal void CloseConnection()
		{
			Stream?.Close();
			TcpClient?.Close();
		}


	}
}
