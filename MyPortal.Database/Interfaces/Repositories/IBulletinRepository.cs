﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Models.Filters;
using MyPortal.Database.Models.Paging;
using MyPortal.Database.Models.QueryResults.School;
using MyPortal.Database.Models.Search;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IBulletinRepository : IReadWriteRepository<Bulletin>, IUpdateRepository<Bulletin>
    {
        Task<IEnumerable<Bulletin>> GetBulletins(BulletinSearchOptions searchOptions);

        Task<IEnumerable<Bulletin>> GetOwn(Guid authorId);

        Task<IEnumerable<BulletinDetailModel>> GetBulletinDetails(BulletinSearchOptions searchOptions);

        Task<BulletinMetadataPageResponse> GetBulletinDetails(BulletinSearchOptions searchOptions,
            PageFilter pageFilter);
    }
}