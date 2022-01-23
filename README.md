# PatternPicker

這個工具能夠針對指定的路徑裡抓出符合 Regex Pattern 的字串。

會輸出成CSV格式的檔案。

## 使用情境

1. 找出指定路徑下所有文字檔裡含有 **中文** 的檔案、行數和字串

## [AppSettings](https://github.com/BreakWolf/PatternPicker/blob/master/appSettings.json)

### FileSelectionSetting:IncludedExtionsions
檢查的檔案副檔名 (不在清單內會排除)

### PatternPickerSetting:RegEx:Match
設定 RegEx 字串，用該字串做 regex 的檢查

### PatternPickerSetting:RegEx:NotMatch
設定 RegEx 字串，用該字串做 regex 的檢查。

優先於Match的檢查，符合該特徵的會優先排除。

### PatternPickerSetting:RegEx:Noise

設定 RegEx 字串，若用 RegEx:Match 查出來的東西又符合這個檢查，則會被排除

這個設計是用來很簡單的增加檢查條件，排除雜訊

### PatternPickerSetting:LeftCommentMark
左註解，在這個格式之後皆不檢查，直到遇到右註解。

### PatternPickerSetting:RightCommentMark
右註解

### PatternPickerSetting:CommentMark
註解符號。該符號之後的該行文字不檢查。

### PatternPickerSetting:OutputFile
輸出路徑和檔名
