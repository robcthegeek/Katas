using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Testing;
using NUnit.Framework;
using Owin;
using Ploeh.AutoFixture;

namespace Katas.Tests
{
    [TestFixture]
    public class HelloWorldMiddlewareTests : AutoTestFixture
    {
        //Requests can also be constructed and submitted with the following helper methods:

        //HttpResponseMessage response = await server.CreateRequest("/")
        //    .AddHeader("header1", "headervalue1")
        //    .GetAsync();

        private OwinMiddlewareTestServer<HelloWorldMiddleware> _server;

        [SetUp]
        public void SetUp()
        {
            _server = new OwinMiddlewareTestServer<HelloWorldMiddleware>();
        }

        [Test]
        public async Task Invoke_NoParams_SetsResponseHeader2()
        {
            var response = await _server.Response(x => x.HttpClient.GetAsync("/"));

            var present = response.Headers.TryGetValues("X-HelloWorld", out var values);

            Assert.True(present, "X-HelloWorld Response Header Missing");
            Assert.Contains("World!", values.ToList());
        }

        [Test]
        public async Task Invoke_NameHeader_SetsResponseHeader()
        {
            var response = await _server.Response(x => x
                .CreateRequest("/")
                .AddHeader("X-Name", "Rob")
                .GetAsync());

            var present = response.Headers.TryGetValues("X-HelloWorld", out var values);

            Assert.True(present, "X-HelloWorld Response Header Missing");
            Assert.Contains("Rob!", values.ToList());
        }

        [Test]
        public async Task Invoke_MockContext_SetsName()
        {
            var context = Fixture.Freeze<IOwinContext>();
            var middleware = new HelloWorldMiddleware(null);

            await middleware.Invoke(context);

            Assert.AreEqual("World", context.Get<string>("HelloWorld:Name"));
        }
    }

    internal class OwinMiddlewareTestServer<T>
    {
        public async Task<HttpResponseMessage> Response(Func<TestServer, Task<HttpResponseMessage>> action)
        {
            using (var server = TestServer.Create(app =>
            {
                app.Use<T>();
            }))
            {
                return await action(server);
            }
        }
    }
}