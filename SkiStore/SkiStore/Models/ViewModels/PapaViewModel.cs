using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkiStore.Models.ViewModels
{
    public abstract class PapaViewModel
    {
        public string AlertMessage { get; set; }
        public string AlertSummary { get; set; }
        public string ErrorMessage { get; set; }
    }
}
