namespace ASPProject.Tests.Tests.Models
{
    using System.Collections.Generic;
    using ASPProjekt.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BinTest
    {
        [TestMethod]
        public void CorrectModel()
        {
            var bin = new Bin
            {
                Trash = new List<Trash>(),
                Id = 1,
                Description = "Description",
                Name = "Bin",
                ApplicationUserId = "1",
            };
            var result = ModelValidator.Validate(bin);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void NameTooShort()
        {
            var bin = new Bin
            {
                Trash = new List<Trash>(),
                Id = 1,
                Description = "Description",
                Name = "Bi",
                ApplicationUserId = "1",
            };
            var result = ModelValidator.Validate(bin);
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void DescriptionTooShort()
        {
            var bin = new Bin
            {
                Trash = new List<Trash>(),
                Id = 1,
                Description = "Den",
                Name = "Bin",
                ApplicationUserId = "1",
            };
            var result = ModelValidator.Validate(bin);
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void NameFirstLetterNotUpper()
        {
            var bin = new Bin
            {
                Trash = new List<Trash>(),
                Id = 1,
                Description = "Description",
                Name = "bin",
                ApplicationUserId = "1",
            };
            var result = ModelValidator.Validate(bin);
            Assert.AreEqual(1, result.Count);
        }
    }
}
