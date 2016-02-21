using System.Linq;
using Mvc3Application.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Mvc3Application.Controllers
{
    public class AlunoController : Controller
    {
        private static readonly Dictionary<int, Aluno> alunosStorage = new Dictionary<int, Aluno>();

        //
        // GET: /Aluno/

        public ActionResult Index()
        {
            lock (alunosStorage)
                return View(alunosStorage.Values);
        }

        //
        // GET: /Aluno/Details/5

        public ActionResult Details(int id)
        {
            lock (alunosStorage)
                return View(alunosStorage[id]);
        }

        //
        // GET: /Aluno/Create

        public ActionResult Create()
        {
            return View(new Aluno());
        }

        //
        // POST: /Aluno/Create

        [HttpPost]
        public ActionResult Create(Aluno aluno)
        {
            if (!ModelState.IsValid)
                return View(aluno);

            try
            {
                lock (alunosStorage)
                {
                    aluno.Id = alunosStorage.Count + 1;
                    alunosStorage.Add(aluno.Id, aluno);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View(aluno);
            }
        }

        //
        // GET: /Aluno/Edit/5

        public ActionResult Edit(int id)
        {
            lock (alunosStorage)
                return View(alunosStorage[id]);
        }

        //
        // POST: /Aluno/Edit/5

        [HttpPost]
        public ActionResult Edit(Aluno aluno)
        {
            if (!ModelState.IsValid)
                return View(aluno);

            try
            {
                lock (alunosStorage)
                    alunosStorage[aluno.Id] = aluno;

                return RedirectToAction("Index");
            }
            catch
            {
                return View(aluno);
            }
        }

        //
        // GET: /Aluno/Delete/5

        public ActionResult Delete(int id)
        {
            lock (alunosStorage)
                return View(alunosStorage[id]);
        }

        //
        // POST: /Aluno/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                lock (alunosStorage)
                    alunosStorage.Remove(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(alunosStorage[id]);
            }
        }

        public ActionResult EditMany()
        {
            lock (alunosStorage)
                return View(alunosStorage.Values.ToArray());
        }

        [HttpPost]
        public ActionResult EditMany(Aluno[] alunos)
        {
            if (!ModelState.IsValid)
                return View(alunos);

            try
            {
                foreach (var aluno in alunos)
                {
                    alunosStorage[aluno.Id] = aluno;
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View(alunos);
            }
        }
    }
}
