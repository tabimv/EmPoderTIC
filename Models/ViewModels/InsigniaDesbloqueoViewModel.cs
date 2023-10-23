using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmPoderTIC.Models.ViewModels
{
    public class InsigniaDesbloqueoViewModel
    {
        public INSIGNIA Insignia { get; set; }
        public CONTROL_INSIGNIA DesbloqueoInsignia { get; set; }
        public bool insigniaBloqueada { get; set; }
    }
}