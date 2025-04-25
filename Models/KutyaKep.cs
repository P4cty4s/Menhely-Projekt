using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using FluentFTP;

namespace Menhely_Projekt.Models
{
    internal class KutyaKep
    {
        public int ID { get; set; }
        public int KutyaID { get; set; }
        public string nev { get; set; }
        public BitmapImage Kep { get; set; }

        public KutyaKep(List<string> imageFileNames)
        {
            string host = "127.0.0.1"; // FTP host
            string username = "Menhely_Projekt"; // FTP username
            string password = "admin"; // FTP password

            

            try
            {
                using (FtpClient client = new FtpClient(host, username, password))
                {
                    client.Connect();

                    foreach (string remoteFileName in imageFileNames)
                    {
                        string tempFilePath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), Guid.NewGuid().ToString() + ".tmp");

                        // Download the file from FTP
                        client.DownloadFile(tempFilePath, remoteFileName);

                        // Load file into MemoryStream
                        byte[] fileBytes = File.ReadAllBytes(tempFilePath);
                        using (MemoryStream memoryStream = new MemoryStream(fileBytes))
                        {
                            // Create BitmapImage from stream
                            BitmapImage bitmap = new BitmapImage();
                            bitmap.BeginInit();
                            bitmap.StreamSource = memoryStream;
                            bitmap.CacheOption = BitmapCacheOption.OnLoad;
                            bitmap.EndInit();
                            bitmap.Freeze(); // Makes it cross-thread accessible and improves performance

                            kutya_kepek.Add(bitmap); // Add to list
                        }

                        // Delete temp file
                        File.Delete(tempFilePath);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading images: " + ex.Message);
            }
        }
    }
}
