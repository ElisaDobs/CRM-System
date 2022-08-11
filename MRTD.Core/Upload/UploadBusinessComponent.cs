using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MRTD.Core.Upload
{
    public class UploadBusinessComponent
    {
        public static string UploadFile(string filePath, byte[] fileData, string fileName)
        {
            string currentPath = Path.Combine(
                                                filePath,
                                                DateTime.Now.ToString("yyyyMMdd"), 
                                                DateTime.Now.ToString("HH")
                                             );

            if (!Directory.Exists(currentPath))
            {
                Directory.CreateDirectory(currentPath);
            }

            using (MemoryStream memory = new MemoryStream(fileData))
            {
                using (FileStream file = new FileStream(Path.Combine(currentPath, fileName), FileMode.Create, FileAccess.Write))
                {
                    memory.WriteTo(file);
                }
            }
            return Path.Combine(currentPath, fileName);
        }
        public static string SaveDocument(string UploadPath, string IDNo, string UploadFolder, string FileName, byte[] UploadData)
        {
            UploadPath = Path.Combine(
                                        UploadPath, 
                                        IDNo, 
                                        UploadFolder
                                     );
            if (!Directory.Exists(UploadPath))
                Directory.CreateDirectory(UploadPath);

            using (MemoryStream memory = new MemoryStream(UploadData))
            {
                using (FileStream file = new FileStream(Path.Combine(UploadPath, FileName), FileMode.Create, FileAccess.Write))
                {
                    memory.WriteTo(file);
                }
            }
            return Path.Combine(UploadPath, FileName);
        }

        public static byte[] GetDocumentByPathName(string Path)
        {
            try
            {
                byte[] document = File.ReadAllBytes(Path);
                return document;
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }
    }
}
