using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DLL_Version_Checker
{
    public class DLL_Checker
    {
        public string Name { get; }
        public string FilePath { get; }
        public List<Version> Versions { get; }

        public DLL_Checker(string name, string filePath)
        {
            Name = name;
            FilePath = filePath;
            Versions = new List<Version>();

            // Attempt to load the library into an assembly object
            var assembly = Assembly.LoadFile(filePath);

            // Get the version number of the library from its assembly
            var version = assembly.GetName().Version;

            // Add the library's version number to its Versions list
            Versions.Add(version);
        }

        public Version AverageVersion()
        {
            return new Version(
                (int)Versions.Average(x => x.Major),
                (int)Versions.Average(x => x.Minor),
                (int)Versions.Average(x => x.Build),
                (int)Versions.Average(x => x.Revision)
            );
        }

        public List<Version> OutdatedVersions(Version averageVersion)
        {
            return Versions.Where(x => x < averageVersion).ToList();
        }
    }
}

