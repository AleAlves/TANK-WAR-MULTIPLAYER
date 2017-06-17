using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Tank_War_Multiplayer
{
    class Servidor
    {
        private static Socket _serverSocket;
        private static List<Socket> _clientSockets = new List<Socket>();
        private const int _BUFFER_SIZE = 2048;
        private static readonly byte[] _buffer = new byte[_BUFFER_SIZE];
        private const string serverName = "Multiplayer Server";



        public void servidor(int port)
        {
            try {
                if(File.Exists("serverLog.txt"))
                    File.Delete("serverLog.txt");
                File.AppendAllText("serverLog.txt", "Server Multiplayer log ("+DateTime.Now.ToString()+")"+ Environment.NewLine +"* Renovado a cada criação de novo jogo." +Environment.NewLine);
            }
            catch(Exception e) { Console.WriteLine("Log error: "+ e); }
            SetupServer(port);
        }


        private static void sendData(string id, string text)
        {
            try
            {
                if (_clientSockets.Count > 0)
                    foreach (var i in _clientSockets)
                    {
                        byte[] byData = Encoding.UTF8.GetBytes(text);
                        i.Send(byData, SocketFlags.None);
                    }
            }
            catch (Exception e) { Console.WriteLine("sendData error: "+ e); }
        }

        static byte[] byData;
        static byte[] byText;

        private static void sendData(string text, Socket client)
        {
            try
            {
                byData = Encoding.UTF8.GetBytes("00351951000");
                byText = Encoding.UTF8.GetBytes(text);
                switch (byText[0])
                {
                    case 49:
                        if (_clientSockets.Count == 2)
                        {
                            _clientSockets[1].Send(byText, SocketFlags.None);
                        }
                        else
                            _clientSockets[0].Send(byData, SocketFlags.None);
                        break;
                    case 50:
                        if (_clientSockets.Count == 2)
                        {
                            _clientSockets[0].Send(byText, SocketFlags.None);
                        }
                        break;
                    case 48:
                        try
                        {
                            if (_clientSockets.Count == 1)
                                _clientSockets[0].Send(byData, SocketFlags.None);
                            else
                                if (_clientSockets.Count == 1)
                            {
                                _clientSockets[0].Send(byData, SocketFlags.None);
                                _clientSockets[1].Send(byData, SocketFlags.None);
                            }
                        }
                        catch { }
                        break;
                }
            }
            catch (Exception e){ Console.WriteLine("Error sending: "+ e); }
        }

        public static bool SetupServer(int port)
        {
            try {
                _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _serverSocket.Bind(new IPEndPoint(IPAddress.Any, port));
                _serverSocket.Listen(5);
                _serverSocket.BeginAccept(AcceptCallback, null);
                return true;
            }
            catch(Exception e) { Console.WriteLine("Setup server error: "+e); return false; }
        }


        private static void CloseAllSockets()
        {
            foreach (Socket socket in _clientSockets)
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }

            _serverSocket.Close();
        }

        private static void AcceptCallback(IAsyncResult AR)
        {
            Socket socket;

            try
            {
                socket = _serverSocket.EndAccept(AR);
            }
            catch (ObjectDisposedException) // I cannot seem to avoid this (on exit when properly closing sockets)
            {
                return;
            }
      
            _clientSockets.Add(socket);
            socket.BeginReceive(_buffer, 0, _BUFFER_SIZE, SocketFlags.None, ReceiveCallback, socket);
            _serverSocket.BeginAccept(AcceptCallback, null);
            sendData("00000000000", socket);
        }

        private static void ReceiveCallback(IAsyncResult AR)
        {
            Socket current = (Socket)AR.AsyncState;
            int received;

            try
            {
                received = current.EndReceive(AR);
            }
            catch (SocketException)
            {
                current.Close(); // Dont shutdown because the socket may be disposed and its disconnected anyway
                _clientSockets.Remove(current);
                return;
            }

            byte[] recBuf = new byte[received];
            Array.Copy(_buffer, recBuf, received);
            string text = Encoding.ASCII.GetString(recBuf);
            char[] textChar = new char[100];
            textChar = text.ToCharArray();
            sendData(text, current);
            current.BeginReceive(_buffer, 0, _BUFFER_SIZE, SocketFlags.None, ReceiveCallback, current);
            try {
                File.AppendAllText("serverLog.txt", "Player :" + text[0] + " x:" + text[1]+""+text[2]+""+text[3]+ " y:" + text[4] + "" + text[5] + "" + text[6]+" Posição:"+text[7]+" Tiro:"+text[8]+" Moviemento:"+text[9]+" Situcação:"+text[10] + Environment.NewLine);
            }
            catch { }
        }
    }
}
