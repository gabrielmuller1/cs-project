﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Crud_Campos_Dealer.Data;
using Crud_Campos_Dealer.Data.Entities;
using Crud_Campos_Dealer.Data.Services;
using Crud_Campos_Dealer.Models.ClienteModel;

namespace Crud_Campos_Dealer.Controllers
{
  [Route("/[controller]")]
  public class ClientesController : Controller
  {
    private readonly Contexto _context;

    private readonly ClienteService _service;

    public ClientesController(Contexto context, ClienteService service)
    {
      _context = context;
      _service = service;
    }
    //Lista todos clientes
    [HttpGet("")]
    public async Task<List<ListaClienteModel>> Listar()
    {
      return await this._service.Listar();
    }

    [HttpGet("Detalhes/{id}")]
    public async Task<DetalhesClienteModel> Detalhes(int id)
    {
      var detailById = await this._service.Detalhes(id);

      if (detailById == null)
      {
        return NotFound("Não foi possivel encontrar o id ${id}");
      }
    }

    [HttpGet("Buscar/{nome}")]
    public async Task<List<ListaClienteModel>> Buscar(string nome)
    {
      var searchName = await this._service.Buscar(nome);

      if (searchName == null)
      {
        return NotFound($"Não foi possivel encontrar o nome {nome}");
      }

      return await this._service.Buscar(nome);
    }

    [HttpPost("")]
    public async Task CreateOrEdit([FromBody] ClienteModel cliente)
    {
      var clientPost = await this._service.CreateOrEdit(cliente);

      if (clientPost == null)
      {
        return throw new Error("Dados Inválidos");
      }
    }

    [HttpDelete("{id}")]
    public async Task Delete(int id)
    {
      deletedId = await this._service.Delete(id);
    }
    [HttpGet("Details/{id}")]
    public async Task<IActionResult> Details(int? id)
    {
      if (id == null || _context.Cliente == null)
      {
        return NotFound();
      }

      var cliente = await this._service.Detalhes(id.Value);
      if (cliente == null)
      {
        return NotFound();
      }

      return View(cliente);
    }
  }


}
