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

                if (this.setting.RegEx.NotMatch.Any(x => Regex.IsMatch(lineContent, x)))
                {
                    continue;
                }

                var matchWords = Regex.Matches(lineContent, setting.RegEx.Match);
                foreach (var word in matchWords)
                {
                    if (this.setting.RegEx.Noise.Any(x => Regex.IsMatch(word.ToString(), x)))
                    {
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

    public void Export()
    {
        if (string.IsNullOrWhiteSpace(setting.Output.FileName))
        {
            return;
        }

        var outputContent = new StringBuilder();
        if (setting.Output.WithHeader)
        {
            outputContent.AppendLine($"FileName{setting.Output.Seperator}LineNumber{setting.Output.Seperator}Match{setting.Output.Seperator}Origin");
        }

        foreach (var match in this.matches)
        {
            outputContent.AppendLine($"{match.FileName}{setting.Output.Seperator}{match.LineNumber}{setting.Output.Seperator}{match.Match}{setting.Output.Seperator}{match.Content}");
        }

        File.WriteAllText(setting.Output.FileName, outputContent.ToString());
    }

}