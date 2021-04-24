using System.Collections.Generic;

namespace Gui.Crm.Services.Shared.Dtos.Responses
{
    public class CategoriesResponse : BaseResponse
    {
        public List<DtoCategory> Data { get; set; }
    }
}
