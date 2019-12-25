using Permisos.Data.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Permisos.Data {
    public class Permiso  {
        [Key]public int Id { get; set; }
        [Required]
        public string NombreEmpleado { get; set; }
        [Required] public string ApellidosEmpleado { get; set; }
        [Required] public TipoPermiso TipoPermiso { get; set; }
        [Required] public DateTime FechaPermiso { get; set; }
        [Required, ForeignKey("TipoPermiso")]
        public int TipoPermisoId { get; set; }
    }
}
