//Do Not Delete This Comment... 
//Made By Serenity on 08/20/16
//Any Code Copied Must Source This Project (its the law (:P)) Please.. i work hard on it since 2016.
//Thank You for looking love you guys...

using System;
using System.ComponentModel;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace XDCKIT
{
    /// <summary>
    /// Made By Serenity If Copied You Must Give Credit... Do Not Delete This Comment..
    /// </summary>
    public class XNotify
    {
        public static TcpClient Notify;
        /// <summary>
        /// Sends A Costume Message Via Xbox Notification System.
        /// </summary>
        /// <param name="Message"></param>
        public static void Show(string Message)
        {
         Show(Message, XNotiyLogo.FLASHING_XBOX_LOGO);
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        /// <summary>
        /// Sends Commands Based On User's Input
        /// </summary>
        /// <param name="Command"></param>
        /// <returns></returns>
        private static string SendTextCommand(string Command)
        {
            byte[] Packet = new byte[1026];
            if (Notify != null)
            {

                try
                {
                    Notify.Client.Send(Encoding.ASCII.GetBytes(Command + "\r\n"));
                    Notify.Client.Receive(Packet);
                    Console.WriteLine(Encoding.ASCII.GetString(Packet).Replace("\0", string.Empty).Replace("\r\n", string.Empty).Replace("\"", string.Empty).Replace("202- multiline response follows\n", string.Empty).Replace("201- connected\n", string.Empty.Replace("200-", string.Empty)));
                    return Encoding.ASCII.GetString(Packet).Replace("\0", string.Empty).Replace("\r\n", string.Empty).Replace("\"", string.Empty).Replace("202- multiline response follows\n", string.Empty).Replace("201- connected\n", string.Empty.Replace("200-", string.Empty));//


                }
                catch
                {
                    throw;
                }
            }
            else throw new Exception("XNotify Failure Detected");
        }
        /// <summary>
        /// Sends A Costume Message Via Xbox Notification System With Costume Logo.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="Logo"></param>
        public static void Show(string message, XNotiyLogo Logo)
        {
            NewConnection();
            string commandBase = "consolefeatures ver=2" + " type=12 params=\"A\\0\\A\\2\\" + 2 + "/" + message.Length + "\\" + XboxExtention.ConvertStringToHex(message, Encoding.ASCII) + "\\" + 1 + "\\";
            string command = commandBase + (int)Logo + "\\\"";
    
            SendTextCommand(command);
            EndConnection();
        }
        private static void EndConnection()
        {
            SendTextCommand("bye");
            Notify.Client.Dispose();
            Notify.Close();
        }
        private static void NewConnection()
        {
            Notify = new TcpClient(XboxClient.IPAddress, 730);
            Notify.SendTimeout = 100;
        }
    }
}
