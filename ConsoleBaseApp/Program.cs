// See https://aka.ms/new-console-template for more information

using Applications;


var subtitlemodifier = new SubtitlesModifier();
await subtitlemodifier.LoadSubtitleFile(@"D:\danie\Videos\Series\Goblin (2016-2017)\Goblin 03.srt");
subtitlemodifier.SetSubtitleTimeAdjust(-17);
await subtitlemodifier.Execute();