﻿using API.Dtos;
using AutoMapper;
using AutoMapper.Execution;
using Core.Entities;
using System.Runtime.CompilerServices;

namespace API.Helpers
{
    public class ProductUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration _config;
        public ProductUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(
            Product source,
            ProductToReturnDto destination,
            string destMember, ResolutionContext context)
        {

            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return _config["ApiUrl"] + source.PictureUrl;
            }
            return null;

        }
    }
}
