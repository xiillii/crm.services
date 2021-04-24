using System.Collections.Generic;

namespace Gui.Crm.Services.Shared.Dtos
{
    public class DtoCategory
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public List<LinkDto> Links { get; set; }
    }
}
