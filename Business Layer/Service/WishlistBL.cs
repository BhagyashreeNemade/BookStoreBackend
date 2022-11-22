using Business_Layer.Interface;
using Common_Layer.Model;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Service
{
    public class WishlistBL : IWishlistBL
    {
        private readonly IWishlistRL wishlistRL;
        public WishlistBL(IWishlistRL wishlistRL)
        {
            this.wishlistRL = wishlistRL;
        }
        public string AddToWishList(int bookId, int userId)
        {
            return this.wishlistRL.AddToWishList(bookId, userId);
        }
        public List<WishlistResponse> GetAllWishList(int userId)
        {
            return this.wishlistRL.GetAllWishList(userId);
        }
        public string RemoveFromWishList(int wishListId)
        {
            return this.wishlistRL.RemoveFromWishList(wishListId);
        }


    }
}