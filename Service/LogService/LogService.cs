namespace Services;

using System.Text.Json;
using Data.Context;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

public class LogService : ILogService
{
    public void AddLog(Log log,string filename)
    {
        string modeljson = JsonSerializer.Serialize(log);
        string filepath = Directory.GetCurrentDirectory();        
        string path = $"{filepath}//{filename}";
        if (!File.Exists(filename))
        {
            File.Create(path);
            File.WriteAllText(path, modeljson);
        }
        else
        {
            File.AppendAllText(path, modeljson);
        }
    }
}