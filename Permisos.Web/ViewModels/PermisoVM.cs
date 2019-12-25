using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Permisos.Web.ViewModels
{
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
