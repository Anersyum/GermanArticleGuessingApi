using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ines_German.API.Dtos;
using Ines_German.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Ines_German.API.Data
{
    public class WordRepository : IWordRepository
    {
        private readonly DataContext context;
        private readonly IConfiguration config;
        public WordRepository(DataContext context, IConfiguration config)
        {
            this.config = config;
            this.context = context;
        }

        public async Task<WordModel> CreateWord(WordDto wordToCreate)
        {
            WordModel word = await this.context.Words.Where(x => x.Word == wordToCreate.Word)
                                    .FirstOrDefaultAsync();
            
            if (word != null)
            {
                return null;
            }

            word = new WordModel()
            {
                Word = wordToCreate.Word,
                Article = wordToCreate.Article
            };

            await this.context.Words.AddAsync(word);
            await this.context.SaveChangesAsync();

            return word;
        }

        public async Task<IEnumerable<WordModel>> GetWordsForGuessing()
        {
            string queryString = "SELECT * FROM Words ORDER BY RANDOM() LIMIT 10;";

            return await this.context.Words.FromSqlRaw(queryString).ToListAsync();
        }

        public async Task<IEnumerable<WordModel>> GetAllWords()
        {
            return await this.context.Words.ToListAsync();
        }

        public async Task<bool> DeleteWord(int id)
        {
            var word = await this.context.Words.Where(x => x.Id == id).FirstOrDefaultAsync();

            this.context.Remove(word);
            await this.context.SaveChangesAsync();

            return true;
        }
    }
}