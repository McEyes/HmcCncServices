using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZSocketNc.Utils
{
    public static class FileHelper
    {
        public static void CreateFile(string filePath, byte[] streamArray)
        {
            string fileDirectoryPath = Path.GetDirectoryName(filePath);

            if (!Directory.Exists(fileDirectoryPath))
            {
                Directory.CreateDirectory(fileDirectoryPath);
            }

            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                fs.Write(streamArray, 0, streamArray.Length);
            }
        }

        public static List<FileInfo> GetDirectoryFile(string directoryPath)
        {
            List<FileInfo> fileList = new List<FileInfo>();

            if (Directory.Exists(directoryPath))
            {
                DirectoryInfo directory = new DirectoryInfo(directoryPath);

                FileInfo[] fileArray = directory.GetFiles();

                if (fileArray.Length > 0)
                {
                    fileList.AddRange(fileArray);
                }

                DirectoryInfo[] directoryArray = directory.GetDirectories();

                if (directoryArray.Length > 0)
                {
                    foreach (DirectoryInfo directoryItem in directoryArray)
                    {
                        List<FileInfo> directoryFileList = GetDirectoryFile(directoryItem.FullName);

                        if (directoryFileList.Count > 0)
                        {
                            fileList.AddRange(directoryFileList);
                        }
                    }
                }
            }

            return fileList;
        }

        public static long GetDirectorySize(string directoryPath)
        {
            long totalSize = 0;

            if (Directory.Exists(directoryPath))
            {
                DirectoryInfo directory = new DirectoryInfo(directoryPath);

                FileInfo[] fileArray = directory.GetFiles();

                if (fileArray.Length > 0)
                {
                    foreach (FileInfo fileItem in fileArray)
                    {
                        totalSize += fileItem.Length;
                    }
                }

                DirectoryInfo[] directoryArray = directory.GetDirectories();

                if (directoryArray.Length > 0)
                {
                    foreach (DirectoryInfo directoryItem in directoryArray)
                    {
                        totalSize += GetDirectorySize(directoryItem.FullName);
                    }
                }
            }

            return totalSize;
        }

        public static long GetFileSize(string filePath)
        {
            if (File.Exists(filePath))
            {
                FileInfo file = new FileInfo(filePath);

                return file.Length;
            }

            return 0;
        }


        public static DateTime? GetFileCreateDate(string filePath)
        {
            if (File.Exists(filePath))
            {
                FileInfo file = new FileInfo(filePath);
                var date = file.CreationTime;
                return date;
            }
            return null;
        }
    }
}