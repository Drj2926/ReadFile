using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadFile
{
  
    class Application
    { 
        static void Main(string[] args)
        {
            string folderPath = @"C:\Users\djimbo\Documents\";
            string fileName = "JETRO.NACHA.Nacha";
            string fileType = ".2016003";
            string findText = "627021000021031009719";
            int numFields = 3;
            int findTextIndex = 0;

            JRDFile read = new JRDFile(fileName,folderPath,fileType,numFields,findText,findTextIndex);


            read.readFile(read);

            //read.writeToFile(read,"Testing 123");
        }
    }
}
