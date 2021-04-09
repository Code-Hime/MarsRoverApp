using MarsRoverApp.Models;
using System.Threading.Tasks;

namespace MarsRoverApp.Services
{
    public interface IMarsRoverService
    {
        Task<MarsRoverApod> GetApod();
        Task DownloadAndSaveApods();
        Task<MarsRoverApod> GetApodByDate(string date);
    }
}
