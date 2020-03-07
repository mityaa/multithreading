using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using increaseDifficulty.client;

namespace increaseDifficulty.server
{
	public class Server
	{
		protected static TcpListener TcpListener;
		protected List<Client> Clients = new List<Client>();

		protected internal void AddConnection(Client client)
		{
			Clients.Add(client);
		}

		protected internal void RemoveConnection(string id)
		{
			var client = Clients.FirstOrDefault(c => c.Id == id);
			if (client != null)
				Clients.Remove(client);
		}

		protected internal void Listen()
		{
			try
			{
				TcpListener = new TcpListener(IPAddress.Any, 8484);
				TcpListener.Start();
				Console.WriteLine("Server started and waiting");
				while (true)
				{
					var tcpClient = TcpListener.AcceptTcpClient();

					var client = new Client(tcpClient, this);
					var clientThread = new Thread(new ThreadStart(client.Process));
					clientThread.Start();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				DropAllConnections();
			}
		}

		protected internal void SendMessage(string message, string id)
		{
			var data = Encoding.Unicode.GetBytes(message);
			foreach (var client in Clients.Where(client => client.Id != id))
			{
				client.Stream.Write(data, 0, data.Length);
			}
		}

		protected internal void DropAllConnections()
		{
			TcpListener.Stop();

			foreach (var client in Clients)
			{
				client.CloseConnection();
			}

			Environment.Exit(0);
		}
	}
}
