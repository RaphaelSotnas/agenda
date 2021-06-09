using Agenda.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Agenda.Controllers
{
    public class AgendaController : Controller
    {
        
        [HttpGet]
        public  IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(string nome, string numero)
        {
            //instancia da Classe Contato
            Contato contato = new Contato();

            //inserir dados no objeto instanciado da classse
            contato.Nome = nome;
            contato.Telefone = numero;

            //instancia da Classe de contexto
            AgendaContext agendaContext = new AgendaContext();

            //adicionar objeto contato no banco de dados
            agendaContext.Contatos.Add(contato);

            //linha de código para salvar alteração no banco
            agendaContext.SaveChanges();
            
            //Retorna uma página HTML com nome especificado, se não tiver especificado gera um erro.
            return View("Finalizado");
        }
        
        [HttpGet]
        public IActionResult Listar()
        {
            AgendaContext agendaContext = new AgendaContext();

            List<Contato> listaDeContato = new List<Contato>();

            listaDeContato = agendaContext.Contatos.ToList();
            
            ViewBag.Contatos = listaDeContato;

            return View();
        }

        [HttpGet]
        public IActionResult Deletar(int id)
        {
            Contato contato = new Contato();
            contato.Id = id;

            AgendaContext agendaContext = new AgendaContext();

            var ContatoDoBanco = contato;

            agendaContext.Contatos.Remove(ContatoDoBanco);
            agendaContext.SaveChanges();

            return View();
        }           

        [HttpGet]
        public IActionResult ChamarEditar(int id)
        {
             AgendaContext agendaContatext = new AgendaContext();

             int resultado = id;

             ViewData["valor"] = resultado;

             return View();
        }

        [HttpPost]
        public IActionResult Editar(string nome, string numero, int id)
        {
            Contato contadoEditado = new Contato();

            contadoEditado.Id = id;
            contadoEditado.Nome = nome;
            contadoEditado.Telefone = numero;

            AgendaContext agendaContext = new AgendaContext();                      

            agendaContext.Contatos.Update(contadoEditado);
            agendaContext.SaveChanges();

            return View("Fim");
        }             

        public IActionResult Fim()
        {
            return View();
        }
    }
}

