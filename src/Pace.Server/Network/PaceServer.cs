using Pace.Common.Network;
using Pace.Common.Network.Packets;
using Pace.Server.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Pace.Server.Network;

public class PaceServer
{
    public event EventHandler<PacketEventArgs> PacketReceived;
    public event EventHandler<ClientEventArgs> ClientConnected;
    public event EventHandler<ClientEventArgs> ClientDisconnected;

    public readonly X509Certificate Certificate;

    public readonly PacketChannel PacketChannel;
    public List<PaceClient> ConnectedClients { get; set; }
    public bool Listening { get; set; }

    private TcpListener listener;

    public PaceServer()
    {
        PacketChannel = new PacketChannel();
        ConnectedClients = new List<PaceClient>();
    }

    public PaceServer(X509Certificate certificate) : this()
    {
        Certificate = certificate;
    }

    public void Start()
    {
        listener?.Stop();

        listener = new TcpListener(IPAddress.Any, ServerConfiguration.Port);
        listener.Start();
        Listening = true;

        Task.Factory.StartNew(HandleClientConnection);
    }

    public void Shutdown()
    {
        Listening = false;
    }

    protected void OnClientConnected(PaceClient client)
    {
        ConnectedClients.Add(client);
        ClientConnected?.Invoke(this, new ClientEventArgs(client));
    }

    protected void OnClientDisconnected(PaceClient client)
    {
        ConnectedClients.Remove(client);
        ClientDisconnected?.Invoke(this, new ClientEventArgs(client));
    }

    protected void OnPacketReceived(PaceClient client, IPacket packet)
    {
        PacketReceived?.Invoke(this, new PacketEventArgs(client, packet));
        PacketChannel.HandlePacket(packet);
    }

    private void HandleClientConnection()
    {
        while (Listening)
        {
            var client = new PaceClient(listener.AcceptTcpClient());
            OnClientConnected(client);

            Task.Factory.StartNew(() =>
            {
                bool isConnected = true;

                while (isConnected)
                {
                    try
                    {
                        var packet = client.ReadPacket();
                        OnPacketReceived(client, packet);
                    }
                    catch (IOException ex)
                    {
                        if (ex.InnerException == null)
                        {
                            throw ex;
                        }

                        if (ex.InnerException.GetType() == typeof(SocketException))
                        {
                            var socketException = (ex.InnerException as SocketException);

                            if (socketException.ErrorCode == (int)SocketError.ConnectionReset)
                            {
                                OnClientDisconnected(client);
                                isConnected = false;
                            }
                        }
                    }
                }
            });
        }

        listener.Stop();
    }
}