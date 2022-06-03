using Catridges.DAL.Interfaces;
using Catridges.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Catridges.DAL.Repository;

public class StateRepository : IStateRepository
{
    private readonly ApplicationDbContext _db;

    public StateRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<bool> Create(State entity)
    {
        await _db.States.AddAsync(entity);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<State> Read(int id)
    {
        return await _db.States.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<State>> ReadAll()
    {
        return await _db.States.ToListAsync();
    }

    public async Task<bool> Update(int id, State entity)
    {
        var state = await _db.States.FirstOrDefaultAsync(x=>x.Id == id);
        if (state != null)
        {
            state.Name = entity.Name;
            _db.States.Update(state);
            await _db.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<bool> Delete(State entity)
    {
        _db.States.Remove(entity);
        await _db.SaveChangesAsync();
        return true;
    }
}