using System.Threading.Tasks;

namespace Hungsum.Framework.UI.Pages
{
    public interface IUcDJAnnexPage
    {
        bool HasRetrieve { get; }

        bool HasDataChanged { get; }

        Task GetItems();

        Task UpdateItems();
    }
}