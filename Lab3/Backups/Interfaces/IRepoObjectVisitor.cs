namespace Backups.Interfaces;

public interface IRepoObjectVisitor
{
    void Visit(IRepoFile file);
    void Visit(IRepoFolder folder);
}