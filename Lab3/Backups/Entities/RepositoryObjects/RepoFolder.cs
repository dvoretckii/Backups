using Backups.Entities.Paths;
using Backups.Patterns.Visitor;

namespace Backups.Entities.RepositoryObjects;

public class RepoFolder : IRepoFolder
{
    private readonly Func<IReadOnlyList<IRepoObject>> _childernFunc;

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