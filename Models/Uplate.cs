using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication5
{
    public partial class Uplate
    {
        public Uplate()
        {
            Nalazis = new HashSet<Nalazi>();
        }

        public long Id { get; set; }
        public long? Iznos { get; set; }
        public string Vrsta { get; set; }

        public virtual ICollection<Nalazi> Nalazis { get; set; }
    }
}
