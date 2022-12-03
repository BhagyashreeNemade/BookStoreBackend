using Common_Layer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Interface
{

     public interface IWishlistBL
    {
        public string AddToWishlist(int bookId, int userId);
        public bool RemoveFromWishlist(int wishlistId);
        public List<WishlistModel> GetWishlistItem(int userId);


    }
}