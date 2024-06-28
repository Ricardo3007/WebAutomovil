using System;
using System.Collections.Generic;

namespace WebAutomovil.Models;

public partial class Cliente
{
    public int Id { get; set; }

    public string Documento { get; set; } = null!;

    public string Nombres { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public bool Estado { get; set; }

    public virtual ICollection<ClienteCarro> ClienteCarros { get; set; } = new List<ClienteCarro>();
}
