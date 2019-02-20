using System;
using System.Collections.Generic;

namespace WebApi
{
    public class ShoppingCartService : IShoppingCartService
    {
        public ShoppingItem Add(ShoppingItem newItem)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<ShoppingItem> GetAllItems()
        {
            throw new NotImplementedException();
        }

        public ShoppingItem GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Guid id)
        {
            throw new NotImplementedException();
        }
    }

    public interface IShoppingCartService
    {
        IReadOnlyList<ShoppingItem> GetAllItems();
        ShoppingItem Add(ShoppingItem newItem);
        ShoppingItem GetById(Guid id);
        void Remove(Guid id);
    }
}