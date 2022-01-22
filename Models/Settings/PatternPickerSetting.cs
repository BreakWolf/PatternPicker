class PatternPickerSetting
{

    public RegexSetting RegEx { get; set; } = new RegexSetting();

    public string[] LeftCommentMark { get; set; } = new string[] { };

    public string[] RightCommentMark { get; set; } = new string[] { };

    public string[] CommentMark { get; set; } = new string[] { };

    public string OutputFile { get; set; } = string.Empty;

    public class RegexSetting
    {
        public string Match { get; set; } = string.Empty;

        public string[] NotMatch { get; set; } = new string[] { };
    }

}

