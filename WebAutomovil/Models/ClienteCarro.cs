using System;
using System.Collections.Generic;

namespace WebAutomovil.Models;

public partial class ClienteCarro
{
    public int Id { get; set; }

    public int Cliente { get; set; }

    public int Carro { get; set; }

    public bool Estado { get; set; }

    public virtual Carro CarroNavigation { get; set; } = null!;

    public virtual Cliente ClienteNavigation { get; set; } = null!;
}
