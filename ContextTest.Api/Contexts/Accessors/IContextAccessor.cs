namespace ContextTest.Api.Contexts.Accessors;

public interface IContextAccessor
{
    IContext? Context { get; set; }
}