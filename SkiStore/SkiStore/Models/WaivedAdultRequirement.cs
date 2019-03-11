using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkiStore.Models
{
    public class WaivedAdultRequirement : IAuthorizationRequirement
    {
        public int MinAge { get; set; }
        public bool RequiresWaiver { get; set; }

        public WaivedAdultRequirement(int minAge, bool requiresWaiver)
        {
            MinAge = minAge;
            RequiresWaiver = requiresWaiver;
        }

    }
}
