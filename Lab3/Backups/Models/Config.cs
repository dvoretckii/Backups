using Backups.Interfaces;

namespace Backups.Models;

public class Config : IConfig
{
    public Config(IStorageAlghoritm alghoritm, IRepository repository, IArchive archive)
    {
        Alghoritm = alghoritm;
        Repository = repository;
        Archive = archive;
    }

    public IStorageAlghoritm Alghoritm { get; }
    public IRepository Repository { get; }

    public IArchive Archive { get; }
}