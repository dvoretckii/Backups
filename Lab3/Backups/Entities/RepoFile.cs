﻿using Backups.Interfaces;

namespace Backups.Entities;

public class RepoFile : IRepoFile
{
    private readonly Func<Stream> _func;

    public RepoFile(Func<Stream> func, IPath path)
    {
        _func = func;
        Path = path;
    }

    public IPath Path { get; }

    public Stream Stream => _func();

    public void Accept(IRepoObjectVisitor visitor)
    {
        visitor.Visit(this);
    }
}