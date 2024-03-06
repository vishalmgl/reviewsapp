using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace reviewsapp.Interfaces
{
    public interface ImodelRepository
    {
        ICollection<Model> GetModels();
    }
}
