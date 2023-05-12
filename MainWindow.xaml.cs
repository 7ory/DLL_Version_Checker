using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DLL_Version_Checker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        private readonly List<string> exclusions = new List<string> { "System", "Microsoft", "mscorlib" };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void checkButton_Click(object sender, RoutedEventArgs e)
        {
          
            libraryList.Items.Clear();

            
            var libraryDirectory = @"C:\Users\User\source\repos\TestApp1\bin\Release\net6.0";

            
            var libraryFiles = Directory.GetFiles(libraryDirectory, "*.dll");

            
            var libraries = new List<DLL_Checker>();

            
            foreach (var libraryFile in libraryFiles)
            {
                
                var libraryName = System.IO.Path.GetFileNameWithoutExtension(libraryFile);

              
                if (exclusions.Any(x => libraryName.StartsWith(x)))
                    continue;

                try
                {
                    
                    var library = new DLL_Checker(libraryName, libraryFile);
                    libraries.Add(library);
                }
                catch (Exception ex)
                {
                    
                    libraryList.Items.Add($"Error checking {libraryName}: {ex.Message}");
                }
            }

            
            foreach (var library in libraries)
            {
                
                var averageVersion = library.AverageVersion();

                
                var outdatedVersions = library.OutdatedVersions(averageVersion);

               
                if (outdatedVersions.Count > 0)
                {
                    libraryList.Items.Add($"{library.Name} ({outdatedVersions.Count} outdated versions)");
                    foreach (var version in outdatedVersions)
                    {
                        libraryList.Items.Add($"\t{version}");
                    }
                }
            }
        }
    }
      
}
