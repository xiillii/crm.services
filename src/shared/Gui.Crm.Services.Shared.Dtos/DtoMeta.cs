using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gui.Crm.Services.Shared.Dtos
{
    public class DtoMeta
    {
        public Guid ResponseId { get; set; }
        public Status Status { get; set; }
        public DateTimeOffset Date { get; set; }
        public List<DtoError> Errors { get; set; }
    }
}
