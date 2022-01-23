class PatternPickerSetting
{

    public RegexSetting RegEx { get; set; } = new RegexSetting();

    public string[] LeftCommentMark { get; set; } = new string[] { };

    public string[] RightCommentMark { get; set; } = new string[] { };

    public string[] CommentMark { get; set; } = new string[] { };

    public OutputSetting Output { get; set; } = new OutputSetting();

    public class RegexSetting
    {
        public string Match { get; set; } = string.Empty;

        public string[] NotMatch { get; set; } = new string[] { };

        public string[] Noise { get; set; } = new string[] { };
    }

    public class OutputSetting{
        public bool WithHeader  { get; set; } = false;
        public string  FileName { get; set; } = string.Empty;

    }

}

