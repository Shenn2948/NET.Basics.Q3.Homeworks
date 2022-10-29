using System;
using Task3.DoNotChange;
using Task3.Exceptions;

namespace Task3;

public class UserTaskController
{
    private readonly UserTaskService _taskService;

    public UserTaskController(UserTaskService taskService)
    {
        _taskService = taskService;
    }

    public bool AddTaskForUser(int userId, string description, IResponseModel model)
    {
        try
        {
            var task = new UserTask(description);
            _taskService.AddTaskForUser(userId, task);
        }
        catch (Exception ex)
        {
            if (ex is not BaseUserTaskException userTaskException)
            {
                throw;
            }

            foreach (var response in userTaskException.ResponseAttributes)
            {
                model.AddAttribute(response.Key, response.Value);
            }

            return false;
        }

        return true;
    }
}