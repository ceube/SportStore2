﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Abstract;
using System.Collections.Generic;
using System.Linq;
using Moq;
using SportsStore.Domain.Concrete;

namespace SportsStore.WebUI.Infrastructure
{
    public class NinjectControllerFactory: DefaultControllerFactory
    {

        private IKernel ninjectKernel;
        
        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null :(IController)ninjectKernel.Get(controllerType);
       } 
        
        private void AddBindings()
        {

            //Mock<IProductRepository> mock = new Mock<IProductRepository>();
            //mock.Setup(m => m.Products).Returns(new List<Product>{
            //    new Product{Name= "Ball",Price=25},
            //    new Product{Name= "Windsurfing kit",Price=79},
            //    new Product{Name= "Shoes",Price=95},

            //}.AsQueryable());

            ninjectKernel.Bind<IProductRepository>().To<EFProductRepository>();
        }

    }
}