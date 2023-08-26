using Interfaces;
using System.IO;
using System.Reflection;
using System.Text;

namespace CarPool.Services
{
    public class Logger : ILog
    {
        public static string fileName = "log" + DateTime.Now.ToString("ddMMyyyy");
        public string filePath = @"D:\taskTechnovert\task-5-car-pooling-web-api-csharp-ankithpal\Backend\Logs\" + fileName + ".txt";
        public  void Log(string message)
        {
            message =" [ "  + DateTime.Now.ToString() + " ]  " +  message;
            try
            {
                if (File.Exists(filePath))
                {
                    File.AppendAllText(filePath, message + Environment.NewLine);
                    return;
                }
                using (FileStream fs = File.Create(filePath))
                {
                    Byte[] tittle = new UTF8Encoding(true).GetBytes(message);
                    fs.Write(tittle, 0, tittle.Length);
                }
            }
            catch(Exception ex) 
            {
                throw ex;
            } 
        }
    }
}
