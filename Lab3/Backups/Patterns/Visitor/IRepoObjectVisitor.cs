using Backups.Entities.RepositoryObjects;

namespace Backups.Patterns.Visitor;

public interface IRepoObjectVisitor
{
    void Visit(IRepoFile file);
    void Visit(IRepoFolder folder);
}