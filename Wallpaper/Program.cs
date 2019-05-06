using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Timers;

namespace ChangeWallpaper
{
    class Program
    {
        [DllImport("user32.dll")]
        public static extern bool SystemParametersInfo(UInt32 uiAction, UInt32 uiParam, string pvParam, UInt32 fWinIni);
        static FileInfo[] images;
        static int currentImage;

        static void Main(string[] args)
        {

            Console.Write("Enter the time (second) to change the picture: ");
            int timeChanger = Convert.ToInt32(Console.ReadLine());

            timeChanger *= 1000;

            DirectoryInfo dirInfo = new DirectoryInfo(@"C:\Users\rocho\Desktop\Walpaper");
            images = dirInfo.GetFiles("*.jpg", SearchOption.TopDirectoryOnly);

            currentImage = 0;

            Timer imageChangeTimer = new Timer(timeChanger);
            imageChangeTimer.Elapsed += new ElapsedEventHandler(imageChangeTimer_Elapsed);
            imageChangeTimer.Start();

            Console.WriteLine("Press key to exit.");
            Console.ReadLine();
        }

        static void imageChangeTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            const uint SPI_SETDESKWALLPAPER = 20;
            const int SPIF_UPDATEINIFILE = 0x01;
            const int SPIF_SENDWININICHANGE = 0x02;

            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, images[currentImage++].FullName, SPIF_SENDWININICHANGE | SPIF_UPDATEINIFILE);
            currentImage = (currentImage >= images.Length) ? 0 : currentImage;
        }
    }
}