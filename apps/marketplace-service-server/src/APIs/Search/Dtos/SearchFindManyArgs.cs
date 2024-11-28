using MarketplaceService.APIs.Common;
using MarketplaceService.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceService.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class SearchFindManyArgs : FindManyInput<Search, SearchWhereInput> { }
