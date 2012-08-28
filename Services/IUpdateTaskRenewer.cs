using Orchard;

namespace Associativy.TagsAdapter.Services
{
    public interface IUpdateTaskRenewer : IDependency
    {
        void RenewTagNodeUpdate();
    }
}
