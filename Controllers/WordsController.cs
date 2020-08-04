using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ines_German.API.Data;
using Ines_German.API.Dtos;
using Ines_German.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Ines_German.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WordsController : ControllerBase
    {
        private readonly IWordRepository wordRepo;
        public WordsController(IWordRepository wordRepo)
        {
            this.wordRepo = wordRepo;

        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateWordAction(WordDto wordToCreate)
        {
            WordModel word = await this.wordRepo.CreateWord(wordToCreate);

            if (word == null)
            {
                return BadRequest(new 
                {
                    success = false,
                    message = "Can't create word",
                    duplicate = true
                });
            }

            return Ok(new {
                success = true,
                word = word
            });
        }

        [HttpGet("get/guessing")]
        public async Task<IActionResult> GetWordAction()
        {
            var wordList = await this.wordRepo.GetWordsForGuessing();

            return Ok(new
            {
                success = true,
                wordList = wordList
            });
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetAllWordsAction()
        {
            var wordsList = await this.wordRepo.GetAllWords();

            return Ok(new 
            {
                success = true,
                wordsList = wordsList
            });
        }
    }
}
