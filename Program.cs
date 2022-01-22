using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;

namespace ChineseWordPicker
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appSettings.json")
                .Build();

            var targetFolder = Directory.GetCurrentDirectory();
            if(args.Length != 0){
                targetFolder = new DirectoryInfo(args[0]).FullName;
            }

            if(!Directory.Exists( targetFolder)){
                Console.WriteLine($"{targetFolder} is not exists.");
                return;
            }

            var targetDirectoryInfo = new DirectoryInfo(targetFolder);
            Console.WriteLine(targetFolder);

            var files = GetAllFiles(config, targetDirectoryInfo);
            PickChinese(config, files);


        }

        private static IEnumerable<FileInfo> GetAllFiles(IConfiguration config, DirectoryInfo directoryInfo){
            var fileSelectionSetting = config.GetSection("FileSelectionSetting").Get<FileSelectionSetting>();

            var fileSelector = new FileSelector(fileSelectionSetting);
            var files = fileSelector.GetAllFiles(directoryInfo);
            Console.WriteLine($"FileCount: {files.Count()}");
            return files;
        }

        private static void PickChinese(IConfiguration config, IEnumerable<FileInfo> fileInfos){
            var patternPickerSetting = config.GetSection("PatternPickerSetting").Get<PatternPickerSetting>();

            var picker = new PatternPicker(patternPickerSetting);
            picker.Pick(fileInfos);
            picker.Export();
        }

    }
}
