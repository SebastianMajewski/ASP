using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ASPProject.Tests.Tests.Models
{
    using ASPProjekt.Models;

    [TestClass]
    public class CommentTest
    {
        [TestMethod]
        public void CorrectModel()
        {
            var comment = new Comment
            {
                Content = "content"
            };
            var result = ModelValidator.Validate(comment);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void ContentTooShort()
        {
            var comment = new Comment
            {
                Content = string.Empty
            };
            var result = ModelValidator.Validate(comment);
            Assert.AreEqual(1, result.Count);
        }
    }
}
