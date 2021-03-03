namespace Modelos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class usuarios
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public usuarios()
        {
            eventos = new HashSet<eventos>();
        }

        [Key]
        public int idUsuario { get; set; }

        [StringLength(250)]
        public string nombre { get; set; }

        [StringLength(250)]
        public string apellido { get; set; }

        [StringLength(300)]
        public string correo { get; set; }

        [StringLength(500)]
        public string direccion { get; set; }

        [StringLength(250)]
        public string usuario { get; set; }

        [StringLength(200)]
        public string clave { get; set; }

        public DateTime? fecha { get; set; }

        public string imagen { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<eventos> eventos { get; set; }
    }
}
