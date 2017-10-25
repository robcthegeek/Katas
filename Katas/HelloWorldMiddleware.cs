using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace Katas
{
    public class HelloWorldMiddleware : OwinMiddleware
    {
        public HelloWorldMiddleware(OwinMiddleware next) : base(next)
        {
        }

        public override Task Invoke(IOwinContext context)
        {
            context.Request.Headers.TryGetValue("X-Name", out var names);
            var name = names.FirstOrDefault("World");

            context.Set("value", "test");

            context.Response.OnSendingHeaders(x =>
            {
                context.Response.Headers.Add("X-HelloWorld", new string[] { $"{name}!" });
            }, null);

            return Next?.Invoke(context);
        }
    }

    internal static class StringExtensions
    {
        internal static string FirstOrDefault(this IEnumerable<string> values, string @default)
        {
            var value = values?.FirstOrDefault();

            return string.IsNullOrWhiteSpace(value)
                ? @default
                : value;
        }
    }
}