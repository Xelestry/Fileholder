using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Fileholder.Domain.Abstract;
using Fileholder.Domain;
using Fileholder.Models;
using Microsoft.AspNet.Identity;

namespace Fileholder.Controllers
{
    public class ErrorController : Controller
    {
        //public ErrorController(Exception error)
        //{
        //    SaveError(error);
        //}

        //public ActionResult SaveError(Exception error)
        //{
        //    const string ROOT_PATH = @"~\ErrorsException\";
        //    Guid fileErrorName = Guid.NewGuid();

        //    string fullPathForAllFiles = ROOT_PATH + fileErrorName + ".txt";

        //    System.IO.File.Create(Server.MapPath(fullPathForAllFiles));

        //    System.IO.File.AppendAllText(fullPathForAllFiles, error.ToString());

        //    return ErrorPage();
        //}

        public ActionResult ErrorPage()
        {
            return View();
        }
    }
}