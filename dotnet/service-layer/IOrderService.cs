using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dk.opusmagus.fd.dtl;

namespace dk.opusmagus.fd.sl 
{
	public interface IOrderService {
		Task<bool> ValidateOrder(PlayerDTO order);
		Task ShipOrder(PlayerDTO order);
		Task<PlayerDTO> GetOrder(String shoppingCardId);
		Task<List<PlayerDTO>> GetOrders();
	}
}