using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Net.Mail;

namespace ReadFile
{
    /// <summary>
    /// STANDARD FLAT FILE PROPERTY CLASS
    /// </summary>
    public class JRDFile
    {
        string fileName { get; set; }
        string folderPath { get; set; }
        string filePath { get; set; }
        string fileType { get; set; }
        string[] outputFields { get; set; }
        string outputFileName { get; set; }
        string outputFolderPath { get; set; }
        string outputPath { get; set; }
        List <string> outputLines { get; set; }
        JRDTextFinder [] textFinder;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="folderPath"></param>
        public JRDFile(string fileName, string folderPath)
        {
            this.fileName = fileName;
            this.folderPath = folderPath;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="folderPath"></param>
        /// <param name="fileType"></param>
        /// <param name="numFields"></param>
        /// <param name="findText"></param>
        /// <param name="findeTextIndex"></param>
        public JRDFile(string fileName, string folderPath,string fileType,JRDTextFinder [] textFinder)
        {
            this.fileName = fileName;
            this.folderPath = folderPath;
            this.filePath = folderPath + fileName + fileType;
            this.fileType = fileType;
            this.textFinder = textFinder;
            outputFields = new string[textFinder.Length + 1];
            outputLines = new List<string>();
            outputFolderPath = folderPath + @"Processed\";
            

        }

        public JRDFile(string filePath,JRDTextFinder [] textFinder)
        {
                this.filePath = filePath;
                this.textFinder = textFinder;
                fileName = Path.GetFileName(filePath);
                outputFields = new string[textFinder.Length + 1];
                outputLines = new List<string> ();
                outputFolderPath = folderPath + @"Processed\";
        }

        //@todo OPTIMIZE THIS CODE
        public List<string> readFiles(JRDFile file)
        {

            string currLine = "";
            int numConstraints = file.textFinder.Length;

            foreach (string directoryPath in Directory.GetFiles(file.folderPath, file.fileName + file.fileType))
            {
                using (StreamReader reader = new StreamReader(directoryPath))
                {
                    while ((currLine = reader.ReadLine()) != null)
                    {
                        //WE MAY HAVE NUMEROUS CONSTRAINTS PER FILE
                        for (int i = 0; i < numConstraints; i++)
                        {
                            string[] fields = new string[file.textFinder[i].fieldCount];

                            if (Regex.Split(currLine, file.textFinder[i].delimeter).Length == file.textFinder[i].fieldCount)
                            {
                                fields = Regex.Split(currLine, file.textFinder[i].delimeter);
                                for (int j = 0; j < fields.Length; j++)
                                {
                                    if (fields[j].Equals(file.textFinder[i].findText))
                                    {
                                        file.outputFields[i] = fields[file.textFinder[i].fieldIndex];
                                    }//if
                                }//for
                            }//if
                        }//for
                    }//while                
                }//using
                outputFields[outputFields.Length - 1] = Path.GetFileName(directoryPath);
                outputLines.Add(createOutputline(file.outputFields));
                //writeToFile(file,Path.GetFileName(directoryPath), file.outputFields);
            }//for
            return outputLines;
        }//method

        private string createOutputline(string [] outputStrings)
        {
            StringBuilder builder = new StringBuilder();
            foreach (string outputString in outputStrings )
            {
                outputString.Replace(",", "");
                builder.Append(outputString + ", ");
            }
            return builder.ToString().Trim().Substring(0, builder.ToString().Trim().Length - 1);
        }

        public void writeToFile(bool includeFileName,List <string> outputLines)
        {
            StringBuilder builder = new StringBuilder();
            Directory.CreateDirectory(this.outputFolderPath);
            using (StreamWriter writer = new StreamWriter( outputFolderPath + DateTime.Now.ToString("yyyy_MM_dd_mm_") +"ParsedFile.txt"))
            {
                foreach (string outputLine in outputLines)
                {
                    if (includeFileName)
                        writer.WriteLine(outputLine);
                    else
                        writer.WriteLine(outputLine.Substring(outputLine.IndexOf(","), outputLine.Length - outputLine.IndexOf(",")));
                }
            }
        }

    }//class

