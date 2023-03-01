using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Commands
{
    public class CommandResponse
    {
        public bool Found { get; set; } = true;
        public bool Check { get; set; } = true; //repodan sıkıntı gelirse

        public int DbCheck { get; set; } = 1;
    }
}
