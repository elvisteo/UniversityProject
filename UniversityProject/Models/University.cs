using Microsoft.AspNetCore.Razor.TagHelpers;

namespace UniversityProject.Models
{
    public class University
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Webpages { get; set; }
        public bool IsBookmark { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModeified { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? DeletedAt { get; set; } 
    }
}
