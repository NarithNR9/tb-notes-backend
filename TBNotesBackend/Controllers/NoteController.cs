using Microsoft.AspNetCore.Mvc;
using TBNotesBackend.Models;
using TBNotesBackend.Repositories;

namespace TBNotesBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteRepository _noteRepository;

        public NoteController(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Add(Note note)
        {
            await _noteRepository.Add(note);
            return Created();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var notes = await _noteRepository.GetAll();
            return Ok(notes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Note>> GetById(int id)
        {
            var note = await _noteRepository.GetById(id);
            if (note == null)
            {
                return NotFound("Note not found");
            }
            return Ok(note);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Note note)
        {
            var existingNote = await _noteRepository.GetById(id);
            if (existingNote == null)
            {
                return NotFound("Note Not Found");
            }
            note.Id = id;
            await _noteRepository.Update(note);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existingNote = await _noteRepository.GetById(id);
            if (existingNote == null)
            {
                return NotFound("Note Not Found");
            }
            await _noteRepository.Delete(id);
            return NoContent();
        }
    }
}
