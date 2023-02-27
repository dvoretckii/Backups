using Backups.Entities.Alghorithms;
using Backups.Entities.Archives;
using Backups.Entities.Repositories;

namespace Backups.Models.Configs;

public interface IConfig
{
    IStorageAlghoritm Alghoritm { get; }
    IRepository Repository { get; }
    IArchive Archive { get; }
}