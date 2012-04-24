using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orchard.ContentManagement;
using Associativy.TagsAdapter.Models;
using Orchard.Data;
using Orchard.Core.Common.Models;

namespace Associativy.TagsAdapter.Services
{
    public class UpdaterService : IUpdaterService
    {
        private readonly IRepository<PendingContentItemRecord> _repository;
        private readonly IContentManager _contentManager;

        public UpdaterService(
            IRepository<PendingContentItemRecord> repository,
            IContentManager contentManager)
        {
            _repository = repository;
            _contentManager = contentManager;
        }

        public void AddToQueue(IContent content)
        {
            // There is already a record saved for this content
            if (_repository.Count(record => record.ContentItemId == content.Id) != 0) return;

            _repository.Create(new PendingContentItemRecord
            {
                ContentItemId = content.Id
            });
        }

        public void RemoveFromQueue(IContent content)
        {
            var pending = _repository.Fetch(record => record.ContentItemId == content.Id).FirstOrDefault();
            if (pending == null) return;

            _repository.Delete(pending);
        }

        public IEnumerable<IContent> GetPendingContents()
        {
            // ToList() as otherwise an exception with message "Expression argument must be of type ICollection." is thrown from
            // Orchard.ContentManagement.DefaultContentQuery on line 90.
            var contentIds = _repository.Table.Select(record => record.ContentItemId).ToList();

            return _contentManager.Query().Where<CommonPartRecord>(record => contentIds.Contains(record.Id)).List();
        }
    }
}
