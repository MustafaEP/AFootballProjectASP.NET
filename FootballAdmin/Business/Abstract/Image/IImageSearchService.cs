using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract.Image
{
    public interface IImageSearchService
    {
        Task<string> GetPlayerImageAsync(string playerName);
    }
}
