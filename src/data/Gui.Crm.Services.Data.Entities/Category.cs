namespace Gui.Crm.Services.Data.Entities
{
    public class Category
    {
        public long CategoryId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; } = false;
    }
}
