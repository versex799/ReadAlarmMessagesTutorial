using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ReadAlarmMessagesTutorial
{
    internal class Program
    {
        static short _ret = 0;
        static ushort _handle = 0;
        static Fanuc _fanuc;

        static void Main(string[] args)
        {
            _fanuc = new Fanuc();
            _handle = _fanuc.Connect("192.168.2.123");

            if (_ret != Focas1.EW_OK)
            {
                Console.WriteLine($"Unable to connect to 192.168.2.123 on port 8193\n\nReturn Code: {_ret}\n\nExiting....");
                Console.Read();
            }
            else
            {
                while (true)
                {
                    Console.WriteLine("ALARMS -\n\n");

                    foreach (var alarm in _fanuc.Alarms.GetActiveAlarms(_handle))
                        Console.WriteLine($"{alarm}\n");

                    Thread.Sleep(1000);

                    Console.Clear();
                }

            }
        }
    }
}