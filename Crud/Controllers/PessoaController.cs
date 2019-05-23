using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Crud.Models;
using Crud.Models.ViewModels;

namespace Crud.Controllers
{
    public class PessoaController : Controller
    {

        // GET: Pessoa
        public ActionResult Index()
        {
            List<ListPessoaViewModel> lst;
            using (TalentosEntities db = new TalentosEntities())
            {
                lst = (from x in db.Pessoa
                       select new ListPessoaViewModel
                       {
                           Id = x.id,
                           Email = x.email,
                           Nome = x.nome,
                           Cidade = x.cidade,
                           Estado = x.estado,
                       }).ToList();
            }
            return View(lst);
        }


        public ActionResult Novo()
        {
            PessoaViewModel model = new PessoaViewModel();
            using (TalentosEntities db = new TalentosEntities())
            {
                model.ConhecimentoList = (from x in db.Conhecimento
                                          select new ConhecimentoModel
                                          {
                                              Id = x.id,
                                              Tecnologia = x.tecnologia
                                          }).ToList();

                model.TrabalhoTempoList = (from x in db.Trabalho_Tempo
                                           select new TrabalhoTempoModel
                                           {
                                               Id = x.id,
                                               Tempo = x.tempo
                                           }).ToList();

                model.TrabalhoHorarioList = (from x in db.Trabalho_Horario
                                             select new TrabalhoHorarioModel
                                             {
                                                 Id = x.id,
                                                 Horario = x.horario,
                                             }).ToList();
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Avancar(PessoaViewModel model)
        {
            return View(model);
        }

        [HttpPost]
        public ActionResult Novo(PessoaViewModel model)
        {
            if (Request.Form["comando"] != null)
            {
                return View(model);
            }
            else
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        using (TalentosEntities db = new TalentosEntities())
                        {
                            using (var dbContextTransaction = db.Database.BeginTransaction())
                            {
                                try
                                {
                                    var x = new Pessoa();
                                    x.email = model.Email;
                                    x.nome = model.Nome;
                                    x.skype = model.Skype;
                                    x.telefone = model.Telefone;
                                    x.linkedin = model.Linkedin;
                                    x.cidade = model.Cidade;
                                    x.estado = model.Estado;
                                    x.portfolio = model.Portfolio;
                                    x.salario_por_hora = model.SalarioPorHora;
                                    x.experiencia_tecnologia = model.ExperienciaTecnologia;
                                    x.link_crud = model.LinkCrud;

                                    db.Pessoa.Add(x);
                                    db.SaveChanges();

                                    int pessoaId = x.id;

                                    for (int i = 0; i < model.ConhecimentoList.Count; i++)
                                    {
                                        var h = new Conhecimento_Pessoa();
                                        h.pessoa_id = pessoaId;
                                        h.conhecimento_id = model.ConhecimentoList[i].Id;
                                        h.nivel = model.ConhecimentoList[i].Nivel != null ? model.ConhecimentoList[i].Nivel.ToString() : "";

                                        db.Conhecimento_Pessoa.Add(h);
                                        db.SaveChanges();
                                    }

                                    for (int i = 0; i < model.TrabalhoHorarioList.Count; i++)
                                    {
                                        if (model.TrabalhoHorarioList[i].IsSelected)
                                        {
                                            var h = new Horario_Pessoa();
                                            h.pessoa_id = pessoaId;
                                            h.horario_id = model.TrabalhoHorarioList[i].Id;

                                            db.Horario_Pessoa.Add(h);
                                            db.SaveChanges();
                                        }
                                    }

                                    for (int i = 0; i < model.TrabalhoTempoList.Count; i++)
                                    {
                                        if (model.TrabalhoTempoList[i].IsSelected)
                                        {
                                            var h = new Tempo_Pessoa();
                                            h.pessoa_id = pessoaId;
                                            h.tempo_id = model.TrabalhoTempoList[i].Id;

                                            db.Tempo_Pessoa.Add(h);
                                            db.SaveChanges();
                                        }
                                    }

                                    dbContextTransaction.Commit();

                                }
                                catch (Exception ex)
                                {
                                    dbContextTransaction.Rollback();
                                    throw new Exception(ex.Message);
                                }
                            }
                        }
                        return Redirect("~/");
                    }
                    return View(model);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public ActionResult Editar(int id)
        {
            PessoaViewModel model = new PessoaViewModel();
            using (TalentosEntities db = new TalentosEntities())
            {
                var p = db.Pessoa.Find(id);
                model.Id = p.id;
                model.Email = p.email;
                model.Nome = p.nome;
                model.Skype = p.skype;
                model.Telefone = p.telefone;
                model.Linkedin = p.linkedin;
                model.Cidade = p.cidade;
                model.Estado = p.estado;
                model.Portfolio = p.portfolio;
                model.SalarioPorHora = p.salario_por_hora;
                model.ExperienciaTecnologia = p.experiencia_tecnologia;
                model.LinkCrud = p.link_crud;
            }


            using (TalentosEntities db = new TalentosEntities())
            {
                model.ConhecimentoList = (from x in db.Conhecimento
                                          select new ConhecimentoModel
                                          {
                                              Id = x.id,
                                              Tecnologia = x.tecnologia
                                          }).ToList();

                List<ConhecimentoModel> c = (from x in db.Conhecimento
                                             join cp in db.Conhecimento_Pessoa on x.id equals cp.conhecimento_id
                                             join p in db.Pessoa on cp.pessoa_id equals p.id
                                             where (p.id == id)
                                             select new ConhecimentoModel
                                             {
                                                 Id = x.id,
                                                 Tecnologia = x.tecnologia,
                                                 Nivel = cp.nivel
                                             }).ToList();

                foreach (var x in model.ConhecimentoList)
                {
                    foreach (var y in c)
                    {
                        if (y.Id == x.Id)
                            x.Nivel = y.Nivel;
                    }
                }


                model.TrabalhoTempoList = (from x in db.Trabalho_Tempo
                                           select new TrabalhoTempoModel
                                           {
                                               Id = x.id,
                                               Tempo = x.tempo,
                                           }).ToList();

                List<TrabalhoTempoModel> t = (from x in db.Trabalho_Tempo
                                              join tp in db.Tempo_Pessoa on x.id equals tp.tempo_id
                                              join p in db.Pessoa on tp.pessoa_id equals p.id
                                              where (p.id == id)
                                              select new TrabalhoTempoModel
                                              {
                                                  Id = x.id,
                                                  Tempo = x.tempo,
                                              }).ToList();

                foreach (var x in model.TrabalhoTempoList)
                {
                    foreach (var y in t)
                    {
                        if (y.Id == x.Id)
                            x.IsSelected = true;
                    }
                }


                model.TrabalhoHorarioList = (from x in db.Trabalho_Horario
                                             select new TrabalhoHorarioModel
                                             {
                                                 Id = x.id,
                                                 Horario = x.horario,
                                             }).ToList();

                List<TrabalhoHorarioModel> h = (from x in db.Trabalho_Horario
                                                join tp in db.Horario_Pessoa on x.id equals tp.horario_id
                                                join p in db.Pessoa on tp.pessoa_id equals p.id
                                                where (p.id == id)
                                                select new TrabalhoHorarioModel
                                                {
                                                    Id = x.id,
                                                    Horario = x.horario,
                                                }).ToList();

                foreach (var x in model.TrabalhoHorarioList)
                {
                    foreach (var y in h)
                    {
                        if (y.Id == x.Id)
                            x.IsSelected = true;
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult AvancarEdicao(PessoaViewModel model)
        {
            return View(model);
        }

        [HttpPost]
        public ActionResult Editar(PessoaViewModel model)
        {
            if (Request.Form["comando"] != null)
            {
                return View(model);
            }
            else
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        using (TalentosEntities db = new TalentosEntities())
                        {
                            using (var dbContextTransaction = db.Database.BeginTransaction())
                            {
                                try
                                {
                                    var x = db.Pessoa.Find(model.Id);
                                    x.email = model.Email;
                                    x.nome = model.Nome;
                                    x.skype = model.Skype;
                                    x.telefone = model.Telefone;
                                    x.linkedin = model.Linkedin;
                                    x.cidade = model.Cidade;
                                    x.estado = model.Estado;
                                    x.portfolio = model.Portfolio;
                                    x.salario_por_hora = model.SalarioPorHora;
                                    x.experiencia_tecnologia = model.ExperienciaTecnologia;
                                    x.link_crud = model.LinkCrud;

                                    db.Entry(x).State = System.Data.Entity.EntityState.Modified;
                                    db.SaveChanges();

                                    int pessoaId = x.id;


                                    db.Database.ExecuteSqlCommand("DELETE FROM Tempo_Pessoa WHERE pessoa_id = " + pessoaId);
                                    db.Database.ExecuteSqlCommand("DELETE FROM Horario_Pessoa WHERE pessoa_id = " + pessoaId);
                                    db.Database.ExecuteSqlCommand("DELETE FROM Conhecimento_Pessoa WHERE pessoa_id = " + pessoaId);


                                    for (int i = 0; i < model.ConhecimentoList.Count; i++)
                                    {
                                        var h = new Conhecimento_Pessoa();
                                        h.pessoa_id = pessoaId;
                                        h.conhecimento_id = model.ConhecimentoList[i].Id;
                                        h.nivel = model.ConhecimentoList[i].Nivel != null ? model.ConhecimentoList[i].Nivel.ToString() : "";

                                        db.Conhecimento_Pessoa.Add(h);
                                        db.SaveChanges();
                                    }

                                    for (int i = 0; i < model.TrabalhoHorarioList.Count; i++)
                                    {
                                        if (model.TrabalhoHorarioList[i].IsSelected)
                                        {
                                            var h = new Horario_Pessoa();
                                            h.pessoa_id = pessoaId;
                                            h.horario_id = model.TrabalhoHorarioList[i].Id;

                                            db.Horario_Pessoa.Add(h);
                                            db.SaveChanges();
                                        }
                                    }

                                    for (int i = 0; i < model.TrabalhoTempoList.Count; i++)
                                    {
                                        if (model.TrabalhoTempoList[i].IsSelected)
                                        {
                                            var h = new Tempo_Pessoa();
                                            h.pessoa_id = pessoaId;
                                            h.tempo_id = model.TrabalhoTempoList[i].Id;

                                            db.Tempo_Pessoa.Add(h);
                                            db.SaveChanges();
                                        }
                                    }

                                    dbContextTransaction.Commit();

                                }
                                catch (Exception ex)
                                {
                                    dbContextTransaction.Rollback();
                                    throw new Exception(ex.Message);
                                }
                            }
                        }
                        return Redirect("~/");
                    }
                    return View(model);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        [HttpGet]
        public ActionResult Excluir(int id)
        {
            using (TalentosEntities db = new TalentosEntities())
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Database.ExecuteSqlCommand("DELETE FROM Tempo_Pessoa WHERE pessoa_id = " + id);
                        db.Database.ExecuteSqlCommand("DELETE FROM Horario_Pessoa WHERE pessoa_id = " + id);
                        db.Database.ExecuteSqlCommand("DELETE FROM Conhecimento_Pessoa WHERE pessoa_id = " + id);
                        var p = db.Pessoa.Find(id);
                        db.Pessoa.Remove(p);
                        db.SaveChanges();
                        dbContextTransaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        throw new Exception(ex.Message);
                    }
                }

            }
            return Redirect("~/");
        }


    }
}