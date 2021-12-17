using System;
using System.Net.FtpClient;
using System.IO;

namespace Mantis_Tests
{
    public class FtpHelper : HelperBase
    {
        private FtpClient client;
        public FtpHelper(ApplicationManager manager) : base(manager)
        {
            client = new FtpClient();
            client.Host = "localhost";
            client.Credentials = new System.Net.NetworkCredential("mantis", "mantis");
            client.Connect();
        }
        public void BackupFile(String path)
        {
            String BackupPath = path + ".bak";
            if (client.FileExists(BackupPath))
            {
                return;
            }
            client.Rename(path, BackupPath);
        }
        public void RestoreFromBackup(String path)
        {
            String BackupPath = path + ".bak";
            if (!client.FileExists(BackupPath))
            {
                return;
            }
            if (client.FileExists(path))
            {
                client.DeleteFile(path);
            }
            client.Rename(BackupPath, path);

        }
        public void UploadFile(String path, Stream localfile)
        {
            if (client.FileExists(path))
            {
                client.DeleteFile(path);
            }
            using (Stream FtpStream = client.OpenWrite(path))
            {
                byte[] buffer = new byte[8 * 1024];
                int count = localfile.Read(buffer, 0, buffer.Length);
                while (count > 0)
                {
                    FtpStream.Write(buffer, 0, count);
                    count = localfile.Read(buffer, 0, buffer.Length);
                }
            }
        }
    }
}
