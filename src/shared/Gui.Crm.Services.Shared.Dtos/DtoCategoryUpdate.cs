using System.ComponentModel.DataAnnotations;

namespace Gui.Crm.Services.Shared.Dtos
{
    public class DtoCategoryUpdate
    {
        [Required] [StringLength(10)] public string Code { get; set; }
        [Required] [StringLength(200)] public string Name { get; set; }
        [Required] public bool Status { get; set; }
    }
}
