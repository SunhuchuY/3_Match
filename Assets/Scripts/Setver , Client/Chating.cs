using System;
using System.Net.Sockets;
using System.Text;

using UnityEngine;

public class Chating : MonoBehaviour
{
    private static TcpClient tcpClient;
    private static NetworkStream clientStream;

    [SerializeField]
    private string message;

    private void Start()
    {
        tcpClient = new TcpClient("127.0.0.1", 8888); // 서버 IP 및 포트에 맞게 수정

        clientStream = tcpClient.GetStream();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            SendMessage();
        }
    }

    public void SendMessage()
    {
        byte[] data = Encoding.UTF8.GetBytes(message);
        clientStream.Write(data, 0, data.Length);
        Console.WriteLine("Sent: " + message);
    }
}
