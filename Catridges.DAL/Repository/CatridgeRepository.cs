using Catridges.DAL.Interfaces;
using Catridges.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Catridges.DAL.Repository;

public class CatridgeRepository : ICatridgeRepository
{
    private readonly ApplicationDbContext _db;

    public CatridgeRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<bool> Create(Catridge entity)
    {
        await _db.Catridge.AddAsync(entity);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<Catridge> Read(int id)
    {
        return await _db.Catridge.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Catridge>> ReadAll()
    {
        return await _db.Catridge.ToListAsync();
    }

    public async Task<bool> Update(int id, Catridge entity)
    {
       var catridge =  await _db.Catridge.FirstOrDefaultAsync(x => x.Id == id);
       if (catridge != null)
       {
           catridge.Model = entity.Model;
           _db.Update(catridge);
           await _db.SaveChangesAsync();
           return true;
       }
       return false;
    }


    public async Task<bool> Delete(Catridge entity)
    {
        _db.Catridge.Remove(entity);
        await _db.SaveChangesAsync();
        return true;
    }
}