using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace TestsGenerator
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string FolderPath = "C:\\Users\\userD\\Desktop\\SPP4\\resource";

            List<string> FilesPath = new List<string>() 
            {
                "C:\\Users\\userD\\Desktop\\SPP4\\TestsGenerator\\NUnitTests\\OneClass.cs",
                "C:\\Users\\userD\\Desktop\\SPP4\\TestsGenerator\\NUnitTests\\TwoClasses.cs"
            };

            Pipeline p = new Pipeline(new PipelineConfiguration(1, 1, 1));
            await p.Execute(FilesPath, FolderPath);
            Console.ReadKey();
        }
    }
}
