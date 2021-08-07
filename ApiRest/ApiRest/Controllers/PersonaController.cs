using AccessData;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ApiRest.Controllers
{
    public class PersonaController : ApiController
    {
 
        private DbPersonaEntities personas = new DbPersonaEntities();

        [HttpGet]
        public IEnumerable<TblPersona> Get()
        {
            using (personas)

            {
                return personas.TblPersona.ToList();
            }
        }

        [HttpGet]
        public TblPersona Get(int id)
        {
            using (DbPersonaEntities personas = new DbPersonaEntities())

            {
                return personas.TblPersona.FirstOrDefault(e => e.IdPersona == id);
            }
        }

        [HttpPost]
        public IHttpActionResult AgregarPersona([FromBody] TblPersona persona) {
            if (ModelState.IsValid)
            {
                personas.TblPersona.Add(persona);
                personas.SaveChanges();
                return Ok(persona);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public IHttpActionResult Actualizar(int id, [FromBody]TblPersona persona)
        {
            if (ModelState.IsValid)
            {
                var ExistPersona = personas.TblPersona.Count(e => e.IdPersona == id) > 0;

                if (ExistPersona)
                {
                    personas.Entry(persona).State = EntityState.Modified;
                    personas.SaveChanges();

                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        public IHttpActionResult Eliminar(int id)
        {
            var persona = personas.TblPersona.Find(id);

            if (persona != null)
            {
                personas.TblPersona.Remove(persona);
                personas.SaveChanges();
                return Ok(persona);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