   public class JRDTextFinder
    {
        public bool isFixedWidth;
        public bool isLineNumber;
        public bool isFieldCount;
        public bool isFieldIndex;
        public bool hasFindText;
        public bool hasDelimeter;
        public long fixedWidth;
        public int lineNumber;
        public int fieldCount;
        public int fieldIndex;
        public string findText;
        public string delimeter;
        public string fieldToFind;


        public JRDTextFinder()
        {

        }

        public JRDTextFinder(int fieldCount, int fieldIndex, int lineNumber)
        {
            this.fieldCount = fieldCount;
            this.fieldIndex = fieldIndex;
            this.lineNumber = lineNumber;
            this.isFieldCount = true;
            this.isFieldIndex = true;
            this.isLineNumber = true;
            this.isFixedWidth = false;
            this.hasFindText = false;
            this.hasDelimeter = false;
            this.fieldToFind = null;
        }

        public JRDTextFinder(int fieldCount, int fieldIndex,string findText,string delimeter)
        {
            this.fieldCount = fieldCount;
            this.fieldIndex = fieldIndex;
            this.findText = findText;
            this.delimeter = delimeter;
            this.isFieldCount = true;
            this.isFieldIndex = true;
            this.isFixedWidth = false;
            this.isLineNumber = false;
            this.hasFindText = true;
            this.hasDelimeter = true;
            this.fieldToFind = null;
        }  
    }

    public class JRDHelpers
    {

        public void sendEmail(string subject, string body, string sender, string[] recipients)
        {

            SmtpClient smtp = new SmtpClient("outlook.corporate.jetrord.com", 25);
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(sender);
            mail.IsBodyHtml = true;
            mail.Subject = subject;
            mail.Body = body;

            foreach (string recepient in recipients)
            {
                mail.To.Add(recepient);
            }

            smtp.Send(mail);
        }

        //@overload default sender here
        public void sendEmail(string subject, string body, string[] recipients)
        {

            SmtpClient smtp = new SmtpClient("outlook.corporate.jetrord.com", 25);
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("support@JetroRD.com");
            mail.IsBodyHtml = true;
            mail.Subject = subject;
            mail.Body = body;

            foreach (string recepient in recipients)
            {
                mail.To.Add(recepient);
            }

            smtp.Send(mail);
        }

        public string convertMainFrameDate(string format, string date)
        {
            string convertedDate = "";
            switch (format.ToLower())
            {
                case "yyyymmdd":
                    convertedDate = date.Trim().Substring(4, 2) + "/" + date.Trim().Substring(date.Length - 2, 2)
                   + "/" + date.Trim().Substring(0, 4);
                    break;
                case "yymmdd":
                    convertedDate = date.Trim().Substring(2, 2) + "/" + date.Trim().Substring(date.Length - 2, 2)
                    + "/" + date.Trim().Substring(0,2);
                    break;
                default:
                    convertedDate = "";
                    break;
            }

            try
            {
                 Convert.ToDateTime(convertedDate);

            }
            catch (Exception e)
            {
                Console.WriteLine("Bad date format will deafault to" + Convert.ToDateTime("1/1/1900"));
                return "1/1/1900";
            }

            return Convert.ToDateTime(convertedDate).ToShortDateString();
        }

        public string getSubstring(bool afterStr,string text ,string findText)
        {
            try
            {
                if (afterStr)
                {
                    return text.Substring(text.IndexOf(findText), text.Length - text.IndexOf(findText));
                }
                else return text.Substring(0, text.IndexOf(findText));
            }catch(Exception e)
            {
                Console.WriteLine("Search Pattern '" + findText + "'not found in text '" + text + "'");
                return text;
            }
        }

    }
}
