using Backups.Entities.Alghorithms;
using Backups.Entities.Archives;
using Backups.Entities.Repositories;

namespace Backups.Models.Configs;

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