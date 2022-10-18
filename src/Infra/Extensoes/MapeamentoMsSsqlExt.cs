﻿using System;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using TemplateApi.Infra.Auxiliares;

namespace TemplateApi.Infra.Extensoes
{
    internal static class MapeamentoMsSsqlExt
    {
        public static string MsSqlJsonParaObjeto<T, P>(
            this BaseMapeamento<T> source,
            Expression<Func<T, P>> expression)
            where T : class
        {
            return $"JSON_QUERY({source.Col(expression)}) AS {source.Prop(expression)}";
        }

        public static string MsSqlCharParaEnum<T, P>(
            this BaseMapeamento<T> source,
            Expression<Func<T, P>> expression,
            Type type)
            where T : class
        {
            object[] opcoes = Enum.GetValues(type).Cast<object>().ToArray();
            StringBuilder sql = new StringBuilder();
            string campo = source.Col(expression);

            if (opcoes.Count() > 1)
            {
                sql.Append($" CASE ");
                for (int i = 1; opcoes.Count() > i; i++)
                {
                    sql.Append($" WHEN {campo} = '{i}' THEN '{opcoes.ElementAt(i)}' ");
                }
                sql.Append($" ELSE '{opcoes.ElementAt(0)}' ");
                sql.Append($" END AS {source.Prop(expression)} ");
            }
            else
            {
                sql.Append($" '{opcoes.ElementAt(0)}' AS {source.Prop(expression)} ");
            }

            return sql.ToString().Trim();
        }

        public static string MsSqlCharParaBoolean<T, P>(
            this BaseMapeamento<T> source,
            Expression<Func<T, P>> expression)
            where T : class
        {
            string campo = source.Col(expression);

            return $@" CASE
                WHEN {campo} = '1' THEN 'true'
                ELSE 'false'
            END AS {source.Prop(expression)}";
        }

        public static string MsSqlBitParaBoolean<T, P>(
            this BaseMapeamento<T> source,
            Expression<Func<T, P>> expression)
            where T : class
        {
            string campo = source.Col(expression);

            return $@" CASE
                WHEN {campo} = 1 THEN 'true'
                ELSE 'false'
            END AS {source.Prop(expression)}";
        }

        public static string MsSqlSNParaBoolean<T, P>(
            this BaseMapeamento<T> source,
            Expression<Func<T, P>> expression)
            where T : class
        {
            string campo = source.Col(expression);

            return $@" CASE
                WHEN {campo} IN ('1','S') THEN 'true'
                ELSE 'false'
            END AS {source.Prop(expression)}";
        }
    }
}
