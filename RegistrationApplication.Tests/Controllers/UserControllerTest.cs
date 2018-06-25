using System.Web.Mvc;
using RegistrationApplication.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegistrationApplication.Controllers;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace RegistrationApplication.Tests.Controllers
{
    [TestClass]
    public class UserControllerTest : Controller
    {
        [TestMethod]
        public void TestCreateView()
        {
            var controller = new UserController();
            var result = controller.Create() as ViewResult;
            Assert.AreEqual("Create", result.ViewName);
        }

        [TestMethod]
        public void TestModelValidinput()
        {
            var model = new UserModel();
            {
                model.Email = "this@Me.com";
                model.Password = "password";
            }

            Assert.IsTrue(ValidateModel(model).Count == 0);
        }

        [TestMethod]
        public void TestModelInValidEmail()
        {
            //No @ Symbol
            var model = new UserModel();
            {
                model.Email = "invalid.com";
                model.Password = "password";
            }

            Assert.IsTrue(ValidateModel(model).Count == 1);
            //No . Symbol.
            {
                model.Email = "invalid@com";
                model.Password = "password";
            }

            Assert.IsTrue(ValidateModel(model).Count == 1);

            //No . or @ Symbol.
            {
                model.Email = "invalidcom";
                model.Password = "password";
            }

            Assert.IsTrue(ValidateModel(model).Count == 1);
        }

        [TestMethod]
        public void TestModelLowerLimitPassword()
        {
            //On Limit Test
            var model = new UserModel();
            {
                model.Email = "valid@test.com";
                model.Password = "Thisis8.";
            }

            Assert.IsTrue(ValidateModel(model).Count == 0);

            //below limit test
            {
                model.Email = "valid@test.com";
                model.Password = "Under8";
            }

            Assert.IsTrue(ValidateModel(model).Count == 1);
        }

        [TestMethod]
        public void TestModelUpperLimitPassword()
        {
            //on limit test
            var model = new UserModel();
            {
                model.Email = "valid@test.com";
                model.Password = "passbelow the 32 character limit";
            }

            Assert.IsTrue(ValidateModel(model).Count == 0);

            //above limit test
            {
                model.Email = "valid@test.com";
                model.Password = "This Password Exceeds the 32 character limit";
            }

            Assert.IsTrue(ValidateModel(model).Count == 1);
        }

        [TestMethod]
        public void TestModelNull()
        {
            //null model test
            var model = new UserModel();
            {
                model.Email = null;
                model.Password = null;
            }

            Assert.IsTrue(ValidateModel(model).Count == 2);
        }

        [TestMethod]
        public void TestModelEmailNull()
        {
            //null email test
            var model = new UserModel();
            {
                model.Email = null;
                model.Password = "Password";
            }

            Assert.IsTrue(ValidateModel(model).Count == 1);
        }

        [TestMethod]
        public void TestModelPasswordNull()
        {
            //null password test
            var model = new UserModel();
            {
                model.Email = "Valid@Email.com";
                model.Password = null;
            }

            Assert.IsTrue(ValidateModel(model).Count == 1);
        }

        [TestMethod]
        public void TestModelUpperLimitEmail()
        {

            //below limit test
            var model = new UserModel();
            {
                model.Email = "ThelongestvalidemailthelongestvalidemailthelongestvalidemailthelongestvalidemailthelongestThelongestvalidemailthelongestvalidemailthelongestvalidemailthelongestvalidemailthelongestvalidemailthelongestvalidemailthelongestvalidemailthelongestvalidemailthelongestvalidemailthelongestvalidemailthelongest@longestemail.co.uk";
                model.Password = "Password";
            }
            Assert.IsTrue(ValidateModel(model).Count == 0);

            //above limit test
            {
                model.Email = "ThelongestvalidemailthelongestvalidemailthelongestvalidemailthelongestvalidemailthelongestThelongestvalidemailthelongestvalidemailthelongestvalidemailthelongestvalidemailthelongestvalidemailthelongestvalidemailthelongestvalidemailthelongestvalidemailthelongestvalidemailthelongestvalidemailthelongesttoolong@longestemail.co.uk";
                model.Password = "Password";
            }

            Assert.IsTrue(ValidateModel(model).Count == 1);
        }


        private IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }
    }

}
