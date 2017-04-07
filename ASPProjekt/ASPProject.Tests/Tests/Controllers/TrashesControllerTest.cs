using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ASPProject.Tests.Tests.Controllers
{
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using ASPProject.Tests.Fakes;

    using ASPProjekt.Controllers;
    using ASPProjekt.Models;

    [TestClass]
    public class TrashesControllerTest
    {
        private TrashesController controller = null;
        private IRepository repository = null;
        private IControllerIdentity identity = null;

        [TestInitialize]
        public void SetUp()
        {
            this.repository = new FakeRepository();
            this.identity = new FakeIdentity();
            var comment = new Comment { Content = "content" };
            var trash = new Trash { Id = 1, Title = "Title", AddTime = DateTime.Now, Content = "content", Comments = new List<Comment>() { comment } };
            var trash2 = new Trash { Id = 2, Title = "Title", AddTime = DateTime.Now, Content = "content", Comments = new List<Comment>() { comment } };
            var trash3 = new Trash { Id = 3, Title = "Title", AddTime = DateTime.Now, Content = "content", Comments = new List<Comment>() { comment } };
            this.repository.Add(trash);
            this.repository.Add(trash2);
            this.repository.Add(trash3);
            this.controller = new TrashesController(this.repository, this.identity);
        }

        [TestMethod]
        public void CreateView()
        {
            var ar = this.controller.Create() as ViewResult;
            Assert.AreEqual("Create", ar.ViewName);
        }

        [TestMethod]
        public void CreateAdd()
        {
            var comment = new Comment { Content = "content" };
            var trash4 = new Trash { Id = 3, Title = "Title", AddTime = DateTime.Now, Content = "content", Comments = new List<Comment>() { comment } };
            this.controller.Create(trash4);
            Assert.AreEqual(4, this.repository.Trash.Count());
        }

        [TestMethod]
        public void CreateAddInvalid()
        {
            var comment = new Comment { Content = "content" };
            var trash4 = new Trash { Id = 3, Title = "T", AddTime = DateTime.Now, Content = "content", Comments = new List<Comment>() { comment } };
            var modelBinder = new ModelBindingContext()
            {
                ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(() => trash4, trash4.GetType()),
                ValueProvider = new NameValueCollectionValueProvider(new NameValueCollection(), CultureInfo.InvariantCulture)
            };
            new DefaultModelBinder().BindModel(new ControllerContext(), modelBinder);
            this.controller.ModelState.Clear();
            this.controller.ModelState.Merge(modelBinder.ModelState);
            var ar = this.controller.Create(trash4) as ViewResult;
            Assert.AreEqual("Create", ar.ViewName);
            Assert.IsFalse(ar.ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public void DeleteBadRequest()
        {
            var ar = this.controller.Delete(null) as HttpStatusCodeResult;
            Assert.AreEqual((int)HttpStatusCode.BadRequest, ar.StatusCode);
        }

        [TestMethod]
        public void DeleteBadId()
        {
            var ar = this.controller.Delete(10);
            Assert.AreEqual(typeof(HttpNotFoundResult), ar.GetType());
        }

        [TestMethod]
        public void DeleteIdOk()
        {
            var ar = this.controller.Delete(1) as ViewResult;
            var trash = this.repository.Trash.FirstOrDefault(x => x.Id == 1);
            Assert.AreEqual("Delete", ar.ViewName);
            Assert.AreEqual(trash, ar.Model);
        }

        [TestMethod]
        public void DeleteConfirmed()
        {
            this.controller.DeleteConfirmed(1);
            Assert.AreEqual(2, this.repository.Trash.Count());
        }

        [TestMethod]
        public void DeleteConfirmedRedirect()
        {
            var ar = this.controller.DeleteConfirmed(1) as RedirectToRouteResult;
            Assert.AreEqual("Details", ar.RouteValues["action"]);
            Assert.AreEqual("Bins", ar.RouteValues["controller"]);
        }

        [TestMethod]
        public void DetailsBadRequest()
        {
            var ar = this.controller.Details(null) as HttpStatusCodeResult;
            Assert.AreEqual((int)HttpStatusCode.BadRequest, ar.StatusCode);
        }

        [TestMethod]
        public void DetailsBadId()
        {
            var ar = this.controller.Details(10);
            Assert.AreEqual(typeof(HttpNotFoundResult), ar.GetType());
        }

        [TestMethod]
        public void DetailsIdOk()
        {
            var ar = this.controller.Details(1) as ViewResult;
            var trash = this.repository.Trash.FirstOrDefault(x => x.Id == 1);
            Assert.AreEqual("Details", ar.ViewName);
            Assert.AreEqual(trash, ar.Model);
        }

        [TestMethod]
        public void EditBadRequest()
        {
            var ar = this.controller.Edit(null as int?) as HttpStatusCodeResult;
            Assert.AreEqual((int)HttpStatusCode.BadRequest, ar.StatusCode);
        }

        [TestMethod]
        public void EditBadId()
        {
            var ar = this.controller.Edit(10);
            Assert.AreEqual(typeof(HttpNotFoundResult), ar.GetType());
        }

        [TestMethod]
        public void EditIdOk()
        {
            var ar = this.controller.Edit(1) as ViewResult;
            var trash = this.repository.Trash.FirstOrDefault(x => x.Id == 1);
            Assert.AreEqual("Edit", ar.ViewName);
            Assert.AreEqual(trash, ar.Model);
        }

        [TestMethod]
        public void EditConfirmed()
        {
            var trash = this.repository.Trash.First();
            trash.Title = "TrashTitle";
            this.controller.Edit(trash);
            Assert.AreEqual("TrashTitle", this.repository.Trash.First(x => x.Id == trash.Id).Title);
        }

        [TestMethod]
        public void EditConfirmedRedirect()
        {
            var trash = this.repository.Trash.First();
            trash.Title = "TrashTitle";
            var ar = this.controller.Edit(trash) as RedirectToRouteResult;
            Assert.AreEqual("Details", ar.RouteValues["action"]);
            Assert.AreEqual("Bins", ar.RouteValues["controller"]);
        }

        [TestMethod]
        public void EditInvalid()
        {
            var comment = new Comment { Content = "content" };
            var trash4 = new Trash { Id = 3, Title = "T", AddTime = DateTime.Now, Content = "content", Comments = new List<Comment>() { comment } };
            var modelBinder = new ModelBindingContext()
            {
                ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(() => trash4, trash4.GetType()),
                ValueProvider = new NameValueCollectionValueProvider(new NameValueCollection(), CultureInfo.InvariantCulture)
            };
            new DefaultModelBinder().BindModel(new ControllerContext(), modelBinder);
            this.controller.ModelState.Clear();
            this.controller.ModelState.Merge(modelBinder.ModelState);
            var ar = this.controller.Edit(trash4) as ViewResult;
            Assert.AreEqual("Edit", ar.ViewName);
            Assert.IsFalse(ar.ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public void IndexReturnView()
        {
            var ar = this.controller.Index() as ViewResult;
            Assert.AreEqual("Index", ar.ViewName);
            CollectionAssert.AreEquivalent(this.repository.Trash.ToList(), (List<Trash>)ar.Model);
        }
    }
}
