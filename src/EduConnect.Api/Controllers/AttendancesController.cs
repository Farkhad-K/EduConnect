using EduConnect.Api.Abstractions.ServicesAbstractions;
using EduConnect.Api.Dtos.AttendanceDtos;
using EduConnect.Api.Mappers.AttendanceMappers;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;


namespace EduConnect.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AttendancesController(IAttendancesService attendanceService) : ControllerBase
{
    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Attendance))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> AddAttendance([FromBody] CreateAttendance attendance, CancellationToken abortionToken = default)
    {
        var newAttandence = await attendanceService.AddAttendanceAsync(attendance.ToEntity(), abortionToken);
        return Ok(newAttandence.ToDto());
    }

    [HttpGet("{classId}/{date}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Attendance>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async ValueTask<IActionResult> GetAllAttendancesOfClass(
        Guid classId, DateOnly date, CancellationToken abortionToken = default)
    {
        var attendances = await attendanceService.GetAttendancesByClassAndDateAsync(classId, date, abortionToken);
        return Ok(attendances.Select(a => a.ToDto()));
    }


    [HttpGet("{studentId}/{classId}/{date}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Attendance))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async ValueTask<IActionResult> GetAttendanceByStudentClassDate(Guid studentId, Guid classId, DateOnly date, CancellationToken abortionToken = default)
    {
        var attendance = await attendanceService.GetAttendanceByStudentClassDateAsync(studentId, classId, date, abortionToken);
        return Ok(attendance?.ToDto());
    }

    [HttpGet("all/{studentId}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Attendance>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async ValueTask<IActionResult> GetAllAttendancesOfAStudent(Guid studentId, CancellationToken abortionToken = default)
    {
        var attendances = await attendanceService.GetAllAttendancesOfAStudentAsync(studentId, abortionToken);
        return Ok(attendances.Select(a => a.ToDto()));
    }

    /* Adding several attendances at once
    [HttpPost("bulk")]
    public async ValueTask<IActionResult> AddBulkAttendance([FromBody] List<CreateAttendance> attendances, CancellationToken ct)
    {
        var result = new List<Attendance>();
        foreach (var attendanceDto in attendances)
        {
            var entity = attendanceDto.ToEntity();
            var added = await service.AddAttendanceAsync(entity, ct);
            result.Add(added);
        }

        return Ok(result);
    }
    */


}