using AgendaAPI.DTOs.Agenda;
using AgendaAPI.DTOs.Conexao;
using AgendaAPI.Models;
using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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
        public async Task<Agenda> InsertAgenda(CreateAgendaDTO agendaDTO, int idUsuario, int idGoogle, string email, string nome)
        {
            string _query = @"INSERT INTO AGENDAS (ID_USUARIO_FK, ID_GOOGLE_FK, EMAIL_FK, NOME_FK, TITULO, DESCRICAO, DT_FIM) VALUES (@ID_USUARIO, @ID_GOOGLE, @EMAIL, @NOME, @TITULO, @DESCRICAO, @DT_FIM);
                             
                            SELECT ID_AGENDA, ID_USUARIO_FK, ID_GOOGLE_FK, EMAIL_FK, NOME_FK, TITULO, DESCRICAO, DT_FIM FROM AGENDAS WHERE ID_AGENDA = LAST_INSERT_ID();";

            using (var con = new MySqlConnection(_connectionString))
            {
                con.Open();
                return await con.QueryFirstOrDefaultAsync<Agenda>(_query, new { ID_USUARIO = idUsuario, ID_GOOGLE = idGoogle, EMAIL = email, NOME = nome, TITULO = agendaDTO.Titulo, DESCRICAO = agendaDTO.Descricao, DT_FIM = agendaDTO.Dt_Fim });
            }
        }

        public async Task<IEnumerable<Agenda>> GetAgendasByIdUsuario(int idGoogle)
        {
            string _query = @"SELECT ID_AGENDA, ID_USUARIO_FK, ID_GOOGLE_FK, EMAIL_FK, NOME_FK, TITULO, DESCRICAO, DT_FIM FROM AGENDAS WHERE ID_GOOGLE_FK = @ID_GOOGLE_FK;";

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
        public async Task<Conexao> RequestConexao(CreateConexaoDTO conexaoDTO, int idSGoogleSolicitante, string email)
        {
            string _query = @"INSERT INTO CONEXOES (ID_GOOGLE_SOLICITANTE_FK, EMAIL_SOLICITANTE_FK, ID_GOOGLE_SOLICITADO_FK, EMAIL_SOLICITADO_FK, ACEITO) VALUES (@ID_GOOGLE_SOLICITANTE_FK, @EMAIL_SOLICITANTE_FK, @ID_GOOGLE_SOLICITADO_FK, @EMAIL_SOLICITADO_FK, NULL);

            SELECT ID_CONEXAO, NOME_SOLICITANTE_FK, ID_GOOGLE_SOLICITANTE_FK, ID_GOOGLE_SOLICITADO_FK, ACEITO FROM CONEXOES WHERE ID_CONEXAO = LAST_INSERT_ID();";

            using (var con = new MySqlConnection(_connectionString))
            {
                con.Open();
                return await con.QueryFirstOrDefaultAsync<Conexao>(_query, new { ID_GOOGLE_SOLICITANTE_FK = idSGoogleSolicitante, EMAIL_SOLICITANTE_FK = email, ID_GOOGLE_SOLICITADO_FK = conexaoDTO.Id_Google_Solicitado_FK, EMAIL_SOLICITADO_FK = conexaoDTO.Email_Solicitado_FK });
            }
        }

        public async Task<IEnumerable<Conexao>> GetSolicitacoesConexoesByIdGoogle(int idGoogleSolicitado)
        {
            string _query = @"SELECT ID_CONEXAO, ID_GOOGLE_SOLICITANTE_FK, EMAIL_SOLICITANTE_FK, ID_GOOGLE_SOLICITADO_FK, EMAIL_SOLICITADO_FK FROM CONEXOES WHERE ID_GOOGLE_SOLICITADO_FK = @ID_GOOGLE_SOLICITADO AND ACEITO IS NULL;";

            using (var con = new MySqlConnection(_connectionString))
            {
                con.Open();
                return await con.QueryAsync<Conexao>(_query, new { ID_GOOGLE_SOLICITADO = idGoogleSolicitado });
            }
        }

        public async Task<int> AcceptConexao(int idConexao, int idGoogleSolicitante, int idGoogleSolicitado)
        {
            string _query = @"UPDATE CONEXOES SET ACEITO = TRUE WHERE ID_CONEXAO = @ID_CONEXAO AND ID_GOOGLE_SOLICITANTE_FK = @ID_GOOGLE_SOLICITANTE_FK AND ID_GOOGLE_SOLICITADO_FK = @ID_GOOGLE_SOLICITADO_FK;";

            using (var con = new MySqlConnection(_connectionString))
            {
                con.Open();
                return await con.ExecuteAsync(_query, new { ID_CONEXAO = idConexao, ID_GOOGLE_SOLICITANTE_FK = idGoogleSolicitante, ID_GOOGLE_SOLICITADO_FK = idGoogleSolicitado });
            }
        }

        public async Task<int> RefuseConexao(int idConexao, int idGoogleSolicitante, int idGoogleSolicitado)
        {
            string _query = @"UPDATE CONEXOES SET ACEITO = FALSE WHERE ID_CONEXAO = @ID_CONEXAO AND ID_GOOGLE_SOLICITANTE_FK = @ID_GOOGLE_SOLICITANTE_FK AND ID_GOOGLE_SOLICITADO_FK = @ID_GOOGLE_SOLICITADO_FK;";

            using (var con = new MySqlConnection(_connectionString))
            {
                con.Open();
                return await con.ExecuteAsync(_query, new { ID_CONEXAO = idConexao, ID_GOOGLE_SOLICITANTE_FK = idGoogleSolicitante, ID_GOOGLE_SOLICITADO_FK = idGoogleSolicitado });
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
            string _query = @"SELECT ID_GOOGLE, EMAIL, NOME FROM USUARIOS WHERE NOME LIKE @NOME;";

            using (var con = new MySqlConnection(_connectionString))
            {
                con.Open();
                return await con.QueryAsync<Usuario>(_query, new { NOME = $"%{email}%" });
            }
        }
        #endregion
    }
}
