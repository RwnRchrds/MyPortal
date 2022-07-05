using System.Threading.Tasks;
using MyPortal.Logic.Models.Requests.Curriculum.Homework;

namespace MyPortal.Logic.Interfaces.Services;

public interface IHomeworkService
{
    Task CreateHomework(CreateHomeworkRequestModel model);
}