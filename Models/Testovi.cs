using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication5
{
    public partial class Testovi
    {
        public Testovi()
        {
            Nalazis = new HashSet<Nalazi>();
        }

        public long Id { get; set; }
        public int? Cijena { get; set; }
        public string Naziv { get; set; }

        public virtual ICollection<Nalazi> Nalazis { get; set; }
    }
}
