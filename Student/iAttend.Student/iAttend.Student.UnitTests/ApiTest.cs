using iAttend.Student.Models;
using iAttend.Student.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace iAttend.Student.UnitTests
{
    [TestFixture]
    public class ApiTest
    {
        [Test]
        public void TestApiPostResponse()
        {
            ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

            //Arrange

            var api = new ApiAccess();
            api.BaseUri = "https://localhost:5001";
            //Act

            var x = api.PostForResponseAsync<ActiveAttendance>("api/teacher/14/start").Result;

            //Assert
            // TODO: Add your test code here
            Assert.AreNotEqual(-1, x.StatusCode);
        }

        [Test]
        public void TestApiGetResponse()
        {
            ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

            //Arrange

            var api = new ApiAccess();
            api.BaseUri = "https://localhost:5001";
            //Act

            var x = api.GetResopnseAsync<List<StudentSubject>>("api/student/subjects/12-A00004").Result;

            //Assert
            // TODO: Add your test code here
            Assert.IsTrue(x.Result.Count > 0);
        }
    }
}
