﻿using TemplateApi.Compartilhado.Json;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Compartilhado.Seguranca;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TemplateApi.Dominio.Entidades
{
    public class Autenticacao
    {
        public static Autenticacao DecodificarToken(string token)
        {
            Autenticacao resultado = null;

            if (!string.IsNullOrWhiteSpace(token))
            {
                string decodificado = Codificacao.Decriptar(token);
                if (!string.IsNullOrWhiteSpace(decodificado))
                {
                    resultado = ContratoJson.Desserializar<Autenticacao>(decodificado);
                    resultado.Token = token;
                }
            }

            return resultado;
        }

        public static Autenticacao GerarInterno(bool haChavePublica)
        {
            Autenticacao resultado = new Autenticacao
            {
                Id = "NULL",
                Nome = "Usuário interno",
                EhInterno = true,
                EstaAutenticado = true,
                CriadoEm = null,
                ExpiraEm = null,
                HaChavePublica = haChavePublica
                
            };

            resultado.AtualizarToken();

            return resultado;
        }

        public static Autenticacao GerarNaoAutenticado(bool haChavePublica)
        {
            Autenticacao resultado = new Autenticacao
            {
                Id = "NULL",
                Nome = "Usuário não autenticado",
                EhInterno = false,
                EstaAutenticado = false,
                CriadoEm = null,
                ExpiraEm = null,
                HaChavePublica = haChavePublica
            };

            resultado.AtualizarToken();

            return resultado;
        }

        [JsonConstructor]
        protected Autenticacao() { }

        public Autenticacao(string id, string nome, string email)
            : this()
        {
            Inicializar();

            Id = id;
            Nome = nome;
            Email = email;
            EstaAutenticado = true;
            AtualizarToken();
        }

        public string Id { get; set; }

        public string Nome { get; set; }

        [Display(Name = "E-mail")]
        public string Email { get; set; }

        public string Token { get; set; }

        [Display(Name = "É interno")]
        public bool EhInterno { get; set; }

        [Display(Name = "Está autenticado")]
        public bool EstaAutenticado { get; set; }

        [Display(Name = "Há chave pública")]
        public bool HaChavePublica { get; set; }

        [Display(Name = "Criado em")]
        public DateTime? CriadoEm { get; set; }

        [Display(Name = "Expira em")]
        public DateTime? ExpiraEm { get; set; }

        private Autorizacao[] _autorizacoes = Array.Empty<Autorizacao>();

        [Display(Name = "Autorizações")]
        public Autorizacao[] Autorizacoes
        {
            get { return _autorizacoes; }
            set { _autorizacoes = value ?? Array.Empty<Autorizacao>(); }
        }

        private void Inicializar()
        {
            CriadoEm = DateTime.Now;
            ExpiraEm = CriadoEm.Value.AddDays(1);
        }

        private void AtualizarToken()
        {
            Autorizacao[] autorizacoes = Autorizacoes;
            Autorizacoes = Array.Empty<Autorizacao>();
            Token = null;

            if (!EhInterno && EstaAutenticado)
            {
                Token = ContratoJson.Serializar(this);
                Token = Codificacao.Encriptar(Token);
            }

            Autorizacoes = autorizacoes;
        }
    }
}