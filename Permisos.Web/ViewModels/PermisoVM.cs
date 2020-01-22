using System;
using System.ComponentModel.DataAnnotations;

namespace Permisos.Web.ViewModels {
    public class PermisoVM
    {
        public int Id { get; set; }

        [Required]public string NombreEmpleado { get; set; }
        [Required] public string ApellidosEmpleado { get; set; }
        [Required] public int TipoPermisoId { get; set; }
        public TipoPermisoVM TipoPermiso { get; set; }
        [Required] public DateTime FechaPermiso { get; set; }
    }
}
