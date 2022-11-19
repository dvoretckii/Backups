using Backups.Interfaces;
using Zio;

namespace Backups.Entities;

public class Path : IPath
{
    private UPath _path;

    public Path(UPath path)
    {
        _path = path;
    }

    public override string ToString()
    {
        return _path.ToString();
    }

    public IPath GetFullPath()
    {
        var newPath = new Path(_path.GetDirectory().ToAbsolute().ToString());
        return newPath;
    }

    public IPath GetName()
    {
        UPath name = _path.GetName();
        var cp = new Path(name);
        return cp;
    }

    public IPath Merge(IPath path)
    {
        string thisPath = ToString();
        string? pathString = path.ToString();
        var cp = new Path(thisPath + '/' + pathString);
        return cp;
    }

    public IPath Merge(string path)
    {
        string thisPath = ToString();
        var cp = new Path(thisPath + '/' + path);
        return cp;
    }
}