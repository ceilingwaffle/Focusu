namespace GUI
{
    //using OsuMemoryDataProvider;

    /// <summary>
    /// Osu Memory Status (copied from OsuMemoryDataProvider.OsuMemoryStatus)
    /// </summary>
    public enum OsuStatus 
    {
        NotRunning = -1,
        MainMenu = 0,
        EditingMap = 1,
        Playing = 2,
        GameShutdownAnimation = 3,
        SongSelectEdit = 4,
        SongSelect = 5,
        WIP_NoIdeaWhatThisIs = 6,
        ResultsScreen = 7,
        GameStartupAnimation = 10,
        MultiplayerRooms = 11,
        MultiplayerRoom = 12,
        MultiplayerSongSelect = 13,
        MultiplayerResultsscreen = 14,
        OsuDirect = 15,
        RankingTagCoop = 17,
        RankingTeam = 18,
        ProcessingBeatmaps = 19,
        Tourney = 22,
        /// <summary>
        /// Indicates that status read in osu memory is not defined in OsuMemoryStatus
        /// </summary>
        Unknown = -2,

        // my own below:
        SongPaused = 101,
        InMapBreak = 102,
        MapStart = 103,
    }
}
