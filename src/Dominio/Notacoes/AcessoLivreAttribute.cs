﻿using System;

namespace DotNetCore.API.Template.Dominio.Notacoes
{
    /// <summary>
    /// Não precisa estar autenticado, nem precisa enviar a chave pública
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public sealed class AcessoLivreAttribute : Attribute
    {

    }
}