﻿namespace DotNetCore.API.Template.Dominio.Interfaces
{
    public interface IArquivo
    {
        string Nome { get; set; }

        string Alias { get; set; }

        string Diretorio { get; set; }

        string Extensao { get; set; }

        string Tipo { get; set; }

        long Peso { get; set; }

        string Checksum { get; set; }

        string Referencia { get; set; }
    }
}
