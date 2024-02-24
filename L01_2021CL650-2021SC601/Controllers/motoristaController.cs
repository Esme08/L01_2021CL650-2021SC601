using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using L01_2021CL650_2021SC601.Models;

namespace L01_2021CL650_2021SC601.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class motoristaController : ControllerBase
    {
        private readonly restauranteContext _motoristaContexto;

        public motoristaController(restauranteContext motoristaContexto)
        {
            _motoristaContexto = motoristaContexto;
        }

        /*Método para leer todos los registros*/
        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            try
            {

                List<motoristas> listadomotorista = (from e in _motoristaContexto.motoristas
                                              select e).ToList();
                if (listadomotorista.Count == 0)
                {
                    return NotFound();
                }
                return Ok(listadomotorista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Método para buscar por nombre
        [HttpGet]
        [Route("GetByPrice/{nombre}")]
        public IActionResult Get(string nombre)
        {
            try
            {

                motoristas? motorista = (from e in _motoristaContexto.motoristas where e.nombreMotorista == nombre select e).FirstOrDefault();
                if (motorista == null)
                {
                    return NotFound();
                }
                return Ok(motorista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //Método para crear motorista

        [HttpPost]
        [Route("Add")]

        public IActionResult CrearMotorista([FromBody] motoristas motorista)
        {
            try
            {
                _motoristaContexto.motoristas.Add(motorista);
                _motoristaContexto.SaveChanges();
                return Ok(motorista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Método para actualizar motorista

        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult ActualizarMotorista(int id, [FromBody] motoristas motoristaModificar)
        {
            try
            {
                /*Para actualizar un registro, se obtiene el registro original de la base de datos 
             al cual alteramos alguna propiedad*/

                motoristas? motoristaActual = (from e in _motoristaContexto.motoristas
                                       where e.motoristaId == id
                                       select e).FirstOrDefault();

                /*Verificamos que el registro exista según ID*/
                if (motoristaActual == null)
                {
                    return NotFound();
                }

                /*Si se  encuentra el registro, se alteran los campos modificables*/

                motoristaActual.nombreMotorista = motoristaModificar.nombreMotorista;
             


                /*Se marca el registro como modificado en el contexto y se envía la modificación a la base de datos*/

                _motoristaContexto.Entry(motoristaActual).State = EntityState.Modified;
                _motoristaContexto.SaveChanges();

                return Ok(motoristaModificar);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        //Método para eliminar

        [HttpDelete]
        [Route("eliminar/{id}")]

        public IActionResult EliminarMotorista(int id)
        {
            try
            {
                /*Para actualizar un registro, se obtiene el registro original de la base de datos el cual eliminaremos*/
                motoristas? motorista = (from e in _motoristaContexto.motoristas
                                 where e.motoristaId == id
                                 select e).FirstOrDefault();

                //Verificamos que exista el registro según su id
                if (motorista == null)
                {
                    return NotFound();
                }

                //Ejecutamos la acción de eliminar el registro 
                _motoristaContexto.motoristas.Attach(motorista);
                _motoristaContexto.motoristas.Remove(motorista);
                _motoristaContexto.SaveChanges();

                return Ok(motorista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
