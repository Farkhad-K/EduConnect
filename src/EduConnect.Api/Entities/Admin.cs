namespace EduConnect.Api.Entities;

public class Admin : UserBase
{
    public string? TokenOfAcademy { set; get; }
    public Guid? AcademyId { get; set; }
    public Academy? Academy { get; set; }
}
