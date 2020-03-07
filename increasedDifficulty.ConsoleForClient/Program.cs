using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace increasedDifficulty.ConsoleForClient
{
	class Program
	{
		private const string Host = "127.0.0.1";
		private const int Port = 8484;

		private static string _userName;

		public static TcpClient Client;
		public static NetworkStream Stream;



		static void Main()
		{
			Console.Write("Set your name ");
			_userName = Console.ReadLine();

			Client = new TcpClient();

			var message = _userName;
			var receiveThread = new Thread(ReceiveMessage);

			try
			{
				Client.Connect(Host, Port);
				Stream = Client.GetStream();

				var data = Encoding.Unicode.GetBytes(message);
				Stream.Write(data, 0, data.Length);

				receiveThread.Start();
				Console.WriteLine("Hi, {0} ", _userName);
				SendMessage();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			finally
			{
				CloseConnection();
			}
		}

		static void SendMessage()
		{
			Console.WriteLine("Send message: ");

			while (true)
			{
				var message = Console.ReadLine();
				var data = Encoding.Unicode.GetBytes(message);
				Stream.Write(data, 0, data.Length);
			}
			// ReSharper disable once FunctionNeverReturns
		}

		static void ReceiveMessage()
		{
			while (true)
			{
				try
				{
					var data = new byte[64];
					var builder = new StringBuilder();
					do
					{
						var bytes = Stream.Read(data, 0, data.Length);
						builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
					}
					while (Stream.DataAvailable);

					var message = builder.ToString();
					Console.WriteLine(message);
				}
				catch
				{
					Console.WriteLine("Connection lost");
					Console.ReadLine();
					CloseConnection();
				}
			}
		}

		static void CloseConnection()
		{
			Stream?.Close();
			Client?.Close();
			Environment.Exit(0);
		}
	}
}
