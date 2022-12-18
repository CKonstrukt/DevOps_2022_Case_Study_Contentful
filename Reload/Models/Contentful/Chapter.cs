using Contentful.Core.Models;

namespace Reload.Models.Contentful
{
    public class Chapter
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Introduction { get; set; }
        public Document? Content { get; set; }
        public Quiz[]? Quizzes { get; set; }
        public dynamic? Sys { get; set; }
    }
}