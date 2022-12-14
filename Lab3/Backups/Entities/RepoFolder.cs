using Backups.Interfaces;

namespace Backups.Entities;

public class RepoFolder : IRepoFolder
{
    private Func<IReadOnlyList<IRepoObject>> _childernFunc;

    public RepoFolder(Func<IReadOnlyList<IRepoObject>> childernFunc, IPath path)
    {
        _childernFunc = childernFunc;
        Path = path;
    }

    public IPath Path { get; }

    public IReadOnlyList<IRepoObject> Children => _childernFunc();
    public void Accept(IRepoObjectVisitor visitor)
    {
        visitor.Visit(this);
    }
}