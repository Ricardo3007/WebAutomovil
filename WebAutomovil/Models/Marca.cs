using System;
using System.Collections.Generic;

namespace WebAutomovil.Models;

public partial class Marca
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public bool Estado { get; set; }

    public virtual ICollection<Carro> Carros { get; set; } = new List<Carro>();
}
