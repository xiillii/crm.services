using System.Collections.Generic;

namespace Gui.Crm.Services.Shared.Dtos.Responses
{
    public class CategoriesResponse : BaseResponse
    {
        public ListLinks _links { get; set; }
        public List<DtoCategory> Value { get; set; }
        public int? Size { get; set; }
        public int? Start { get; set; }
    }
}
