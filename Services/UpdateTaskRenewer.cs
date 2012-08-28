﻿using Orchard.Environment;
using Orchard.Services;
using Orchard.Tasks.Scheduling;

namespace Associativy.TagsAdapter.Services
{
    public class UpdateTaskRenewer : IUpdateTaskRenewer, IOrchardShellEvents
    {
        private readonly IScheduledTaskManager _scheduledTaskManager;
        private readonly IClock _clock;

        public UpdateTaskRenewer(
            IScheduledTaskManager scheduledTaskManager,
            IClock clock)
        {
            _scheduledTaskManager = scheduledTaskManager;
            _clock = clock;
        }

        public void RenewTagNodeUpdate()
        {
            _scheduledTaskManager.CreateTask("AssociativyTagNodeUpdate", _clock.UtcNow.AddHours(1), null);
        }

        public void Activated()
        {
            RenewTagNodeUpdate();
        }

        public void Terminating()
        {
        }
    }
}
