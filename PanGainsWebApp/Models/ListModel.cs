namespace PanGainsWebApp.Models
{
    public class ListModel
    {
        public List<Account> AccountModel { get; set; }
        public List<ChallengeStats> ChallengeStatsModel { get; set; }
        public List<CompletedWorkout> CompletedWorkoutModel { get; set; }
        public List<DaysWorkedOut> DaysWorkedOutModel { get; set; }
        public List<Exercise> ExerciseModel { get; set; }
        public List<Folder> FolderModel { get; set; }
        public List<Leaderboard> LeaderboardModel { get; set; }
        public List<Routine> RoutineModel { get; set; }
        public List<Set> SetModel { get; set; }
        public List<Social> SocialModel { get; set; }
        public List<Statistics> StatisticsModel { get; set; }
        public List<YourExercise> YourExerciseModel { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string SearchAccount { get; set; }

    }
}
