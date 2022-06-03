using Catridges.DAL.Interfaces;
using Catridges.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Catridges.DAL.Repository;

public class SubDivisionRepository : ISubdivisionRepository
{
    private readonly ApplicationDbContext _db;

    public SubDivisionRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<bool> Create(Subdivision entity)
    {
        await _db.Subdivisions.AddAsync(entity);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<Subdivision> Read(int id)
    {
        return await _db.Subdivisions.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Subdivision>> ReadAll()
    {
        return await _db.Subdivisions.ToListAsync();
    }

    public async Task<bool> Update(int id, Subdivision entity)
    {
        var subdivision = await _db.Subdivisions.FirstOrDefaultAsync(x=>x.Id == id);
        if (subdivision != null)
        {
            subdivision.Name = entity.Name;
            _db.Subdivisions.Update(subdivision);
            await _db.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<bool> Delete(Subdivision entity)
    {
        _db.Subdivisions.Remove(entity);
        await _db.SaveChangesAsync();
        return true;
    }
}