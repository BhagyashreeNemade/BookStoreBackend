using Common_Layer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Interface
{
    public interface IWishlistRL
    {
        public string AddToWishlist(int bookId, int userId);
        public bool RemoveFromWishlist(int wishlistId);
        public List<WishlistModel> GetWishlistItem(int userId);


    }
}