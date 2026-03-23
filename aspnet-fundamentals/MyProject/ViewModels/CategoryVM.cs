using MyProject.Models;

namespace MyProject.ViewModels
{
    public class CategoryVM
    {
        public List<Category> Categories { get; set; }
        public string? PageTitle { get; set; }
        public int TotalCount { get; set; }
    }
}