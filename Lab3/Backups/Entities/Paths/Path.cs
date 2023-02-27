using Backups.Exceptions;
using Zio;

namespace Backups.Entities.Paths
{
    public class Path : IPath
    {
        private readonly UPath _path;

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
            UPath fullPath = _path.GetDirectory().ToAbsolute();
            return new Path(fullPath);
        }

        public IPath GetName()
        {
            UPath name = _path.GetName();
            return new Path(name);
        }

        public IPath Merge(IPath path)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            string pathString = path.ToString();
            if (string.IsNullOrWhiteSpace(pathString))
            {
                throw new ArgumentException("The path argument cannot be null or whitespace.", nameof(path));
            }

            var combinedPath = UPath.Combine(_path, ((Path)path)._path);
            return new Path(combinedPath);
        }

        public IPath Merge(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException("The path argument cannot be null or whitespace.", nameof(path));
            }

            var combinedPath = UPath.Combine(_path, path);
            return new Path(combinedPath);
        }

        public void ValidatePath()
        {
            if (string.IsNullOrWhiteSpace(_path.ToString()))
            {
                throw BackupsException.IncorrectPath();
            }
        }
    }
}