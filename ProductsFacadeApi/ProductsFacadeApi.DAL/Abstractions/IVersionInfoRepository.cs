using System.Threading.Tasks;

namespace ProductsFacadeApi.DAL.Abstractions
{
    /// <summary>
    /// Репозиторий IVersionInfo
    /// </summary>
    public interface IVersionInfoRepository
    {
        /// <summary>
        /// Получить количество записей
        /// </summary>
        /// <returns></returns>
        Task<int> Count();
    }
}
