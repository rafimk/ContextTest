﻿namespace ContextTest.Api.Contexts.Accessors;

public interface IMessageContextAccessor
{
    MessageContext? MessageContext { get; set; }
}