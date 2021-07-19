using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace clsConexao
{
    public class clsConexaoSQL
    {
        #region DECLARAÇÃO DE ATRIBUTOS

        private string stringConexao;
        private SqlCommand comando = null;

        private SqlConnection conexao;

        #endregion

        #region Métodos de Conexão

        #region Abertura de Conexão
        /// <summary>
        /// Abre a Conexão
        /// </summary>
        private void Abrir()
        {
            try
            {
                conexao = new SqlConnection();
                conexao.ConnectionString = stringConexao;
                conexao.Open();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region Fechamento de Conexão
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

        #endregion

        #region Métodos Funcionais

        #region Query

        /// <summary>
        /// Executa um comando SQL "Sem Retorno", ou seja um comando que não retorna valor (Ex. Insert, Update, Delete).
        /// </summary>
        /// <param name="ComandoSQL">Comando (Query) SQL a ser executado.</param>
        public void ComandoSR(string ComandoSQL)
        {
            try
            {
                Abrir();
                SqlCommand cSQL = new SqlCommand
                {
                    CommandType = CommandType.Text,
                    CommandText = ComandoSQL,
                    Connection = conexao
                };
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
        public SqlDataReader ComandoCR(string ComandoSQL)
        {
            try
            {
                Abrir();
                SqlCommand cSQL = new SqlCommand();
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

        #region Stored Procedure

        /// <summary>
        /// Início do procedimento para chamada de uma Stored Procedure.
        /// </summary>
        /// <param name="Nome">Nome da procedure a ser utilizada.</param>
        public void IniciarStoredProcedure(string Nome)
        {
            try
            {
                Abrir();
                SqlCommand cSQL = new SqlCommand();
                cSQL.CommandType = CommandType.StoredProcedure;
                cSQL.CommandText = Nome;
                comando = cSQL;
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

        #region Adiciona parâmetros

        #region Inteiros
        /// <summary>
        /// Adiciona um parametro do tipo inteiro à Stored Procedure anteriormente iniciada.
        /// </summary>
        /// <param name="Valor">Valor do parametro.</param>
        /// <param name="NomeParametro">Nome do Parametro. </param>
        public void AdicionarParametroInteiro(string NomeParametro, int Valor)
        {
            try
            {
                Abrir();
                SqlCommand cSQL = new SqlCommand();
                cSQL = comando;

                SqlParameter parametro = new SqlParameter();
                parametro.ParameterName = NomeParametro;
                parametro.SqlDbType = SqlDbType.Int;
                parametro.Direction = ParameterDirection.Input;
                parametro.Value = Valor;

                comando.Parameters.Add(parametro);

                comando = cSQL;
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

        #region Decimal
        /// <summary>
        /// Adiciona um parametro do tipo decimal à Stored Procedure anteriormente iniciada.
        /// </summary>
        /// <param name="Valor">Valor do parametro.</param>
        /// <param name="NomeParametro">Nome do Parâmetro</param>
        public void AdicionarParametroDecimal(string NomeParametro,double Valor)
        {
            try
            {
                Abrir();
                SqlCommand cSQL = new SqlCommand();
                cSQL = comando;

                SqlParameter parametro = new SqlParameter();
                parametro.ParameterName = NomeParametro;
                parametro.SqlDbType = SqlDbType.Float;
                parametro.Direction = ParameterDirection.Input;
                parametro.Value = Valor;

                comando.Parameters.Add(parametro);

                comando = cSQL;
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

        #region String
        /// <summary>
        /// Adiciona um parametro do tipo texto à Stored Procedure anteriormente iniciada.
        /// </summary>
        /// <param name="Valor">Valor do parametro.</param>
        /// <param name="NomeParametro">Nome do parâmetro</param>
        
        public void AdicionarParametroTexto(string NomeParametro,string Valor)
        {
            try
            {
                Abrir();
                SqlCommand cSQL = new SqlCommand();
                cSQL = comando;

                SqlParameter parametro = new SqlParameter();
                parametro.ParameterName = NomeParametro;
                parametro.SqlDbType = SqlDbType.NVarChar;
                parametro.Direction = ParameterDirection.Input;
                parametro.Value = Valor;

                comando.Parameters.Add(parametro);

                comando = cSQL;
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

        #region Bool
        /// <summary>
        /// Adiciona um parametro do tipo booleano à Stored Procedure anteriormente iniciada.
        /// </summary>
        /// <param name="Valor">Valor do parametro.</param>
        /// <param name="NomeParametro">Nome do parametro</param>
        public void AdicionarParametroBooleano(string NomeParametro,bool Valor)
        {
            try
            {
                Abrir();
                SqlCommand cSQL = new SqlCommand();
                cSQL = comando;

                SqlParameter parametro = new SqlParameter();
                parametro.ParameterName = NomeParametro;
                parametro.SqlDbType = SqlDbType.Bit;
                parametro.Direction = ParameterDirection.Input;
                parametro.Value = Valor;

                comando.Parameters.Add(parametro);

                comando = cSQL;
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

        #endregion

        #region Chamada Stored Procedures

        /// <summary>
        /// Executa a Stored Procedure "Sem Retorno" iniciada, com ou sem parametros adicionados.
        /// </summary>
        public void ChamarStoredProcedureSR()
        {
            try
            {
                Abrir();
                SqlCommand cSQL = new SqlCommand();
                cSQL = comando;
                cSQL.Connection = conexao;

                cSQL.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                comando = null;
                FecharConexao();
            }
        }

        /// <summary>
        /// Executa a Stored Procedure "Com Retorno" iniciada, com ou sem parametros adicionados, e disponibiliza o resultado em um DataReader.
        /// </summary>
        public SqlDataReader ChamarStoredProcedureCR()
        {
            try
            {
                Abrir();
                SqlCommand cSQL = new SqlCommand();
                cSQL = comando;
                cSQL.Connection = conexao;

                return cSQL.ExecuteReader();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                comando = null;
            }
        }

        #endregion

        #endregion

        #endregion

        #region Construtor

        /// <summary>
        /// Para instanciar a classe de conexão com o banco de dados, criando a linha de conexão SQL Server.
        /// </summary>
        /// <param name="DatabaseName">Base de dados utilizada.</param>
        /// <param name="Servidor">Endereço IP do Servidor.</param>
        /// <param name="Usuario">Identificação do usuário.</param>
        /// <param name="Senha">Senha do usuário.</param>
        public clsConexaoSQL(string DatabaseName, string Servidor, string Usuario, string Senha)
        {
            stringConexao = "Data Source = " + Servidor + ";Initial Catalog = " + DatabaseName + "; User ID = " + Usuario + "; Password = " + Senha;
        }

        #endregion
    }
}
