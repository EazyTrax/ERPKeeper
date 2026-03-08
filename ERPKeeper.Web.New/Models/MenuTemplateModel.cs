namespace ERPKeeper.Web.New.Models;

public class MenuTemplateModel
{
    public int Level { get; set; }
    public string DisplayName { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public int? Count { get; set; }
    public string? Icon { get; set; }
}
