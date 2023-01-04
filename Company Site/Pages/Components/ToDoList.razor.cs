using Company_Site.Data;
using Company_Site.DB;

using Microsoft.AspNetCore.Components;

namespace Company_Site.Pages.Components
{
    public partial class ToDoList : ComponentBase
    {
        #region Private Members

        /// <summary>
        /// The list of tasks for the user
        /// </summary>
        private List<WorkTask> tasks = new List<WorkTask>();

        /// <summary>
        /// The new task item
        /// </summary>
        private WorkTask NewTask = new WorkTask();

        /// <summary>
        /// Indicates whether the form for the task is shown or not
        /// </summary>
        private bool IsTaskFormShown = false;

        /// <summary>
        /// The error which is being displayed
        /// </summary>
        private List<string> _errors = new List<string>();

        /// <summary>
        /// The users which exists in the system
        /// </summary>
        private List<Data.User> users = new List<Data.User>();

        /// <summary>
        /// The list of selected users
        /// </summary>
        private List<int> SelectedUsers = new List<int>();

        /// <summary>
        /// Indicates the state of finish all check box
        /// </summary>
        private bool FinishAllCheck = false;

        #endregion

        #region Public Properties

        [CascadingParameter(Name = "UserId")]
        public int? UserId { get; set; }

        #endregion

        #region Injected Members

        [Inject]
        private ApplicationDbContext _dbContext { get; set; }

        #endregion

        #region Overriden Methods

        protected override void OnInitialized()
        {
            if(UserId != null)
            {
                LoadData();
                users = _dbContext.Users.ToList();
                SelectedUsers.Add(UserId.Value);
            }
        }

        #endregion

        #region Private Methods

        private void LoadData()
        {
            if (UserId != null)
            {
                List<int> userTasks = _dbContext.TaskAssignments.Where(f => f.UserId == UserId.Value).Select(f => f.TaskId).Distinct().ToList();
                tasks = _dbContext.Tasks.Where(f => userTasks.Contains(f.Id)).ToList();
                //Checking if all the task have been selected or not
                if (!tasks.Any(f => !f.Completed))
                {
                    FinishAllCheck = true;
                }
                else
                    FinishAllCheck = false;
                StateHasChanged();
            }
        }

        private void SaveTask()
        {
            _errors.Clear();
            if(SelectedUsers.Count == 0)
            {
                _errors.Add("Please select atleast 1 Assignee");
                return;
            }

            _dbContext.Tasks.Add(NewTask);
            _dbContext.SaveChanges();
            foreach(int userId in SelectedUsers)
            {
                _dbContext.TaskAssignments.Add(new TaskAssignment()
                {
                    TaskId = NewTask.Id,
                    UserId = userId
                });
            }
            _dbContext.SaveChanges();
            NewTask = new WorkTask();
            SelectedUsers = new List<int>();
            if(UserId != null)
            {
                SelectedUsers.Add(UserId.Value);
            }
            LoadData();
            IsTaskFormShown = false;
        }

        private void Cancel()
        {
            Clear();
            IsTaskFormShown = false;
        }

        private void AddTask()
        {
            IsTaskFormShown = true;
            if(UserId != null)
            {
                SelectedUsers.Add(UserId.Value);
            }
        }

        private void Clear()
        {
            NewTask = new WorkTask();
            SelectedUsers = new List<int>();
            _errors = new List<string>();
            if (UserId != null)
            {
                SelectedUsers.Add(UserId.Value);
            }
        }

        private void OnChange(object args)
        {
            List<int>? sUsers = args as List<int>;
            if(sUsers != null)
            {
                SelectedUsers = sUsers;
            }
        }

        private void Delete(int id)
        {
            List<TaskAssignment> assignments = _dbContext.TaskAssignments.Where(f => f.TaskId == id).ToList();
            _dbContext.TaskAssignments.RemoveRange(assignments);
            WorkTask task = _dbContext.Tasks.Where(f => f.Id == id).First();
            _dbContext.Tasks.Remove(task);
            _dbContext.SaveChanges();
            LoadData();
        }

        private void ChangeState(int id, bool state)
        {
            WorkTask task = _dbContext.Tasks.Where(f => f.Id == id).First();
            task.Completed = state;
            _dbContext.Tasks.Update(task);
            _dbContext.SaveChanges();
            LoadData();
        }

        /// <summary>
        /// Changes the status of the all the tasks for the user
        /// </summary>
        /// <param name="value"></param>
        private void ToggleFinishAll(bool value)
        {
            List<int> userTasks = _dbContext.TaskAssignments.Where(f => f.UserId == UserId.Value).Select(f => f.TaskId).Distinct().ToList();
            List<WorkTask> tasksToUpdate = _dbContext.Tasks.Where(f => userTasks.Contains(f.Id)).ToList();
            tasksToUpdate.ForEach(f => f.Completed = value);
            _dbContext.UpdateRange(tasksToUpdate);
            _dbContext.SaveChanges();
            LoadData();
        }

        #endregion

    }
}
