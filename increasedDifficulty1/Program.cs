using System;
using System.Threading;
using increasedDifficulty.server;

namespace increasedDifficulty
{
    class Program
    {
		static Server _server;
		static Thread _listenThread; 
		static void Main(string[] args)
		{
			try
			{
				_server = new Server();
				_listenThread = new Thread(_server.Listen);
				_listenThread.Start(); 
			}
			catch (Exception ex)
			{
				_server.DropAllConnections();
				Console.WriteLine(ex.Message);
			}
		}
	}
}
