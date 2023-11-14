﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModels.EF;
using ViewModels.Client;

namespace DataModels.Repository.Interface
{
    public interface ICommentRepository : IRepositoryBase<Comments,int>
    {
        Task<IEnumerable<CommentViewModel>> GetPaging(int animeId, int pageNumber, int pageSize);
    }
}
