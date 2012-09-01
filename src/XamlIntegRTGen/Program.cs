using System;
using System.Collections;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace XamlIntegRTGen
{
    class Program
    {
        private static string _dataFileTemplate = @"
using System.Collections.Generic;
using XamlIntegRT;

namespace {0}
{{
    public class XamlFilesData : IXamlFilesData
    {{
        public Dictionary<string, string> FilesToVerify {{ get; private set; }}

        public XamlFilesData()
        {{
            FilesToVerify = new Dictionary<string, string>
            {{
                {1}
            }};
        }}
    }}
}}";

        static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("XamlIntegRT Generator - https://github.com/AndreiMarukovich/XamlIntegRT");
                Console.WriteLine("Usage:");
                Console.WriteLine("  XamlIntegRTGen.exe <project_folder_path> <file_path> <namespace> [/ads]");
                return;
            }

            var adsOnly = args.Length >= 4 && args[3].ToLower() == "/ads";

            string[] files;
            try
            {
                files = Directory.GetFiles(args[0], "*.xaml", SearchOption.AllDirectories);
            }
            catch (Exception e)
            {
                Console.WriteLine("Can't access folder {0} [{1}]", args[0], e.Message);
                return;
            }

            var duplicates = new Hashtable();
            var sb = new StringBuilder();
            foreach (var file in files)
            {
                var name = Path.GetFileName(file);
                if (String.IsNullOrWhiteSpace(name) || duplicates.ContainsKey(name))
                    continue;

                duplicates.Add(name, name);
                var xaml = File.ReadAllText(file).Trim();
                if (!adsOnly || xaml.Contains("AdControl"))
                {
                    var hash = GetFileHash(xaml);
                    sb.AppendFormat("\t\t{{\"{0}\", \"{1}\"}},\n", name, hash);
                }
            }

            try
            {
                var content = String.Format(_dataFileTemplate, args[2], sb.ToString());
                File.WriteAllText(args[1], content);
            }
            catch (Exception e)
            {
                Console.WriteLine("Can't create {0} [{1}]", args[1], e.Message);
                return;
            }

            Console.WriteLine("{0} is done", args[1]);
        }

        private static string GetFileHash(string xaml)
        {
            var data = Encoding.BigEndianUnicode.GetBytes(xaml);

            var hash = SHA1.Create();
            var hashData = hash.ComputeHash(data);

            return Convert.ToBase64String(hashData);
        }
    }
}
