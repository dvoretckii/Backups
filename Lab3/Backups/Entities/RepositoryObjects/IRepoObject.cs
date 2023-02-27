using Backups.Entities.Paths;
using Backups.Patterns.Visitor;

namespace Backups.Entities.RepositoryObjects;

public interface IRepoObject
{
    IPath Path { get; }
    void Accept(IRepoObjectVisitor visitor);
}