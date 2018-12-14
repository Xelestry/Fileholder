using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using System.Web.Mvc;
using System.Web.Routing;
using Fileholder.Domain.Abstract;
using Fileholder.Domain.Concrete;
using Fileholder.Domain.Entities;
using Fileholder.Infrastructure;

namespace Fileholder.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {
            ninjectKernel.Bind<IAllFilesRepository>().To<EFAllFilesRepository>();
            ninjectKernel.Bind<IAllFilesAndGroupFilesLinkRepository>().To<EFAllFilesAndGroupFilesLinkRepository>();
            ninjectKernel.Bind<IGroupFilesOfOneUploadRepository>().To<EFGroupFilesOfOneUploadRepository>();
            ninjectKernel.Bind<IFileConfigsRepository>().To<EFFileConfigsRepository>();
        }
    }
}