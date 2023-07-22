using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DMAWS_T2203E_LaVietAnh.Entities
{
    [Table("ProjectEmployees")]
    public class ProjectEmployee
    {
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public int ProjectId { get; set; }
        [Required]
        public string Tasks { get; set; }
        public virtual Employee Employees { get; set; }
        public virtual Project Projects { get; set; }    
    }
}
