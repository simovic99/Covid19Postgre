using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication5
{
    public partial class Zupanije
    {
        public Zupanije()
        {
            Gradovis = new HashSet<Gradovi>();
        }

        public long Id { get; set; }
        public int? BrojStanovnika { get; set; }
        public string Naziv { get; set; }

        public virtual ICollection<Gradovi> Gradovis { get; set; }
    }
}
