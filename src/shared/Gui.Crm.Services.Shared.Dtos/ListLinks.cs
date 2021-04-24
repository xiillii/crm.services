using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gui.Crm.Services.Shared.Dtos
{
    public class ListLinks
    {
        public string Base { get; set; }
        public string Context { get; set; }
        public string Next { get; set; }
        public string Prev { get; set; }
        public string Self { get; set; }
    }
}
