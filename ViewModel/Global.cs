using CinemaManagement.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaManagement.ViewModel
{
    class Global
    {
        static public User user { get; set; }

        static public string getCurDirectoryOfProject()
        {
            DirectoryInfo parentDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory);
            String curDirectory = "";

            if (parentDirectory != null)
            {
                // Get the parent directory of the parent directory
                DirectoryInfo grandParentDirectory = Directory.GetParent(parentDirectory.FullName);

                if (grandParentDirectory != null)
                {
                    // Get the parent directory of the grandparent directory
                    DirectoryInfo greatGrandParentDirectory = Directory.GetParent(grandParentDirectory.FullName);

                    if (greatGrandParentDirectory != null)
                    {
                        // Get the parent directory of the great-grandparent directory
                        DirectoryInfo greatGreatGrandParentDirectory = Directory.GetParent(greatGrandParentDirectory.FullName);

                        if (greatGreatGrandParentDirectory != null)
                        {
                            curDirectory = greatGreatGrandParentDirectory.FullName;
                            Debug.WriteLine("Great-great-grandparent directory: " + curDirectory);
                        }
                        else
                        {
                            Debug.WriteLine("No great-great-grandparent directory found.");
                        }
                    }
                    else
                    {
                        Debug.WriteLine("No great-grandparent directory found.");
                    }
                }
                else
                {
                    Debug.WriteLine("No grandparent directory found.");
                }
            }
            else
            {
                Debug.WriteLine("No parent directory found.");
            }
            return curDirectory;
        }
    }
}
