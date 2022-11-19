namespace Backups.Interfaces;

public interface IRepoObject
{
    IPath Path { get; }
    void Accept(IRepoObjectVisitor visitor);
}