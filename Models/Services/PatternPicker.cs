using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

class PatternPicker
{
    private List<CSVModel> matches;

    private readonly PatternPickerSetting setting;

    public PatternPicker(PatternPickerSetting setting)
    {
        this.setting = setting;
        this.matches = new List<CSVModel>();
    }

    public void Pick(IEnumerable<FileInfo> fileInfos)
    {
        foreach (var fileInfo in fileInfos)
        {
            var contentLines = File.ReadAllLines(fileInfo.FullName, System.Text.Encoding.UTF8);

            var isInComment = false;
            var lineNumber = 0;
            foreach (var line in contentLines)
            {
                lineNumber++;
                var lineContent = line.Trim();


                if (this.setting.LeftCommentMark.Any(x => lineContent.Contains(x)))
                {
                    isInComment = true;
                    continue;
                }

                if (this.setting.RightCommentMark.Any(x => lineContent.Contains(x)))
                {
                    isInComment = false;
                    continue;
                }

                if (this.setting.CommentMark.Any(x => lineContent.Contains(x)))
                {
                    continue;
                }

                if (isInComment)
                {
                    continue;
                }

                var matchWords = Regex.Matches(lineContent, setting.RegEx.Match);
                foreach (var word in matchWords)
                {
                    if(this.setting.RegEx.NotMatch.Any(x => Regex.IsMatch(word.ToString(), x))){
                        continue;
                    }
                    
                    matches.Add(new CSVModel
                    {
                        FileName = fileInfo.FullName,
                        LineNumber = lineNumber,
                        Content = lineContent,
                        Match = word.ToString()
                    });
                }
            }
        }
    }

    public void Export(){
        var outputContent = new StringBuilder();
        foreach(var match in this.matches){
            outputContent.AppendLine($"{match.FileName},{match.LineNumber},{match.Match},{match.Content}");
        }
        
        File.WriteAllText(setting.OutputFile, outputContent.ToString());
    }

}