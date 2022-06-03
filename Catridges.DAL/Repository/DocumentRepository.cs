using Catridges.DAL.Interfaces;
using Catridges.Domain.Entity;
using Catridges.Domain.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Catridges.DAL.Repository;

public class DocumentRepository : IDocumentRepository
{
    private readonly ApplicationDbContext _db;

    public DocumentRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<bool> Create(Document entity)
    {
       await _db.Documents.AddAsync(entity);
       await _db.SaveChangesAsync();
       return true;
    }

    public async Task<Document> Read(int id)
    {
        return await _db.Documents.Include(x => x.Catridge)
            .Include(x => x.Printer)
            .Include(x => x.State)
            .Include(x => x.Subdivision)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Document>> ReadAll()
    {

        return await _db.Documents.Include(x => x.Catridge)
            .Include(x => x.Printer)
            .Include(x => x.State)
            .Include(x => x.Subdivision).ToListAsync();
    }

    public async Task<bool> Update(int id, Document entity)
    {
        var document =  await _db.Documents.FirstOrDefaultAsync(x => x.Id == id);
        if (document != null)
        {
            document.DateTime = entity.DateTime;
            document.Catridge = entity.Catridge;
            document.Printer = entity.Printer;
            document.Subdivision = entity.Subdivision;
            document.State = entity.State;
            document.Number = entity.Number;
            _db.Update(document);
            await _db.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<bool> Delete(Document entity)
    {
        _db.Documents.Remove(entity);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<DocumentCreateViewModel> GetDocumentCreateViewModel()
    {
        var catridges = await _db.Catridge.ToListAsync();
        var printers = await _db.Printers.ToListAsync();
        var states = await _db.States.ToListAsync();
        var subdivisions = await _db.Subdivisions.ToListAsync();
        var documentCreateViewModel = new DocumentCreateViewModel()
        {
            Catridges = catridges,
            Printers = printers,
            States = states,
            Subdivisions = subdivisions
        };
        return documentCreateViewModel;
    }
}