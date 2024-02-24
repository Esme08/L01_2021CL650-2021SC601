using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using L01_2021CL650_2021SC601.Models;

namespace L01_2021CL650_2021SC601.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class platosController : ControllerBase
    {
        private readonly restauranteContext _platosContexto;

        public platosController(restauranteContext platosContexto)
        {
            _platosContexto = platosContexto;
        }

        /*Método para leer todos los registros*/
        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            try
            {

                List<platos> listadoplatos = (from e in _platosContexto.platos
                                                select e).ToList();
                if (listadoplatos.Count == 0)
                {
                    return NotFound();
                }
                return Ok(listadoplatos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Método para buscar por precio
        [HttpGet]
        [Route("GetByPrice/{precio}")]
        public IActionResult Get(decimal precio)
        {
            try
            {

                platos? plato = (from e in _platosContexto.platos where e.precio < precio select e).FirstOrDefault();
                if (plato == null)
                {
                    return NotFound();
                }
                return Ok(plato);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //Método para crear platos

        [HttpPost]
        [Route("Add")]

        public IActionResult CrearPlato([FromBody] platos platos)
        {
            try
            {
                _platosContexto.platos.Add(platos);
                _platosContexto.SaveChanges();
                return Ok(platos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Método para actualizar platos

        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult ActualizarPlato(int id, [FromBody] platos platoModificar)
        {
            try
            {
                /*Para actualizar un registro, se obtiene el registro original de la base de datos 
             al cual alteramos alguna propiedad*/

                platos? platoActual = (from e in _platosContexto.platos
                                         where e.platoId == id
                                         select e).FirstOrDefault();

                /*Verificamos que el registro exista según ID*/
                if (platoActual == null)
                {
                    return NotFound();
                }

                /*Si se  encuentra el registro, se alteran los campos modificables*/

                platoActual.nombrePlato = platoModificar.nombrePlato;
                platoActual.precio = platoModificar.precio;
               

                /*Se marca el registro como modificado en el contexto y se envía la modificación a la base de datos*/

                _platosContexto.Entry(platoActual).State = EntityState.Modified;
                _platosContexto.SaveChanges();

                return Ok(platoModificar);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        //Método para eliminar

        [HttpDelete]
        [Route("eliminar/{id}")]

        public IActionResult Eliminarplato(int id)
        {
            try
            {
                /*Para actualizar un registro, se obtiene el registro original de la base de datos el cual eliminaremos*/
                platos? plato = (from e in _platosContexto.platos
                                   where e.platoId == id
                                   select e).FirstOrDefault();

                //Verificamos que exista el registro según su id
                if (plato == null)
                {
                    return NotFound();
                }

                //Ejecutamos la acción de eliminar el registro 
                _platosContexto.platos.Attach(plato);
                _platosContexto.platos.Remove(plato);
                _platosContexto.SaveChanges();

                return Ok(plato);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

