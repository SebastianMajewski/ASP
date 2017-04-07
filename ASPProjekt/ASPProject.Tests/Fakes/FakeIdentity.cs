namespace ASPProject.Tests.Fakes
{
    using System;
    using System.Security.Principal;

    using ASPProjekt.Controllers;

    using Microsoft.AspNet.Identity;

    public class FakeIdentity : IControllerIdentity
    {
        public string Name => "Name";

        public string AuthenticationType => "Type";

        public bool IsAuthenticated => true;

        public T GetUserId<T>() where T : IConvertible
        {
            return (T)Convert.ChangeType(1, typeof(T));
        }

        public string GetUserId()
        {
            return "1";
        }

        public string GetUserName()
        {
            return "Name";
        }
    }
}
