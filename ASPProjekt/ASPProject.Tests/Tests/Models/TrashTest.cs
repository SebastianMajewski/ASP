using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ASPProject.Tests.Tests.Models
{
    using System.Collections.Generic;

    using ASPProjekt.Models;

    [TestClass]
    public class TrashTest
    {
        [TestMethod]
        public void CorrectModel()
        {
            var trash = new Trash
            {
                Title = "Title",
                AddTime = DateTime.Now,
                Content = "content",
            };
            var result = ModelValidator.Validate(trash);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void TitleTooShort()
        {
            var trash = new Trash
            {
                Title = "Tit",
                AddTime = DateTime.Now,
                Content = "content",
            };
            var result = ModelValidator.Validate(trash);
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void AllowHtml()
        {
            var trash = new Trash
            {
                Title = "Title",
                AddTime = DateTime.Now,
                Content = "<p>content</p>",
            };
            var result = ModelValidator.Validate(trash);
            Assert.AreEqual(0, result.Count);
        }
    }
}
