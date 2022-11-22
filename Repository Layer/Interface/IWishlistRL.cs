using Common_Layer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Interface
{
    public interface IWishlistRL
    {
        public string AddToWishList(int bookId, int userId);
        public List<WishlistResponse> GetAllWishList(int userId);
        public string RemoveFromWishList(int wishListId);



    }
}