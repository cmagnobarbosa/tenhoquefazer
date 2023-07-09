namespace TodoApi.Models;

public class TodoDatabaseSettings
{
    public string? TodosCollectionName { get; set; }
    public string? ConnectionString { get; set; }
    public string? DatabaseName { get; set; }
}