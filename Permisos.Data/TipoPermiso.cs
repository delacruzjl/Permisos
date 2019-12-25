using Permisos.Data.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace Permisos.Data {
    public class TipoPermiso  {
        [Key]public int Id { get; set; }
        [Required] public string Descripcion { get; set; }

    }
}
