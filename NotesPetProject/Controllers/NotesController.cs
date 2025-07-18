using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotesPetProject.Contracts;
using NotesPetProject.Data;
using NotesPetProject.Models;

namespace NotesPetProject.Controllers;

[ApiController]
[Route("[controller]")]
public class NotesController : ControllerBase
{
    private readonly NotesDbContext _context;
    public NotesController(NotesDbContext context)
    {
        _context = context;
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateNoteRequest request, CancellationToken cancellationToken)
    {

        var note = new Note(
            request.Id,
            request.Title,
            request.Description
            );
        await _context.Notes.AddAsync(note, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Ok("Note created successfully.");
    }
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetNotesRequest request, CancellationToken cancellationToken)
    {
        var notesQuery = _context.Notes
            .Where(x => !string.IsNullOrEmpty(request.Search) &&
            x.Title.ToLower().Contains(request.Search.ToLower()));
        Expression<Func<Note, object>> selectorKey =
           request.SortItem?.ToLower() switch
           {
               "date" =>
                  note => note.CreatedAt,
               "title" =>
                   note => note.Title,

               _ =>
                     note => note.Id
           };
        notesQuery = request.SortOrder == "desc"
            ? notesQuery.OrderByDescending(selectorKey) :
            notesQuery.OrderBy(x => x.CreatedAt);
        var notes = await notesQuery.Select(x =>
        new NoteDto
        (
            x.Id,
            x.Title,
            x.Desctiprion,
            x.CreatedAt

        )).ToListAsync(cancellationToken);

        return Ok(new GetNotesResponse(notes));
    }
}
