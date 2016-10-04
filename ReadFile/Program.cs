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
            //FILE 
            string folderPath = @"C:\Users\djimbo\Documents\";
            string fileName = "JETRO.NACHA.Nacha";
            string fileType = ".2016003";

            //FILE CONSTRAINTS 0
            int fieldCount0 = 3;
            int fieldIndex0 = 1;
            string findText0 = "627021000021031009719";
            string findDelimeter0 = @"\s{1,}";

            //FILE CONSTRAINTS 1
            int fieldCount1 = 4;
            int fieldIndex1 = 3;
            string findText1 = "5200JETRO C & C ENT.";
            string findDelimeter1 = @"\s{1,}";

            JRDTextFinder finder0 = new JRDTextFinder(fieldCount0, fieldIndex0, findText0, findDelimeter0);
            JRDTextFinder finder1 = new JRDTextFinder(fieldCount1, fieldIndex1, findText1, findDelimeter1);

            //LIST FOR JRD FILE CONSTRUCTOR

            JRDTextFinder[] textFinders = new JRDTextFinder[2];

            textFinders[0] = finder0;
            textFinders[1] = finder1;


            JRDFile read = new JRDFile(fileName,folderPath,fileType,));


            read.readFile(read);

        }
    }
}
