namespace ASPProjekt.Controllers
{
    using System;
    using System.Security.Principal;
    using Microsoft.AspNet.Identity;

    public class ControllerIdentity : IControllerIdentity
    {
        private readonly IIdentity identity;

        public ControllerIdentity(IIdentity identity)
        {
            this.identity = identity;
        }

        public string AuthenticationType => this.identity.AuthenticationType;

        public bool IsAuthenticated => this.identity.IsAuthenticated;

        public string Name => this.identity.Name;

        public string GetUserId()
        {
            return this.identity.GetUserId();
        }

        public T GetUserId<T>() where T : IConvertible
        {
            return this.identity.GetUserId<T>();
        }

        public string GetUserName()
        {
            return this.identity.GetUserName();
        }
    }
}