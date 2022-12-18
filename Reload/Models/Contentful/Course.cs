namespace Reload.Models.Contentful
{
    public class Course
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public Chapter[]? Chapters { get; set; }
        public int ChapterStartIdx { get; set; }
        public dynamic? Cover { get; set; }

        public string getChaptersId(Chapter[] chapters)
        {
            string seperatedId = "";
            foreach (Chapter chapter in chapters)
            {
                if (seperatedId.Length == 0)
                {
                    seperatedId += chapter.Sys?["id"];
                }
                else
                {
                    seperatedId += "," + chapter.Sys?["id"];
                }
            }
            return seperatedId;
        }
    }
}
