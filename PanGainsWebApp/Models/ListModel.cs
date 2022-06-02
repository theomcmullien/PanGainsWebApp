namespace PanGainsWebApp.Models
{
    public class ListModel
    {
        public IList<Account> AccountModel { get; set; }
        public IList<ChallengeStats> ChallengeStatsModel { get; set; }
        public IList<CompletedWorkout> CompletedWorkoutModel { get; set; }
        public IList<DaysWorkedOut> DaysWorkedOutModel { get; set; }
        public IList<Exercise> ExerciseModel { get; set; }
        public IList<Folder> FolderModel { get; set; }
        public IList<Leaderboard> LeaderboardModel { get; set; }
        public IList<Routine> RoutineModel { get; set; }
        public IList<Set> SetModel { get; set; }
        public IList<Social> SocialModel { get; set; }
        public IList<Statistics> StatisticsModel { get; set; }
        public IList<YourExercise> YourExerciseModel { get; set; }

    }
}
