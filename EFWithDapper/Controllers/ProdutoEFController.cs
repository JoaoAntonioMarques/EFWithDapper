using Microsoft.AspNetCore.Mvc;

namespace EFWithDapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoEFController : ControllerBase
    {
        private readonly IRepository<Produto> _produtoRepository;

        public ProdutoEFController(IRepository<Produto> produtoRepository)
        {
            _produtoRepository = produtoRepository ?? throw new ArgumentNullException(nameof(produtoRepository));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var produtos = await _produtoRepository.GetAllAsync();
            return Ok(produtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var produto = await _produtoRepository.GetByIdAsync(id);
            if (produto == null)
            {
                return NotFound();
            }
            return Ok(produto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Produto produto)
        {
            if (produto == null)
            {
                return BadRequest();
            }

            await _produtoRepository.AddAsync(produto);
            return CreatedAtAction(nameof(GetById), new { id = produto.Id }, produto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Produto produto)
        {
            if (produto == null || id != produto.Id)
            {
                return BadRequest();
            }

            var existingProduto = await _produtoRepository.GetByIdAsync(id);
            if (existingProduto == null)
            {
                return NotFound();
            }

            await _produtoRepository.UpdateAsync(produto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var produto = await _produtoRepository.GetByIdAsync(id);
            if (produto == null)
            {
                return NotFound();
            }

            await _produtoRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
