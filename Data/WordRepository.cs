using System.Linq;
using System.Threading.Tasks;
using Ines_German.API.Dtos;
using Ines_German.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Ines_German.API.Data
{
    public class WordRepository : IWordRepository
    {
        private readonly DataContext context;
        public WordRepository(DataContext context)
        {
            this.context = context;

        }
        public async Task<WordModel> CreateWord(WordDto wordToCreate)
        {
            WordModel word = new WordModel() {
                Word = wordToCreate.Word,
                Article = wordToCreate.Article
            };

            await this.context.Words.AddAsync(word);
            await this.context.SaveChangesAsync();

            return word;
        }

        public async Task<WordModel> GetWord(int id)
        {
            return await this.context.Words.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}