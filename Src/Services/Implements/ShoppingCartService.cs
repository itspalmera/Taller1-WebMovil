using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sprache;
using Taller1_WebMovil.Src.DTOs.ShoppingCart;
using Taller1_WebMovil.Src.Interface;
using Taller1_WebMovil.Src.Mapper;
using Taller1_WebMovil.Src.Services.Interfaces;

namespace Taller1_WebMovil.Src.Services.Implements
{
    public class ShoppingCartService : IShoppingCartService
    {
    private readonly IShoppingCartRepository _shoppingCartRepository;

    public ShoppingCartService(IShoppingCartRepository shoppingCartRepository){
        
        _shoppingCartRepository = shoppingCartRepository;
    }

        public async Task<bool> DeleteCartItem(string userId, int id)
        {
            var result = await _shoppingCartRepository.DeleteCartItem(userId,id);
            return result;
        }

        public async Task<IEnumerable<ShoppingCartInfoDto?>> GetCartItems(string userId)
        {
            var listCartItems = await _shoppingCartRepository.GetCartItems(userId);
            return listCartItems;
        }
    }
}