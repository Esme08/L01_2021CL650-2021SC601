using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using L01_2021CL650_2021SC601.Models;

namespace L01_2021CL650_2021SC601.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class pedidosController : ControllerBase
    {
        private readonly restauranteContext _pedidosContexto;

        public pedidosController (restauranteContext pedidosContexto)
        {
            _pedidosContexto = pedidosContexto;
        }

        /*Método para leer todos los registros*/
        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            try
            {

                List<pedidos> listadopedidos = (from e in _pedidosContexto.pedidos
                                                select e).ToList();
                if (listadopedidos.Count == 0)
                {
                    return NotFound();
                }
                return Ok(listadopedidos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Método para buscar por cliente
        [HttpGet]
        [Route("GetByClient/{clienteid}")]
        public IActionResult Get(int clienteid)
        {
            try
            {

                pedidos? pedidos = (from e in _pedidosContexto.pedidos where e.clienteId == clienteid select e).FirstOrDefault();
                if (pedidos == null)
                {
                    return NotFound();
                }
                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Método para buscar por motorista
        [HttpGet]
        [Route("GetByDriver/{motoristaid}")]
        public IActionResult GetByDriver(int motoristaid)
        {
            try
            {

                pedidos? pedidos = (from e in _pedidosContexto.pedidos where e.motoristaId == motoristaid select e).FirstOrDefault();
                if (pedidos == null)
                {
                    return NotFound();
                }
                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Método para crear pedidos

        [HttpPost]
        [Route("Add")]

        public IActionResult CrearPedido([FromBody] pedidos pedido)
        {
            try
            {
                _pedidosContexto.pedidos.Add(pedido);
                _pedidosContexto.SaveChanges();
                return Ok(pedido);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Método para actualizar pedidos

        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult ActualizarPedido(int id, [FromBody] pedidos pedidoModificar)
        {
            try
            {
                /*Para actualizar un registro, se obtiene el registro original de la base de datos 
             al cual alteramos alguna propiedad*/

                pedidos? pedidoActual = (from e in _pedidosContexto.pedidos
                                         where e.pedidoId == id
                                         select e).FirstOrDefault();

                /*Verificamos que el registro exista según ID*/
                if (pedidoActual == null)
                {
                    return NotFound();
                }

                /*Si se  encuentra el registro, se alteran los campos modificables*/

                pedidoActual.motoristaId = pedidoModificar.motoristaId;
                pedidoActual.clienteId = pedidoModificar.clienteId;
                pedidoActual.platoId = pedidoModificar.platoId;
                pedidoActual.cantidad = pedidoModificar.cantidad;
                pedidoActual.precio = pedidoModificar.precio;

                /*Se marca el registro como modificado en el contexto y se envía la modificación a la base de datos*/

                _pedidosContexto.Entry(pedidoActual).State = EntityState.Modified;
                 _pedidosContexto.SaveChanges();

                return Ok(pedidoModificar);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        //Método para eliminar

        [HttpDelete]
        [Route("eliminar/{id}")]

        public IActionResult Eliminarpedido(int id)
        {
            try
            {
                /*Para actualizar un registro, se obtiene el registro original de la base de datos el cual eliminaremos*/
                pedidos? equipo = (from e in _pedidosContexto.pedidos
                                   where e.pedidoId == id
                                   select e).FirstOrDefault();

                //Verificamos que exista el registro según su id
                if (equipo == null)
                {
                    return NotFound();
                }

                //Ejecutamos la acción de eliminar el registro 
                _pedidosContexto.pedidos.Attach(equipo);
                _pedidosContexto.pedidos.Remove(equipo);
                _pedidosContexto.SaveChanges();

                return Ok(equipo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
