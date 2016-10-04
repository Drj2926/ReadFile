using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

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
        JRDTextFinder [] textFinder;
        int fieldsToFind;
        //int fixedWidth { get; set; }
        //bool isFixedWidth { get; set; }
        //int numFields { get; set; }
        //string findText { get; set; }
        //bool hasFindText { get; set; }
        //int findTextIndex { get; set; }
        //JRDFileField fields;

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
        //this.fieldsToFind = fieldsToFind;
        this.textFinder = textFinder;
    }


    public void readFile(JRDFile file)
    {
        using (StreamReader reader = new StreamReader(file.filePath))
        {
            string currLine = "";
            int numConstraints = file.textFinder.Length;
             
            //WE MAY HAVE NUMEROUS CONSTRAINTS PER FILE
            for(int i = 0; i < numConstraints; i++)
            {
                string[] fields = new string[file.textFinder[i].fieldCount];
                while ((currLine = reader.ReadLine()) != null)
                {
                    if (Regex.Split(currLine, file.textFinder[i].delimeter).Length == file.textFinder[i].fieldCount)
                    {
                        fields = Regex.Split(currLine, file.textFinder[i].delimeter);
                        if (fields[file.textFinder[i].fieldIndex].Equals(file.textFinder[i].findText))
                        {
                            writeToFile(file, currLine);
                        }//if
                    }//if
                }//while
            }
        }//using
    }//method

    public void writeToFile(JRDFile file, string outputString)
    {   
        using (StreamWriter writer = new StreamWriter(file.filePath + ".txt", true))
        {
            writer.WriteLine(outputString);

        }//using
    }//method

}//CLASS

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
        string[] fields;


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
            fields = new string[fieldCount];
        }
    }


    class JRDLibSqlConnection
    {
    }

    
}
