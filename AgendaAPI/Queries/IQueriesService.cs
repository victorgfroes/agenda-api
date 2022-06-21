using AgendaAPI.DTOs.Agenda;
using AgendaAPI.DTOs.Conexao;
using AgendaAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgendaAPI.Queries
{
    public interface IQueriesService
    {
        #region Queries Agendas
        Task<Agenda> InsertAgenda(CreateAgendaDTO agendaDTO, int idUsuario, int idGoogle, string email, string nome);
        Task<IEnumerable<Agenda>> GetAgendasByIdGoogle(int idGoogle);
        Task<int> UpdateAgendaByIdAgenda(UpdateAgendaDTO agendaDTO, int idAgenda);
        Task<int> UpdateTituloByIdAgenda(UpdateTituloAgendaDTO tituloAgendaDTO, int idAgenda);
        Task<int> UpdateDescricaoByIdAgenda(UpdateDescricaoAgendaDTO descricaoAgendaDTO, int idAgenda);
        Task<int> UpdateDtFimByIdAgenda(UpdateDtFimAgendaDTO dtFimAgendaDTO, int idAgenda);
        Task<int> DeleteAgendaByIdAgenda(int idAgenda);
        #endregion

        #region Queries Conexões
        Task<Conexao> RequestConexao(CreateConexaoDTO conexaoDTO, int idGoogleSolicitante, string nomeSolicitante, string email, string foto);        
        Task<IEnumerable<Conexao>> GetSolicitacoesConexoesEmAbertoByIdGoogle(int idGoogleSolicitado);
        Task<int> AcceptConexao(int idConexao, int idGoogleSolicitante, int idGoogleSolicitado);
        Task<int> RefuseConexao(int idConexao, int idGoogleSolicitante, int idGoogleSolicitado);
        Task<IEnumerable<Conexao>> GetConexoesByIdGoogle(int idGoogleSolicitante);
        Task<int> DeleteConexao(int idConexao, int idGoogleSolicitante, int idGoogleSolicitado);
        bool GetOpenAndAcceptedConexao(int idGoogleSolicitante, int idGoogleSolicitado);
        #endregion

        #region Queries Usuários
        Task<Usuario> InsertUsuario(int idGoogle, string email, string nome, string foto);
        Task<IEnumerable<Usuario>> GetUsuario(string email);
        bool GetUsuarioJaCadastrado(int idGoogle, string email);
        #endregion

        #region Utils
        string PercentDecode(string texto);
        #endregion
    }
}
