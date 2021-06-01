using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication5
{
    public partial class Nalazi
    {
        public long Id { get; set; }
        public long? TestFk { get; set; }
        public DateTime? Datum { get; set; }
        public long? UstanovaFk { get; set; }
        public long? PacijentFk { get; set; }
        public long? UplataFk { get; set; }
        public string Rezultat { get; set; }

        public virtual Pacijenti PacijentFkNavigation { get; set; }
        public virtual Testovi TestFkNavigation { get; set; }
        public virtual Uplate UplataFkNavigation { get; set; }
        public virtual Ustanove UstanovaFkNavigation { get; set; }
    }
}
