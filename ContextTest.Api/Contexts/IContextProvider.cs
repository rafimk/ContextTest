namespace ContextTest.Api.Contexts;

public interface IContextProvider
{
    IContext Current(string? user = null);
}