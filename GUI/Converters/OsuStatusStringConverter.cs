using GUI.Options;
using System;
using System.Globalization;
using System.Windows.Data;

namespace GUI.Converters
{
    [ValueConversion(typeof(OsuStatus), typeof(string))]
    public class OsuStatusStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            OsuStatus newOsuStatus = (OsuStatus)value;
            
            switch (newOsuStatus)
            {
                case OsuStatus.EditingMap:
                    return "Editing Map";
                case OsuStatus.GameShutdownAnimation:
                    return "Shutting Down";
                case OsuStatus.GameStartupAnimation:
                    return "Starting Up";
                case OsuStatus.MainMenu:
                    return "Main Menu";
                case OsuStatus.MultiplayerResultsscreen:
                    return "MP Results";
                case OsuStatus.MultiplayerRoom:
                    return "MP Room";
                case OsuStatus.MultiplayerRooms:
                    return "MP Rooms";
                case OsuStatus.MultiplayerSongSelect:
                    return "MP Song Select";
                case OsuStatus.NotRunning:
                    return "Not Running";
                case OsuStatus.OsuDirect:
                    return "osu! Direct";
                case OsuStatus.Playing:
                    return "Playing a Map";
                case OsuStatus.ProcessingBeatmaps:
                    return "Processing Maps";
                case OsuStatus.RankingTagCoop:
                    return "Ranking Tag Co-op";
                case OsuStatus.RankingTeam:
                    return "Ranking Team";
                case OsuStatus.ResultsScreen:
                    return "Results Screen";
                case OsuStatus.SongSelect:
                    return "Song Select";
                case OsuStatus.SongSelectEdit:
                    return "Song Select Edit";
                case OsuStatus.Tourney:
                    return "Tourney";
                case OsuStatus.Unknown:
                    return "Unknown";

                case OsuStatus.SongPaused:
                    return "Paused";
                default:
                    return "Unknown";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
