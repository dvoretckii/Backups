namespace Backups.Interfaces;

public interface IConfig
{
    IStorageAlghoritm Alghoritm { get; }
    IRepository Repository { get; }
    IArchive Archive { get; }
}