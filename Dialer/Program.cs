using System;
using System.Diagnostics;

namespace Dialer
{
    class Program
    {
        static void Main(string[] args)
        {
            var dail = new Program();
            var connection = "TeemoNicolas";
            var userName = "2015212605";
            var password = "mq2020.";

            Console.WriteLine("***牛逼的自动拨号系统***\n" +
                "0: 退出\n" +
                "1: 断开\n" +
                "2: 运行Hosts批处理（该功能暂时不可用）\n" +
                "其他: 拨号连接");

            string choise = Console.ReadLine();
            switch (choise)
            {
                case "0":
                    System.Environment.Exit(0);
                    break;
                case "1":
                    dail.DisConnect(connection);
                    break;
                case "2":
                    dail.RunBat();  //替换hosts
                    break;
                default:
                    string res = dail.Connect(connection, userName, password);
                    if (res.IndexOf("已连接 " + userName + "。") <= -1)
                    {
                        Console.WriteLine("拨号连接成功...");
                    }
                    //Console.ReadKey(); 等待输入任意字符
                    System.Environment.Exit(0);
                    break;
            }
        }

        public string Connect(string conn, string user, string passwd)
        {
            return Connection("rasdial " + conn + " " + user + " " + passwd + "&exit");
        }

        public string DisConnect(string conn)
        {
            return Connection("rasdial " + conn + " /disconnect&exit");
        }

        public void RunBat()
        {
            Process process = new Process();
            process.StartInfo.FileName = "D:\\Users\\Teemo Nicolas\\Documents\\Progects\\windows_bat\\Windows自动替换脚本.bat";
            process.StartInfo.UseShellExecute = true;
            // process.StartInfo.Arguments = "hello world";
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            process.WaitForExit();
        }

        private string Connection(string dial)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.StandardInput.WriteLine(dial);
            p.StandardInput.AutoFlush = true;
            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            p.Close();
            Console.WriteLine(output);
            return output;
        }
    }
}
