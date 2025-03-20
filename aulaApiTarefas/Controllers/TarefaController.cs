using Microsoft.AspNetCore.Mvc;
using aulaApiTarefas.Models;
using aulaApiTarefas.Data;
using System.Collections.Generic;

namespace aulaApiTarefas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        private readonly TarefaRepository _tarefaRepository;

        public TarefaController(TarefaRepository tarefaRepository)
        {
            _tarefaRepository = tarefaRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Tarefa>> Get()
        {
            return Ok(_tarefaRepository.GetAll());
        }

        [HttpPost]
        public ActionResult Post([FromBody] Tarefa tarefa)
        {
            _tarefaRepository.Add(tarefa);
            return CreatedAtAction(nameof(Get), new { id = tarefa.Id }, tarefa);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Tarefa tarefa)
        {
            tarefa.Id = id;
            _tarefaRepository.Update(tarefa);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _tarefaRepository.Delete(id);
            return NoContent();
        }
    }
}
