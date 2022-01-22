using System.Collections.Generic;
using System.IO;
using System.Linq;

class FileSelector
{
    private readonly FileSelectionSetting fileSelectionSetting;

    public FileSelector(FileSelectionSetting fileSelectionSetting)
    {
        this.fileSelectionSetting = fileSelectionSetting;
    }

    private IEnumerable<FileInfo> GetFiles(DirectoryInfo directoryInfo)
    {
        var files = directoryInfo.GetFiles();
        var rtn = new List<FileInfo>();

        foreach (var fileInfo in files)
        {
            var extension = fileInfo.Extension.Replace(".", string.Empty);
            if (fileSelectionSetting.IncludedExtionsions.Contains(extension))
            {
                rtn.Add(fileInfo);
            }
        }

        return rtn;
    }

    private IEnumerable<DirectoryInfo> GetFolders(DirectoryInfo directoryInfo)
    {
        return directoryInfo.GetDirectories();
    }

    public IEnumerable<FileInfo> GetAllFiles(DirectoryInfo targetDirectoryInfo)
    {
        var rtn = new List<FileInfo>();
        var subFolders = GetFolders(targetDirectoryInfo);
        if (subFolders.Count() > 0)
        {
            foreach (var subfolder in subFolders)
            {
                rtn.AddRange(GetAllFiles(subfolder));
            }
        }

        var files = GetFiles(targetDirectoryInfo);
        rtn.AddRange(files);

        return rtn;
    }

}