using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnsitechLibrary.Services
{
    public class InventoryRepository : IInventoryRepository
    {
        public void AddToInventory(int productId, int quantity)
        {
            throw new NotImplementedException();
        }

        public int GetAvailableQuantity(int productId)
        {
            throw new NotImplementedException();
        }

        public void RemoveFromInventory(int productId, int quantity)
        {
            throw new NotImplementedException();
        }
    }
}