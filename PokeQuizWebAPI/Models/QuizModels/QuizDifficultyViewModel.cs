namespace PokeQuizWebAPI.Models.QuizModels
{
    public class QuizDifficultyViewModel
    {
        public int[] AmountOfQuestions = new[] { 5, 10, 25, 50, 100 };
        public int SelectedNumberOfQuestions { get; set; }
    }
}
