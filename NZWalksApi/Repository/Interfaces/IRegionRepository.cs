﻿using NZWalksApi.Controllers;
using NZWalksApi.Models.Domain;
using System;

namespace NZWalksApi.Repository.Interfaces
{
    public interface IRegionRepository
    {
      Task<List<Region>> GetAllAsync();

       Task<Region?> GetByIdAsync(Guid id);

        Task<Region> CreateAsync(Region region);

        Task<Region?> UpdateAsync(Guid id ,Region region);

        Task<Region?> DeleteAsync(Guid id);

    }
}
