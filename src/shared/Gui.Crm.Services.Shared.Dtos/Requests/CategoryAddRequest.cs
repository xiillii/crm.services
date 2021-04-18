using System.ComponentModel.DataAnnotations;

namespace Gui.Crm.Services.Shared.Dtos.Requests
{
    public class CategoryAddRequest
    {
        [Required]
        public DtoCategoryAdd Data { get; set; }
    }
}
