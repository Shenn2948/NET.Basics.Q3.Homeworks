using System;
using System.Linq;
using Task3.DoNotChange;
using Task3.Exceptions;

namespace Task3;

public class UserTaskService
{
    private readonly IUserDao _userDao;

    public UserTaskService(IUserDao userDao)
    {
        _userDao = userDao;
    }

    public void AddTaskForUser(int userId, UserTask task)
    {
        if (userId < 0)
        {
            throw new InvalidUserException("action_result", "Invalid userId");
        }

        var user = _userDao.GetUser(userId);
        if (user == null)
        {
            throw new UserNotFoundException("action_result", "User not found");
        }

        var tasks = user.Tasks;
        if (tasks.Any(t => string.Equals(task.Description, t.Description, StringComparison.OrdinalIgnoreCase)))
        {
            throw new UsersTaskHasConflictException("action_result", "The task already exists");
        }

        tasks.Add(task);
    }
}