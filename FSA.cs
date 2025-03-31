using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentFTP;
using Microsoft.Win32;

namespace Menhely_Projekt
{
    public class FSA
    {
        public static void Main(string IdName)
        {
            string host = "127.0.0.1";
            string username = "Menhely_Projekt";
            string password = "admin";

            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            if (openFileDialog.ShowDialog() == true)
            {
                string selectedFilePath = openFileDialog.FileName;
                string remotePath = "/uploads/" + System.IO.Path.GetFileName(selectedFilePath);

                using (FtpClient client = new FtpClient(host, username, password))
                {
                    client.Connect();
                    client.UploadFile(selectedFilePath,remotePath,FtpRemoteExists.Overwrite);
                    Console.WriteLine("Siker "+remotePath);
                }
            } else
            {
                Console.WriteLine("Nincs fájl kiválasztva");
            }
            
        }
    }
}
