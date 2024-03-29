﻿using System;
using System.Collections.Generic;

namespace TransactIt.Domain.Models
{
    public class MainAccountGroup
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual IEnumerable<SubAccountGroup> SubAccountGroups { get; set; }
    }
}
