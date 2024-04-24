using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnsitechLibrary.Services
{
    public interface IInventoryRepository
    {
        void AddToInventory(int productId, int quantity);
        void RemoveFromInventory(int productId, int quantity);
        int GetAvailableQuantity(int productId);
    }
}