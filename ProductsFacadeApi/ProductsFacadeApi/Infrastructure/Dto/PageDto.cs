namespace ProductsFacadeApi.Infrastructure.Dto
{
    /// <summary>
    /// Dto для постраничного отображения товаров.
    /// </summary>
    public class PageDto
    {
        /// <summary>
        /// Размер страницы по умолчанию.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Текущая страница.
        /// </summary>
        public int PageIndex { get; set; }
    }
}