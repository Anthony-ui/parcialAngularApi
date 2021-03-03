namespace Modelos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class eventos
    {
        [Key]
        public int idEvento { get; set; }

        public DateTime? fecha { get; set; }

        [StringLength(300)]
        public string evento { get; set; }

        [StringLength(300)]
        public string lugar { get; set; }

        public decimal? costo { get; set; }

        public int? idUsuario { get; set; }

        public virtual usuarios usuarios { get; set; }
    }
}
