using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication5
{
    public partial class Gradovi
    {
        public Gradovi()
        {
            Pacijentis = new HashSet<Pacijenti>();
            Ustanoves = new HashSet<Ustanove>();
        }

        public long Id { get; set; }
        public long? FkZupanija { get; set; }
        public int? BrojStanovnika { get; set; }
        public string Naziv { get; set; }

        public virtual Zupanije FkZupanijaNavigation { get; set; }
        public virtual ICollection<Pacijenti> Pacijentis { get; set; }
        public virtual ICollection<Ustanove> Ustanoves { get; set; }
    }
}
