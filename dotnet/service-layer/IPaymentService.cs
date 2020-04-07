using System.Threading.Tasks;
using dk.opusmagus.fd.dtl;

namespace dk.opusmagus.fd.sl
{
    public interface IPaymentService
    {
        Task<bool> ProcessPayment(PlayerDTO order);
    }
}