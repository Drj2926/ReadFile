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
        int fixedWidth { get; set; }
        bool isFixedWidth { get; set; }
        int numFields { get; set; }
        string delimeter { get; set; }
        bool hasDelimeter { get; set; }
        string findText { get; set; }
        bool hasFindText { get; set; }
        int findTextIndex { get; set; }
        string[] fields;
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
    public JRDFile(string fileName, string folderPath,string fileType, int numFields,string findText,int findeTextIndex)
    {
        this.fileName = fileName;
        this.folderPath = folderPath;
        this.filePath = folderPath + fileName + fileType;
        this.fileType = fileType;
        this.numFields = numFields;
        this.findText = findText;
        this.findTextIndex = findTextIndex;
        fields = new string [numFields];
    }


    public void readFile(JRDFile file)
    {
        using (StreamReader reader = new StreamReader(file.filePath))
        {
            string currLine = "";
            string[] fields = new string[file.numFields]; 
            while ((currLine = reader.ReadLine()) != null)
            {
                    if(Regex.Split(currLine,@"\s{2,}").Length == numFields)
                {
                    fields = Regex.Split(currLine, @"\s{2,}");

                    if (fields[findTextIndex].Equals(findText))
                    {
                        writeToFile(file, currLine);
                    }//if
                }//if
            }//while
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




    class JRDLibSqlConnection
    {
    }

    
}
