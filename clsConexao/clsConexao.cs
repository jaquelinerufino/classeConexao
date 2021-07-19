using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Odbc;

namespace clsConexao
{
    public class clsConexao
    {
        #region DECLARAÇÃO DE ATRIBUTOS

        private string _strCon;
        private OdbcCommand _cSQL = null;

        private OdbcConnection conexao;

        #endregion

        #region MÉTODOS DE CONEXÃO

        private void Abrir()
        {
            try
            {
                conexao = new OdbcConnection();
                conexao.ConnectionString = _strCon;
                conexao.Open();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Fecha e remove a conexão da memória. O uso deste método só é necessário após o uso dos métodos que retornam um DataReader.
        /// </summary>
        public void FecharConexao()
        {
            if (conexao.State == ConnectionState.Open)
            {
                conexao.Close();
                conexao.Dispose();
            }
        }

        #endregion

        #region MÉTODOS FUNCIONAIS

        #region QUERY

        /// <summary>
        /// Executa um comando SQL "Sem Retorno", ou seja um comando que não retorna valor (Ex. Insert, Update, Delete).
        /// </summary>
        /// <param name="ComandoSQL">Comando (Query) SQL a ser executado.</param>
        public void ComandoSR(string ComandoSQL)
        {
            try
            {
                Abrir();
                OdbcCommand cSQL = new OdbcCommand();
                cSQL.CommandType = CommandType.Text;
                cSQL.CommandText = ComandoSQL;
                cSQL.Connection = conexao;
                cSQL.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                FecharConexao();
            }
        }

        /// <summary>
        /// Executa um comando SQL "Com Retorno", ou seja um comando que retorna valor (Ex. Select) e o disponibiliza em um DataReader.
        /// </summary>
        /// <param name="ComandoSQL">Comando (Query) SQL a ser executado.</param>
        public OdbcDataReader ComandoCR(string ComandoSQL)
        {
            try
            {
                Abrir();
                OdbcCommand cSQL = new OdbcCommand();
                cSQL.CommandType = CommandType.Text;
                cSQL.CommandText = ComandoSQL;
                cSQL.Connection = conexao;
                return cSQL.ExecuteReader();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region STORED PROCEDURE

        #region INICIAÇÃO

        /// <summary>
        /// Início do procedimento para chamada de uma Stored Procedure.
        /// </summary>
        /// <param name="Nome">Nome da procedure a ser utilizada.</param>
        public void IniciarStoredProcedure(string Nome)
        {
            try
            {
                Abrir();
                OdbcCommand cSQL = new OdbcCommand();
                cSQL.CommandType = CommandType.StoredProcedure;
                cSQL.CommandText = string.Concat("call ", Nome);
                _cSQL = cSQL;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                FecharConexao();
            }
        }

        #endregion

        #region ADICIONAR PARAMETROS

        /// <summary>
        /// Adiciona um parametro do tipo inteiro à Stored Procedure anteriormente iniciada.
        /// </summary>
        /// <param name="Valor">Valor do parametro.</param>
        public void AdicionarParametroInteiro(int Valor)
        {
            try
            {
                Abrir();
                OdbcCommand cSQL = new OdbcCommand();
                cSQL = _cSQL;

                if (cSQL.CommandText.EndsWith(")"))
                    cSQL.CommandText = cSQL.CommandText.Replace(")", ",?)");
                else
                    cSQL.CommandText = string.Concat(cSQL.CommandText, "(?)");

                cSQL.Parameters.Add(new OdbcParameter("?", Valor));

                _cSQL = cSQL;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                FecharConexao();
            }
        }

        /// <summary>
        /// Adiciona um parametro do tipo decimal à Stored Procedure anteriormente iniciada.
        /// </summary>
        /// <param name="Valor">Valor do parametro.</param>
        public void AdicionarParametroDecimal(double Valor)
        {
            try
            {
                Abrir();
                OdbcCommand cSQL = new OdbcCommand();
                cSQL = _cSQL;

                if (cSQL.CommandText.EndsWith(")"))
                    cSQL.CommandText = cSQL.CommandText.Replace(")", ",?)");
                else
                    cSQL.CommandText = string.Concat(cSQL.CommandText, "(?)");

                cSQL.Parameters.Add(new OdbcParameter("?", Valor));

                _cSQL = cSQL;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                FecharConexao();
            }
        }

        /// <summary>
        /// Adiciona um parametro do tipo texto à Stored Procedure anteriormente iniciada.
        /// </summary>
        /// <param name="Valor">Valor do parametro.</param>
        public void AdicionarParametroTexto(string Valor)
        {
            try
            {
                Abrir();
                OdbcCommand cSQL = new OdbcCommand();
                cSQL = _cSQL;

                if (cSQL.CommandText.EndsWith(")"))
                    cSQL.CommandText = cSQL.CommandText.Replace(")", ",?)");
                else
                    cSQL.CommandText = string.Concat(cSQL.CommandText, "(?)");

                cSQL.Parameters.Add(new OdbcParameter("?", Valor));

                _cSQL = cSQL;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                FecharConexao();
            }
        }

        /// <summary>
        /// Adiciona um parametro do tipo booleano à Stored Procedure anteriormente iniciada.
        /// </summary>
        /// <param name="Valor">Valor do parametro.</param>
        public void AdicionarParametroBooleano(bool Valor)
        {
            try
            {
                Abrir();
                OdbcCommand cSQL = new OdbcCommand();
                cSQL = _cSQL;

                if (cSQL.CommandText.EndsWith(")"))
                    cSQL.CommandText = cSQL.CommandText.Replace(")", ",?)");
                else
                    cSQL.CommandText = string.Concat(cSQL.CommandText, "(?)");

                cSQL.Parameters.Add(new OdbcParameter("?", Valor));

                _cSQL = cSQL;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                FecharConexao();
            }
        }

        #endregion

        #region CHAMADA SPs

        /// <summary>
        /// Executa a Stored Procedure "Sem Retorno" iniciada, com ou sem parametros adicionados.
        /// </summary>
        public void ChamarStoredProcedureSR()
        {
            try
            {
                Abrir();
                OdbcCommand cSQL = new OdbcCommand();
                cSQL = _cSQL;
                cSQL.Connection = conexao;

                cSQL.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                _cSQL = null;
                FecharConexao();
            }
        }

        /// <summary>
        /// Executa a Stored Procedure "Com Retorno" iniciada, com ou sem parametros adicionados, e disponibiliza o resultado em um DataReader.
        /// </summary>
        public OdbcDataReader ChamarStoredProcedureCR()
        {
            try
            {
                Abrir();
                OdbcCommand cSQL = new OdbcCommand();
                cSQL = _cSQL;
                cSQL.Connection = conexao;

                return cSQL.ExecuteReader();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                _cSQL = null;
            }
        }

        #endregion

        #endregion

        #endregion

        #region CONSTRUTOR

        /// <summary>
        /// Para instanciar a classe de conexão com o banco de dados, criando a linha de conexão MySQL.
        /// </summary>
        /// <param name="BD_Fonte">Base de dados utilizada.</param>
        /// <param name="Servidor">Endereço IP do Servidor.</param>
        /// <param name="ID_Usuario">Identificação do usuário.</param>
        /// <param name="Senha">Senha do usuário.</param>
        public clsConexao(string BD_Fonte, string Servidor, string ID_Usuario, string Senha)
        {
            _strCon = "DRIVER={MySQL ODBC 3.51 Driver};DATABASE=" + BD_Fonte + ";SERVER=" + Servidor + ";UID=" + ID_Usuario + ";PWD=" + Senha;
            //_strCon = string.Concat("DRIVER={MySQL ODBC 3.51 Driver};DATABASE=", _baseDeDados, ";SERVER=", _servidor, ";UID=", _IDUsuario, ";PWD=", _senha);
            //_strCon = String.Format("DRIVER={MySQL ODBC 3.51 Driver};DATABASE={0};SERVER={1};UID={2};PWD={3}", _baseDeDados, _servidor, _IDUsuario, _senha);
        }

        #endregion
    }
}
