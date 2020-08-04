using System.Collections.Generic;
using System.Threading.Tasks;
using Ines_German.API.Dtos;
using Ines_German.API.Models;

namespace Ines_German.API.Data
{
    public interface IWordRepository
    {
        Task<WordModel> CreateWord(WordDto wordToCreate);
        Task<IEnumerable<WordModel>> GetWordsForGuessing();
    }
}