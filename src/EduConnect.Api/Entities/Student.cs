using EduConnect.Api.Utilities;

namespace EduConnect.Api.Entities;

public class Student
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? UniqueToken { get; set; }

    public Guid? AcademyId { get; set; }
    public Academy? Academy { get; set; }
    public ICollection<Class> Classes { get; set; } = new List<Class>();
    public ICollection<Parent> Parents { get; set; } = new List<Parent>();
}

/*
using EduConnect.Api.Abstractions;
using EduConnect.Api.Dtos.StudentDtos;
using EduConnect.Api.Exceptions;
using EduConnect.Api.Mappers.StudentMappers;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController(
    IStudentsService studentsService,
    IClassesService classesService,
    ILogger<StudentsController> logger) : ControllerBase
{
    [HttpGet]
    public async ValueTask<IActionResult> GetAllStudentsAsync(CancellationToken abortionToken = default)
    {
        var academyId = GetAcademyIdFromToken();

        if (academyId == null)
            return Forbid("You do not have permission to access this resource.");

        var students = await studentsService.GetAllStudentsAsync(abortionToken);
        return Ok(students.Where(s => s.AcademyId == academyId.Value).Select(s => s.ToDto()));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetStudentByIdAsync(Guid id, CancellationToken abortionToken = default)
    {
        try
        {
            var student = await studentsService.GetStudenByIdAsync(id, abortionToken);
            return Ok(student.ToDto());
        }
        catch (StudentNotFoundException)
        {
            return NotFound($"Student with ID {id} not found.");
        }
    }

    // Change the method to handle creating a student with classes in service layer
    [HttpPost]
    public async Task<IActionResult> CreateStudentAsync([FromBody] CreateStudent studentDto, CancellationToken abortionToken = default)
    {
        var academyId = GetAcademyIdFromToken();

        if (academyId == null)
            return Forbid("You do not have permission to create a student.");

        var student = studentDto.ToEntity(academyId.Value);
        
        try
        {
            if (studentDto.ClassIds.Any())
            {
                foreach (var classId in studentDto.ClassIds)
                {
                    var classEntity = await classesService.GetClassByIdAsync(classId, abortionToken);
                    classEntity.Students.Add(student);
                }

            }

            var createdStudent = await studentsService.CreateStudentAsync(student, abortionToken);

            return Ok(createdStudent.ToDto());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating student.");
            return StatusCode(500, "An error occurred while creating the student.");
        }
    }

    [HttpGet("by-token/{token}")]
    public async ValueTask<IActionResult> GetStudentByTokenAsync([FromRoute] string token, CancellationToken abortionToken = default)
    {
        try
        {
            var student = await studentsService.GetStudenByTokenAsync(token, abortionToken);
            return Ok(student.ToDto());
        }
        catch (StudentWithTokenNotFoundException)
        {
            return NotFound($"Student with token {token} not found.");
        }
    }

    private Guid? GetAcademyIdFromToken()
    {
        var academyIdClaim = User.Claims.FirstOrDefault(c => c.Type == "AcademyId")?.Value;
        return Guid.TryParse(academyIdClaim, out var academyId) ? academyId : null;
    }
}

this is the studentscontroller and it's working really well so far, and one thing I am concerned about, the logic of my app is something like this: (I am telling this with parent pov, in frontend) when a parent searches his child/ren using token of a student, if the student with this token exists he will see the data about the student and in frontend at the end of the info there will be a button named add child, the backend should fetch the student to the parent, so next time when parent log's in he can automatically see the child(thus when parent logs in for the first time he will have just empty array of student and in order to add his child he should search first and add it)
here are the information about entities:

namespace EduConnect.Api.Entities;

public class Parent : UserBase
{
    public ICollection<Student> Students { get; set; } = new List<Student>();
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduConnect.Api.Entities.Configurations;

public class ParentConfigurations : UserBaseConfigurations<Parent>
{
    public override void Configure(EntityTypeBuilder<Parent> builder)
    {
        base.Configure(builder);

        builder.HasMany(p => p.Students)
            .WithMany(s => s.Parents)
            .UsingEntity(j => j.ToTable("ParentStudents"));
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduConnect.Api.Entities.Configurations;

public class StudentConfigurations : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Name)
            .HasColumnType("varchar(50)")
            .IsRequired();
        builder.HasIndex(s => s.UniqueToken)
            .IsUnique();
        builder.Property(s => s.UniqueToken)
               .IsRequired()
               .HasMaxLength(6);

        builder.HasOne(s => s.Academy)
               .WithMany(a => a.Students)
               .HasForeignKey(s => s.AcademyId)
               .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(s => s.Classes)
            .WithMany(c => c.Students)
            .UsingEntity(j => j.ToTable("StudentClasses"));
    }
}

using EduConnect.Api.Utilities;

namespace EduConnect.Api.Entities;

public class Student
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? UniqueToken { get; set; }

    public Guid? AcademyId { get; set; }
    public Academy? Academy { get; set; }
    public ICollection<Class> Classes { get; set; } = new List<Class>();
    public ICollection<Parent> Parents { get; set; } = new List<Parent>();
}  
*/