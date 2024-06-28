using System;
using System.Collections.Generic;

namespace WebAutomovil.Models;

public partial class Carro
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public int Marca { get; set; }

    public bool Estado { get; set; }

    public virtual ICollection<ClienteCarro> ClienteCarros { get; set; } = new List<ClienteCarro>();

    public virtual Marca MarcaNavigation { get; set; } = null!;
}
