using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication5
{
    public partial class Pacijenti
    {
        public Pacijenti()
        {
            Nalazis = new HashSet<Nalazi>();
        }

        public long Id { get; set; }
        public string Prezime { get; set; }
        public long? GradFk { get; set; }
        public DateTime? DatumRodjenja { get; set; }
        public string Ime { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public string FullName
        {
            get
            {
                return Prezime + " " + Ime;
            }
        }
        public virtual Gradovi GradFkNavigation { get; set; }
        public virtual ICollection<Nalazi> Nalazis { get; set; }
    }
}
