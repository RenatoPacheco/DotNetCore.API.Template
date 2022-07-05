﻿using System;
using Dapper;
using DotNetCore.API.Template.Dominio.Entidades;
using DotNetCore.API.Template.Dominio.Interfaces;
using DotNetCore.API.Template.Repositorio.Contexto;
using DotNetCore.API.Template.Repositorio.Adaptadores;
using DotNetCore.API.Template.Repositorio.FormatoJson;

namespace DotNetCore.API.Template.Repositorio.Persistencias.UsuarioPers
{
    internal class EditarUsuarioPers : Comum.SimplesRep
    {
        public EditarUsuarioPers(
            Conexao conexao,
            IUnidadeTrabalho udt,
            EhUnicoUsuarioPers persEhUnicoUsuario)
            : base(conexao, udt)
        {
            _persEhUnicoUsuario = persEhUnicoUsuario;
        }

        private readonly EhUnicoUsuarioPers _persEhUnicoUsuario;

        public void Editar(Usuario dados)
        {
            Notifications.Clear();
            UsuarioJson json = new UsuarioJson();

            Validate(dados);

            _persEhUnicoUsuario.EhUnico(dados);
            Validate(_persEhUnicoUsuario);

            if (IsValid())
            {
                dados.AlteradoEm = DateTime.Now;

                string sqlString = @$"
                    UPDATE [dbo].[{json.Tabela}] SET
                            [{json.Coluna(x => x.Nome)}] = @Nome
                           ,[{json.Coluna(x => x.Email)}] = @Email
                           ,[{json.Coluna(x => x.AlteradoEm)}] = @AlteradoEm
                           ,[{json.Coluna(x => x.Status)}] = @Status
                    WHERE [{json.Coluna(x => x.Id)}] = @Id
                ";

                object sqlObject = new
                {
                    Id = dados.Id,
                    Nome = new DbString { Value = dados.Nome, IsAnsi = true },
                    Email = new DbString { Value = dados.Email, IsAnsi = true },
                    Status = new DbString { Value = StatusAdapt.EnumParaSql(dados.Status), IsAnsi = true },
                    AlteradoEm = dados.AlteradoEm
                };

                Conexao.Sessao.Execute(
                    sqlString, sqlObject, Conexao.Transicao);
            }
        }
    }
}
