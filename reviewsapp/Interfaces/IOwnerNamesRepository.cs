using reviewsapp.models;

namespace reviewsapp.Interfaces
{
    public interface IOwnerNamesRepository
    {
        ICollection<OwnerName>GetOwnerNames();
        OwnerName GetOwner(int OwnerId);
        ICollection<OwnerName> GetOwnerofAModel(int modId);
        ICollection<Model>GetModelByOwnernames(int ownerId);
        bool OwnersExist(int ownerId);
        bool CreateOwner(OwnerName ownerName);
        bool UpdateOwnerNames(OwnerName ownerName);
        bool DeleteOwner(OwnerName ownerName);
        bool Save();
    }
}
