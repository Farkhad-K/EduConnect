using EduConnect.Api.Utilities;

namespace EduConnect.Api.Entities;

public class Teacher : UserBase
{
    public Guid? AcademyId { get; set; }
    public Academy? Academy { get; set; }
    public ICollection<Class> Classes { get; set; } = new List<Class>();
}
