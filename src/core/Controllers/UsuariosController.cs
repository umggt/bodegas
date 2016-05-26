﻿using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bodegas.Modelos;
using Bodegas.Repositorios;
using System.Net;

namespace Bodegas.Controllers
{
    [Route("[controller]")]
    public class UsuariosController : Controller
    {
        private readonly UsuariosRepositorio usuarios;

        public UsuariosController(UsuariosRepositorio usuarios)
        {
            this.usuarios = usuarios;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int pagina = 1, int elementos = 20, string ordenamiento = null)
        {
            var result = await usuarios.ObtenerTodosAsync(new PaginacionParametros { Pagina = pagina, ElementosPorPagina = elementos, Ordenamiento = ordenamiento?.Split(',')?.Select(x => new { order = !x.StartsWith("-"), field = x.TrimStart('-') })?.GroupBy(x => x.field)?.ToDictionary(x => x.Key, x => x.First().order) });
            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetUsuario")]
        public async Task<IActionResult> GetSingle(int id)
        {
            var result = await usuarios.ObtenerUnicoAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UsuarioDetalle usuario)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            var usuarioId = await usuarios.CrearAsync(usuario);
            var result = await usuarios.ObtenerUnicoAsync(usuarioId);
            return CreatedAtRoute("GetUsuario", new { id = usuarioId }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UsuarioDetalle usuario)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            var modificado = await usuarios.EditarAsync(id, usuario);
            if (modificado)
            {
                var result = await usuarios.ObtenerUnicoAsync(id);
                return Ok(result);
            }
            else
            {
                return new HttpStatusCodeResult((int) HttpStatusCode.NotModified);
            }
        }
    }
}
