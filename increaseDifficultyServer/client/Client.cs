using System;
using System.Net.Sockets;
using System.Text;
using increaseDifficulty.server;

namespace increaseDifficulty.client
{
	public class Client
	{
		public string UserName { get; set; }
		protected internal string Id;
		protected internal NetworkStream Stream;
		private readonly TcpClient _tcpClient;
		private readonly Server _server;

		public Client(TcpClient client, Server server)
		{
			Id = Guid.NewGuid().ToString();
			_tcpClient = client;
			_server = server;
			server.AddConnection(this);
		}

		protected internal void CloseConnection()
		{
			Stream?.Close();
			_tcpClient?.Close();
		}

		public void Process()
		{
			try
			{
				Stream = _tcpClient.GetStream();
				var message = GetMessage();
				UserName = message;

				message = UserName + "now in chat";
				_server.SendMessage(message, Id);
				Console.WriteLine(message);

				while (true)
				{
					try
					{
						message = GetMessage();
						message = $"{UserName}: {message}";
						Console.WriteLine(message);
						_server.SendMessage(message, Id);
					}
					catch
					{
						message = $"{UserName} left the chat";
						Console.WriteLine(message);
						_server.SendMessage(message, Id);
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			finally
			{
				_server.RemoveConnection(Id);
				CloseConnection();
			}
		}

		private string GetMessage()
		{
			var data = new byte[64];
			var stringBuilder = new StringBuilder();

			do
			{
				var bytes = Stream.Read(data, 0, data.Length);
				stringBuilder.Append(Encoding.Unicode.GetString(data, 0, bytes));
			} while (Stream.DataAvailable);

			return stringBuilder.ToString();
		}
	}
}
