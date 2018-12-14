using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.IO;
using Fileholder.Domain.Abstract;
using Fileholder.Domain.Entities;
using System.Web.Routing;
using Fileholder.Models;
using ICSharpCode.SharpZipLib;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Core;

//TODO: удалить класс
namespace Fileholder.Controllers
{
    public class UserFilesController : Controller
    {
        private IAllFilesRepository repository;

        public UserFilesController(IAllFilesRepository userFilesRepository)
        {
            repository = userFilesRepository;
        }

        [HttpPost]
        public ActionResult ShowAllFiles()
        {
            return View();
        }
    }
}