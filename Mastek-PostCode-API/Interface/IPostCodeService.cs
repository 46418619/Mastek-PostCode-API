using System.Threading.Tasks;
using System.Collections.Generic;
using Mastek_PostCode_API.Model;

namespace Mastek_PostCode_API.Interface
{
    public interface IPostCodeService
    {
        Task<PostCodeDetailViewModel> GetPostCodeDetailAsync(string pC);
        Task<List<string>> GetAutoCompleteAsync(string pC);
    }
}
