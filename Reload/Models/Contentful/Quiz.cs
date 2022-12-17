namespace Reload.Models.Contentful
{
    public class Quiz
    {
        public string? Question { get; set; }
        public string[]? PossibleAnswers { get; set; }
        public int CorrectAnswerId { get; set; }
    }
}
