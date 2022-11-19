using System.IO.Compression;
using Backups.Interfaces;

namespace Backups.Entities;

public class RepoObjectVisitor : IRepoObjectVisitor
{
    private readonly ZipArchive _archive;
    private readonly Stack<IPath> _zipArchivePaths = new Stack<IPath>();
    private readonly Stack<List<IStorageObject>> _storageObjects = new Stack<List<IStorageObject>>();

    public RepoObjectVisitor(ZipArchive zipArchive)
    {
        _archive = zipArchive;
        _storageObjects.Push(new List<IStorageObject>());
    }

    public IReadOnlyList<IStorageObject> GetTopStackList()
    {
        return _storageObjects.Peek().AsReadOnly();
    }

    public void Visit(IRepoFile file)
    {
        IPath entryName = (_zipArchivePaths.Count >= 1) ? _zipArchivePaths.Peek().Merge(file.Path.GetName()) : file.Path.GetName();
        ZipArchiveEntry entry = _archive.CreateEntry(entryName.ToString());
        using (Stream entryStream = entry.Open())
        {
            file.Stream.CopyTo(entryStream);
        }

        _storageObjects.Peek().Add(new ZipFile(entryName));
    }

    public void Visit(IRepoFolder folder)
    {
        IPath entryName = (_zipArchivePaths.Count > 0) ? _zipArchivePaths.Peek().Merge(folder.Path.GetName()) : folder.Path.GetName();
        IReadOnlyCollection<IRepoObject> repoObjects = folder.Children;
        if (repoObjects.Count == 0)
        {
            _storageObjects.Peek().Add(new ZipFolder(new List<IStorageObject>(), entryName));
            _archive.CreateEntry($"{entryName}/");
        }
        else
        {
            _zipArchivePaths.Push(entryName);
            _storageObjects.Push(new List<IStorageObject>());
            foreach (IRepoObject repoObject in repoObjects)
            {
                repoObject.Accept(this);
            }

            IReadOnlyList<IStorageObject> zipItems = _storageObjects.Pop().AsReadOnly();
            _storageObjects.Peek().Add(new ZipFolder(zipItems, entryName));
            _zipArchivePaths.Pop();
        }
    }
}