using Microsoft.AspNetCore.Mvc;

namespace EFWithDapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoDapperController : ControllerBase
    {
        private readonly IDapperRepository _dapperRepository;

        public ProdutoDapperController(IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var produtos = await _dapperRepository.GetAllAsync<Produto>();
                return Ok(produtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var produto = await _dapperRepository.GetByIdAsync<Produto>(id);
                if (produto == null)
                {
                    return NotFound();
                }
                return Ok(produto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Produto produto)
        {
            try
            {
                if (produto == null)
                {
                    return BadRequest();
                }

                await _dapperRepository.AddAsync(produto);
                return CreatedAtAction(nameof(GetById), new { id = produto.Id }, produto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Produto produto)
        {
            try
            {
                if (produto == null || id != produto.Id)
                {
                    return BadRequest();
                }

                await _dapperRepository.UpdateAsync(produto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _dapperRepository.DeleteAsync<Produto>(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }
    }
}
