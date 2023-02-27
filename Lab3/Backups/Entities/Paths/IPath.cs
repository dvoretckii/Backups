namespace Backups.Entities.Paths;

public interface IPath
{
    string ToString();
    IPath Merge(IPath path);
    IPath Merge(string path);
    IPath GetFullPath();
    IPath GetName();
    void ValidatePath();
}