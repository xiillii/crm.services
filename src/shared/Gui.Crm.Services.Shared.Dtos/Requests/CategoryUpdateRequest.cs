using System.ComponentModel.DataAnnotations;

namespace Gui.Crm.Services.Shared.Dtos.Requests
{
    public class CategoryUpdateRequest
    {
        [Required]
        public DtoCategoryUpdate Data { get; set; }
    }
}
