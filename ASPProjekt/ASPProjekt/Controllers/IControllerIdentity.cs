namespace ASPProjekt.Controllers
{
    using System;

    public interface IControllerIdentity
    {
        string AuthenticationType { get; }

        bool IsAuthenticated { get; }

        string Name { get; }

        string GetUserId();

        T GetUserId<T>() where T : IConvertible;

        string GetUserName();
    }
}