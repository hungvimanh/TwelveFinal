﻿using System;
using System.Collections.Generic;

namespace TwelveFinal.Repositories.Models
{
    public partial class TownDAO
    {
        public TownDAO()
        {
            PersonalInformations = new HashSet<PersonalInformationDAO>();
        }

        public Guid Id { get; set; }
        public long CX { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Guid DistrictId { get; set; }

        public virtual DistrictDAO District { get; set; }
        public virtual ICollection<PersonalInformationDAO> PersonalInformations { get; set; }
    }
}
