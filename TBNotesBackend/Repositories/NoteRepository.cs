using Dapper;
using DotNetEnv;
using Microsoft.Data.SqlClient;
using TBNotesBackend.Models;
using TBNotesBackend.Repositories;

public class NoteRepository : INoteRepository
{
    private readonly IConfiguration _configuration;


    public NoteRepository(IConfiguration configuration) 
    {
            _configuration = configuration;

    }

    // Create a new note
    async public Task Add(Note note)
    {
        using var connection = GetConnection();
        await connection
            .ExecuteAsync("INSERT INTO Notes (title, content) VALUES (@Title, @Content);", note);
    }

    // Delete a note
    async public Task Delete(int id)
    {
        using var connection = GetConnection();
        await connection
            .ExecuteAsync("DELETE FROM Notes WHERE Id = @id", new { Id = id });
    }

    // Get all notes
    async public Task<List<Note>> GetAll()
    {
        using var connection = GetConnection();
        var note = await connection
            .QueryAsync<Note>("SELECT * FROM Notes ORDER BY updated_at DESC");

        return note.ToList();
    }

    // Get note by id
    async public Task<Note> GetById(int id)
    {
        using var connection = GetConnection();
        var note = await connection
                    .QueryFirstOrDefaultAsync<Note>("SELECT * FROM Notes WHERE ID = @Id ", new { Id = id });
        return note;
    }

    // Update an existing note
    async public Task Update(Note note)
    {
        using var connection = GetConnection();
        await connection
            .ExecuteAsync("UPDATE Notes SET title = @Title, content = @Content, updated_at = GETDATE() WHERE id = @Id", note);
    }

    private SqlConnection GetConnection()
    {
        return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
    }
}
