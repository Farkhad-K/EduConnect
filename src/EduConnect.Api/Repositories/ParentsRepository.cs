using EduConnect.Api.Abstractions.RepositoriesAbstractions;
using EduConnect.Api.Data;
using EduConnect.Api.Dtos.StudentDtos;
using EduConnect.Api.Entities;
using EduConnect.Api.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Api.Repositories;

public class ParentsRepository(
    IEduConnectDbContext context) : IParentsRepository
{
    public async ValueTask<bool> AttachToParent(Guid parentId, Guid studentId, CancellationToken cancellationToken = default)
    {
        var parent = await context.Parents
            .FirstOrDefaultAsync(p => p.Id == parentId, cancellationToken)
            ?? throw new ParentNotFoundException(parentId);
        var student = await context
            .Students
            .FirstOrDefaultAsync(s => s.Id == studentId, cancellationToken)
            ?? throw new StudentNotFoundException(studentId);

        parent.Students.Add(student);

        return await context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async ValueTask<Parent> GetChildren(Guid parentId, CancellationToken cancellationToken = default)
        => await context.Parents.Include(s => s.Students)
            .FirstOrDefaultAsync(p => p.Id == parentId)
            ?? throw new ParentNotFoundException(parentId);

}


/*
public async ValueTask<Student?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.Students.Include(c => c.Classes)
        .ThenInclude(c => c.Teacher).FirstOrDefaultAsync(s => s.Id == id, cancellationToken)
        ?? throw new StudentNotFoundException(id);
*/