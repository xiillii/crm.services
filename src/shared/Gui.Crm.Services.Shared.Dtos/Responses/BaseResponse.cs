using System;
using System.Collections.Generic;

namespace Gui.Crm.Services.Shared.Dtos.Responses
{
    public abstract class BaseResponse
    {
        public Guid ResponseId { get; set; }
        public Status Status { get; set; }
        public DateTimeOffset Date { get; set; }
        public List<Error> Errors { get; set; }
    }
}
