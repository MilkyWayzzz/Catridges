using Catridges.Domain.Entity;

namespace Catridges.Domain.ViewModels;

public class DocumentCreateViewModel
{
    public List<Catridge>? Catridges { get; set; }
    
    public List<Printer>?  Printers { get; set; }
    
    public List<Subdivision>?  Subdivisions { get; set; }
    
    public List<State>?  States { get; set; }
}