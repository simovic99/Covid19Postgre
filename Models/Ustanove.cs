using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication5
{
    public partial class Ustanove
    {
        public Ustanove()
        {
            Nalazis = new HashSet<Nalazi>();
        }

        public long Id { get; set; }
        public long? GradFk { get; set; }
        public string Naziv { get; set; }

        public virtual Gradovi GradFkNavigation { get; set; }
        public virtual ICollection<Nalazi> Nalazis { get; set; }
    }
}
