using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace product.stock.api
{
    /// <summary>
    /// Controlador da API de Estoque
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        /// <summary>
        /// Camada de Serviços de Registro
        /// </summary>
        private readonly StockService _service;

        /// <summary>
        /// Construtor padrão para iniciar a API
        /// </summary>
        /// <param name="service">Camada de Serviços de Registro</param>        
        public StockController(StockService service)
        {
            _service = service;
        }

        /// <summary>
        /// Inserir novo produto
        /// </summary>
        /// <param name="viewModel">>Modelo de Produto</param>
        /// <returns>Insere um novo produto no estoque</returns>
        /// <response code="200">Insere um novo produto no estoque</response>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public IActionResult InsertProduct([FromBody]ProductVM viewModel)
        {
            if (ModelState.IsValid)
                return Ok(_service.Insert(viewModel));

            return BadRequest(ModelState);
        }

        /// <summary>
        /// Atualizar um produto 
        /// </summary>
        /// <param name="viewModel">Modelo de Produto</param>
        /// <returns>Atualiza os dados de um produto</returns>
        /// <response code="200">Atualiza os dados de um produto</response>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut]
        public IActionResult UpdateProduct([FromBody]ProductVM viewModel)
        {
            if (ModelState.IsValid)
                return Ok(_service.Update(viewModel));

            return BadRequest(ModelState);
        }

        /// <summary>
        /// Listar todos os produtos do estoque
        /// </summary>
        /// <returns>Lista contendo todos os produtos do estoque</returns>
        /// <response code="200">Lista contendo todos os produtos do estoque</response>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]        
        public IActionResult ListProduct()
        {
            var data = _service.List();

            if (data == null)
                return NotFound();

            return Ok(data);
        }

        /// <summary>
        /// Exibir produto pelo id
        /// </summary>
        /// <returns>Exibe um produto através do identificador</returns>
        /// <response code="200">Exibe um produto através do identificador</response>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("{id}")]
        public IActionResult SelectProduct(long id)
        {
            if (id == 0)
                return NotFound();

             var registro = _service.Select(id);

            if (registro == null)
                return NotFound();

            return Ok(registro);
        }

        /// <summary>
        /// Remover produto pelo id
        /// </summary>
        /// <returns>Remove um produto através do identificador</returns>
        /// <response code="200">Remove um produto através do identificador</response>
        [HttpDelete("{id}")]        
        public IActionResult DeleteProduct(int id)
        {
            if (id == 0)
                return NotFound();
            
            var registro = _service.Select(id);

            if (registro == null)
                return NotFound();
            
            _service.Delete(id);

            return Ok(true);
        }

        /// <summary>
        /// Inserir novo usuário
        /// </summary>
        /// <param name="user">>Modelo de Usuário</param>
        /// <returns>Insere um novo usuário para autenticação</returns>
        /// <response code="200">Insere um novo usuário para autenticação</response>
        [HttpPost("User")]
        public IActionResult InsertUser([FromBody]UserVM user)
        {   
            if (ModelState.IsValid)
                return Ok(_service.Insert(user));

            return BadRequest(ModelState);
        }

        /// <summary>
        /// Realizar autenticação do usuário
        /// </summary>
        /// <param name="auth">>Modelo de Usuário</param>
        /// <returns>Realiza a autenticação através do usuário</returns>
        /// <response code="200">Realiza a autenticação através do usuário</response>
        [HttpPost("Login")]
        public IActionResult LoginUser([FromBody]UserVM auth)
        {
            if (ModelState.IsValid)
                return Ok(_service.Login(auth).ToAuthVM());

            return BadRequest(ModelState);
        }
    }
}
