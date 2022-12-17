using System.ComponentModel.DataAnnotations;

namespace Reload.Models.DataTables
{
    public class CourseProgress
    {
        [Key]
        public int CourseProgressId { get; set; }

        [Required]
        public string CourseName { get; set; }

        public int ChapterIdx { get; set; }

        public string UserId { get; set; }

        public CourseProgress(string courseName, int chapterIdx, string userId)
        {
            CourseName = courseName;
            ChapterIdx = chapterIdx;
            UserId = userId;
        }
    }
}
