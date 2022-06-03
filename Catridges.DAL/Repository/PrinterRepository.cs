using Catridges.DAL.Interfaces;
using Catridges.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Catridges.DAL.Repository;

public class PrinterRepository : IPrinterRepository
{
    private readonly ApplicationDbContext _db;

    public PrinterRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<bool> Create(Printer entity)
    {
        await _db.Printers.AddAsync(entity);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<Printer> Read(int id)
    {
        return await _db.Printers.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Printer>> ReadAll()
    {
        return await _db.Printers.ToListAsync();
    }

    public async Task<bool> Update(int id, Printer entity)
    {
        var printer =  await _db.Printers.FirstOrDefaultAsync(x => x.Id == id);
        if (printer != null)
        {
            printer.Model = entity.Model;
            _db.Update(printer);
            await _db.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<bool> Delete(Printer entity)
    {
        _db.Printers.Remove(entity);
        await _db.SaveChangesAsync();
        return true;
    }
}