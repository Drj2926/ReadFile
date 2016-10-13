using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ReadFile
{
  
    class Application
    { 
        static void Main(string[] args)
        {
          
            //MULTI FILE INFO
            string folderPath1 = @"C:\Users\djimbo.CORPORATE\Desktop\ParsingApp\To Chase\To Chase\";
            string fileName1 = "JETRO.NACHA.Nacha";
            string fileType1 = "*.2016*";

            //FILE CONSTRAINTS 0
            int fieldCount0 = 3;
            int fieldIndex0 = 1;
            string findText0 = "627021000021031009719";
            string findDelimeter0 = @"\s{2,}";

            //FILE CONSTRAINTS 1
            int fieldCount1 = 4;
            int fieldIndex1 = 2;
            string findText1 = "5200JETRO C & C ENT.";
            string findDelimeter1 = @"\s{2,}";

            //INITIALIZE JRD FILE CONSTRAINTS
            JRDTextFinder finder0 = new JRDTextFinder(fieldCount0, fieldIndex0, findText0, findDelimeter0);
            JRDTextFinder finder1 = new JRDTextFinder(fieldCount1, fieldIndex1, findText1, findDelimeter1);

            //ARRAY FOR JRD FILE CONSTRUCTOR
            JRDTextFinder[] textFinders = new JRDTextFinder[2];
            textFinders[0] = finder0;
            textFinders[1] = finder1;


            JRDFile file = new JRDFile(fileName1, folderPath1, fileType1, textFinders);
            //file.readFiles(file);
            List<string> test = file.readFiles(file);
            file.writeToFile(true,test);

            JRDHelpers helper = new JRDHelpers();
            
        }
    }
}
