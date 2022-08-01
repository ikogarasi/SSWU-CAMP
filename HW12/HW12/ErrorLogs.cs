using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW12
{
    public class ErrorLogs
    {
        private Dictionary<DateTime, string> _errorLogs;
        private string sFilePath = string.Empty;
        public static readonly string[] sDateFormats = new string[] { "dd.M.yyyy H:m:s", "dd-M-yyyy H:m:s", "dd/M/yyyy H:m:s" };
        
        public ErrorLogs(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException();
            _errorLogs = new Dictionary<DateTime, string>();
            sFilePath = filePath;
        }

        public string this[DateTime date]
        {
            set
            {
                _errorLogs[date] = value;
                WriteError(date, value);
            }
        }

        public List<string> GetLogsForDate(DateTime date)
        {
            List<string> temp = new List<string>();
            
            foreach (var item in _errorLogs)
            {
               var _key = item.Key;

                if (DateTime.Compare(date, _key) <= 0)
                {
                    temp.Add($"{_key.Day}.{_key.Month}.{_key.Year} {_key.Hour}:{_key.Minute}:{_key.Second} - {item.Value}");
                }
            }

            return temp;
        }

        private void WriteError(DateTime date, string errorMessage)
        {
            using (StreamWriter sw = File.AppendText(sFilePath))
                sw.WriteLine($"{date.Day}.{date.Month}.{date.Year} {date.Hour}:{date.Minute}:{date.Second} - {errorMessage}");
        }
    }
}
