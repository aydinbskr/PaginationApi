using PaginationApi.Helpers;

namespace PaginationApi.Services
{
    public interface IUriService
    {
        public Uri GetPageUri(PaginationFilter filter, string route);
    }
}
