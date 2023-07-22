using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DMAWS_T2203E_LaVietAnh.Entities
{
    [Table("Projects")]
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }
        [Required]
        [StringLength(150, MinimumLength = 2)]
        public string ProjectName { get; set; }
        [Required]
        public DateTime ProjectStartDate { get; set; }
        public DateTime? ProjectEndDate { get; set; }

        public bool IsValidProjectDates()
        {
            if (ProjectEndDate.HasValue && ProjectStartDate >= ProjectEndDate.Value)
            {
                return false;
            }
            return true;
        }
        public virtual ICollection<ProjectEmployee> ProjectEmployees { get; set; }
        
    }
}
