namespace Services;
using Entities.Models;
public interface ILogService
{
    void AddLog(Log log,string filename);
}