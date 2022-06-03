namespace Catridges.Domain.Entity;

public class Document
{
    public int Id { get; set; }
    
    public int Number { get; set; }
    
    public DateTime DateTime { get; set; }
    
    public Catridge? Catridge { get; set; }
    
    public State? State { get; set; }
    
    public Printer? Printer { get; set; }
    
    public Subdivision? Subdivision { get; set; }
}