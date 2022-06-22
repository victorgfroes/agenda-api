using AgendaAPI.DTOs.Agenda;
using AgendaAPI.DTOs.Conexao;
using AgendaAPI.Models;
using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AgendaAPI.Queries
{
    public class QueriesService : IQueriesService
    {
        private readonly string _connectionString;

        public QueriesService(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException("Connection string não encontrada", nameof(connectionString));
            }
            _connectionString = connectionString;
        }

        #region Queries Agendas
        public async Task<Agenda> InsertAgenda(CreateAgendaDTO agendaDTO, int idUsuario, int idGoogle, string email, string nome, string foto)
        {
            string _query = @"INSERT INTO AGENDAS (ID_USUARIO_FK, ID_GOOGLE_FK, EMAIL_FK, NOME_FK, FOTO_FK, TITULO, DESCRICAO, DT_FIM) VALUES (@ID_USUARIO, @ID_GOOGLE, @EMAIL, @NOME, @FOTO, @TITULO, @DESCRICAO, @DT_FIM);
                             
                            SELECT ID_AGENDA, ID_USUARIO_FK, ID_GOOGLE_FK, EMAIL_FK, NOME_FK, FOTO_FK, TITULO, DESCRICAO, DT_FIM FROM AGENDAS WHERE ID_AGENDA = LAST_INSERT_ID();";

            using (var con = new MySqlConnection(_connectionString))
            {
                con.Open();
                return await con.QueryFirstOrDefaultAsync<Agenda>(_query, new { ID_USUARIO = idUsuario, ID_GOOGLE = idGoogle, EMAIL = email, NOME = nome, FOTO = foto, TITULO = agendaDTO.Titulo, DESCRICAO = agendaDTO.Descricao, DT_FIM = agendaDTO.Dt_Fim });
            }
        }

        public async Task<IEnumerable<Agenda>> GetAgendasByIdGoogle(int idGoogle)
        {
            string _query = @"SELECT ID_AGENDA, ID_USUARIO_FK, ID_GOOGLE_FK, EMAIL_FK, NOME_FK, FOTO_FK, TITULO, DESCRICAO, DT_FIM FROM AGENDAS WHERE ID_GOOGLE_FK = @ID_GOOGLE_FK;";

            using (var con = new MySqlConnection(_connectionString))
            {
                con.Open();
                return await con.QueryAsync<Agenda>(_query, new { ID_GOOGLE_FK = idGoogle });
            }
        }

        public async Task<int> UpdateAgendaByIdAgenda(UpdateAgendaDTO agendaDTO, int idAgenda)
        {
            string _query = @"UPDATE AGENDAS SET TITULO = @TITULO, DESCRICAO = @DESCRICAO, DT_FIM = @DT_FIM WHERE ID_AGENDA = @ID_AGENDA;";

            using (var con = new MySqlConnection(_connectionString))
            {
                con.Open();
                return await con.ExecuteAsync(_query, new { TITULO = agendaDTO.Titulo, DESCRICAO = agendaDTO.Descricao, DT_FIM = agendaDTO.Dt_Fim, ID_AGENDA = idAgenda });
            }
        }

        public async Task<int> UpdateTituloByIdAgenda(UpdateTituloAgendaDTO tituloAgendaDTO, int idAgenda)
        {
            string _query = @"UPDATE AGENDAS SET TITULO = @TITULO WHERE ID_AGENDA = @ID_AGENDA;";

            using (var con = new MySqlConnection(_connectionString))
            {
                con.Open();
                return await con.ExecuteAsync(_query, new { TITULO = tituloAgendaDTO.Titulo, ID_AGENDA = idAgenda });
            }
        }

        public async Task<int> UpdateDescricaoByIdAgenda(UpdateDescricaoAgendaDTO descricaoAgendaDTO, int idAgenda)
        {
            string _query = @"UPDATE AGENDAS SET DESCRICAO = @DESCRICAO WHERE ID_AGENDA = @ID_AGENDA;";

            using (var con = new MySqlConnection(_connectionString))
            {
                con.Open();
                return await con.ExecuteAsync(_query, new { DESCRICAO = descricaoAgendaDTO.Descricao, ID_AGENDA = idAgenda });
            }
        }

        public async Task<int> UpdateDtFimByIdAgenda(UpdateDtFimAgendaDTO dtFimAgendaDTO, int idAgenda)
        {
            string _query = @"UPDATE AGENDAS SET DT_FIM = @DT_FIM WHERE ID_AGENDA = @ID_AGENDA;";

            using (var con = new MySqlConnection(_connectionString))
            {
                con.Open();
                return await con.ExecuteAsync(_query, new { DT_FIM = dtFimAgendaDTO.Dt_Fim, ID_AGENDA = idAgenda });
            }
        }

        public async Task<int> DeleteAgendaByIdAgenda(int idAgenda)
        {
            string _query = @"DELETE FROM AGENDAS WHERE ID_AGENDA = @ID_AGENDA";

            using (var con = new MySqlConnection(_connectionString))
            {
                con.Open();
                return await con.ExecuteAsync(_query, new { ID_AGENDA = idAgenda });
            }
        }
        #endregion

        #region Queries Conexões
        public async Task<Conexao> RequestConexao(CreateConexaoDTO conexaoDTO, int idGoogleSolicitante, string nomeSolicitante, string email, string foto)
        {
            string _query = @"INSERT INTO CONEXOES (ID_GOOGLE_SOLICITANTE_FK, NOME_SOLICITANTE_FK, EMAIL_SOLICITANTE_FK, FOTO_SOLICITANTE_FK, ID_GOOGLE_SOLICITADO_FK, EMAIL_SOLICITADO_FK, ACEITO) VALUES (@ID_GOOGLE_SOLICITANTE, @NOME_SOLICITANTE, @EMAIL_SOLICITANTE, @FOTO_SOLICITANTE, @ID_GOOGLE_SOLICITADO, @EMAIL_SOLICITADO, NULL);

            SELECT ID_CONEXAO, ID_GOOGLE_SOLICITANTE_FK, NOME_SOLICITANTE_FK, EMAIL_SOLICITANTE_FK, FOTO_SOLICITANTE_FK, ID_GOOGLE_SOLICITADO_FK, EMAIL_SOLICITADO_FK, ACEITO FROM CONEXOES WHERE ID_CONEXAO = LAST_INSERT_ID();";

            using (var con = new MySqlConnection(_connectionString))
            {
                con.Open();
                return await con.QueryFirstOrDefaultAsync<Conexao>(_query, new { ID_GOOGLE_SOLICITANTE = idGoogleSolicitante, NOME_SOLICITANTE = nomeSolicitante, EMAIL_SOLICITANTE = email, FOTO_SOLICITANTE = foto, ID_GOOGLE_SOLICITADO = conexaoDTO.Id_Google_Solicitado_FK, EMAIL_SOLICITADO = conexaoDTO.Email_Solicitado_FK });
            }
        }

        public async Task<IEnumerable<Conexao>> GetSolicitacoesConexoesEmAbertoByIdGoogle(int idGoogleSolicitado)
        {
            string _query = @"SELECT ID_CONEXAO, ID_GOOGLE_SOLICITANTE_FK, NOME_SOLICITANTE_FK, EMAIL_SOLICITANTE_FK, FOTO_SOLICITANTE_FK, ID_GOOGLE_SOLICITADO_FK, EMAIL_SOLICITADO_FK FROM CONEXOES WHERE ID_GOOGLE_SOLICITADO_FK = @ID_GOOGLE_SOLICITADO AND ACEITO IS NULL;";

            using (var con = new MySqlConnection(_connectionString))
            {
                con.Open();
                return await con.QueryAsync<Conexao>(_query, new { ID_GOOGLE_SOLICITADO = idGoogleSolicitado });
            }
        }

        public async Task<int> AcceptConexao(int idConexao, int idGoogleSolicitante, int idGoogleSolicitado)
        {
            string _query = @"UPDATE CONEXOES SET ACEITO = TRUE WHERE ID_CONEXAO = @ID_CONEXAO AND ID_GOOGLE_SOLICITANTE_FK = @ID_GOOGLE_SOLICITANTE_FK AND ID_GOOGLE_SOLICITADO_FK = @ID_GOOGLE_SOLICITADO_FK AND ACEITO IS NULL;";

            using (var con = new MySqlConnection(_connectionString))
            {
                con.Open();
                return await con.ExecuteAsync(_query, new { ID_CONEXAO = idConexao, ID_GOOGLE_SOLICITANTE_FK = idGoogleSolicitante, ID_GOOGLE_SOLICITADO_FK = idGoogleSolicitado });
            }
        }

        public async Task<int> RefuseConexao(int idConexao, int idGoogleSolicitante, int idGoogleSolicitado)
        {
            string _query = @"UPDATE CONEXOES SET ACEITO = FALSE WHERE ID_CONEXAO = @ID_CONEXAO AND ID_GOOGLE_SOLICITANTE_FK = @ID_GOOGLE_SOLICITANTE_FK AND ID_GOOGLE_SOLICITADO_FK = @ID_GOOGLE_SOLICITADO_FK AND ACEITO IS NULL;";

            using (var con = new MySqlConnection(_connectionString))
            {
                con.Open();
                return await con.ExecuteAsync(_query, new { ID_CONEXAO = idConexao, ID_GOOGLE_SOLICITANTE_FK = idGoogleSolicitante, ID_GOOGLE_SOLICITADO_FK = idGoogleSolicitado });
            }
        }

        public async Task<IEnumerable<Conexao>> GetConexoesByIdGoogle(int idGoogleSolicitante)
        {
            string _query = @"SELECT ID_CONEXAO, ID_GOOGLE_SOLICITANTE_FK, NOME_SOLICITANTE_FK, EMAIL_SOLICITANTE_FK, FOTO_SOLICITANTE_FK, ID_GOOGLE_SOLICITADO_FK, EMAIL_SOLICITADO_FK FROM CONEXOES WHERE ID_GOOGLE_SOLICITANTE_FK = @ID_GOOGLE_SOLICITANTE AND ACEITO = TRUE;";

            using (var con = new MySqlConnection(_connectionString))
            {
                con.Open();
                return await con.QueryAsync<Conexao>(_query, new { ID_GOOGLE_SOLICITANTE = idGoogleSolicitante });
            }
        }

        public async Task<int> DeleteConexao(int idConexao, int idGoogleSolicitante, int idGoogleSolicitado)
        {
            string _query = @"DELETE FROM CONEXOES WHERE ID_CONEXAO = @ID_CONEXAO AND ID_GOOGLE_SOLICITANTE_FK = @ID_GOOGLE_SOLICITANTE AND ID_GOOGLE_SOLICITADO_FK = @ID_GOOGLE_SOLICITADO AND ACEITO = TRUE";

            using (var con = new MySqlConnection(_connectionString))
            {
                con.Open();
                return await con.ExecuteAsync(_query, new { ID_CONEXAO = idConexao, ID_GOOGLE_SOLICITANTE = idGoogleSolicitante, ID_GOOGLE_SOLICITADO = idGoogleSolicitado });
            }
        }

        public bool GetOpenAndAcceptedConexao(int idGoogleSolicitante, int idGoogleSolicitado)
        {
            string _query = @"SELECT ID_CONEXAO FROM CONEXOES WHERE ID_GOOGLE_SOLICITANTE_FK = @ID_GOOGLE_SOLICITANTE AND ID_GOOGLE_SOLICITADO_FK = @ID_GOOGLE_SOLICITADO AND ACEITO IS NULL OR ACEITO = TRUE";

            using (var con = new MySqlConnection(_connectionString))
            {
                con.Open();
                return con.ExecuteScalar<bool>(_query, new { ID_GOOGLE_SOLICITANTE = idGoogleSolicitante, ID_GOOGLE_SOLICITADO = idGoogleSolicitado });
            }
        }
        #endregion

        #region Queries Usuários
        public async Task<Usuario> InsertUsuario(int idGoogle, string email, string nome, string foto)
        {
            string _query = @"INSERT INTO USUARIOS (ID_GOOGLE, EMAIL, NOME, FOTO) VALUES (@ID_GOOGLE, @EMAIL, @NOME, @FOTO);

            SELECT ID_USUARIO, ID_GOOGLE, EMAIL, NOME, FOTO FROM USUARIOS WHERE ID_USUARIO = LAST_INSERT_ID();";

            using (var con = new MySqlConnection(_connectionString))
            {
                con.Open();
                return await con.QueryFirstOrDefaultAsync<Usuario>(_query, new { ID_GOOGLE = idGoogle, EMAIL = email, NOME = nome, FOTO = foto });
            }
        }
        
        public async Task<IEnumerable<Usuario>> GetUsuario(string email)
        {
            string _query = @"SELECT ID_USUARIO, ID_GOOGLE, EMAIL, NOME, FOTO FROM USUARIOS WHERE EMAIL LIKE @EMAIL;";

            using (var con = new MySqlConnection(_connectionString))
            {
                con.Open();
                return await con.QueryAsync<Usuario>(_query, new { EMAIL = $"%{email}%" });
            }
        }

        public bool GetUsuarioJaCadastrado(int idGoogle, string email)
        {
            string _query = "SELECT ID_GOOGLE, EMAIL, NOME, FOTO FROM USUARIOS WHERE ID_GOOGLE = @ID_GOOGLE AND EMAIL = @EMAIL;";

            using (var con = new MySqlConnection(_connectionString))
            {
                con.Open();
                return con.ExecuteScalar<bool>(_query, new { ID_GOOGLE = idGoogle, EMAIL = email });
            }
        }
        #endregion

        #region Utils
        public string PercentDecode(string texto)
        {
            StringBuilder sb = new StringBuilder(texto);

            sb.Replace("%20", " ");
            sb.Replace("%21", "!");
            sb.Replace("%23", "#");
            sb.Replace("%24", "$");
            sb.Replace("%25", "%");
            sb.Replace("%26", "&");
            sb.Replace("%27", "'");
            sb.Replace("%28", "(");
            sb.Replace("%29", ")");
            sb.Replace("%2A", "*");
            sb.Replace("%2B", "+");
            sb.Replace("%2C", ",");
            sb.Replace("%2F", "/");
            sb.Replace("%3A", ":");
            sb.Replace("%3B", ";");
            sb.Replace("%3D", "=");
            sb.Replace("%3F", "?");
            sb.Replace("%40", "@");
            sb.Replace("%5B", "[");
            sb.Replace("%5D", "]");

            return sb.ToString();
        }
        #endregion
    }
}
