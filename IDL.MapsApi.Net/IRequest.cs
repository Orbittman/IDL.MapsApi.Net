﻿namespace IDL.MapsApi.Net
{
    public interface IRequest<T>
    {
        string Path { get; }

        string RootPath { get; }
    }
}
